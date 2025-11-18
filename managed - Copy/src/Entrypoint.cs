using System.Runtime.InteropServices;
using System.Security;
using Spectre.Console;
using SwiftlyS2.Core;

namespace SwiftlyS2;

internal class Entrypoint
{
    [UnmanagedCallersOnly]
    [SecurityCritical]
    public unsafe static void Start( IntPtr nativeTable, int nativeTableSize, IntPtr basePath )
    {
        try
        {
            Bootstrap.Start(nativeTable, nativeTableSize, Marshal.PtrToStringUTF8(basePath)!);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            AnsiConsole.WriteException(e);
        }
    }
}