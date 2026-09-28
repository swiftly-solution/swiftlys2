using System.Text;

namespace SwiftlyS2.Core.Services.Profiler;

internal static class LightweightSummaryWriter
{
    private const double TickRateHz = 64.0;
    private const double BudgetMs = 1000.0 / TickRateHz;
    private const int SectionWidth = 70;
    private const string Bar = "━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";

    public static string Write( LightweightSnapshot snapshot )
    {
        var sb = new StringBuilder();

        var durationSec = snapshot.SessionElapsedMs / 1000.0;
        var totalTicks = (long)(durationSec * TickRateHz);

        var allNodes = snapshot.MethodGroups.Values.SelectMany(d => d.Values).ToList();
        var activeTotal = allNodes.Sum(n => n.TotalMs);
        var totalCalls = allNodes.Sum(n => n.Count);
        var avgMsPerTick = totalTicks > 0 ? activeTotal / totalTicks : 0.0;

        _ = sb.AppendLine(Bar);
        _ = sb.AppendLine(" SwiftlyS2 Profiler - Summary");
        _ = sb.AppendLine(Bar);
        _ = sb.AppendLine("  mode      Harmony");
        _ = sb.AppendLine($"  captured  {durationSec:F2} s  ·  {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} UTC");
        _ = sb.AppendLine(Bar);
        _ = sb.AppendLine();

        if (activeTotal <= 0 && snapshot.CustomGroups.Count == 0)
        {
            _ = sb.AppendLine("Empty capture — no calls recorded.");
            return sb.ToString();
        }

        _ = sb.AppendLine($"  cpu       {activeTotal:N0} ms managed  /  {totalCalls:N0} calls");
        _ = sb.AppendLine($"  ticks     {totalTicks:N0}  →  {avgMsPerTick:F3} ms/tick avg");
        _ = sb.AppendLine();
        _ = sb.AppendLine("  Legend:  ms/t = managed time per tick (steady drag)");
        _ = sb.AppendLine("           tot  = total time across capture (sporadic spikes)");
        _ = sb.AppendLine("           inc% = share of active time    exc% = same (no call-tree split at this level)");
        _ = sb.AppendLine("  Read:    high ms/t                → slow function");
        _ = sb.AppendLine("           high tot · low ms/t      → random spikes");
        _ = sb.AppendLine();

        var pluginNodes = snapshot.MethodGroups
            .Where(kv => kv.Key != "SwiftlyS2")
            .SelectMany(kv => kv.Value.Values)
            .ToList();
        var swiftlys2Nodes = snapshot.MethodGroups.TryGetValue("SwiftlyS2", out var s2) ? s2.Values.ToList() : [];

        WriteNodeSection(sb, "Plugins", pluginNodes, activeTotal, totalTicks);
        WriteNodeSection(sb, "SwiftlyS2", swiftlys2Nodes, activeTotal, totalTicks);

        WriteCustomSection(sb, snapshot.CustomGroups);

        return sb.ToString();
    }

    private static void WriteNodeSection(
        StringBuilder sb,
        string label,
        List<LightweightRecordingSnapshot> nodes,
        double activeTotal,
        long totalTicks )
    {
        var sectionTotal = nodes.Sum(n => n.TotalMs);
        var sectionPct = activeTotal > 0 ? sectionTotal / activeTotal * 100.0 : 0.0;
        var sectionMsPerTick = totalTicks > 0 ? sectionTotal / totalTicks : 0.0;

        var heading = $"▸ {label}";
        var pctStr = $"{Math.Min(sectionPct, 999.0):F2}%  ·  {sectionMsPerTick:F3} ms/tick";
        var pad = Math.Max(1, SectionWidth - heading.Length - pctStr.Length);
        _ = sb.AppendLine($"{heading}{new string(' ', pad)}{pctStr}");
        _ = sb.AppendLine(new string('-', SectionWidth));
        _ = sb.AppendLine();

        if (nodes.Count == 0)
        {
            _ = sb.AppendLine("  No calls recorded.");
            _ = sb.AppendLine();
            return;
        }

        _ = sb.AppendLine($"  {"ms/t",7}  {"tot ms",6}  {"calls",6}  {"inc%",5}   {"exc%",5}       {"Async",5}  Method");

        var ordered = nodes.OrderByDescending(n => n.TotalMs).ToList();
        foreach (var n in ordered)
        {
            var pct = Math.Min(activeTotal > 0 ? n.TotalMs / activeTotal * 100.0 : 0.0, 999.0);
            var msPerTick = totalTicks > 0 ? n.TotalMs / totalTicks : 0.0;
            var flag = msPerTick > BudgetMs ? "▲!" : "  ";
            var asyncTag = IsAsync(n.Operation) ? "Async" : "     ";
            _ = sb.AppendLine($"  {msPerTick,7:F3}  {n.TotalMs,6:F0}  {n.Count,6}  {pct,5:F2}%  {pct,5:F2}%  {flag}  {asyncTag}  {n.Operation}");
        }
        _ = sb.AppendLine();
    }

    private static bool IsAsync( string? name )
        => name != null && (name.Contains(">d__", StringComparison.Ordinal) || name.Contains(".MoveNext()", StringComparison.Ordinal));

    private static void WriteCustomSection(
        StringBuilder sb,
        Dictionary<string, Dictionary<string, LightweightRecordingSnapshot>> pluginGroups )
    {
        _ = sb.AppendLine("▸ Custom Recordings");
        _ = sb.AppendLine(new string('-', SectionWidth));
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
        var allNodes = pluginGroups.Values.SelectMany(d => d.Values).ToList();
        var rootFirst = allNodes.Count > 0 ? allNodes.Min(n => n.FirstMs) : 0;
        var rootLast = allNodes.Count > 0 ? allNodes.Max(n => n.LastMs) : 0;
        var rootCalls = allNodes.Sum(n => n.Count);

        _ = sb.AppendLine(FormatRow("", "All Recordings",
            100.0, 0.0,
            rootFirst, rootLast,
            rootTotal, rootCalls > 0 ? rootTotal / rootCalls : 0,
            0, 0, 0, 0, 0,
            allNodes.Sum(n => n.ExcBudgetCount)));

        var sortedGroups = groupTotals.OrderByDescending(kv => kv.Value).ToList();
        for (var gi = 0; gi < sortedGroups.Count; gi++)
        {
            var groupIsLast = gi == sortedGroups.Count - 1;
            var groupName = sortedGroups[gi].Key;
            var groupTotal = sortedGroups[gi].Value;
            var leaves = pluginGroups[groupName].Values.OrderByDescending(n => n.TotalMs).ToList();
            var groupFirst = leaves.Count > 0 ? leaves.Min(n => n.FirstMs) : 0;
            var groupLast = leaves.Count > 0 ? leaves.Max(n => n.LastMs) : 0;
            var groupCalls = leaves.Sum(n => n.Count);
            var groupMean = groupCalls > 0 ? groupTotal / groupCalls : 0;
            var groupIncPct = rootTotal > 0 ? groupTotal / rootTotal * 100.0 : 0;
            var groupExcBudget = leaves.Sum(n => n.ExcBudgetCount);
            var gConnector = groupIsLast ? "└─" : "├─";

            _ = sb.AppendLine(FormatRow($"  {gConnector} ", $"[{groupName}]",
                groupIncPct, 0.0,
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
}
