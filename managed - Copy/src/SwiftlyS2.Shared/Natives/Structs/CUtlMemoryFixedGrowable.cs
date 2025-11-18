using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CUtlMemoryFixedGrowable<T, TBuffer>
    where T : unmanaged
    where TBuffer : unmanaged
{
    private CUtlMemory<T> _memory;
    private TBuffer _fixedMemory;

    public CUtlMemoryFixedGrowable(int size, int growSize = 0)
    {
        _memory = new CUtlMemory<T>((nint)Unsafe.AsPointer(ref _fixedMemory), size, false);
    }

    public readonly nint Base => _memory.Base;
    public readonly int AllocationCount => _memory.Count;
}

[InlineArray(512)]
public struct FixedCharBuffer512
{
    private byte _element0;
}

[InlineArray(64)]
public struct FixedPtrBuffer64
{
    private nint _element0;
}