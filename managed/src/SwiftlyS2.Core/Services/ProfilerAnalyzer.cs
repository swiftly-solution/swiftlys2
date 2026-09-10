using System.Text;
using Microsoft.Diagnostics.Tracing.Etlx;
using Microsoft.Diagnostics.Tracing.Stacks;
using Microsoft.Extensions.Logging;

namespace SwiftlyS2.Core.Services;

internal static class ProfilerAnalyzer
{
    private const int TickRateHz = 64;
    private const double BudgetMs = 1000.0 / TickRateHz;

    public static void Analyze( string netTracePath, ILogger logger )
    {
        if (!File.Exists(netTracePath)) return;

        var summaryPath = netTracePath.EndsWith(".nettrace", StringComparison.OrdinalIgnoreCase)
            ? netTracePath.Replace(".nettrace", ".summary.txt")
            : netTracePath + ".summary.txt";

        var etlxPath = TraceLog.CreateFromEventPipeDataFile(netTracePath);
        using var traceLog = new TraceLog(etlxPath);

        var stackSource = new TraceEventStackSource(traceLog.Events) {
            ShowUnknownAddresses = false,
        };

        var callTree = new CallTree(ScalingPolicyKind.TimeMetric) {
            StackSource = stackSource
        };

        WriteSummary(netTracePath, summaryPath, traceLog, callTree, logger);
    }

    private static void WriteSummary(
        string netTracePath,
        string summaryPath,
        TraceLog traceLog,
        CallTree callTree,
        ILogger logger )
    {
        var sb = new StringBuilder();
        var totalMetric = callTree.Root.InclusiveMetric;
        var fi = new FileInfo(netTracePath);
        var duration = traceLog.SessionEndTimeRelativeMSec / 1000.0;

        const string Bar = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";
        _ = sb.AppendLine(Bar);
        _ = sb.AppendLine($" SwiftlyS2 Profiler - Summary");
        _ = sb.AppendLine(Bar);
        _ = sb.AppendLine($"  file      {Path.GetFileName(netTracePath)} ({fi.Length / 1024} KB)");
        _ = sb.AppendLine($"  captured  {duration:F2} s  ·  {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC");
        _ = sb.AppendLine($"  events    {traceLog.EventCount:N0}/{traceLog.EventCount + traceLog.EventsLost:N0}");
        _ = sb.AppendLine(Bar);
        _ = sb.AppendLine();

        if (totalMetric <= 0)
        {
            _ = sb.AppendLine("Empty trace — no samples captured.");
            File.WriteAllText(summaryPath, sb.ToString());
            logger.LogWarning("[Profiler] Trace has zero total metric (empty capture?).");
            return;
        }

        var nodesByMethod = callTree.ByID;
        var pluginNodes = new List<CallTreeNodeBase>();
        var frameworkNodes = new List<CallTreeNodeBase>();
        var otherNodes = new List<CallTreeNodeBase>();
        var activeTotal = 0f;

        foreach (var n in nodesByMethod)
        {
            if (!IsRelevant(n.Name)) continue;
            activeTotal += n.ExclusiveMetric;

            switch (GetCategory(n.Name))
            {
                case Category.Plugin: pluginNodes.Add(n); break;
                case Category.Framework: frameworkNodes.Add(n); break;
                case Category.Other: otherNodes.Add(n); break;
                case Category.Skip: break;
            }
        }
        if (activeTotal <= 0) activeTotal = totalMetric;

        var totalTicks = (long)(duration * TickRateHz);

        var pluginAvg = totalTicks > 0 ? activeTotal / totalTicks : 0f;
        _ = sb.AppendLine($"  cpu       {activeTotal:N0} active samples  /  {totalMetric:N0} raw");
        _ = sb.AppendLine($"  ticks     {totalTicks:N0}  →  {pluginAvg:F3} ms/tick plugin avg");
        _ = sb.AppendLine();
        _ = sb.AppendLine("  Legend:  ms/t = managed CPU per tick (steady drag)");
        _ = sb.AppendLine("           tot  = total managed CPU across capture (sporadic spikes)");
        _ = sb.AppendLine("           inc% = self + callees    exc% = self only    % of active CPU");
        _ = sb.AppendLine("  Read:    high ms/t                → slow function");
        _ = sb.AppendLine("           high tot · low ms/t      → random spikes");
        _ = sb.AppendLine();

        WriteNodeSection(sb, "Plugins", pluginNodes, activeTotal, totalTicks);
        WriteNodeSection(sb, "SwiftlyS2", frameworkNodes, activeTotal, totalTicks);
        WriteNodeSection(sb, "Runtime", otherNodes, activeTotal, totalTicks);

        var (memStats, exceptions, pluginGroups) = ParseTraceEvents(traceLog);
        WriteMemorySection(sb, memStats, traceLog.SessionEndTimeRelativeMSec);
        WriteExceptionsSection(sb, exceptions);
        WriteCustomSection(sb, pluginGroups);

        File.WriteAllText(summaryPath, sb.ToString());
        logger.LogInformation("[Profiler] Wrote analysis report → {Path}", summaryPath);
    }

    private enum Category { Plugin, Framework, Other, Skip }

    private static (string assembly, string method) Split( string name )
    {
        if (string.IsNullOrEmpty(name)) return (string.Empty, string.Empty);

        if (name.Contains('!'))
        {
            var exploded = name.Split('!');
            return (exploded[0], string.Join('!', exploded.Skip(1)));
        }
        else return (string.Empty, name);
    }

    private static Category GetCategory( string? name )
    {
        if (string.IsNullOrEmpty(name)) return Category.Skip;
        var (_, method) = Split(name);
        if (string.IsNullOrEmpty(method)) return Category.Other;

        if (method.StartsWith("SwiftlyS2.Core.Services.ProfilerAnalyzer", StringComparison.Ordinal)) return Category.Skip;
        if (method.StartsWith("SwiftlyS2.Core.Misc.FileLogger", StringComparison.Ordinal)) return Category.Skip;

        if (method.StartsWith("SwiftlyS2.", StringComparison.Ordinal)) return Category.Framework;
        if (IsLibrary(method)) return Category.Other;
        return Category.Plugin;
    }

    private static readonly string[] LibraryPrefixes =
    [
        "System.",
        "Microsoft.",
        "MS.Internal.",
        "Internal.",
        "Interop+",
        "Interop.",
        "<CrtImplementationDetails>",
        "<>f__AnonymousType",
        "NLog.",
        "log4net.",
        "Tomlyn.",
        "NetEscapades.",
        "LiteDB.",
        "MySql.",
        "MySqlConnector.",
        "Npgsql.",
        "MongoDB.",
        "SqlSugar.",
        "Dapper.",
        "EntityFramework",
        "McMaster.",
        "Mono.",
        "Spectre.",
        "Semver.",
    ];

    private static bool IsLibrary( string method )
    {
        foreach (var p in LibraryPrefixes)
            if (method.StartsWith(p, StringComparison.Ordinal)) return true;
        return false;
    }

    private static bool IsRelevant( string? name )
    {
        if (string.IsNullOrEmpty(name)) return false;
        if (name.StartsWith("Thread (", StringComparison.Ordinal)) return false;
        if (name.StartsWith("Process", StringComparison.Ordinal)) return false;
        if (name.StartsWith("ROOT")) return false;
        if (name.StartsWith("BROKEN")) return false;
        if (name.StartsWith("0x", StringComparison.Ordinal)) return false;
        if (name.Contains("!0x", StringComparison.Ordinal)) return false;

        var trimmed = name.Trim();
        if (trimmed.Length == 0) return false;
        if (trimmed == "?") return false;
        if (trimmed.StartsWith("?", StringComparison.Ordinal) && trimmed.Length < 6) return false;
        if (trimmed.StartsWith("UNMANAGED_CODE_TIME", StringComparison.Ordinal)) return false;
        if (trimmed.Contains("cs2!", StringComparison.Ordinal)) return false;

        if (name.Contains("LifoSemaphore", StringComparison.Ordinal)) return false;
        if (name.Contains("Monitor.Wait", StringComparison.Ordinal)) return false;
        if (name.Contains("WaitOneNoCheck", StringComparison.Ordinal)) return false;
        if (name.Contains("WaitHandle.WaitOne", StringComparison.Ordinal)) return false;
        if (name.Contains("SemaphoreSlim.WaitCore", StringComparison.Ordinal)) return false;
        if (name.Contains("SemaphoreSlim.WaitUntilCountOrTimeout", StringComparison.Ordinal)) return false;
        if (name.Contains("PollGCWorker", StringComparison.Ordinal)) return false;
        if (name.Contains("Interop+Sys.Read", StringComparison.Ordinal)) return false;
        if (name.Contains("WorkerThreadStart", StringComparison.Ordinal)) return false;
        if (name.Contains("GateThreadStart", StringComparison.Ordinal)) return false;
        if (name.Contains("TimerThread", StringComparison.Ordinal)) return false;
        if (name.Contains("BlockingCollection", StringComparison.Ordinal)) return false;
        if (name.Contains("BackgroundWorkerSink.Pump", StringComparison.Ordinal)) return false;
        if (name.Contains("FileSystemWatcher", StringComparison.Ordinal)) return false;
        if (name.Contains("TryTakeWithNoTimeValidation", StringComparison.Ordinal)) return false;

        if (name.Contains("AsyncMethodBuilder", StringComparison.Ordinal)) return false;
        if (name.Contains("ExecutionContext.RunInternal", StringComparison.Ordinal)) return false;
        if (name.Contains("Task.ExecuteWithThreadLocal", StringComparison.Ordinal)) return false;
        if (name.Contains("Task.RunContinuations", StringComparison.Ordinal)) return false;
        if (name.Contains("ThreadPoolWorkQueue", StringComparison.Ordinal)) return false;
        if (name.Contains("IThreadPoolWorkItem.Execute", StringComparison.Ordinal)) return false;
        if (name.Contains("NativeRuntimeEventSource", StringComparison.Ordinal)) return false;
        if (name.Contains("AssemblyLoadContext", StringComparison.Ordinal)) return false;
        if (name.Contains("ManifestBuilder.CreateManifestString", StringComparison.Ordinal)) return false;

        if (name.Contains("SocketPal.", StringComparison.Ordinal)) return false;
        if (name.Contains("SslStreamPal.", StringComparison.Ordinal)) return false;
        if (name.Contains("SslStream.", StringComparison.Ordinal)) return false;
        if (name.Contains("ServerSession.", StringComparison.Ordinal)) return false;
        if (name.Contains("StreamByteHandler", StringComparison.Ordinal)) return false;

        if (name.Contains("System.Threading.Thread", StringComparison.Ordinal)) return false;
        if (name.Contains("Thread.StartCallback", StringComparison.Ordinal)) return false;
        if (name.Contains("Thread.StartCore", StringComparison.Ordinal)) return false;
        if (name.Contains("SocketAsyncEngine.EventLoop", StringComparison.Ordinal)) return false;
        if (name.Contains("SocketAsyncContext", StringComparison.Ordinal)) return false;

        return true;
    }

    private static void WriteNodeSection(
        StringBuilder sb,
        string label,
        List<CallTreeNodeBase> nodes,
        float activeTotal,
        long totalTicks )
    {
        var sectionTotal = nodes.Sum(n => n.ExclusiveMetric);
        var sectionPct = activeTotal > 0 ? sectionTotal / activeTotal * 100f : 0f;
        var sectionMsPerTick = totalTicks > 0 ? sectionTotal / totalTicks : 0f;

        const int W = 70;
        var heading = $"▸ {label}";
        var pctStr = $"{Math.Min(sectionPct, 999f):F2}%  ·  {sectionMsPerTick:F3} ms/tick";
        var pad = Math.Max(1, W - heading.Length - pctStr.Length);
        _ = sb.AppendLine($"{heading}{new string(' ', pad)}{pctStr}");
        _ = sb.AppendLine(new string('-', W));
        _ = sb.AppendLine();

        if (nodes.Count == 0)
        {
            _ = sb.AppendLine("  No samples available.");
            _ = sb.AppendLine();
            return;
        }

        _ = sb.AppendLine($"  {"ms/t",7}  {"tot ms",6}  {"calls",6}  {"inc%",5}   {"exc%",5}       {"Async",5}  Method");

        var ordered = nodes.OrderByDescending(n => n.InclusiveMetric).ToList();
        for (var i = 0; i < ordered.Count; i++)
        {
            var n = ordered[i];
            var ex = Math.Min(n.ExclusiveMetric / activeTotal * 100f, 999f);
            var inc = Math.Min(n.InclusiveMetric / activeTotal * 100f, 999f);
            var msTotal = n.InclusiveMetric;
            var msPerTick = totalTicks > 0 ? msTotal / totalTicks : 0f;
            var calls = (int)n.InclusiveCount;
            var flag = msPerTick > BudgetMs ? "▲!" : "  ";
            var asyncTag = IsAsync(n.Name) ? "Async" : "     ";
            _ = sb.AppendLine($"  {msPerTick,7:F3}  {msTotal,6:F0}  {calls,6}  {inc,5:F2}%  {ex,5:F2}%  {flag}  {asyncTag}  {n.DisplayName}");
        }
        _ = sb.AppendLine();
    }

    private static void CollectTopBranches( CallTreeNode node, List<CallTreeNode> output, int depthBudget )
    {
        if (depthBudget <= 0) return;
        if (node.Callees is not { Count: > 0 }) return;

        foreach (var child in node.Callees)
        {
            if (child.Name is null) continue;
            if (IsThreadShell(child.Name) || !IsRelevant(child.Name))
            {
                CollectTopBranches(child, output, depthBudget - 1);
                continue;
            }
            output.Add(child);
        }
    }

    private static void DescendChain(
        StringBuilder sb,
        CallTreeNode node,
        float activeTotal,
        long totalTicks,
        int depth,
        int depthBudget,
        float cutoff,
        HashSet<string> visited )
    {
        var msTotal = node.InclusiveMetric;
        var msPerTick = totalTicks > 0 ? msTotal / totalTicks : 0f;
        var pct = Math.Min(msTotal / activeTotal * 100f, 999f);
        var prefix = new string(' ', 4 + (depth * 2)) + (depth == 0 ? "  " : "→ ");
        _ = sb.AppendLine($"{prefix}{msPerTick,6:F3} ms/t  {msTotal,5:F0} ms  {pct,5:F2}%  {node.Name}");

        if (depthBudget <= 0) return;
        if (node.Callees is not { Count: > 0 }) return;
        if (node.Name is not null) _ = visited.Add(node.Name);

        var bestChild = node.Callees
            .Where(c => c.Name is not null
                     && IsRelevant(c.Name)
                     && !IsThreadShell(c.Name)
                     && !visited.Contains(c.Name))
            .OrderByDescending(c => c.InclusiveMetric)
            .FirstOrDefault();

        if (bestChild is null) return;
        if (bestChild.InclusiveMetric / activeTotal * 100f < cutoff) return;

        DescendChain(sb, bestChild, activeTotal, totalTicks, depth + 1, depthBudget - 1, cutoff, visited);
    }

    private static bool IsAsync( string? name )
        => name != null && (name.Contains(">d__", StringComparison.Ordinal) || name.Contains(".MoveNext()", StringComparison.Ordinal));

    private static bool IsThreadShell( string name )
        => name.StartsWith("Thread (", StringComparison.Ordinal)
        || name.StartsWith("Process", StringComparison.Ordinal)
        || name.StartsWith("ROOT", StringComparison.Ordinal)
        || name.Contains("WorkerThreadStart", StringComparison.Ordinal)
        || name.Contains("StartCallback", StringComparison.Ordinal)
        || name.Contains("StartCore", StringComparison.Ordinal)
        || name.Contains("TimerThread", StringComparison.Ordinal)
        || name.Contains("GateThreadStart", StringComparison.Ordinal)
        || name.Contains("IThreadPoolWorkItem.Execute", StringComparison.Ordinal)
        || name.Contains("BlockingCollection", StringComparison.Ordinal)
        || name.Contains("BackgroundWorkerSink.Pump", StringComparison.Ordinal)
        || name.Contains("SocketAsyncEngine.EventLoop", StringComparison.Ordinal);

    private readonly record struct AllocEntry( string TypeName, double TotalMB, int TickCount );

    private readonly record struct ExceptionEntry(
        string TypeName,
        string Message,
        bool Unhandled,
        int Count,
        string[] Stack
    );

    private readonly record struct MemoryStats(
        int[] GcCountPerGen,
        double TotalPauseMs,
        double PeakHeapMB,
        double FinalHeapMB,
        double FinalGen0MB,
        double FinalGen1MB,
        double FinalGen2MB,
        double FinalLohMB,
        AllocEntry[] TopAllocs
    );

    private static (MemoryStats Memory, List<ExceptionEntry> Exceptions, Dictionary<string, Dictionary<string, RecordingNode>> Recordings)
        ParseTraceEvents( TraceLog traceLog )
    {
        var gcCountPerGen = new int[3];
        var totalPauseMs = 0.0;
        var peakHeapMB = 0.0;
        var finalHeapMB = 0.0;
        var finalGen0MB = 0.0;
        var finalGen1MB = 0.0;
        var finalGen2MB = 0.0;
        var finalLohMB = 0.0;
        var suspendStart = double.NaN;
        var allocByType = new Dictionary<string, (double bytes, int ticks)>();

        var rawExceptions = new List<(string TypeName, string Message, bool Unhandled, string[] Stack)>();

        var pluginGroups = new Dictionary<string, Dictionary<string, RecordingNode>>();

        foreach (var evt in traceLog.Events)
        {
            if (evt.ProviderName == "Microsoft-Windows-DotNETRuntime")
            {
                var id = (int)evt.ID;

                if (id == 1)
                {
                    if (evt.PayloadByName("Depth") is { } depth)
                    {
                        var gen = Convert.ToInt32(depth);
                        if ((uint)gen < 3) gcCountPerGen[gen]++;
                    }
                }
                else if (id == 9)
                {
                    suspendStart = evt.TimeStampRelativeMSec;
                }
                else if (id == 3)
                {
                    if (!double.IsNaN(suspendStart))
                    {
                        totalPauseMs += evt.TimeStampRelativeMSec - suspendStart;
                        suspendStart = double.NaN;
                    }
                }
                else if (id == 4)
                {
                    var g0 = Convert.ToDouble(evt.PayloadByName("GenerationSize0")) / 1024.0 / 1024.0;
                    var g1 = Convert.ToDouble(evt.PayloadByName("GenerationSize1")) / 1024.0 / 1024.0;
                    var g2 = Convert.ToDouble(evt.PayloadByName("GenerationSize2")) / 1024.0 / 1024.0;
                    var loh = Convert.ToDouble(evt.PayloadByName("GenerationSize3")) / 1024.0 / 1024.0;
                    var total = g0 + g1 + g2 + loh;
                    if (total > peakHeapMB) peakHeapMB = total;
                    finalHeapMB = total;
                    finalGen0MB = g0;
                    finalGen1MB = g1;
                    finalGen2MB = g2;
                    finalLohMB = loh;
                }
                else if (id == 10)
                {
                    if (evt.PayloadByName("TypeName") is string typeName && typeName.Length > 0)
                    {
                        var bytes = evt.PayloadByName("AllocationAmount64") is { } raw64
                            ? Convert.ToDouble(raw64)
                            : Convert.ToDouble(evt.PayloadByName("AllocationAmount"));
                        if (!allocByType.TryGetValue(typeName, out var acc))
                            allocByType[typeName] = (bytes, 1);
                        else
                            allocByType[typeName] = (acc.bytes + bytes, acc.ticks + 1);
                    }
                }
                else if (id == 80)
                {
                    var typeName = evt.PayloadByName("ExceptionType") as string;
                    if (!string.IsNullOrEmpty(typeName))
                    {
                        var message = evt.PayloadByName("ExceptionMessage") as string ?? string.Empty;
                        if (message.Length > 80) message = message[..80] + "…";

                        var flags = 0;
                        if (evt.PayloadByName("ExceptionFlags") is { } flagsObj)
                            flags = Convert.ToInt32(flagsObj);
                        var unhandled = (flags & 4) != 0;

                        var frames = new List<string>();
                        var stackIdx = evt.CallStackIndex();
                        if (stackIdx != CallStackIndex.Invalid)
                        {
                            var frame = traceLog.CallStacks[stackIdx];
                            while (frame != null)
                            {
                                var method = frame.CodeAddress.FullMethodName;
                                if (!string.IsNullOrEmpty(method) && IsRelevant(method))
                                    frames.Add(method);
                                frame = frame.Caller;
                            }
                        }

                        rawExceptions.Add((typeName, message, unhandled, frames.ToArray()));
                    }
                }
            }
            else if (evt.ProviderName == "SwiftlyS2-Profiler")
            {
                var id = (int)evt.ID;
                if (id != 2 && id != 3) continue;

                if (evt.PayloadByName("name") is not string rawName) continue;
                var durationMs = Convert.ToDouble(evt.PayloadByName("durationMs"));
                var timestamp = evt.TimeStampRelativeMSec;

                if (!TryParseEventName(rawName, out var ident, out var op)) continue;

                if (!pluginGroups.TryGetValue(ident, out var ops))
                    pluginGroups[ident] = ops = [];
                if (!ops.TryGetValue(op, out var node))
                    ops[op] = node = new RecordingNode { Identifier = ident, Operation = op };

                node.Samples.Add(new CustomSample(durationMs, timestamp));
            }
        }

        var topAllocs = allocByType
            .OrderByDescending(kv => kv.Value.bytes)
            .Take(30)
            .Select(kv => new AllocEntry(kv.Key, kv.Value.bytes / 1024.0 / 1024.0, kv.Value.ticks))
            .ToArray();

        var memStats = new MemoryStats(gcCountPerGen, totalPauseMs, peakHeapMB, finalHeapMB,
            finalGen0MB, finalGen1MB, finalGen2MB, finalLohMB, topAllocs);

        var exceptions = rawExceptions
            .GroupBy(e => (e.TypeName, StackKey: string.Join("|", e.Stack)))
            .OrderByDescending(g => g.Count())
            .Select(g => new ExceptionEntry(
                g.Key.TypeName,
                g.First().Message,
                g.Any(e => e.Unhandled),
                g.Count(),
                g.First().Stack))
            .ToList();

        foreach (var ops in pluginGroups.Values)
            foreach (var node in ops.Values)
                ComputeStats(node);

        return (memStats, exceptions, pluginGroups);
    }

    private static void WriteMemorySection( StringBuilder sb, MemoryStats mem, double traceDurationMs )
    {
        const int W = 70;
        _ = sb.AppendLine("▸ Memory");
        _ = sb.AppendLine(new string('-', W));
        _ = sb.AppendLine();

        var totalGc = mem.GcCountPerGen[0] + mem.GcCountPerGen[1] + mem.GcCountPerGen[2];
        if (totalGc == 0 && mem.PeakHeapMB <= 0)
        {
            _ = sb.AppendLine("  No GC events captured in trace.");
            _ = sb.AppendLine();
            return;
        }

        var pausePct = traceDurationMs > 0 ? mem.TotalPauseMs / traceDurationMs * 100.0 : 0.0;
        _ = sb.AppendLine($"  {"GC Counts",-12} Gen0 {mem.GcCountPerGen[0]}   Gen1 {mem.GcCountPerGen[1]}   Gen2 {mem.GcCountPerGen[2]}   (total {totalGc})");
        _ = sb.AppendLine($"  {"Pause",-12} {mem.TotalPauseMs:F1} ms total  ·  {pausePct:F1}% of trace");

        if (mem.PeakHeapMB > 0)
        {
            _ = sb.AppendLine($"  {"Heap",-12} peak {mem.PeakHeapMB:F1} MB  ·  final {mem.FinalHeapMB:F1} MB");
            _ = sb.AppendLine($"  {"",12} Gen0 {mem.FinalGen0MB:F1} MB   Gen1 {mem.FinalGen1MB:F1} MB   Gen2 {mem.FinalGen2MB:F1} MB   LOH {mem.FinalLohMB:F1} MB");
        }

        _ = sb.AppendLine();
        _ = sb.AppendLine("  Allocations  (GCAllocationTick sampling, ~100 KB / tick)");
        _ = sb.AppendLine(new string('-', W));
        _ = sb.AppendLine();

        if (mem.TopAllocs.Length == 0)
        {
            _ = sb.AppendLine("  No allocation events captured in trace.");
            _ = sb.AppendLine();
            return;
        }

        var totalAllocMB = mem.TopAllocs.Sum(a => a.TotalMB);
        _ = sb.AppendLine($"  {"MB",8}  {"ticks",6}  {"inc%",5}  {"exc%",5}  {"",2}  Type");

        foreach (var entry in mem.TopAllocs)
        {
            var pct = totalAllocMB > 0 ? entry.TotalMB / totalAllocMB * 100.0 : 0.0;
            var flag = pct > 10.0 ? "▲!" : "  ";
            _ = sb.AppendLine($"  {entry.TotalMB,8:F2}  {entry.TickCount,6}  {pct,5:F2}%  {pct,5:F2}%  {flag}  {entry.TypeName}");
        }

        _ = sb.AppendLine();
    }

    private static void WriteExceptionsSection( StringBuilder sb, List<ExceptionEntry> exceptions )
    {
        const int W = 70;
        _ = sb.AppendLine("▸ Exceptions");
        _ = sb.AppendLine(new string('-', W));
        _ = sb.AppendLine();

        if (exceptions.Count == 0)
        {
            _ = sb.AppendLine("  No exceptions captured in trace.");
            _ = sb.AppendLine();
            return;
        }

        foreach (var ex in exceptions)
        {
            var flag = ex.Unhandled ? "▲!" : "  ";
            _ = sb.AppendLine($"  {ex.Count,6}x  {flag}  {ex.TypeName}");
            if (ex.Message.Length > 0)
                _ = sb.AppendLine($"          ↳ {ex.Message}");
            if (ex.Stack.Length > 0)
            {
                _ = sb.AppendLine($"          Stack:");
                foreach (var frame in ex.Stack)
                    _ = sb.AppendLine($"            at {frame}");
            }
            else
            {
                _ = sb.AppendLine($"          No Stack");
            }
            _ = sb.AppendLine();
        }
    }

    private static void WriteCustomSection(
        StringBuilder sb,
        Dictionary<string, Dictionary<string, RecordingNode>> pluginGroups )
    {
        _ = sb.AppendLine("▸ Custom Recordings");
        _ = sb.AppendLine(new string('-', 70));
        _ = sb.AppendLine();

        if (pluginGroups.Count == 0)
        {
            _ = sb.AppendLine("  No custom recordings found.");
            _ = sb.AppendLine();
            return;
        }

        const string ColHeader =
            "  Inc%    Exc%   First(ms)   Last(ms)  Total(ms)   ms/call       p50       p75       p95       p99    stddev  ExcBudget  Name";
        const string Divider =
            "--------------------------------------------------------------------------------------------------------------------------------------";
        _ = sb.AppendLine(ColHeader);
        _ = sb.AppendLine(Divider);

        var groupTotals = pluginGroups.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Values.Sum(n => n.TotalMs));

        var rootTotal = groupTotals.Values.Sum();
        var rootFirst = pluginGroups.Values.SelectMany(d => d.Values).Min(n => n.FirstMs);
        var rootLast = pluginGroups.Values.SelectMany(d => d.Values).Max(n => n.LastMs);
        var rootExc = rootTotal - groupTotals.Values.Sum();
        var rootExcPct = rootTotal > 0 ? rootExc / rootTotal * 100.0 : 0;

        _ = sb.AppendLine(FormatRow("", "All Recordings",
            100.0, rootExcPct,
            rootFirst, rootLast,
            rootTotal, rootTotal > 0 ? rootTotal / pluginGroups.Values.SelectMany(d => d.Values).Sum(n => n.Samples.Count) : 0,
            0, 0, 0, 0, 0,
            pluginGroups.Values.SelectMany(d => d.Values).Sum(n => n.ExcBudgetCount)));

        var sortedGroups = groupTotals.OrderByDescending(kv => kv.Value).ToList();
        for (var gi = 0; gi < sortedGroups.Count; gi++)
        {
            var groupIsLast = gi == sortedGroups.Count - 1;
            var groupName = sortedGroups[gi].Key;
            var groupTotal = sortedGroups[gi].Value;
            var leaves = pluginGroups[groupName].Values.OrderByDescending(n => n.TotalMs).ToList();
            var groupFirst = leaves.Count > 0 ? leaves.Min(n => n.FirstMs) : 0;
            var groupLast = leaves.Count > 0 ? leaves.Max(n => n.LastMs) : 0;
            var groupMean = leaves.Count > 0 ? groupTotal / leaves.Sum(n => n.Samples.Count) : 0;
            var groupExcMs = groupTotal - leaves.Sum(n => n.TotalMs);
            var groupIncPct = rootTotal > 0 ? groupTotal / rootTotal * 100.0 : 0;
            var groupExcPct = rootTotal > 0 ? groupExcMs / rootTotal * 100.0 : 0;
            var groupExcBudget = leaves.Sum(n => n.ExcBudgetCount);
            var gConnector = groupIsLast ? "└─" : "├─";

            _ = sb.AppendLine(FormatRow($"  {gConnector} ", $"[{groupName}]",
                groupIncPct, groupExcPct,
                groupFirst, groupLast,
                groupTotal, groupMean,
                0, 0, 0, 0, 0,
                groupExcBudget));

            var leafPrefix = groupIsLast ? "       " : "  │    ";
            for (var li = 0; li < leaves.Count; li++)
            {
                var leafIsLast = li == leaves.Count - 1;
                var leaf = leaves[li];
                var lConnector = leafIsLast ? "└─" : "├─";
                var leafIncPct = groupTotal > 0 ? leaf.TotalMs / groupTotal * 100.0 : 0;

                _ = sb.AppendLine(FormatRow($"{leafPrefix}{lConnector} ", leaf.Operation,
                    leafIncPct, leafIncPct,
                    leaf.FirstMs, leaf.LastMs,
                    leaf.TotalMs, leaf.MeanMs,
                    leaf.P50, leaf.P75, leaf.P95, leaf.P99,
                    leaf.StdDevMs, leaf.ExcBudgetCount));
            }
        }

        _ = sb.AppendLine();
    }

    private static string FormatRow(
        string prefix, string label,
        double incPct, double excPct,
        double firstMs, double lastMs,
        double totalMs, double msCall,
        double p50, double p75, double p95, double p99,
        double stddev, int excBudget )
    {
        static string N( double v, int w, string fmt = "F3" ) => v.ToString(fmt).PadLeft(w);
        static string P( double v ) => $"{v,6:F2}%";

        return $"{P(incPct)} {P(excPct)} {N(firstMs, 11)} {N(lastMs, 11)} {N(totalMs, 10)} {N(msCall, 9)} " +
               $"{N(p50, 9)} {N(p75, 9)} {N(p95, 9)} {N(p99, 9)} {N(stddev, 9)} {excBudget,9}  {prefix}{label}";
    }

    private static bool TryParseEventName( string raw, out string identifier, out string operation )
    {
        identifier = operation = "";
        if (!raw.StartsWith('[')) return false;
        var close = raw.IndexOf(']');
        if (close < 1 || close + 2 >= raw.Length) return false;
        identifier = raw[1..close];
        operation = raw[(close + 2)..];
        return identifier.Length > 0 && operation.Length > 0;
    }

    private static int PercentileIndex( int n, int p )
        => Math.Clamp((int)Math.Ceiling(p / 100.0 * n) - 1, 0, n - 1);

    private static void ComputeStats( RecordingNode node )
    {
        var samples = node.Samples;
        if (samples.Count == 0) return;

        node.FirstMs = samples.Min(s => s.TimestampMs);
        node.LastMs = samples.Max(s => s.TimestampMs);

        var durations = samples.Select(s => s.DurationMs).OrderBy(d => d).ToList();
        var n = durations.Count;

        node.TotalMs = durations.Sum();
        node.MeanMs = node.TotalMs / n;

        var sumSq = durations.Sum(d => (d - node.MeanMs) * (d - node.MeanMs));
        node.StdDevMs = Math.Sqrt(sumSq / n);

        node.P50 = durations[PercentileIndex(n, 50)];
        node.P75 = durations[PercentileIndex(n, 75)];
        node.P95 = durations[PercentileIndex(n, 95)];
        node.P99 = durations[PercentileIndex(n, 99)];

        node.ExcBudgetCount = durations.Count(d => d > BudgetMs);
    }

    private readonly record struct CustomSample( double DurationMs, double TimestampMs );

    private sealed class RecordingNode
    {
        public required string Identifier { get; init; }
        public required string Operation { get; init; }
        public List<CustomSample> Samples { get; } = [];
        public double TotalMs, MeanMs, StdDevMs, P50, P75, P95, P99;
        public double FirstMs, LastMs;
        public int ExcBudgetCount;
    }
}
