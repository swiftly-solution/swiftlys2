using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

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
            var stackTrace = new StackTrace(1, true); // Skip this method
            var frames = new List<object>();

            int frameIndex = 0;
            foreach (var frame in stackTrace.GetFrames())
            {
                var method = frame.GetMethod();
                if (method == null) continue;

                var declaringType = method.DeclaringType;
                var typeName = declaringType?.FullName ?? "<unknown>";
                var methodName = method.Name;
                var fileName = frame.GetFileName();
                var lineNumber = frame.GetFileLineNumber();
                var ilOffset = frame.GetILOffset();

                var frameInfo = new Dictionary<string, object> {
                    ["index"] = frameIndex++,
                    ["type"] = typeName,
                    ["method"] = methodName,
                    ["ilOffset"] = $"0x{ilOffset:X4}"
                };

                if (!string.IsNullOrEmpty(fileName))
                {
                    frameInfo["file"] = Path.GetFileName(fileName);
                    frameInfo["line"] = lineNumber;
                }

                // Add method signature if available
                var parameters = method.GetParameters();
                if (parameters.Length > 0)
                {
                    var paramTypes = parameters.Select(p => p.ParameterType.Name).ToArray();
                    frameInfo["parameters"] = string.Join(", ", paramTypes);
                }

                frames.Add(frameInfo);
            }

            var result = new Dictionary<string, object> {
                ["captureMethod"] = "StackTrace.GetFrames",
                ["captureTimeUtc"] = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                ["threadId"] = Environment.CurrentManagedThreadId,
                ["threadName"] = Thread.CurrentThread.Name ?? "(unnamed)",
                ["frameCount"] = frames.Count,
                ["frames"] = frames
            };

            var json = JsonSerializer.Serialize(result, new JsonSerializerOptions {
                WriteIndented = false
            });

            var bytes = Encoding.UTF8.GetBytes(json);
            int copyLen = Math.Min(bytes.Length, bufferSize - 1);

            Marshal.Copy(bytes, 0, (IntPtr)buffer, copyLen);
            buffer[copyLen] = 0; // Null terminate

            return copyLen;
        }
        catch
        {
            return 0;
        }
    }
}