#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeNetMessages
{

    private unsafe static delegate* unmanaged<int, nint> _AllocateNetMessageByID;

    public unsafe static nint AllocateNetMessageByID(int msgid)
    {
        var ret = _AllocateNetMessageByID(msgid);
        return ret;
    }

    private unsafe static delegate* unmanaged<byte*, nint> _AllocateNetMessageByPartialName;

    public unsafe static nint AllocateNetMessageByPartialName(string name)
    {
        using var nameStr = new ScopedCString(name);
        fixed (byte* nameBufferPtr = nameStr)
        {
            var ret = _AllocateNetMessageByPartialName(nameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, void> _DeallocateNetMessage;

    public unsafe static void DeallocateNetMessage(nint netmsg)
    {
        _DeallocateNetMessage(netmsg);
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte> _HasField;

    public unsafe static bool HasField(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _HasField(netmsg, fieldNameBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, int, nint, byte> _GetValue;

    public unsafe static bool GetValue(nint netmsg, string fieldName, int index, int kind, nint value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetValue(netmsg, fieldNameBufferPtr, index, kind, value);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, int, nint, byte> _SetValue;

    /// <summary>
    /// index is -1 for a field that is not repeated
    /// </summary>
    public unsafe static bool SetValue(nint netmsg, string fieldName, int index, int kind, nint value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _SetValue(netmsg, fieldNameBufferPtr, index, kind, value);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, nint, byte> _AddValue;

    public unsafe static bool AddValue(nint netmsg, string fieldName, int kind, nint value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _AddValue(netmsg, fieldNameBufferPtr, kind, value);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<int*, nint, byte*, int, byte*> _GetString;

    public unsafe static string GetString(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var length = 0;
            var returnedPtr = _GetString(&length, netmsg, fieldNameBufferPtr, index);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, byte*, void> _SetString;

    public unsafe static void SetString(nint netmsg, string fieldName, int index, string value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        using var valueStr = new ScopedCString(value);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                _SetString(netmsg, fieldNameBufferPtr, index, valueBufferPtr);
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte*, void> _AddString;

    public unsafe static void AddString(nint netmsg, string fieldName, string value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        using var valueStr = new ScopedCString(value);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                _AddString(netmsg, fieldNameBufferPtr, valueBufferPtr);
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint, byte*, int, int> _GetBytes;

    public unsafe static byte[] GetBytes(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetBytes(null, netmsg, fieldNameBufferPtr, index);
            var pool = ArrayPool<byte>.Shared;
            var retBuffer = pool.Rent(ret + 1);
            fixed (byte* retBufferPtr = retBuffer)
            {
                ret = _GetBytes(retBufferPtr, netmsg, fieldNameBufferPtr, index);
                var retBytes = new byte[ret];
                for (int i = 0; i < ret; i++) retBytes[i] = retBufferPtr[i];
                pool.Return(retBuffer);
                return retBytes;
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, byte*, int, void> _SetBytes;

    public unsafe static void SetBytes(nint netmsg, string fieldName, int index, byte[] value)
    {
        var valueLength = value.Length;
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            fixed (byte* valueBufferPtr = value)
            {
                _SetBytes(netmsg, fieldNameBufferPtr, index, valueBufferPtr, valueLength);
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte*, int, void> _AddBytes;

    public unsafe static void AddBytes(nint netmsg, string fieldName, byte[] value)
    {
        var valueLength = value.Length;
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            fixed (byte* valueBufferPtr = value)
            {
                _AddBytes(netmsg, fieldNameBufferPtr, valueBufferPtr, valueLength);
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint> _GetNestedMessage;

    public unsafe static nint GetNestedMessage(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetNestedMessage(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, nint> _GetRepeatedNestedMessage;

    public unsafe static nint GetRepeatedNestedMessage(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedNestedMessage(netmsg, fieldNameBufferPtr, index);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint> _AddNestedMessage;

    public unsafe static nint AddNestedMessage(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _AddNestedMessage(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int> _GetRepeatedFieldSize;

    public unsafe static int GetRepeatedFieldSize(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedFieldSize(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, void> _ClearRepeatedField;

    public unsafe static void ClearRepeatedField(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _ClearRepeatedField(netmsg, fieldNameBufferPtr);
        }
    }

    private unsafe static delegate* unmanaged<nint, void> _Clear;

    public unsafe static void Clear(nint netmsg)
    {
        _Clear(netmsg);
    }

    private unsafe static delegate* unmanaged<nint, int, int, void> _SendMessage;

    public unsafe static void SendMessage(nint netmsg, int msgid, int playerid)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        _SendMessage(netmsg, msgid, playerid);
    }

    private unsafe static delegate* unmanaged<nint, int, ulong, void> _SendMessageToPlayers;

    /// <summary>
    /// each bit in player_mask represents a playerid
    /// </summary>
    public unsafe static void SendMessageToPlayers(nint netmsg, int msgid, ulong playermask)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        _SendMessageToPlayers(netmsg, msgid, playermask);
    }

    private unsafe static delegate* unmanaged<int, int, void> _RegisterMessageHook;

    /// <summary>
    /// hookType: 0 = server, 1 = client, 2 = server internal, only registered messages are sent to the hooks
    /// </summary>
    public unsafe static void RegisterMessageHook(int hookType, int messageid)
    {
        _RegisterMessageHook(hookType, messageid);
    }

    private unsafe static delegate* unmanaged<int, int, void> _UnregisterMessageHook;

    public unsafe static void UnregisterMessageHook(int hookType, int messageid)
    {
        _UnregisterMessageHook(hookType, messageid);
    }

    private unsafe static delegate* unmanaged<nint, void> _SetNetMessageServerHook;

    /// <summary>
    /// the callback should receive the following: uint64* playermask_ptr, int netmessage_id, void* netmsg, return HookResult int
    /// </summary>
    public unsafe static void SetNetMessageServerHook(nint callback)
    {
        _SetNetMessageServerHook(callback);
    }

    private unsafe static delegate* unmanaged<nint, void> _SetNetMessageClientHook;

    /// <summary>
    /// the callback should receive the following: int32 playerid, int netmessage_id, void* netmsg, return HookResult int
    /// </summary>
    public unsafe static void SetNetMessageClientHook(nint callback)
    {
        _SetNetMessageClientHook(callback);
    }

    private unsafe static delegate* unmanaged<nint, void> _SetNetMessageServerHookInternal;

    /// <summary>
    /// callback should receive the following: int32 playerid, int netmessage_id, void* netmsg, return HookResult int
    /// </summary>
    public unsafe static void SetNetMessageServerHookInternal(nint callback)
    {
        _SetNetMessageServerHookInternal(callback);
    }
}