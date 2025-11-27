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

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, nint, int, void> _AcceptInput;

  public unsafe static void AcceptInput(nint entity, string input, nint activator, nint caller, nint value, int outputID) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AcceptInput(entity, inputBufferPtr, activator, caller, value, outputID);
      pool.Return(inputBuffer);
    }
  }

  private unsafe static delegate* unmanaged<nint, byte*, nint, nint, nint, float, void> _AddEntityIOEvent;

  public unsafe static void AddEntityIOEvent(nint entity, string input, nint activator, nint caller, nint value, float delay) {
    var pool = ArrayPool<byte>.Shared;
    var inputLength = Encoding.UTF8.GetByteCount(input);
    var inputBuffer = pool.Rent(inputLength + 1);
    Encoding.UTF8.GetBytes(input, inputBuffer);
    inputBuffer[inputLength] = 0;
    fixed (byte* inputBufferPtr = inputBuffer) {
      _AddEntityIOEvent(entity, inputBufferPtr, activator, caller, value, delay);
      pool.Return(inputBuffer);
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