using System.Reflection;
using System.Runtime.InteropServices;
using Spectre.Console;

namespace SwiftlyS2.Core.Natives;

internal class NativeBinding
{
    public static bool IsMainThread => NativeCore.IsMainThread();

    public static void ThrowIfNonMainThread()
    {
        if (!IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
    }

    public static void BindNatives( IntPtr nativeTable, int nativeTableSize )
    {
        var currentClassname = "";
        var currentFunctionName = "";

        unsafe
        {
            try
            {
                var pNativeTables = (NativeFunction*)nativeTable;

                for (var i = 0; i < nativeTableSize; i++)
                {
                    var name = Marshal.PtrToStringUTF8(pNativeTables[i].Name)!;

                    var names = name.Split('.');
                    var className = names[0];
                    var funcName = names[1];

                    currentClassname = className;
                    currentFunctionName = funcName;

                    var nativeNameSpace = "SwiftlyS2.Core.Natives.Native" + className;

                    var nativeClass = Type.GetType(nativeNameSpace);
                    if(nativeClass == null)
                    {
                        Console.WriteLine($"Failed to bind native function {funcName} in class {className}. Class not found.");
                        continue;
                    }

                    var nativeStaticField = nativeClass.GetField("_" + funcName,
                        BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

                    if(nativeStaticField == null)
                    {
                        Console.WriteLine($"Failed to bind native function {funcName} in class {className}. Field not found.");
                        continue;
                    }

                    nativeStaticField.SetValue(null, pNativeTables[i].Function);
                }
            }
            catch (Exception e)
            {
                if (!GlobalExceptionHandler.Handle(ref e)) return;
                Console.WriteLine($"Failed to bind native function {currentFunctionName} in class {currentClassname}.");
                AnsiConsole.WriteException(e);
            }
        }
    }
}
