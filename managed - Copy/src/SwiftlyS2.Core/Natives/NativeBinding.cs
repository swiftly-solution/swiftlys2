using System.Reflection;
using System.Runtime.InteropServices;
using Spectre.Console;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal class NativeBinding
{
  public static void BindNatives( IntPtr nativeTable, int nativeTableSize )
  {
    unsafe
    {
      try
      {
        var pNativeTables = (NativeFunction*)nativeTable;


        for (int i = 0; i < nativeTableSize; i++)
        {
          var name = Marshal.PtrToStringUTF8(pNativeTables[i].Name)!;

          var names = name.Split('.');
          var className = names[0];
          var funcName = names[1];

          var nativeNameSpace = "SwiftlyS2.Core.Natives.Native" + className;

          var nativeClass = Type.GetType(nativeNameSpace)!;
          var nativeStaticField = nativeClass.GetField("_" + funcName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
          nativeStaticField!.SetValue(null, pNativeTables[i].Function);
          var mainThreadIDStaticField = nativeClass.GetField("_MainThreadID", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
          mainThreadIDStaticField!.SetValue(null, Thread.CurrentThread.ManagedThreadId);
        }
      }
      catch (Exception e)
      {
        if (!GlobalExceptionHandler.Handle(e)) return;
        AnsiConsole.WriteException(e);
      }
    }
  }

}
