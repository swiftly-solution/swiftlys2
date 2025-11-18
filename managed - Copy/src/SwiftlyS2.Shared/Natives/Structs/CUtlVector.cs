
using System.Collections;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Schemas;

[StructLayout(LayoutKind.Sequential, Pack = 8, Size = 24)]
public struct CUtlVector<T> : IEnumerable<T>
{
    private int _size;
    private CUtlMemory<T> _memory;

    public int ElementSize => SchemaSize.Get<T>();

    /// <summary>
    /// Please use <see cref="ManagedCUtlVector{T}"/> instead to construct it.
    /// If you really want to use this, you should call <see cref="Purge"/> after you are done with it.
    /// </summary>
    public CUtlVector(int growSize, int initSize)
    {
        _memory = new(growSize, initSize);
        _size = 0;
    }

    /// <summary>
    /// Please use <see cref="ManagedCUtlVector{T}"/> instead to construct it.
    /// If you really want to use this, you should call <see cref="Purge"/> after you are done with it.
    /// </summary>
    public CUtlVector(nint memory, int allocationCount, int numElements)
    {
        _memory = new(memory, allocationCount, false);
        _size = numElements;
    }

    public void Purge()
    {
        RemoveAll();
        _memory.Purge();
    }

    public void EnsureCapacity(int num)
    {
        _memory.EnsureCapacity(num);
    }

    public void SetExternalBuffer(nint memory, int allocationCount, int numELements, bool readOnly)
    {
        _memory.SetExternalBuffer(memory, allocationCount, readOnly);
        _size = numELements;
    }

    public void AssumeMemory(nint memory, int allocationCount, int numElements)
    {
        _memory.AssumeMemory(memory, allocationCount);
        _size = numElements;
    }

    public nint DetachMemory()
    {
        _size = 0;
        return _memory.DetachMemory();
    }

    public bool IsValidIndex(int index)
    {
        return (uint)index < (uint)_size && index >= 0;
    }

    public void GrowVector(int count)
    {
        if (_size + count > _memory.Count)
        {
            _memory.Grow(_size + count - _memory.Count);
        }

        _size += count;
    }

    public int InsertBeforeIdx(int elem)
    {
        GrowVector(1);
        MemoryHelpers.ShiftElementsRight(_memory.Base, elem, 1, _size, ElementSize);
        return elem;
    }

    public int InsertAfterIdx(int elem)
    {
        return InsertBeforeIdx(elem + 1);
    }

    public int InsertBefore(int idx, T value)
    {
        GrowVector(1);
        MemoryHelpers.ShiftElementsRight(_memory.Base, idx, 1, _size, ElementSize);
        this[idx] = value;
        return idx;
    }

    public int InsertAfter(int idx, T value)
    {
        return InsertBefore(idx + 1, value);
    }

    public int AddToHead(T value)
    {
        return InsertBefore(0, value);
    }

    public int AddToTail(T value)
    {
        return InsertBefore(_size, value);
    }

    public int AddVectorToTail(CUtlVector<T> other)
    {
        int baseCount = Count;
        var srcCount = other.Count;
        EnsureCapacity(baseCount + srcCount);

        _size += srcCount;
        for (var i = 0; i < srcCount; i++)
            this[baseCount + i] = other[i];

        return baseCount;
    }

    public int Find(T value)
    {
        for (int i = 0; i < _size; i++)
        {
            if (this[i].Equals(value))
                return i;
        }

        return -1;
    }

    public void FillWithValue(T value)
    {
        for (int i = 0; i < _size; i++)
            this[i] = value;
    }

    public bool HasElement(T value)
    {
        return Find(value) != -1;
    }

    public void FastRemove(int elem)
    {
        if (!IsValidIndex(elem))
            return;

        this[elem] = default;
        if (_size > 0)
        {
            if (elem != _size - 1)
                NativeAllocator.Copy(_memory.Base + (elem * ElementSize), _memory.Base + ((_size - 1) * ElementSize), (ulong)ElementSize);
            --_size;
        }
    }

    public bool FindAndRemove(T value)
    {
        int idx = Find(value);
        if (idx != -1)
        {
            Remove(idx);
            return true;
        }
        return false;
    }

    public bool FindAndFastRemove(T value)
    {
        int idx = Find(value);
        if (idx != -1)
        {
            FastRemove(idx);
            return true;
        }
        return false;
    }

    public void RemoveMultiple(int idx, int count)
    {
        if (count <= 0 || !IsValidIndex(idx) || idx + count > _size)
            return;

        for (int i = idx; i < idx + count; i++)
            this[i] = default;

        MemoryHelpers.ShiftElementsLeft(_memory.Base, idx, count, _size, ElementSize);
        _size -= count;
    }

    public void RemoveMultipleFromHead(int count)
    {
        RemoveMultiple(0, count);
    }

    public void RemoveMultipleFromTail(int count)
    {
        if (count <= 0 || count > _size)
            return;

        for (int i = _size - count; i < _size; i++)
            this[i] = default;

        _size -= count;
    }

    public void Remove(int elem)
    {
        if (!IsValidIndex(elem))
            return;

        this[elem] = default;
        MemoryHelpers.ShiftElementsLeft(_memory.Base, elem, 1, _size, ElementSize);
        --_size;
    }

    public void RemoveAll()
    {
        if (_size == 0)
            return;

        for (int i = 0; i < _size; i++)
            this[i] = default;

        _size = 0;
    }

    public ref T this[int index] => ref _memory[index];
    public ref T Head() => ref _memory[0];
    public ref T Tail() => ref _memory[_size - 1];
    public nint Base => _memory.Base;
    public int Count => _size;
    public int Capacity => _memory.Count;

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _size; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}