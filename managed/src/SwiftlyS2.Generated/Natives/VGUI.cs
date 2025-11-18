#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeVGUI
{
    private static readonly int _MainThreadID;

    private static readonly unsafe delegate* unmanaged< ulong > _RegisterScreenText;

    public unsafe static ulong RegisterScreenText()
    {
        var ret = _RegisterScreenText();
        return ret;
    }

    private static readonly unsafe delegate* unmanaged< ulong, void > _UnregisterScreenText;

    public unsafe static void UnregisterScreenText( ulong textid )
    {
        _UnregisterScreenText(textid);
    }

    private static readonly unsafe delegate* unmanaged< ulong, Color, int, byte, byte, void > _ScreenTextCreate;

    public unsafe static void ScreenTextCreate( ulong textid, Color col, int fontsize, bool drawBackground, bool isMenu )
    {
        _ScreenTextCreate(textid, col, fontsize, drawBackground ? (byte)1 : (byte)0, isMenu ? (byte)1 : (byte)0);
    }

    private static readonly unsafe delegate* unmanaged< ulong, byte*, void > _ScreenTextSetText;

    public unsafe static void ScreenTextSetText( ulong textid, string text )
    {
        var pool = ArrayPool<byte>.Shared;
        var textLength = Encoding.UTF8.GetByteCount(text);
        var textBuffer = pool.Rent(textLength + 1);
        _ = Encoding.UTF8.GetBytes(text, textBuffer);
        textBuffer[textLength] = 0;
        fixed (byte* textBufferPtr = textBuffer)
        {
            _ScreenTextSetText(textid, textBufferPtr);
            pool.Return(textBuffer);
        }
    }

    private static readonly unsafe delegate* unmanaged< ulong, Color, void > _ScreenTextSetColor;

    public unsafe static void ScreenTextSetColor( ulong textid, Color col )
    {
        _ScreenTextSetColor(textid, col);
    }

    private static readonly unsafe delegate* unmanaged< ulong, float, float, void > _ScreenTextSetPosition;

    /// <summary>
    /// 0.0-1.0, where 0.0 is bottom/left, and 1.0 is top/right
    /// </summary>
    public unsafe static void ScreenTextSetPosition( ulong textid, float x, float y )
    {
        _ScreenTextSetPosition(textid, x, y);
    }
}