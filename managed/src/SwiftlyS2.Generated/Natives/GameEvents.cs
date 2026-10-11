#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeGameEvents
{

    private unsafe static delegate* unmanaged<nint, byte*, int, nint, byte> _GetValue;

    public unsafe static bool GetValue(nint _event, string key, int kind, nint value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetValue(_event, keyBufferPtr, kind, value);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, nint, byte> _SetValue;

    public unsafe static bool SetValue(nint _event, string key, int kind, nint value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _SetValue(_event, keyBufferPtr, kind, value);
            return ret == 1;
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

    private unsafe static delegate* unmanaged<byte*, void> _UnregisterListener;

    public unsafe static void UnregisterListener(string eventName)
    {
        using var eventNameStr = new ScopedCString(eventName);
        fixed (byte* eventNameBufferPtr = eventNameStr)
        {
            _UnregisterListener(eventNameBufferPtr);
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