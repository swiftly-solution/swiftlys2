using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CUtlVectorFixedGrowable<T, TBuffer>
    where T : unmanaged
    where TBuffer : unmanaged
{
    private int _size;
    private CUtlMemoryFixedGrowable<T, TBuffer> _memory;

    public CUtlVectorFixedGrowable(int maxSize, int growSize = 0)
    {
        _memory = new CUtlMemoryFixedGrowable<T, TBuffer>(maxSize, growSize);
        _size = 0;
    }

    public void SetSize(int size)
    {
        _size = size;
    }

    public void RemoveAll()
    {
        _size = 0;
    }

    public int AddToTail(T value)
    {
        if (_size >= MaxSize) {
            throw new InvalidOperationException("Vector is full.");
        }
        int idx = _size;
        _size++;
        this[idx] = value;
        return idx;
    }

    public ref T this[int index]
    {
        get
        {
            if (index < 0 || index >= _size) {
                throw new IndexOutOfRangeException("Index is out of range.");
            }
            return ref Unsafe.AsRef<T>((void*)(_memory.Base + index * sizeof(T)));
        }
    }

    // need revisit later
    public readonly int MaxSize => Unsafe.SizeOf<TBuffer>() / Unsafe.SizeOf<T>();

    public readonly int Count => _size;
    public readonly nint Base => _memory.Base;
}