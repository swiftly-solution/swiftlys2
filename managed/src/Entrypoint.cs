using System.Runtime.InteropServices;
using Spectre.Console;
using SwiftlyS2.Core;
using SwiftlyS2.Core.Misc;

namespace SwiftlyS2;

internal class Entrypoint
{
    [UnmanagedCallersOnly]
    public static unsafe void Start( IntPtr nativeTable, int nativeTableSize, GlobalState.Native* globalState )
    {
        try
        {
            GlobalState.Load(globalState);
            Bootstrap.Start(nativeTable, nativeTableSize);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }
}