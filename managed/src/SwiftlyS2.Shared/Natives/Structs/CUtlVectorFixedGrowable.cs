using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CUtlVectorFixedGrowable<T, TBuffer>
    where T : unmanaged
    where TBuffer : unmanaged
{
    private int size;
    private CUtlMemoryFixedGrowable<T, TBuffer> memory;

    public CUtlVectorFixedGrowable( int maxSize, int growSize = 0 )
    {
        memory = new CUtlMemoryFixedGrowable<T, TBuffer>(maxSize, growSize);
        size = 0;
    }

    public void SetSize( int size )
    {
        this.size = size;
    }

    public void RemoveAll()
    {
        size = 0;
    }

    public int AddToTail( T value )
    {
        if (size >= MaxSize)
        {
            throw new InvalidOperationException("Vector is full.");
        }
        var idx = size;
        size++;
        this[idx] = value;
        return idx;
    }

    public ref T this[int index] {
        get {
            if (index < 0 || index >= size)
            {
                throw new IndexOutOfRangeException("Index is out of range.");
            }
            return ref Unsafe.AsRef<T>((void*)(memory.Base + index * sizeof(T)));
        }
    }

    // need revisit later
    public readonly int MaxSize => Unsafe.SizeOf<TBuffer>() / Unsafe.SizeOf<T>();

    public readonly int Count => size;
    public readonly nint Base => memory.Base;
}