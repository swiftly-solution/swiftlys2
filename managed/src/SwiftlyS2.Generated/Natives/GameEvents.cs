#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeGameEvents
{

    private unsafe static delegate* unmanaged<nint, byte*, byte> _GetBool;

    public unsafe static bool GetBool(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetBool(_event, keyBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int> _GetInt;

    public unsafe static int GetInt(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetInt(_event, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, ulong> _GetUint64;

    public unsafe static ulong GetUint64(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetUint64(_event, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, float> _GetFloat;

    public unsafe static float GetFloat(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetFloat(_event, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<int*, nint, byte*, byte*> _GetString;

    public unsafe static string GetString(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var length = 0;
            var returnedPtr = _GetString(&length, _event, keyBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint> _GetPtr;

    public unsafe static nint GetPtr(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetPtr(_event, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint> _GetEHandle;

    /// <summary>
    /// returns the pointer stored inside the handle
    /// </summary>
    public unsafe static nint GetEHandle(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetEHandle(_event, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint> _GetEntity;

    public unsafe static nint GetEntity(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetEntity(_event, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int> _GetEntityIndex;

    public unsafe static int GetEntityIndex(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetEntityIndex(_event, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int> _GetPlayerSlot;

    public unsafe static int GetPlayerSlot(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetPlayerSlot(_event, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint> _GetPlayerController;

    public unsafe static nint GetPlayerController(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetPlayerController(_event, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint> _GetPlayerPawn;

    public unsafe static nint GetPlayerPawn(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetPlayerPawn(_event, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint> _GetPawnEHandle;

    /// <summary>
    /// returns the pointer stored inside the handle
    /// </summary>
    public unsafe static nint GetPawnEHandle(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetPawnEHandle(_event, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int> _GetPawnEntityIndex;

    public unsafe static int GetPawnEntityIndex(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetPawnEntityIndex(_event, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte, void> _SetBool;

    public unsafe static void SetBool(nint _event, string key, bool value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetBool(_event, keyBufferPtr, value ? (byte)1 : (byte)0);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, void> _SetInt;

    public unsafe static void SetInt(nint _event, string key, int value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetInt(_event, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, ulong, void> _SetUint64;

    public unsafe static void SetUint64(nint _event, string key, ulong value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetUint64(_event, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, float, void> _SetFloat;

    public unsafe static void SetFloat(nint _event, string key, float value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetFloat(_event, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte*, void> _SetString;

    public unsafe static void SetString(nint _event, string key, string value)
    {
        using var keyStr = new ScopedCString(key);
        using var valueStr = new ScopedCString(value);
        fixed (byte* keyBufferPtr = keyStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                _SetString(_event, keyBufferPtr, valueBufferPtr);
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint, void> _SetPtr;

    public unsafe static void SetPtr(nint _event, string key, nint value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetPtr(_event, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint, void> _SetEntity;

    public unsafe static void SetEntity(nint _event, string key, nint value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetEntity(_event, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, void> _SetEntityIndex;

    public unsafe static void SetEntityIndex(nint _event, string key, int value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetEntityIndex(_event, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, void> _SetPlayerSlot;

    public unsafe static void SetPlayerSlot(nint _event, string key, int value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetPlayerSlot(_event, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte> _HasKey;

    public unsafe static bool HasKey(nint _event, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _HasKey(_event, keyBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte> _IsReliable;

    public unsafe static bool IsReliable(nint _event)
    {
        var ret = _IsReliable(_event);
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<nint, byte> _IsLocal;

    public unsafe static bool IsLocal(nint _event)
    {
        var ret = _IsLocal(_event);
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<byte*, void> _RegisterListener;

    public unsafe static void RegisterListener(string eventName)
    {
        using var eventNameStr = new ScopedCString(eventName);
        fixed (byte* eventNameBufferPtr = eventNameStr)
        {
            _RegisterListener(eventNameBufferPtr);
        }
    }

    private unsafe static delegate* unmanaged<nint, void> _SetListenerPreHandler;

    /// <summary>
    /// the callback should receive the following: uint32 eventNameHash, IntPtr gameEvent, bool* dontBroadcast, return HookResult int
    /// </summary>
    public unsafe static void SetListenerPreHandler(nint callback)
    {
        _SetListenerPreHandler(callback);
    }

    private unsafe static delegate* unmanaged<nint, void> _SetListenerPostHandler;

    /// <summary>
    /// the callback should receive the following: uint32 eventNameHash, IntPtr gameEvent, bool* dontBroadcast, return HookResult int
    /// </summary>
    public unsafe static void SetListenerPostHandler(nint callback)
    {
        _SetListenerPostHandler(callback);
    }

    private unsafe static delegate* unmanaged<byte*, nint> _CreateEvent;

    public unsafe static nint CreateEvent(string eventName)
    {
        using var eventNameStr = new ScopedCString(eventName);
        fixed (byte* eventNameBufferPtr = eventNameStr)
        {
            var ret = _CreateEvent(eventNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, void> _FreeEvent;

    public unsafe static void FreeEvent(nint _event)
    {
        _FreeEvent(_event);
    }

    private unsafe static delegate* unmanaged<nint, byte, void> _FireEvent;

    public unsafe static void FireEvent(nint _event, bool dontBroadcast)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        _FireEvent(_event, dontBroadcast ? (byte)1 : (byte)0);
    }

    private unsafe static delegate* unmanaged<nint, int, void> _FireEventToClient;

    public unsafe static void FireEventToClient(nint _event, int playerid)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        _FireEventToClient(_event, playerid);
    }

    private unsafe static delegate* unmanaged<int, byte*, byte> _IsPlayerListeningToEventName;

    public unsafe static bool IsPlayerListeningToEventName(int playerid, string eventName)
    {
        using var eventNameStr = new ScopedCString(eventName);
        fixed (byte* eventNameBufferPtr = eventNameStr)
        {
            var ret = _IsPlayerListeningToEventName(playerid, eventNameBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<int, nint, byte> _IsPlayerListeningToEvent;

    public unsafe static bool IsPlayerListeningToEvent(int playerid, nint _event)
    {
        var ret = _IsPlayerListeningToEvent(playerid, _event);
        return ret == 1;
    }
}