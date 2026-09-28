using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Misc;

internal class ExternDLL
{
    [DllImport("tier0", SetLastError = true)]
    public static extern nint UtlVectorMemory_Alloc(nint pMemory, [MarshalAs(UnmanagedType.Bool)] bool bRealloc, int newSize, int oldSize);

    [DllImport("tier0", SetLastError = true)]
    public static extern void UtlVectorMemory_FailedAllocation(int totalElements, int newElements);

    [DllImport("tier0", SetLastError = true)]
    public static extern int UtlVectorMemory_CalcNewAllocationCount(int allocationCount, int growSize, int newSize, int bytesItem);
}