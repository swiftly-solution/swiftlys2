using System.Text.Json;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Core.Diagnostics;

internal static class StackTraceExport
{
    /// <summary>
    /// Get current managed stack trace as JSON string
    /// Called from native C++ crash handler
    /// </summary>
    /// <param name="buffer">Buffer to write JSON to</param>
    /// <param name="bufferSize">Size of the buffer</param>
    /// <returns>Number of bytes written, or 0 on failure</returns>
    [UnmanagedCallersOnly]
    public static unsafe int GetStackTraceJson( byte* buffer, int bufferSize )
    {
        if (buffer == null || bufferSize <= 0)
        {
            return 0;
        }

        try
        {
            var stackTrace = new StackTrace(1, true); // Skip current method
            var frames = new List<object>();

            int frameIndex = 0;
            foreach (var frame in stackTrace.GetFrames())
            {
                var method = frame.GetMethod();
                if (method == null)
                {
                    continue;
                }

                var frameInfo = new Dictionary<string, object> {
                    ["index"] = frameIndex++,
                    ["type"] = method.DeclaringType?.FullName ?? "<unknown>",
                    ["method"] = method.Name,
                    ["ilOffset"] = $"0x{frame.GetILOffset():X4}"
                };

                var fileName = frame.GetFileName();
                if (!string.IsNullOrWhiteSpace(fileName))
                {
                    frameInfo["file"] = Path.GetFileName(fileName);
                    frameInfo["line"] = frame.GetFileLineNumber();
                }

                frameInfo["pattern"] = $"{(method is MethodInfo mi ? mi.ReturnType.Name : "Void")} ({string.Join(", ", method.GetParameters().Select(p => p.ParameterType.Name))})";

                frames.Add(frameInfo);
            }

            // ThreadPool.GetAvailableThreads(out var availableWorkerThreads, out var availableCompletionPortThreads);
            // ThreadPool.GetMaxThreads(out var maxWorkerThreads, out var maxCompletionPortThreads);
            // var busyWorkerThreads = maxWorkerThreads - availableWorkerThreads;
            // var processThreadCount = System.Diagnostics.Process.GetCurrentProcess().Threads.Count;

            var result = new Dictionary<string, object> {
                ["note"] = "This stack trace is for reference only and may not be fully accurate",
                ["captureMethod"] = "StackTrace.GetFrames",
                ["frameCount"] = frames.Count,
                ["frames"] = frames,
                // ["data"] = new Dictionary<string, object> {
                //     ["threadId"] = Environment.CurrentManagedThreadId,
                //     ["threadName"] = Thread.CurrentThread.Name ?? "(unnamed)",
                //     ["heapMemory"] = $"{GC.GetTotalMemory(false) / 1024.0f / 1024.0f:0.00} MB",
                //     ["processThreads"] = processThreadCount,
                //     ["workerThreads"] = $"{busyWorkerThreads}/{maxWorkerThreads} (Busy/Max)",
                //     ["completionPortThreads"] = $"{maxCompletionPortThreads - availableCompletionPortThreads}/{maxCompletionPortThreads} (Busy/Max)"
                // }
            };

            var utf8 = JsonSerializer.SerializeToUtf8Bytes(result);
            int len = Math.Min(utf8.Length, bufferSize - 1);
            utf8.AsSpan(0, len).CopyTo(new Span<byte>(buffer, len));
            buffer[len] = 0;

            return len;
        }
        catch
        {
            return 0;
        }
    }
}