#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;

namespace SwiftlyS2.Core.Natives;

internal static class NativeConsoleOutput
{
    private static readonly int _MainThreadID;

    private static readonly unsafe delegate* unmanaged< nint, ulong > _AddConsoleListener;

    /// <summary>
    /// callback should receive: string message
    /// </summary>
    public unsafe static ulong AddConsoleListener( nint callback )
    {
        var ret = _AddConsoleListener(callback);
        return ret;
    }

    private static readonly unsafe delegate* unmanaged< ulong, void > _RemoveConsoleListener;

    public unsafe static void RemoveConsoleListener( ulong listenerId )
    {
        _RemoveConsoleListener(listenerId);
    }

    private static readonly unsafe delegate* unmanaged< byte > _IsEnabled;

    /// <summary>
    /// returns whether console filtering is enabled
    /// </summary>
    public unsafe static bool IsEnabled()
    {
        var ret = _IsEnabled();
        return ret == 1;
    }

    private static readonly unsafe delegate* unmanaged< void > _ToggleFilter;

    /// <summary>
    /// toggles the console filter on/off
    /// </summary>
    public unsafe static void ToggleFilter()
    {
        _ToggleFilter();
    }

    private static readonly unsafe delegate* unmanaged< void > _ReloadFilterConfiguration;

    /// <summary>
    /// reloads the filter configuration from file
    /// </summary>
    public unsafe static void ReloadFilterConfiguration()
    {
        _ReloadFilterConfiguration();
    }

    private static readonly unsafe delegate* unmanaged< byte*, byte > _NeedsFiltering;

    /// <summary>
    /// checks if a message needs filtering
    /// </summary>
    public unsafe static bool NeedsFiltering( string text )
    {
        var pool = ArrayPool<byte>.Shared;
        var textLength = Encoding.UTF8.GetByteCount(text);
        var textBuffer = pool.Rent(textLength + 1);
        _ = Encoding.UTF8.GetBytes(text, textBuffer);
        textBuffer[textLength] = 0;
        fixed (byte* textBufferPtr = textBuffer)
        {
            var ret = _NeedsFiltering(textBufferPtr);
            pool.Return(textBuffer);
            return ret == 1;
        }
    }

    private static readonly unsafe delegate* unmanaged< byte*, int > _GetCounterText;

    /// <summary>
    /// gets the counter text showing how many messages were filtered
    /// </summary>
    public unsafe static string GetCounterText()
    {
        var ret = _GetCounterText(null);
        var pool = ArrayPool<byte>.Shared;
        var retBuffer = pool.Rent(ret + 1);
        fixed (byte* retBufferPtr = retBuffer)
        {
            ret = _GetCounterText(retBufferPtr);
            var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
            pool.Return(retBuffer);
            return retString;
        }
    }
}