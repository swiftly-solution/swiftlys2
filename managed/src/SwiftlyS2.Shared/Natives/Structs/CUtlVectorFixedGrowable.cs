using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CUtlVectorFixedGrowable<T, TBuffer>
    where T : unmanaged
    where TBuffer : unmanaged
{
    private CUtlMemoryFixedGrowable<T, TBuffer> _memory;

    public CUtlVectorFixedGrowable( int maxSize, int growSize = 0 )
    {
        _memory = new CUtlMemoryFixedGrowable<T, TBuffer>(maxSize, growSize);
        Count = 0;
    }

    public void SetSize( int size )
    {
        Count = size;
    }

    public void RemoveAll()
    {
        Count = 0;
    }

    public int AddToTail( T value )
    {
        if (Count >= MaxSize)
        {
            throw new InvalidOperationException("Vector is full.");
        }
        var idx = Count;
        Count++;
        this[idx] = value;
        return idx;
    }

    public ref T this[int index] {
        get {
            if (index < 0 || index >= Count)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }
            return ref Unsafe.AsRef<T>((void*)(_memory.Base + index * sizeof(T)));
        }
    }

    // need revisit later
    public readonly int MaxSize => Unsafe.SizeOf<TBuffer>() / Unsafe.SizeOf<T>();

    public int Count { get; private set; }
    public readonly nint Base => _memory.Base;
}