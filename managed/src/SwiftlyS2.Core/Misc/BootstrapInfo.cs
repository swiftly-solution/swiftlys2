using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace SwiftlyS2.Core.Misc;

internal static class BootstrapInfo
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Native
    {
        public int Size;
        public IntPtr RootPath;
        public IntPtr LogPath;
        public IntPtr LogLevel;
        public int HideLogInConsole;
        public int ManagedLogEnabled;
        public int ManagedLogIntervalMs;
    }

    public static string RootPath { get; private set; } = AppContext.BaseDirectory;
    public static string LogPath { get; private set; } = string.Empty;
    public static LogLevel MinLogLevel { get; private set; } = LogLevel.Information;
    public static bool HideLogInConsole { get; private set; }
    public static bool ManagedLogEnabled { get; private set; } = true;
    public static int ManagedLogIntervalMs { get; private set; } = 2000;

    public static unsafe void Load( Native* native )
    {
        if (native->Size != sizeof(Native))
            throw new InvalidOperationException($"BootstrapInfo size mismatch: native {native->Size}, managed {sizeof(Native)}. Core and managed builds are out of sync.");

        RootPath = Marshal.PtrToStringUTF8(native->RootPath)!;
        LogPath = Marshal.PtrToStringUTF8(native->LogPath)!;
        MinLogLevel = Marshal.PtrToStringUTF8(native->LogLevel)?.ToUpperInvariant() switch {
            "DEBUG" => LogLevel.Debug,
            "WARNING" => LogLevel.Warning,
            "ERROR" => LogLevel.Error,
            "OFF" => LogLevel.None,
            _ => LogLevel.Information
        };
        HideLogInConsole = native->HideLogInConsole != 0;
        ManagedLogEnabled = native->ManagedLogEnabled != 0;
        ManagedLogIntervalMs = native->ManagedLogIntervalMs > 0 ? native->ManagedLogIntervalMs : 2000;
    }
}
