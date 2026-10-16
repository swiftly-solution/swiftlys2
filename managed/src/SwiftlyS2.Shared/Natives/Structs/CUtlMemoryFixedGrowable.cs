using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CUtlMemoryFixedGrowable<T, TBuffer> : IDisposable
    where T : unmanaged
    where TBuffer : unmanaged
{
    private CUtlMemory<T> memory;
    private readonly TBuffer fixedMemory;

    public CUtlMemoryFixedGrowable( int size, int growSize = 0 )
    {
        ArgumentOutOfRangeException.ThrowIfNegative(size);
        if (size > Unsafe.SizeOf<TBuffer>() / sizeof(T)) throw new ArgumentOutOfRangeException(nameof(size));
        memory = new CUtlMemory<T>(growSize, size);
    }

    public readonly nint Base => memory.Base;
    public readonly int AllocationCount => memory.Count;

    public void Dispose() => memory.Purge();
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
