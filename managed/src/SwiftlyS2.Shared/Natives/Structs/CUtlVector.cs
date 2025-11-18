
using System.Collections;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Schemas;

[StructLayout(LayoutKind.Sequential, Pack = 8, Size = 24)]
public struct CUtlVector<T> : IEnumerable<T>
{
    private CUtlMemory<T> _memory;

    public int ElementSize => SchemaSize.Get<T>();

    /// <summary>
    /// Please use <see cref="ManagedCUtlVector{T}"/> instead to construct it.
    /// If you really want to use this, you should call <see cref="Purge"/> after you are done with it.
    /// </summary>
    public CUtlVector( int growSize, int initSize )
    {
        _memory = new(growSize, initSize);
        Count = 0;
    }

    /// <summary>
    /// Please use <see cref="ManagedCUtlVector{T}"/> instead to construct it.
    /// If you really want to use this, you should call <see cref="Purge"/> after you are done with it.
    /// </summary>
    public CUtlVector( nint memory, int allocationCount, int numElements )
    {
        _memory = new(memory, allocationCount, false);
        Count = numElements;
    }

    public void Purge()
    {
        RemoveAll();
        _memory.Purge();
    }

    public void EnsureCapacity( int num )
    {
        _memory.EnsureCapacity(num);
    }

    public void SetExternalBuffer( nint memory, int allocationCount, int numELements, bool readOnly )
    {
        _memory.SetExternalBuffer(memory, allocationCount, readOnly);
        Count = numELements;
    }

    public void AssumeMemory( nint memory, int allocationCount, int numElements )
    {
        _memory.AssumeMemory(memory, allocationCount);
        Count = numElements;
    }

    public nint DetachMemory()
    {
        Count = 0;
        return _memory.DetachMemory();
    }

    public bool IsValidIndex( int index )
    {
        return (uint)index < (uint)Count && index >= 0;
    }

    public void GrowVector( int count )
    {
        if (Count + count > _memory.Count)
        {
            _memory.Grow(Count + count - _memory.Count);
        }

        Count += count;
    }

    public int InsertBeforeIdx( int elem )
    {
        GrowVector(1);
        MemoryHelpers.ShiftElementsRight(_memory.Base, elem, 1, Count, ElementSize);
        return elem;
    }

    public int InsertAfterIdx( int elem )
    {
        return InsertBeforeIdx(elem + 1);
    }

    public int InsertBefore( int idx, T value )
    {
        GrowVector(1);
        MemoryHelpers.ShiftElementsRight(_memory.Base, idx, 1, Count, ElementSize);
        this[idx] = value;
        return idx;
    }

    public int InsertAfter( int idx, T value )
    {
        return InsertBefore(idx + 1, value);
    }

    public int AddToHead( T value )
    {
        return InsertBefore(0, value);
    }

    public int AddToTail( T value )
    {
        return InsertBefore(Count, value);
    }

    public int AddVectorToTail( CUtlVector<T> other )
    {
        var baseCount = Count;
        var srcCount = other.Count;
        EnsureCapacity(baseCount + srcCount);

        Count += srcCount;
        for (var i = 0; i < srcCount; i++)
            this[baseCount + i] = other[i];

        return baseCount;
    }

    public int Find( T value )
    {
        for (var i = 0; i < Count; i++)
        {
            if (this[i].Equals(value))
                return i;
        }

        return -1;
    }

    public void FillWithValue( T value )
    {
        for (var i = 0; i < Count; i++)
            this[i] = value;
    }

    public bool HasElement( T value )
    {
        return Find(value) != -1;
    }

    public void FastRemove( int elem )
    {
        if (!IsValidIndex(elem))
            return;

        this[elem] = default;
        if (Count > 0)
        {
            if (elem != Count - 1)
                NativeAllocator.Copy(_memory.Base + (elem * ElementSize), _memory.Base + ((Count - 1) * ElementSize), (ulong)ElementSize);
            --Count;
        }
    }

    public bool FindAndRemove( T value )
    {
        var idx = Find(value);
        if (idx != -1)
        {
            Remove(idx);
            return true;
        }
        return false;
    }

    public bool FindAndFastRemove( T value )
    {
        var idx = Find(value);
        if (idx != -1)
        {
            FastRemove(idx);
            return true;
        }
        return false;
    }

    public void RemoveMultiple( int idx, int count )
    {
        if (count <= 0 || !IsValidIndex(idx) || idx + count > Count)
            return;

        for (var i = idx; i < idx + count; i++)
            this[i] = default;

        MemoryHelpers.ShiftElementsLeft(_memory.Base, idx, count, Count, ElementSize);
        Count -= count;
    }

    public void RemoveMultipleFromHead( int count )
    {
        RemoveMultiple(0, count);
    }

    public void RemoveMultipleFromTail( int count )
    {
        if (count <= 0 || count > Count)
            return;

        for (var i = Count - count; i < Count; i++)
            this[i] = default;

        Count -= count;
    }

    public void Remove( int elem )
    {
        if (!IsValidIndex(elem))
            return;

        this[elem] = default;
        MemoryHelpers.ShiftElementsLeft(_memory.Base, elem, 1, Count, ElementSize);
        --Count;
    }

    public void RemoveAll()
    {
        if (Count == 0)
            return;

        for (var i = 0; i < Count; i++)
            this[i] = default;

        Count = 0;
    }

    public ref T this[int index] => ref _memory[index];
    public ref T Head() => ref _memory[0];
    public ref T Tail() => ref _memory[Count - 1];
    public nint Base => _memory.Base;
    public int Count { get; private set; }
    public int Capacity => _memory.Count;

    public IEnumerator<T> GetEnumerator()
    {
        for (var i = 0; i < Count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}