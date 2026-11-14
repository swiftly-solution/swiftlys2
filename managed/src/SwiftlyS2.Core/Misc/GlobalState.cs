using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;

namespace SwiftlyS2.Core.Misc;

internal static class GlobalState
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Native
    {
        public int Size;
        public nint RootPath;
        public nint LogPath;
        public nint LogLevel;
        public int HideLogInConsole;
        public int ManagedLogEnabled;
        public int ManagedLogIntervalMs;
        public int ProfilerLevel;
        public int PluginManualLoadState;
        public nint PluginLoadOrder;
        public nint ServerLanguage;
        public int IsFollowingServerGuidelines;
        public int UsePlayerLanguage;
        public int UseAutoHotReload;
    }

    public static string RootPath { get; private set; } = AppContext.BaseDirectory;
    public static string LogPath { get; private set; } = string.Empty;
    public static LogLevel MinLogLevel { get; private set; } = LogLevel.Information;
    public static bool HideLogInConsole { get; private set; }
    public static bool ManagedLogEnabled { get; private set; } = true;
    public static int ManagedLogIntervalMs { get; private set; } = 2000;
    public static int ProfilerLevel { get; private set; }
    public static bool PluginManualLoadState { get; private set; }
    public static string PluginLoadOrder { get; private set; } = string.Empty;
    public static string ServerLanguage { get; private set; } = "en";
    public static bool IsFollowingServerGuidelines { get; private set; } = true;
    public static bool UsePlayerLanguage { get; private set; } = true;
    public static bool UseAutoHotReload { get; private set; } = true;

    public static new string ToString() =>
        $"GlobalState {{ RootPath = {RootPath}, LogPath = {LogPath}, MinLogLevel = {MinLogLevel}, " +
        $"HideLogInConsole = {HideLogInConsole}, ManagedLogEnabled = {ManagedLogEnabled}, " +
        $"ManagedLogIntervalMs = {ManagedLogIntervalMs}, ProfilerLevel = {ProfilerLevel}, " +
        $"PluginManualLoadState = {PluginManualLoadState}, PluginLoadOrder = {PluginLoadOrder}, " +
        $"ServerLanguage = {ServerLanguage}, IsFollowingServerGuidelines = {IsFollowingServerGuidelines}, " +
        $"UsePlayerLanguage = {UsePlayerLanguage}, UseAutoHotReload = {UseAutoHotReload} }}";

    public static unsafe void Load( Native* native )
    {
        if (native->Size != sizeof(Native))
            throw new InvalidOperationException($"GlobalState size mismatch: native {native->Size}, managed {sizeof(Native)}. Core and managed builds are out of sync.");

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
        ProfilerLevel = native->ProfilerLevel;
        PluginManualLoadState = native->PluginManualLoadState != 0;
        PluginLoadOrder = Marshal.PtrToStringUTF8(native->PluginLoadOrder) ?? string.Empty;
        ServerLanguage = Marshal.PtrToStringUTF8(native->ServerLanguage) ?? "en";
        IsFollowingServerGuidelines = native->IsFollowingServerGuidelines != 0;
        UsePlayerLanguage = native->UsePlayerLanguage != 0;
        UseAutoHotReload = native->UseAutoHotReload != 0;
    }
}
