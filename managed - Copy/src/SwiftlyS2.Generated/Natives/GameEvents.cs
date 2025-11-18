#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeGameEvents {
  private static int _MainThreadID;

  private unsafe static delegate* unmanaged<nint, byte*, byte> _GetBool;

  public unsafe static bool GetBool(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetBool(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret == 1;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, int> _GetInt;

  public unsafe static int GetInt(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetInt(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, ulong> _GetUint64;

  public unsafe static ulong GetUint64(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetUint64(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, float> _GetFloat;

  public unsafe static float GetFloat(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetFloat(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<byte*, nint, byte*, int> _GetString;

  public unsafe static string GetString(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetString(null, _event, keyBufferPtr);
      var retBuffer = pool.Rent(ret + 1);
      fixed (byte* retBufferPtr = retBuffer) {
        ret = _GetString(retBufferPtr, _event, keyBufferPtr);
        var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
        pool.Return(retBuffer);
        pool.Return(keyBuffer);
        return retString;
      }
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint> _GetPtr;

  public unsafe static nint GetPtr(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetPtr(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint> _GetEHandle;

  /// <summary>
  /// returns the pointer stored inside the handle
  /// </summary>
  public unsafe static nint GetEHandle(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetEHandle(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint> _GetEntity;

  public unsafe static nint GetEntity(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetEntity(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, int> _GetEntityIndex;

  public unsafe static int GetEntityIndex(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetEntityIndex(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, int> _GetPlayerSlot;

  public unsafe static int GetPlayerSlot(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetPlayerSlot(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint> _GetPlayerController;

  public unsafe static nint GetPlayerController(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetPlayerController(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint> _GetPlayerPawn;

  public unsafe static nint GetPlayerPawn(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetPlayerPawn(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint> _GetPawnEHandle;

  /// <summary>
  /// returns the pointer stored inside the handle
  /// </summary>
  public unsafe static nint GetPawnEHandle(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetPawnEHandle(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, int> _GetPawnEntityIndex;

  public unsafe static int GetPawnEntityIndex(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _GetPawnEntityIndex(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, byte, void> _SetBool;

  public unsafe static void SetBool(nint _event, string key, bool value) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      _SetBool(_event, keyBufferPtr, value ? (byte)1 : (byte)0);
      pool.Return(keyBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, int, void> _SetInt;

  public unsafe static void SetInt(nint _event, string key, int value) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      _SetInt(_event, keyBufferPtr, value);
      pool.Return(keyBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, ulong, void> _SetUint64;

  public unsafe static void SetUint64(nint _event, string key, ulong value) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      _SetUint64(_event, keyBufferPtr, value);
      pool.Return(keyBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, float, void> _SetFloat;

  public unsafe static void SetFloat(nint _event, string key, float value) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      _SetFloat(_event, keyBufferPtr, value);
      pool.Return(keyBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, byte*, void> _SetString;

  public unsafe static void SetString(nint _event, string key, string value) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    var valueLength = Encoding.UTF8.GetByteCount(value);
    var valueBuffer = pool.Rent(valueLength + 1);
    Encoding.UTF8.GetBytes(value, valueBuffer);
    valueBuffer[valueLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      fixed (byte* valueBufferPtr = valueBuffer) {
        _SetString(_event, keyBufferPtr, valueBufferPtr);
        pool.Return(keyBuffer);
        pool.Return(valueBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, void> _SetPtr;

  public unsafe static void SetPtr(nint _event, string key, nint value) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      _SetPtr(_event, keyBufferPtr, value);
      pool.Return(keyBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, void> _SetEntity;

  public unsafe static void SetEntity(nint _event, string key, nint value) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      _SetEntity(_event, keyBufferPtr, value);
      pool.Return(keyBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, int, void> _SetEntityIndex;

  public unsafe static void SetEntityIndex(nint _event, string key, int value) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      _SetEntityIndex(_event, keyBufferPtr, value);
      pool.Return(keyBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, int, void> _SetPlayerSlot;

  public unsafe static void SetPlayerSlot(nint _event, string key, int value) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      _SetPlayerSlot(_event, keyBufferPtr, value);
      pool.Return(keyBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, byte> _HasKey;

  public unsafe static bool HasKey(nint _event, string key) {
    var pool = ArrayPool<byte>.Shared;
    var keyLength = Encoding.UTF8.GetByteCount(key);
    var keyBuffer = pool.Rent(keyLength + 1);
    Encoding.UTF8.GetBytes(key, keyBuffer);
    keyBuffer[keyLength] = 0;
    fixed (byte* keyBufferPtr = keyBuffer) {
      var ret = _HasKey(_event, keyBufferPtr);
      pool.Return(keyBuffer);
      return ret == 1;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte> _IsReliable;

  public unsafe static bool IsReliable(nint _event) {
    var ret = _IsReliable(_event);
    return ret == 1;
  }

  private unsafe static delegate* unmanaged<nint, byte> _IsLocal;

  public unsafe static bool IsLocal(nint _event) {
    var ret = _IsLocal(_event);
    return ret == 1;
  }

  private unsafe static delegate* unmanaged<byte*, void> _RegisterListener;

  public unsafe static void RegisterListener(string eventName) {
    var pool = ArrayPool<byte>.Shared;
    var eventNameLength = Encoding.UTF8.GetByteCount(eventName);
    var eventNameBuffer = pool.Rent(eventNameLength + 1);
    Encoding.UTF8.GetBytes(eventName, eventNameBuffer);
    eventNameBuffer[eventNameLength] = 0;
    fixed (byte* eventNameBufferPtr = eventNameBuffer) {
      _RegisterListener(eventNameBufferPtr);
      pool.Return(eventNameBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, ulong> _AddListenerPreCallback;

  /// <summary>
  /// the callback should receive the following: uint32 eventNameHash, IntPtr gameEvent, bool* dontBroadcast, return bool (true -> ignored, false -> supercede)
  /// </summary>
  public unsafe static ulong AddListenerPreCallback(nint callback) {
    var ret = _AddListenerPreCallback(callback);
    return ret;
  }

  private unsafe static delegate* unmanaged<nint, ulong> _AddListenerPostCallback;

  /// <summary>
  /// the callback should receive the following: uint32 eventNameHash, IntPtr gameEvent, bool* dontBroadcast, return bool (true -> ignored, false -> supercede)
  /// </summary>
  public unsafe static ulong AddListenerPostCallback(nint callback) {
    var ret = _AddListenerPostCallback(callback);
    return ret;
  }

  private unsafe static delegate* unmanaged<ulong, void> _RemoveListenerPreCallback;

  public unsafe static void RemoveListenerPreCallback(ulong listenerID) {
    _RemoveListenerPreCallback(listenerID);
  }

  private unsafe static delegate* unmanaged<ulong, void> _RemoveListenerPostCallback;

  public unsafe static void RemoveListenerPostCallback(ulong listenerID) {
    _RemoveListenerPostCallback(listenerID);
  }

  private unsafe static delegate* unmanaged<byte*, nint> _CreateEvent;

  public unsafe static nint CreateEvent(string eventName) {
    var pool = ArrayPool<byte>.Shared;
    var eventNameLength = Encoding.UTF8.GetByteCount(eventName);
    var eventNameBuffer = pool.Rent(eventNameLength + 1);
    Encoding.UTF8.GetBytes(eventName, eventNameBuffer);
    eventNameBuffer[eventNameLength] = 0;
    fixed (byte* eventNameBufferPtr = eventNameBuffer) {
      var ret = _CreateEvent(eventNameBufferPtr);
      pool.Return(eventNameBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<nint, void> _FreeEvent;

  public unsafe static void FreeEvent(nint _event) {
    _FreeEvent(_event);
  }

  private unsafe static delegate* unmanaged<nint, byte, void> _FireEvent;

  public unsafe static void FireEvent(nint _event, bool dontBroadcast) {
    _FireEvent(_event, dontBroadcast ? (byte)1 : (byte)0);
  }

  private unsafe static delegate* unmanaged<nint, int, void> _FireEventToClient;

  public unsafe static void FireEventToClient(nint _event, int playerid) {
    _FireEventToClient(_event, playerid);
  }

  private unsafe static delegate* unmanaged<int, byte*, byte> _IsPlayerListeningToEventName;

  public unsafe static bool IsPlayerListeningToEventName(int playerid, string eventName) {
    var pool = ArrayPool<byte>.Shared;
    var eventNameLength = Encoding.UTF8.GetByteCount(eventName);
    var eventNameBuffer = pool.Rent(eventNameLength + 1);
    Encoding.UTF8.GetBytes(eventName, eventNameBuffer);
    eventNameBuffer[eventNameLength] = 0;
    fixed (byte* eventNameBufferPtr = eventNameBuffer) {
      var ret = _IsPlayerListeningToEventName(playerid, eventNameBufferPtr);
      pool.Return(eventNameBuffer);
      return ret == 1;
    }
  }

  private unsafe static delegate* unmanaged<int, nint, byte> _IsPlayerListeningToEvent;

  public unsafe static bool IsPlayerListeningToEvent(int playerid, nint _event) {
    var ret = _IsPlayerListeningToEvent(playerid, _event);
    return ret == 1;
  }
}