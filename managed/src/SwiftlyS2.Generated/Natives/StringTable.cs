#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeStringTable
{

    private unsafe static delegate* unmanaged<byte*, nint> _ContainerFindTable;

    public unsafe static nint ContainerFindTable(string tableName)
    {
        using var tableNameStr = new ScopedCString(tableName);
        fixed (byte* tableNameBufferPtr = tableNameStr)
        {
            var ret = _ContainerFindTable(tableNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<int, nint> _ContainerGetTableById;

    public unsafe static nint ContainerGetTableById(int tableId)
    {
        var ret = _ContainerGetTableById(tableId);
        return ret;
    }

    private unsafe static delegate* unmanaged<nint, int> _GetTableId;

    public unsafe static int GetTableId(nint table)
    {
        var ret = _GetTableId(table);
        return ret;
    }

    private unsafe static delegate* unmanaged<int*, nint, byte*> _GetTableName;

    public unsafe static string GetTableName(nint table)
    {
        var length = 0;
        var returnedPtr = _GetTableName(&length, table);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<nint, int> _GetNumStrings;

    public unsafe static int GetNumStrings(nint table)
    {
        var ret = _GetNumStrings(table);
        return ret;
    }

    private unsafe static delegate* unmanaged<nint, byte*, int> _FindStringIndex;

    public unsafe static int FindStringIndex(nint table, string str)
    {
        using var strStr = new ScopedCString(str);
        fixed (byte* strBufferPtr = strStr)
        {
            var ret = _FindStringIndex(table, strBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, int, byte> _IsStringIndexValid;

    public unsafe static bool IsStringIndexValid(nint table, int index)
    {
        var ret = _IsStringIndexValid(table, index);
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<int*, nint, int, byte*> _GetString;

    public unsafe static string GetString(nint table, int index)
    {
        var length = 0;
        var returnedPtr = _GetString(&length, table, index);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<nint, int, nint> _GetStringUserData;

    public unsafe static nint GetStringUserData(nint table, int index)
    {
        var ret = _GetStringUserData(table, index);
        return ret;
    }

    private unsafe static delegate* unmanaged<nint, int, nint, int, byte, byte> _SetStringUserData;

    public unsafe static bool SetStringUserData(nint table, int index, nint userData, int userDataSize, bool forceOverride)
    {
        var ret = _SetStringUserData(table, index, userData, userDataSize, forceOverride ? (byte)1 : (byte)0);
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<nint, byte*, int> _AddString;

    public unsafe static int AddString(nint table, string str)
    {
        using var strStr = new ScopedCString(str);
        fixed (byte* strBufferPtr = strStr)
        {
            var ret = _AddString(table, strBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint, int, byte*, byte, nint, int, int> _Serialize;

    public unsafe static byte[] Serialize(nint table, int index, string keyName, bool newKey, nint userData, int userDataSize)
    {
        using var keyNameStr = new ScopedCString(keyName);
        fixed (byte* keyNameBufferPtr = keyNameStr)
        {
            var ret = _Serialize(null, table, index, keyNameBufferPtr, newKey ? (byte)1 : (byte)0, userData, userDataSize);
            var pool = ArrayPool<byte>.Shared;
            var retBuffer = pool.Rent(ret + 1);
            fixed (byte* retBufferPtr = retBuffer)
            {
                ret = _Serialize(retBufferPtr, table, index, keyNameBufferPtr, newKey ? (byte)1 : (byte)0, userData, userDataSize);
                var retBytes = new byte[ret];
                for (int i = 0; i < ret; i++) retBytes[i] = retBufferPtr[i];
                pool.Return(retBuffer);
                return retBytes;
            }
        }
    }
}