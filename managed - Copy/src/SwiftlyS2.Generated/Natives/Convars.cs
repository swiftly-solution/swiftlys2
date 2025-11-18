#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeConvars {
  private static int _MainThreadID;

  private unsafe static delegate* unmanaged<int, byte*, void> _QueryClientConvar;

  public unsafe static void QueryClientConvar(int playerid, string cvarName) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      _QueryClientConvar(playerid, cvarNameBufferPtr);
      pool.Return(cvarNameBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, int> _AddQueryClientCvarCallback;

  /// <summary>
  /// the callback should receive the following: int32 playerid, string cvarName, string cvarValue
  /// </summary>
  public unsafe static int AddQueryClientCvarCallback(nint callback) {
    var ret = _AddQueryClientCvarCallback(callback);
    return ret;
  }

  private unsafe static delegate* unmanaged<int, void> _RemoveQueryClientCvarCallback;

  public unsafe static void RemoveQueryClientCvarCallback(int callbackID) {
    _RemoveQueryClientCvarCallback(callbackID);
  }

  private unsafe static delegate* unmanaged<nint, ulong> _AddGlobalChangeListener;

  /// <summary>
  /// the callback should receive the following: string convarName, int playerid, string newValue, string oldValue
  /// </summary>
  public unsafe static ulong AddGlobalChangeListener(nint callback) {
    var ret = _AddGlobalChangeListener(callback);
    return ret;
  }

  private unsafe static delegate* unmanaged<ulong, void> _RemoveGlobalChangeListener;

  public unsafe static void RemoveGlobalChangeListener(ulong callbackID) {
    _RemoveGlobalChangeListener(callbackID);
  }

  private unsafe static delegate* unmanaged<nint, ulong> _AddConvarCreatedListener;

  /// <summary>
  /// the callback should receive the following: string convarName
  /// </summary>
  public unsafe static ulong AddConvarCreatedListener(nint callback) {
    var ret = _AddConvarCreatedListener(callback);
    return ret;
  }

  private unsafe static delegate* unmanaged<ulong, void> _RemoveConvarCreatedListener;

  public unsafe static void RemoveConvarCreatedListener(ulong callbackID) {
    _RemoveConvarCreatedListener(callbackID);
  }

  private unsafe static delegate* unmanaged<nint, ulong> _AddConCommandCreatedListener;

  /// <summary>
  /// the callback should receive the following: string commandName
  /// </summary>
  public unsafe static ulong AddConCommandCreatedListener(nint callback) {
    var ret = _AddConCommandCreatedListener(callback);
    return ret;
  }

  private unsafe static delegate* unmanaged<ulong, void> _RemoveConCommandCreatedListener;

  public unsafe static void RemoveConCommandCreatedListener(ulong callbackID) {
    _RemoveConCommandCreatedListener(callbackID);
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, short, nint, nint, void> _CreateConvarInt16;

  public unsafe static void CreateConvarInt16(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, short defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarInt16(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, ushort, nint, nint, void> _CreateConvarUInt16;

  public unsafe static void CreateConvarUInt16(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, ushort defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarUInt16(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, int, nint, nint, void> _CreateConvarInt32;

  public unsafe static void CreateConvarInt32(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, int defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarInt32(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, uint, nint, nint, void> _CreateConvarUInt32;

  public unsafe static void CreateConvarUInt32(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, uint defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarUInt32(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, long, nint, nint, void> _CreateConvarInt64;

  public unsafe static void CreateConvarInt64(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, long defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarInt64(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, ulong, nint, nint, void> _CreateConvarUInt64;

  public unsafe static void CreateConvarUInt64(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, ulong defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarUInt64(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, byte, nint, nint, void> _CreateConvarBool;

  public unsafe static void CreateConvarBool(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, bool defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarBool(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue ? (byte)1 : (byte)0, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, float, nint, nint, void> _CreateConvarFloat;

  public unsafe static void CreateConvarFloat(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, float defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarFloat(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, double, nint, nint, void> _CreateConvarDouble;

  public unsafe static void CreateConvarDouble(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, double defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarDouble(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, Color, nint, nint, void> _CreateConvarColor;

  public unsafe static void CreateConvarColor(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, Color defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarColor(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, Vector2D, nint, nint, void> _CreateConvarVector2D;

  public unsafe static void CreateConvarVector2D(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, Vector2D defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarVector2D(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, Vector, nint, nint, void> _CreateConvarVector;

  public unsafe static void CreateConvarVector(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, Vector defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarVector(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, Vector4D, nint, nint, void> _CreateConvarVector4D;

  public unsafe static void CreateConvarVector4D(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, Vector4D defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarVector4D(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, QAngle, nint, nint, void> _CreateConvarQAngle;

  public unsafe static void CreateConvarQAngle(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, QAngle defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        _CreateConvarQAngle(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
        pool.Return(cvarNameBuffer);
        pool.Return(helpMessageBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, byte*, nint, nint, void> _CreateConvarString;

  public unsafe static void CreateConvarString(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, string defaultValue, nint minValue, nint maxValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var helpMessageLength = Encoding.UTF8.GetByteCount(helpMessage);
    var helpMessageBuffer = pool.Rent(helpMessageLength + 1);
    Encoding.UTF8.GetBytes(helpMessage, helpMessageBuffer);
    helpMessageBuffer[helpMessageLength] = 0;
    var defaultValueLength = Encoding.UTF8.GetByteCount(defaultValue);
    var defaultValueBuffer = pool.Rent(defaultValueLength + 1);
    Encoding.UTF8.GetBytes(defaultValue, defaultValueBuffer);
    defaultValueBuffer[defaultValueLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* helpMessageBufferPtr = helpMessageBuffer) {
        fixed (byte* defaultValueBufferPtr = defaultValueBuffer) {
          _CreateConvarString(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValueBufferPtr, minValue, maxValue);
          pool.Return(cvarNameBuffer);
          pool.Return(helpMessageBuffer);
          pool.Return(defaultValueBuffer);
        }
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, void> _DeleteConvar;

  public unsafe static void DeleteConvar(string cvarName) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      _DeleteConvar(cvarNameBufferPtr);
      pool.Return(cvarNameBuffer);
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte> _ExistsConvar;

  public unsafe static bool ExistsConvar(string cvarName) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      var ret = _ExistsConvar(cvarNameBufferPtr);
      pool.Return(cvarNameBuffer);
      return ret == 1;
    }
  }

  private unsafe static delegate* unmanaged<byte*, int> _GetConvarType;

  public unsafe static int GetConvarType(string cvarName) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      var ret = _GetConvarType(cvarNameBufferPtr);
      pool.Return(cvarNameBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<int, byte*, byte*, void> _SetClientConvarValueString;

  public unsafe static void SetClientConvarValueString(int playerid, string cvarName, string defaultValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var defaultValueLength = Encoding.UTF8.GetByteCount(defaultValue);
    var defaultValueBuffer = pool.Rent(defaultValueLength + 1);
    Encoding.UTF8.GetBytes(defaultValue, defaultValueBuffer);
    defaultValueBuffer[defaultValueLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* defaultValueBufferPtr = defaultValueBuffer) {
        _SetClientConvarValueString(playerid, cvarNameBufferPtr, defaultValueBufferPtr);
        pool.Return(cvarNameBuffer);
        pool.Return(defaultValueBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, ulong> _GetFlags;

  public unsafe static ulong GetFlags(string cvarName) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      var ret = _GetFlags(cvarNameBufferPtr);
      pool.Return(cvarNameBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<byte*, ulong, void> _SetFlags;

  public unsafe static void SetFlags(string cvarName, ulong flags) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      _SetFlags(cvarNameBufferPtr, flags);
      pool.Return(cvarNameBuffer);
    }
  }

  private unsafe static delegate* unmanaged<byte*, nint> _GetMinValuePtrPtr;

  public unsafe static nint GetMinValuePtrPtr(string cvarName) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      var ret = _GetMinValuePtrPtr(cvarNameBufferPtr);
      pool.Return(cvarNameBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<byte*, nint> _GetMaxValuePtrPtr;

  public unsafe static nint GetMaxValuePtrPtr(string cvarName) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      var ret = _GetMaxValuePtrPtr(cvarNameBufferPtr);
      pool.Return(cvarNameBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte> _HasDefaultValue;

  public unsafe static bool HasDefaultValue(string cvarName) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      var ret = _HasDefaultValue(cvarNameBufferPtr);
      pool.Return(cvarNameBuffer);
      return ret == 1;
    }
  }

  private unsafe static delegate* unmanaged<byte*, nint> _GetDefaultValuePtr;

  public unsafe static nint GetDefaultValuePtr(string cvarName) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      var ret = _GetDefaultValuePtr(cvarNameBufferPtr);
      pool.Return(cvarNameBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<byte*, nint, void> _SetDefaultValue;

  public unsafe static void SetDefaultValue(string cvarName, nint defaultValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      _SetDefaultValue(cvarNameBufferPtr, defaultValue);
      pool.Return(cvarNameBuffer);
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte*, void> _SetDefaultValueString;

  public unsafe static void SetDefaultValueString(string cvarName, string defaultValue) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    var defaultValueLength = Encoding.UTF8.GetByteCount(defaultValue);
    var defaultValueBuffer = pool.Rent(defaultValueLength + 1);
    Encoding.UTF8.GetBytes(defaultValue, defaultValueBuffer);
    defaultValueBuffer[defaultValueLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      fixed (byte* defaultValueBufferPtr = defaultValueBuffer) {
        _SetDefaultValueString(cvarNameBufferPtr, defaultValueBufferPtr);
        pool.Return(cvarNameBuffer);
        pool.Return(defaultValueBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, nint> _GetValuePtr;

  public unsafe static nint GetValuePtr(string cvarName) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      var ret = _GetValuePtr(cvarNameBufferPtr);
      pool.Return(cvarNameBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<byte*, nint, void> _SetValuePtr;

  public unsafe static void SetValuePtr(string cvarName, nint value) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      _SetValuePtr(cvarNameBufferPtr, value);
      pool.Return(cvarNameBuffer);
    }
  }

  private unsafe static delegate* unmanaged<byte*, nint, void> _SetValueInternalPtr;

  public unsafe static void SetValueInternalPtr(string cvarName, nint value) {
    var pool = ArrayPool<byte>.Shared;
    var cvarNameLength = Encoding.UTF8.GetByteCount(cvarName);
    var cvarNameBuffer = pool.Rent(cvarNameLength + 1);
    Encoding.UTF8.GetBytes(cvarName, cvarNameBuffer);
    cvarNameBuffer[cvarNameLength] = 0;
    fixed (byte* cvarNameBufferPtr = cvarNameBuffer) {
      _SetValueInternalPtr(cvarNameBufferPtr, value);
      pool.Return(cvarNameBuffer);
    }
  }
}