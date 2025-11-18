using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Schemas;

namespace SwiftlyS2.Shared.Natives;

public enum BufferMarkers
{
    ExternalConstBufferMarker = 1 << 30,
    ExternalBufferMarker = 1 << 31
}

[StructLayout(LayoutKind.Sequential)]
public struct CUtlMemory<T>
{
    private nint _memory;
    private uint _allocationCount;
    private uint _growSize;

    public int ElementSize => SchemaSize.Get<T>();

    /// <summary>
    /// Please use <see cref="ManagedCUtlMemory{T}"/> instead to construct it.
    /// If you really want to use this, you should call <see cref="Purge"/> after you are done with it.
    /// </summary>
    public CUtlMemory(int growSize, int initSize)
    {
        _memory = 0;
        _allocationCount = 0;
        _growSize = 0;
        Init(growSize, initSize);
    }

    /// <summary>
    /// Please use <see cref="ManagedCUtlMemory{T}"/> instead to construct it.
    /// If you really want to use this, you should call <see cref="Purge"/> after you are done with it.
    /// </summary>
    public CUtlMemory(nint memory, int numelements, bool readOnly)
    {
        _memory = 0;
        _allocationCount = 0;
        _growSize = 0;
        SetExternalBuffer(memory, numelements, readOnly);
    }

    public void Init(int growSize, int initSize)
    {
        Purge();

        _growSize = (uint)(growSize & ~(int)(BufferMarkers.ExternalBufferMarker | BufferMarkers.ExternalConstBufferMarker));
        _allocationCount = (uint)initSize;
        if (initSize > 0)
            _memory = NativeAllocator.Alloc((nuint)(initSize * ElementSize));
    }

    public void Purge()
    {
        if (_memory != 0 && !ExternallyAllocated)
        {
            NativeAllocator.Free(_memory);
            _memory = 0;
        }
        _allocationCount = 0;
        _growSize = 0;
    }

    public void Purge(int numElements)
    {
        if (numElements < 0 || numElements > _allocationCount) return;
        if (numElements == 0)
        {
            Purge();
            return;
        }

        if (IsReadOnly) return;
        if (numElements == _allocationCount) return;
        if (_memory == 0) return;

        _memory = ExternDLL.UtlVectorMemory_Alloc(_memory, !ExternallyAllocated, numElements * ElementSize, (int)(_allocationCount * ElementSize));

        if (ExternallyAllocated)
            _growSize &= ~(int)(BufferMarkers.ExternalBufferMarker | BufferMarkers.ExternalConstBufferMarker);

        _allocationCount = (uint)numElements;
    }

    public void ConvertToGrowableMemory(int growSize)
    {
        if (!ExternallyAllocated) return;
        if (_memory == 0) return;

        _growSize = (uint)(growSize & ~(int)(BufferMarkers.ExternalBufferMarker | BufferMarkers.ExternalConstBufferMarker));
        if (_allocationCount > 0)
        {
            var numBytes = (int)(_allocationCount * ElementSize);
            var newmem = NativeAllocator.Alloc((nuint)numBytes);
            NativeAllocator.Copy(newmem, _memory, (ulong)numBytes);
            _memory = newmem;
        }
        else
        {
            _memory = 0;
        }
    }

    public void SetExternalBuffer(nint memory, int numelements, bool readOnly)
    {
        Purge();

        _memory = memory;
        _allocationCount = (uint)numelements;
        _growSize = (uint)(readOnly ? (int)BufferMarkers.ExternalConstBufferMarker : (int)BufferMarkers.ExternalBufferMarker);
    }

    public void AssumeMemory(nint memory, int numelements)
    {
        Purge();

        _memory = memory;
        _allocationCount = (uint)numelements;
        _growSize &= ~(int)(BufferMarkers.ExternalBufferMarker | BufferMarkers.ExternalConstBufferMarker);
    }

    public nint DetachMemory()
    {
        if (ExternallyAllocated) return 0;

        var mem = _memory;
        _memory = 0;
        _allocationCount = 0;
        _growSize = 0;
        return mem;
    }

    public void Grow(int num)
    {
        if (IsReadOnly) return;
        if (_allocationCount + num > int.MaxValue)
        {
            ExternDLL.UtlVectorMemory_FailedAllocation((int)_allocationCount, num);
            return;
        }

        var allocationRequested = _allocationCount + (uint)num;
        var newAllocationCount = ExternDLL.UtlVectorMemory_CalcNewAllocationCount((int)_allocationCount, (int)(_growSize & ~(int)(BufferMarkers.ExternalBufferMarker | BufferMarkers.ExternalConstBufferMarker)), (int)allocationRequested, ElementSize);

        if (newAllocationCount < allocationRequested)
        {
            if (newAllocationCount == 0 && newAllocationCount - 1 >= allocationRequested)
                --newAllocationCount;
            else
            {
                while (newAllocationCount < allocationRequested)
                {
                    newAllocationCount = (int)((newAllocationCount + allocationRequested) / 2);
                }
            }
        }

        _memory = ExternDLL.UtlVectorMemory_Alloc(_memory, !ExternallyAllocated, newAllocationCount * ElementSize, (int)(_allocationCount * ElementSize));

        if (ExternallyAllocated)
            _growSize &= ~(int)(BufferMarkers.ExternalBufferMarker | BufferMarkers.ExternalConstBufferMarker);

        _allocationCount = (uint)newAllocationCount;
    }

    public void EnsureCapacity(int num)
    {
        if (_allocationCount >= num) return;
        if (IsReadOnly) return;

        _memory = ExternDLL.UtlVectorMemory_Alloc(_memory, !ExternallyAllocated, num * ElementSize, (int)(_allocationCount * ElementSize));
        _allocationCount = (uint)num;
        if (ExternallyAllocated)
            _growSize &= ~(int)(BufferMarkers.ExternalBufferMarker | BufferMarkers.ExternalConstBufferMarker);
    }

    public void SetGrowSize(int size)
    {
        _growSize |= (uint)(size & ~(int)(BufferMarkers.ExternalBufferMarker | BufferMarkers.ExternalConstBufferMarker));
    }

    public bool IsValidIndex(int index) => (uint)index < _allocationCount && index >= 0;

    public ref T this[int index]
    {
        get
        {
            unsafe
            {
                return ref Unsafe.AsRef<T>((byte*)_memory + int.CreateChecked(index * ElementSize));
            }
        }
    }
    public bool ExternallyAllocated => (_growSize & (int)(BufferMarkers.ExternalBufferMarker | BufferMarkers.ExternalConstBufferMarker)) != 0;
    public bool IsReadOnly => (_growSize & (int)BufferMarkers.ExternalConstBufferMarker) != 0;
    public nint Base => _memory;
    public int Count => (int)_allocationCount;
}