#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeEntitySystem {
  private static int _MainThreadID;

  private unsafe static delegate* unmanaged<nint, nint, void> _Spawn;

  public unsafe static void Spawn(nint entity, nint keyvalues) {
    _Spawn(entity, keyvalues);
  }

  private unsafe static delegate* unmanaged<nint, void> _Despawn;

  public unsafe static void Despawn(nint entity) {
    _Despawn(entity);
  }

  private unsafe static delegate* unmanaged<byte*, nint> _CreateEntityByName;

  public unsafe static nint CreateEntityByName(string name) {
    var pool = ArrayPool<byte>.Shared;
    var nameLength = Encoding.UTF8.GetByteCount(name);
    var nameBuffer = pool.Rent(nameLength + 1);
    Encoding.UTF8.GetBytes(name, nameBuffer);
    nameBuffer[nameLength] = 0;
    fixed (byte* nameBufferPtr = nameBuffer) {
      var ret = _CreateEntityByName(nameBufferPtr);
      pool.Return(nameBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, int, int, void> _AcceptInputInt32;

  public unsafe static void AcceptInputInt32(nint entity, string input, nint activator, nint caller, int value, int outputID) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AcceptInputInt32(entity, inputBufferPtr, activator, caller, value, outputID);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, uint, int, void> _AcceptInputUInt32;

  public unsafe static void AcceptInputUInt32(nint entity, string input, nint activator, nint caller, uint value, int outputID) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AcceptInputUInt32(entity, inputBufferPtr, activator, caller, value, outputID);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, long, int, void> _AcceptInputInt64;

  public unsafe static void AcceptInputInt64(nint entity, string input, nint activator, nint caller, long value, int outputID) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AcceptInputInt64(entity, inputBufferPtr, activator, caller, value, outputID);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, ulong, int, void> _AcceptInputUInt64;

  public unsafe static void AcceptInputUInt64(nint entity, string input, nint activator, nint caller, ulong value, int outputID) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AcceptInputUInt64(entity, inputBufferPtr, activator, caller, value, outputID);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, float, int, void> _AcceptInputFloat;

  public unsafe static void AcceptInputFloat(nint entity, string input, nint activator, nint caller, float value, int outputID) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AcceptInputFloat(entity, inputBufferPtr, activator, caller, value, outputID);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, double, int, void> _AcceptInputDouble;

  public unsafe static void AcceptInputDouble(nint entity, string input, nint activator, nint caller, double value, int outputID) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AcceptInputDouble(entity, inputBufferPtr, activator, caller, value, outputID);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, byte, int, void> _AcceptInputBool;

  public unsafe static void AcceptInputBool(nint entity, string input, nint activator, nint caller, bool value, int outputID) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AcceptInputBool(entity, inputBufferPtr, activator, caller, value ? (byte)1 : (byte)0, outputID);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, byte*, int, void> _AcceptInputString;

  public unsafe static void AcceptInputString(nint entity, string input, nint activator, nint caller, string value, int outputID) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    var valueLength = Encoding.UTF8.GetByteCount(value);
    var valueBuffer = pool.Rent(valueLength + 1);
    Encoding.UTF8.GetBytes(value, valueBuffer);
    valueBuffer[valueLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      fixed (byte* valueBufferPtr = valueBuffer) {
        _AcceptInputString(entity, inputBufferPtr, activator, caller, valueBufferPtr, outputID);
        pool.Return(inputBuffer);
        pool.Return(valueBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, int, float, void> _AddEntityIOEventInt32;

  public unsafe static void AddEntityIOEventInt32(nint entity, string input, nint activator, nint caller, int value, float delay) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AddEntityIOEventInt32(entity, inputBufferPtr, activator, caller, value, delay);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, uint, float, void> _AddEntityIOEventUInt32;

  public unsafe static void AddEntityIOEventUInt32(nint entity, string input, nint activator, nint caller, uint value, float delay) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AddEntityIOEventUInt32(entity, inputBufferPtr, activator, caller, value, delay);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, long, float, void> _AddEntityIOEventInt64;

  public unsafe static void AddEntityIOEventInt64(nint entity, string input, nint activator, nint caller, long value, float delay) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AddEntityIOEventInt64(entity, inputBufferPtr, activator, caller, value, delay);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, ulong, float, void> _AddEntityIOEventUInt64;

  public unsafe static void AddEntityIOEventUInt64(nint entity, string input, nint activator, nint caller, ulong value, float delay) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AddEntityIOEventUInt64(entity, inputBufferPtr, activator, caller, value, delay);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, float, float, void> _AddEntityIOEventFloat;

  public unsafe static void AddEntityIOEventFloat(nint entity, string input, nint activator, nint caller, float value, float delay) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AddEntityIOEventFloat(entity, inputBufferPtr, activator, caller, value, delay);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, double, float, void> _AddEntityIOEventDouble;

  public unsafe static void AddEntityIOEventDouble(nint entity, string input, nint activator, nint caller, double value, float delay) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AddEntityIOEventDouble(entity, inputBufferPtr, activator, caller, value, delay);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, byte, float, void> _AddEntityIOEventBool;

  public unsafe static void AddEntityIOEventBool(nint entity, string input, nint activator, nint caller, bool value, float delay) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AddEntityIOEventBool(entity, inputBufferPtr, activator, caller, value ? (byte)1 : (byte)0, delay);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, byte*, float, void> _AddEntityIOEventString;

  public unsafe static void AddEntityIOEventString(nint entity, string input, nint activator, nint caller, string value, float delay) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    var valueLength = Encoding.UTF8.GetByteCount(value);
    var valueBuffer = pool.Rent(valueLength + 1);
    Encoding.UTF8.GetBytes(value, valueBuffer);
    valueBuffer[valueLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      fixed (byte* valueBufferPtr = valueBuffer) {
        _AddEntityIOEventString(entity, inputBufferPtr, activator, caller, valueBufferPtr, delay);
        pool.Return(inputBuffer);
        pool.Return(valueBuffer);
      }
    }
  }

  private unsafe static delegate* unmanaged<nint, byte> _IsValidEntity;

  public unsafe static bool IsValidEntity(nint entity) {
    var ret = _IsValidEntity(entity);
    return ret == 1;
  }

  private unsafe static delegate* unmanaged<nint> _GetGameRules;

  public unsafe static nint GetGameRules() {
    var ret = _GetGameRules();
    return ret;
  }

  private unsafe static delegate* unmanaged<nint> _GetEntitySystem;

  public unsafe static nint GetEntitySystem() {
    var ret = _GetEntitySystem();
    return ret;
  }

  private unsafe static delegate* unmanaged<uint, byte> _EntityHandleIsValid;

  public unsafe static bool EntityHandleIsValid(uint handle) {
    var ret = _EntityHandleIsValid(handle);
    return ret == 1;
  }

  private unsafe static delegate* unmanaged<uint, nint> _EntityHandleGet;

  public unsafe static nint EntityHandleGet(uint handle) {
    var ret = _EntityHandleGet(handle);
    return ret;
  }

  private unsafe static delegate* unmanaged<nint, uint> _GetEntityHandleFromEntity;

  public unsafe static uint GetEntityHandleFromEntity(nint entity) {
    var ret = _GetEntityHandleFromEntity(entity);
    return ret;
  }

  private unsafe static delegate* unmanaged<nint> _GetFirstActiveEntity;

  public unsafe static nint GetFirstActiveEntity() {
    var ret = _GetFirstActiveEntity();
    return ret;
  }

  private unsafe static delegate* unmanaged<byte*, byte*, nint, ulong> _HookEntityOutput;

  /// <summary>
  /// CEntityIOOutput*, string outputName, CEntityInstance* activator, CEntityInstance* caller, float delay -> int (HookResult)
  /// </summary>
  public unsafe static ulong HookEntityOutput(string className, string outputName, nint callback) {
    var pool = ArrayPool<byte>.Shared;
    var classNameLength = Encoding.UTF8.GetByteCount(className);
    var classNameBuffer = pool.Rent(classNameLength + 1);
    Encoding.UTF8.GetBytes(className, classNameBuffer);
    classNameBuffer[classNameLength] = 0;
    var outputNameLength = Encoding.UTF8.GetByteCount(outputName);
    var outputNameBuffer = pool.Rent(outputNameLength + 1);
    Encoding.UTF8.GetBytes(outputName, outputNameBuffer);
    outputNameBuffer[outputNameLength] = 0;
    fixed (byte* classNameBufferPtr = classNameBuffer) {
      fixed (byte* outputNameBufferPtr = outputNameBuffer) {
        var ret = _HookEntityOutput(classNameBufferPtr, outputNameBufferPtr, callback);
        pool.Return(classNameBuffer);
        pool.Return(outputNameBuffer);
        return ret;
      }
    }
  }

  private unsafe static delegate* unmanaged<ulong, void> _UnhookEntityOutput;

  public unsafe static void UnhookEntityOutput(ulong hookid) {
    _UnhookEntityOutput(hookid);
  }

  private unsafe static delegate* unmanaged<uint, nint> _GetEntityByIndex;

  public unsafe static nint GetEntityByIndex(uint index) {
    var ret = _GetEntityByIndex(index);
    return ret;
  }
}