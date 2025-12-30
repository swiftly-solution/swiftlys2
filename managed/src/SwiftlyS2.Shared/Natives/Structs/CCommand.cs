using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CCommand
{
    private enum COMMAND : int
    {
        MAX_ARGC = 64,
        MAX_LENGTH = 512,
    };

    private int argv0Size;
    private CUtlVectorFixedGrowable<byte, FixedCharBuffer512> argSBuffer;
    private CUtlVectorFixedGrowable<byte, FixedCharBuffer512> argvBuffer;
    private CUtlVectorFixedGrowable<nint, FixedPtrBuffer64> args;

    public readonly int ArgC => args.Count;
    public readonly string? ArgS => argv0Size == 0 ? null : Marshal.PtrToStringUTF8(argSBuffer.Base + argv0Size);
    public readonly string? GetCommandString => ArgC == 0 ? null : Marshal.PtrToStringUTF8(argSBuffer.Base);

    public readonly string? Arg( int index ) => (index < 0 || index >= ArgC) ? null : Marshal.PtrToStringUTF8(args[index]);
    public readonly string? this[int index] => Arg(index);

    // Idk if this will work
    public CCommand()
    {
        argv0Size = 0;
        argSBuffer = new CUtlVectorFixedGrowable<byte, FixedCharBuffer512>((int)COMMAND.MAX_LENGTH);
        argvBuffer = new CUtlVectorFixedGrowable<byte, FixedCharBuffer512>((int)COMMAND.MAX_LENGTH);
        args = new CUtlVectorFixedGrowable<nint, FixedPtrBuffer64>((int)COMMAND.MAX_ARGC);
        EnsureBuffers();
        Reset();
    }

    public CCommand( string commandString ) : this()
    {
        _ = Tokenize(commandString);
    }

    private void EnsureBuffers()
    {
        argSBuffer.SetSize(MaxCommandLength());
        argvBuffer.SetSize(MaxCommandLength());
    }

    public void Reset()
    {
        argv0Size = 0;
        ((byte*)argSBuffer.Base)[0] = 0;
        argSBuffer.RemoveAll();
        argvBuffer.RemoveAll();
        args.RemoveAll();
    }

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
            Unsafe.CopyBlock((byte*)argSBuffer.Base, pCmd, (uint)nLen);
            ((byte*)argSBuffer.Base)[nLen] = 0;
        }

        var pSBuf = (byte*)argSBuffer.Base;
        var pArgvBuf = (byte*)argvBuffer.Base;
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

                    _ = args.AddToTail((nint)(pArgvBuf + nArgvBufferSize));

                    if (args.Count == 1)
                    {
                        // For the first token, argv0Size should point to where ArgS begins
                        // If this is the only token, ArgS should be empty, so argv0Size = string length
                        if (i == nLen) // This is the last token (end of string)
                        {
                            argv0Size = nLen; // Point to the null terminator
                        }
                        else
                        {
                            argv0Size = i + 1; // Point to after the space following the first token
                        }
                    }

                    nArgvBufferSize = (int)(pDest - pArgvBuf) + 1;
                }
                tokenStart = i + 1;

                if (ch == 0)
                {
                    break;
                }
            }
        }

        return args.Count > 0;
    }
}