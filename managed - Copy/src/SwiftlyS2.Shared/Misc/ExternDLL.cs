using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Misc;

class ExternDLL
{
    [DllImport("tier0", SetLastError = true)]
    public static extern IntPtr UtlVectorMemory_Alloc(IntPtr pMemory, [MarshalAs(UnmanagedType.Bool)] bool bRealloc, int newSize, int oldSize);

    [DllImport("tier0", SetLastError = true)]
    public static extern void UtlVectorMemory_FailedAllocation(int totalElements, int newElements);

    [DllImport("tier0", SetLastError = true)]
    public static extern int UtlVectorMemory_CalcNewAllocationCount(int allocationCount, int growSize, int newSize, int bytesItem);

    public static IntPtr LoadLibrary(string dllName)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return NativeLibrary.Load("lib" + dllName + ".so");
        }
        else
        {
            return NativeLibrary.Load(dllName + ".dll");
        }
    }

    public static IntPtr GetAddress(IntPtr libraryHandle, string functionName)
    {
        return NativeLibrary.GetExport(libraryHandle, functionName);
    }

    public static void CloseLibrary(IntPtr libraryHandle)
    {
        NativeLibrary.Free(libraryHandle);
    }

    public static T GetExportedFunction<T>(string dllName, string functionName) where T : Delegate
    {
        IntPtr hModule = LoadLibrary(dllName);
        if (hModule == IntPtr.Zero)
            throw new Exception("Module not found: " + dllName);

        IntPtr pFunc = GetAddress(hModule, functionName);
        if (pFunc == IntPtr.Zero)
            throw new Exception("Export not found: " + functionName);

        T func = Marshal.GetDelegateForFunctionPointer<T>(pFunc);
        CloseLibrary(hModule);
        return func;
    }

    public static unsafe T GetExportedVariable<T>(string dllName, string variableName)
    {
        IntPtr hModule = LoadLibrary(dllName);
        if (hModule == IntPtr.Zero)
            throw new Exception("Module not found: " + dllName);

        IntPtr pVar = GetAddress(hModule, variableName);
        if (pVar == IntPtr.Zero)
            throw new Exception("Export not found: " + variableName);

        T v = Unsafe.Read<T>(pVar.ToPointer());
        CloseLibrary(hModule);
        return v;
    }
}