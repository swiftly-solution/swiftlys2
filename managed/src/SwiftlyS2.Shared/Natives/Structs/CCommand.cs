using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CCommand
{
    private enum COMMAND : int
    {
        MAX_ARGC = 64,
        MAX_LENGTH = 512,
    };

    private int _argv0Size;
    private CUtlVectorFixedGrowable<byte, FixedCharBuffer512> _argSBuffer;
    private CUtlVectorFixedGrowable<byte, FixedCharBuffer512> _argvBuffer;
    private CUtlVectorFixedGrowable<nint, FixedPtrBuffer64> _args;

    // Idk if this will work
    public CCommand()
    {
        _argv0Size = 0;
        _argSBuffer = new CUtlVectorFixedGrowable<byte, FixedCharBuffer512>((int)COMMAND.MAX_LENGTH);
        _argvBuffer = new CUtlVectorFixedGrowable<byte, FixedCharBuffer512>((int)COMMAND.MAX_LENGTH);
        _args = new CUtlVectorFixedGrowable<nint, FixedPtrBuffer64>((int)COMMAND.MAX_ARGC);
        EnsureBuffers();
        Reset();
    }

    public CCommand( string commandString ) : this()
    {
        _ = Tokenize(commandString);
    }

    private void EnsureBuffers()
    {
        _argSBuffer.SetSize(MaxCommandLength());
        _argvBuffer.SetSize(MaxCommandLength());
    }

    public void Reset()
    {
        _argv0Size = 0;
        ((byte*)_argSBuffer.Base)[0] = 0;
        _args.RemoveAll();
    }

    public readonly int ArgC => _args.Count;

    public readonly string? ArgS => _argv0Size == 0 ? null : Marshal.PtrToStringUTF8(_argSBuffer.Base + _argv0Size);

    public readonly string? GetCommandString => ArgC == 0 ? null : Marshal.PtrToStringUTF8(_argSBuffer.Base);

    public readonly string? Arg( int index ) => (index < 0 || index >= ArgC) ? null : Marshal.PtrToStringUTF8(_args[index]);

    public readonly string? this[int index] => Arg(index);

    public readonly int FindArg( string name )
    {
        var nArgC = ArgC;
        for (var i = 1; i < nArgC; i++)
        {
            var arg = Arg(i);
            if (arg != null && string.Equals(arg, name, StringComparison.OrdinalIgnoreCase))
            {
                return (i + 1) < nArgC ? i + 1 : -1;
            }
        }
        return -1;
    }

    public readonly int FindArgInt( string name, int defaultVal )
    {
        var idx = FindArg(name);
        if (idx != -1)
        {
            var arg = Arg(idx);
            if (arg != null && int.TryParse(arg, out var result))
            {
                return result;
            }
        }
        return defaultVal;
    }

    public static int MaxCommandLength() => (int)COMMAND.MAX_LENGTH - 1;

    public bool Tokenize( string commandString )
    {
        if (string.IsNullOrWhiteSpace(commandString))
        {
            return false;
        }

        Reset();

        var cmdBytes = System.Text.Encoding.UTF8.GetBytes(commandString);
        var nLen = cmdBytes.Length;

        if (nLen >= MaxCommandLength())
        {
            return false;
        }

        fixed (byte* pCmd = cmdBytes)
        {
            Unsafe.CopyBlock((byte*)_argSBuffer.Base, pCmd, (uint)nLen);
            ((byte*)_argSBuffer.Base)[nLen] = 0;
        }

        var pSBuf = (byte*)_argSBuffer.Base;
        var pArgvBuf = (byte*)_argvBuffer.Base;
        var nArgvBufferSize = 0;
        var inQuotes = false;
        var tokenStart = 0;

        for (var i = 0; i <= nLen; ++i)
        {
            var ch = i < nLen ? pSBuf[i] : (byte)0;
            var isBreak = (ch == 0 || ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r') && !inQuotes;

            if (ch == '"')
            {
                inQuotes = !inQuotes;
                continue;
            }

            if (isBreak)
            {
                if (i > tokenStart)
                {
                    var tokenLen = i - tokenStart;
                    var pDest = pArgvBuf + nArgvBufferSize;

                    for (var j = 0; j < tokenLen; ++j)
                    {
                        var srcCh = pSBuf[tokenStart + j];
                        if (srcCh != '"')
                        {
                            *pDest++ = srcCh;
                        }
                    }
                    *pDest = 0;

                    _ = _args.AddToTail((nint)(pArgvBuf + nArgvBufferSize));

                    if (_args.Count == 1)
                    {
                        _argv0Size = tokenStart;
                    }

                    nArgvBufferSize = (int)(pDest - pArgvBuf) + 1;
                }
                tokenStart = i + 1;

                if (ch == 0) break;
            }
        }

        return _args.Count > 0;
    }
}