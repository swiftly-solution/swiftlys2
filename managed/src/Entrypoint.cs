using System.Runtime.InteropServices;
using Spectre.Console;
using SwiftlyS2.Core;

namespace SwiftlyS2;

internal class Entrypoint
{
    [UnmanagedCallersOnly]
    public static unsafe void Start( IntPtr nativeTable, int nativeTableSize, IntPtr basePath, IntPtr logsPath, IntPtr setStackTraceCallbackPtr )
    {
        try
        {
            Bootstrap.Start(nativeTable, nativeTableSize, Marshal.PtrToStringUTF8(basePath)!, Marshal.PtrToStringUTF8(logsPath)!, setStackTraceCallbackPtr);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }
}