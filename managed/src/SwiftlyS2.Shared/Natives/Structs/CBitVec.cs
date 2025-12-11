using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.Natives;

public interface ICBitVec
{
    public bool IsFixedSize();
    public uint NumDWords();
    public uint GetNumBits();
    public void ClearAll();
    public void SetAll();
    public void Set( uint index );
    public void Set( int index );
    public void Clear( uint index );
    public void Clear( int index );
    public bool IsSet( uint index );
    public bool IsSet( int index );
    public int Count();
    public bool IsAllClear();
}

public static unsafe class CBitVecOperations
{
    public static void ClearAll( uint* buffer, int intCount )
    {
        for (int i = 0; i < intCount; i++)
            buffer[i] = 0;
    }

    public static void SetAll( uint* buffer, int intCount )
    {
        for (int i = 0; i < intCount; i++)
            buffer[i] = uint.MaxValue;
    }

    public static void Set( uint* buffer, uint index, uint maxBits )
    {
        if (index >= maxBits) throw new IndexOutOfRangeException($"The index {index} is out of range. Maximum allowed index is {maxBits - 1}");
        buffer[index >> 5] |= (uint)(1 << ((int)index & 31));
    }

    public static void Set( uint* buffer, int index, uint maxBits )
    {
        if (index < 0 || index >= maxBits) throw new IndexOutOfRangeException($"The index {index} is out of range. Valid range is 0 to {maxBits - 1}");
        buffer[index >> 5] |= (uint)(1 << (index & 31));
    }

    public static void Clear( uint* buffer, uint index, uint maxBits )
    {
        if (index >= maxBits) throw new IndexOutOfRangeException($"The index {index} is out of range. Maximum allowed index is {maxBits - 1}");
        buffer[index >> 5] &= ~(uint)(1 << ((int)index & 31));
    }

    public static void Clear( uint* buffer, int index, uint maxBits )
    {
        if (index < 0 || index >= maxBits) throw new IndexOutOfRangeException($"The index {index} is out of range. Valid range is 0 to {maxBits - 1}");
        buffer[index >> 5] &= ~(uint)(1 << (index & 31));
    }

    public static bool IsSet( uint* buffer, uint index, uint maxBits )
    {
        if (index >= maxBits) throw new IndexOutOfRangeException($"The index {index} is out of range. Maximum allowed index is {maxBits - 1}");
        return (buffer[index >> 5] & ((uint)(1 << ((int)index & 31)))) != 0;
    }

    public static bool IsSet( uint* buffer, int index, uint maxBits )
    {
        if (index < 0 || index >= maxBits) throw new IndexOutOfRangeException($"The index {index} is out of range. Valid range is 0 to {maxBits - 1}");
        return (buffer[index >> 5] & ((uint)(1 << (index & 31)))) != 0;
    }

    public static int Count( uint* buffer, int intCount )
    {
        int count = 0;
        for (int i = 0; i < intCount; i++)
        {
            uint v = buffer[i];
            while (v != 0)
            {
                v &= v - 1;
                count++;
            }
        }
        return count;
    }

    public static bool IsAllClear( uint* buffer, int intCount )
    {
        return Count(buffer, intCount) == 0;
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CBitVec64 : ICBitVec
{
    public fixed uint _buffer[2];

    public CBitVec64()
    {
        ClearAll();
    }

    public bool IsFixedSize() => true;
    public uint NumDWords() => 2;
    public uint GetNumBits() => 64;

    public void ClearAll()
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.ClearAll(ptr, 2);
        }
    }

    public void SetAll()
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.SetAll(ptr, 2);
        }
    }

    public void Set( uint index )
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.Set(ptr, index, 64);
        }
    }

    public void Set( int index )
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.Set(ptr, index, 64);
        }
    }

    public void Clear( uint index )
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.Clear(ptr, index, 64);
        }
    }

    public void Clear( int index )
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.Clear(ptr, index, 64);
        }
    }

    public bool IsSet( uint index )
    {
        fixed (uint* ptr = _buffer)
        {
            return CBitVecOperations.IsSet(ptr, index, 64);
        }
    }

    public bool IsSet( int index )
    {
        fixed (uint* ptr = _buffer)
        {
            return CBitVecOperations.IsSet(ptr, index, 64);
        }
    }

    public int Count()
    {
        fixed (uint* ptr = _buffer)
        {
            return CBitVecOperations.Count(ptr, 2);
        }
    }

    public bool IsAllClear()
    {
        fixed (uint* ptr = _buffer)
        {
            return CBitVecOperations.IsAllClear(ptr, 2);
        }
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CBitVec16384 : ICBitVec
{
    public fixed uint _buffer[512];

    public CBitVec16384()
    {
        ClearAll();
    }

    public bool IsFixedSize() => true;
    public uint NumDWords() => 512;
    public uint GetNumBits() => 16384;

    public void ClearAll()
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.ClearAll(ptr, 512);
        }
    }

    public void SetAll()
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.SetAll(ptr, 512);
        }
    }

    public void Set( uint index )
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.Set(ptr, index, 16384);
        }
    }

    public void Set( int index )
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.Set(ptr, index, 16384);
        }
    }

    public void Clear( uint index )
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.Clear(ptr, index, 16384);
        }
    }

    public void Clear( int index )
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.Clear(ptr, index, 16384);
        }
    }

    public bool IsSet( uint index )
    {
        fixed (uint* ptr = _buffer)
        {
            return CBitVecOperations.IsSet(ptr, index, 16384);
        }
    }

    public bool IsSet( int index )
    {
        fixed (uint* ptr = _buffer)
        {
            return CBitVecOperations.IsSet(ptr, index, 16384);
        }
    }

    public int Count()
    {
        fixed (uint* ptr = _buffer)
        {
            return CBitVecOperations.Count(ptr, 512);
        }
    }

    public bool IsAllClear()
    {
        fixed (uint* ptr = _buffer)
        {
            return CBitVecOperations.IsAllClear(ptr, 512);
        }
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct CBitVec4096 : ICBitVec
{
    public fixed uint _buffer[128];

    public CBitVec4096()
    {
        ClearAll();
    }

    public bool IsFixedSize() => true;
    public uint NumDWords() => 128;
    public uint GetNumBits() => 4096;

    public void ClearAll()
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.ClearAll(ptr, 128);
        }
    }

    public void SetAll()
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.SetAll(ptr, 128);
        }
    }

    public void Set( uint index )
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.Set(ptr, index, 4096);
        }
    }

    public void Set( int index )
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.Set(ptr, index, 4096);
        }
    }

    public void Clear( uint index )
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.Clear(ptr, index, 4096);
        }
    }

    public void Clear( int index )
    {
        fixed (uint* ptr = _buffer)
        {
            CBitVecOperations.Clear(ptr, index, 4096);
        }
    }

    public bool IsSet( uint index )
    {
        fixed (uint* ptr = _buffer)
        {
            return CBitVecOperations.IsSet(ptr, index, 4096);
        }
    }

    public bool IsSet( int index )
    {
        fixed (uint* ptr = _buffer)
        {
            return CBitVecOperations.IsSet(ptr, index, 4096);
        }
    }

    public int Count()
    {
        fixed (uint* ptr = _buffer)
        {
            return CBitVecOperations.Count(ptr, 128);
        }
    }

    public bool IsAllClear()
    {
        fixed (uint* ptr = _buffer)
        {
            return CBitVecOperations.IsAllClear(ptr, 128);
        }
    }
}