using System.Collections;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.Natives;

[StructLayout(LayoutKind.Sequential)]
public struct CUtlLeanVector<T, I>
    : IDisposable, IEnumerable<T>
    where I : unmanaged, IBinaryInteger<I>, IMinMaxValue<I>
{
    public I Count;
    public I Allocated;
    public nint Elements;

    [StructLayout(LayoutKind.Sequential)]
    public struct Iterator_t
    {
        public I Index;

        public Iterator_t(I i) => Index = i;

        public static bool operator ==(Iterator_t a, Iterator_t b) => a.Index == b.Index;
        public static bool operator !=(Iterator_t a, Iterator_t b) => a.Index != b.Index;

        public override bool Equals(object? obj)
        {
            if (obj is Iterator_t other)
                return this == other;
            return false;
        }
    }

    public int ElementSize => SchemaSize.Get<T>();

    private I ExternalBufferMarker => I.One << ((Marshal.SizeOf<I>() * 8) - 1);

    /// <summary>
    /// Please use <see cref="ManagedCUtlLeanVector{T, I}"/> instead to construct it.
    /// If you really want to use this, you should call <see cref="Purge"/> after you are done with it.
    /// </summary>
    public CUtlLeanVector(I growSize, I initSize)
    {
        Count = (I)(object)0;
        Allocated = (I)(object)0;
        EnsureCapacity(int.CreateChecked(initSize), true);
    }

    /// <summary>
    /// Please use <see cref="ManagedCUtlLeanVector{T, I}"/> instead to construct it.
    /// If you really want to use this, you should call <see cref="Purge"/> after you are done with it.
    /// </summary>
    public CUtlLeanVector(nint memory, I allocationCount, I numElements)
    {
        Count = numElements;
        Allocated = allocationCount | ExternalBufferMarker;
        Elements = memory;
    }

    public void EnsureCapacity(int num, bool force)
    {
        if (num <= NumAllocated)
            return;

        I minAllocated = I.CreateChecked((31 + ElementSize - 1) / ElementSize);
        I maxAllocated = I.MaxValue;
        if (I.CreateChecked(num) > maxAllocated)
            throw new ArgumentOutOfRangeException(nameof(num), $"num {num} exceeds max {maxAllocated}");

        I newAllocated = I.CreateChecked(num);
        if (!force)
            newAllocated = I.CreateChecked(MemoryHelpers.CalcNewDoublingCount(NumAllocated, num, int.CreateChecked(minAllocated), int.CreateChecked(maxAllocated)));

        nint newElements;
        if (ExternallyAllocated)
        {
            newElements = NativeAllocator.Alloc(ulong.CreateChecked(newAllocated) * (ulong)ElementSize);
            NativeAllocator.Move(newElements, Elements, ulong.CreateChecked(Count) * (ulong)ElementSize);
        }
        else
        {
            newElements = NativeAllocator.Resize(Elements, ulong.CreateChecked(newAllocated) * (ulong)ElementSize);
        }

        Elements = newElements;
        Allocated = newAllocated;
    }

    public void SetExternalBuffer(nint memory, I allocationCount, I numElements)
    {
        Purge();

        Elements = memory;
        Allocated = allocationCount | ExternalBufferMarker;
        Count = numElements;
    }

    public void AssumeMemory(nint memory, I allocationCount, I numElements)
    {
        Purge();

        Elements = memory;
        Allocated = allocationCount;
        Count = numElements;
    }

    public nint DetachMemory()
    {
        if (ExternallyAllocated) return 0;

        nint memory = Elements;
        Elements = 0;
        Count = I.CreateChecked(0);
        Allocated = I.CreateChecked(0);
        return memory;
    }

    public void RemoveAll()
    {
        if (Count == I.CreateChecked(0))
            return;

        for (I i = I.CreateChecked(0); i < Count; i++)
            this[i] = default;

        Count = I.CreateChecked(0);
    }

    public void Purge()
    {
        if (!ExternallyAllocated)
        {
            if (Allocated > I.CreateChecked(0) && Elements != 0)
                NativeAllocator.Free(Elements);

            Allocated = I.CreateChecked(0);
            Elements = 0;
        }
    }

    public bool IsIdxValid(I idx) => idx >= I.CreateChecked(0) && idx < Count;
    public ref T Element(I idx)
    {
        if (!IsIdxValid(idx))
            throw new IndexOutOfRangeException($"Index {idx} is out of range (0 - {Count - I.One})");

        return ref this[idx];
    }

    public ref T Head() => ref Element(I.CreateChecked(0));
    public ref T Tail() => ref Element(Count - I.One);
    public bool IsValidIndex(I idx) => IsIdxValid(idx);

    public I AddToTail()
    {
        EnsureCapacity(int.CreateChecked(Count + I.One), false);
        return Count++;
    }

    public I AddToTail(T element)
    {
        I idx = AddToTail();
        this[idx] = element;
        return idx;
    }

    public void SetCount(I count)
    {
        if (count < I.CreateChecked(0))
            throw new ArgumentOutOfRangeException(nameof(count), "count must be >= 0");

        EnsureCapacity(int.CreateChecked(count), false);

        if (Count > count)
        {
            for (I i = count; i < Count; i++)
                this[i] = default;
        }

        Count = count;
    }

    public I Find(T element)
    {
        for (I i = I.CreateChecked(0); i < Count; i++)
        {
            if (this[i].Equals(element))
                return i;
        }

        return -I.One;
    }

    public void FastRemove(I elem)
    {
        if (!IsValidIndex(elem))
            return;

        this[elem] = default;
        if (Count > I.Zero)
        {
            if (elem != Count - I.One)
                NativeAllocator.Copy(Elements + int.CreateChecked(elem * I.CreateChecked(ElementSize)), Elements + int.CreateChecked((Count - I.One) * I.CreateChecked(ElementSize)), (ulong)ElementSize);
            --Count;
        }
    }

    public void Remove(I elem)
    {
        if (!IsValidIndex(elem))
            return;

        this[elem] = default;
        MemoryHelpers.ShiftElementsLeft(Elements, int.CreateChecked(elem), 1, int.CreateChecked(Count), ElementSize);
        --Count;
    }

    public void RemoveMultiple(I idx, I count)
    {
        if (count <= I.Zero || !IsValidIndex(idx) || idx + count > Count)
            return;

        for (I i = idx; i < idx + count; i++)
            this[i] = default;

        MemoryHelpers.ShiftElementsLeft(Elements, int.CreateChecked(idx), int.CreateChecked(count), int.CreateChecked(Count), ElementSize);
        Count -= count;
    }

    public void RemoveMultipleFromHead(I count)
    {
        RemoveMultiple(I.Zero, count);
    }

    public void RemoveMultipleFromTail(I count)
    {
        if (count <= I.Zero || count > Count)
            return;

        for (I i = Count - count; i < Count; i++)
            this[i] = default;

        Count -= count;
    }

    public bool FindAndRemove(T value)
    {
        I idx = Find(value);
        if (idx != -I.One)
        {
            Remove(idx);
            return true;
        }
        return false;
    }

    public bool FindAndFastRemove(T value)
    {
        I idx = Find(value);
        if (idx != -I.One)
        {
            FastRemove(idx);
            return true;
        }
        return false;
    }

    public void SetSize(I size) => SetCount(size);

    public void Dispose()
    {
        Purge();
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (I i = I.Zero; i < Count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public int NumAllocated => (int)((ulong)(object)Allocated & ~(ulong)(object)ExternalBufferMarker);
    public bool ExternallyAllocated => ((ulong)(object)Allocated & (ulong)(object)ExternalBufferMarker) != 0;
    public nint Base => Elements;
    public ref T this[I index]
    {
        get
        {
            unsafe
            {
                return ref Unsafe.AsRef<T>((byte*)Elements + int.CreateChecked(index * I.CreateChecked(ElementSize)));
            }
        }
    }
}