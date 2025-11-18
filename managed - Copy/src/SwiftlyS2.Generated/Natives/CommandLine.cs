#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeCommandLine {
  private static int _MainThreadID;

  private unsafe static delegate* unmanaged<byte*, byte> _HasParameter;

  public unsafe static bool HasParameter(string parameter) {
    var pool = ArrayPool<byte>.Shared;
    var parameterLength = Encoding.UTF8.GetByteCount(parameter);
    var parameterBuffer = pool.Rent(parameterLength + 1);
    Encoding.UTF8.GetBytes(parameter, parameterBuffer);
    parameterBuffer[parameterLength] = 0;
    fixed (byte* parameterBufferPtr = parameterBuffer) {
      var ret = _HasParameter(parameterBufferPtr);
      pool.Return(parameterBuffer);
      return ret == 1;
    }
  }

  private unsafe static delegate* unmanaged<int> _GetParameterCount;

  public unsafe static int GetParameterCount() {
    var ret = _GetParameterCount();
    return ret;
  }

  private unsafe static delegate* unmanaged<byte*, byte*, byte*, int> _GetParameterValueString;

  public unsafe static string GetParameterValueString(string parameter, string defaultValue) {
    var pool = ArrayPool<byte>.Shared;
    var parameterLength = Encoding.UTF8.GetByteCount(parameter);
    var parameterBuffer = pool.Rent(parameterLength + 1);
    Encoding.UTF8.GetBytes(parameter, parameterBuffer);
    parameterBuffer[parameterLength] = 0;
    var defaultValueLength = Encoding.UTF8.GetByteCount(defaultValue);
    var defaultValueBuffer = pool.Rent(defaultValueLength + 1);
    Encoding.UTF8.GetBytes(defaultValue, defaultValueBuffer);
    defaultValueBuffer[defaultValueLength] = 0;
    fixed (byte* parameterBufferPtr = parameterBuffer) {
      fixed (byte* defaultValueBufferPtr = defaultValueBuffer) {
        var ret = _GetParameterValueString(null, parameterBufferPtr, defaultValueBufferPtr);
        var retBuffer = pool.Rent(ret + 1);
        fixed (byte* retBufferPtr = retBuffer) {
          ret = _GetParameterValueString(retBufferPtr, parameterBufferPtr, defaultValueBufferPtr);
          var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
          pool.Return(retBuffer);
          pool.Return(parameterBuffer);
          pool.Return(defaultValueBuffer);
          return retString;
        }
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, int, int> _GetParameterValueInt;

  public unsafe static int GetParameterValueInt(string parameter, int defaultValue) {
    var pool = ArrayPool<byte>.Shared;
    var parameterLength = Encoding.UTF8.GetByteCount(parameter);
    var parameterBuffer = pool.Rent(parameterLength + 1);
    Encoding.UTF8.GetBytes(parameter, parameterBuffer);
    parameterBuffer[parameterLength] = 0;
    fixed (byte* parameterBufferPtr = parameterBuffer) {
      var ret = _GetParameterValueInt(parameterBufferPtr, defaultValue);
      pool.Return(parameterBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<byte*, float, float> _GetParameterValueFloat;

  public unsafe static float GetParameterValueFloat(string parameter, float defaultValue) {
    var pool = ArrayPool<byte>.Shared;
    var parameterLength = Encoding.UTF8.GetByteCount(parameter);
    var parameterBuffer = pool.Rent(parameterLength + 1);
    Encoding.UTF8.GetBytes(parameter, parameterBuffer);
    parameterBuffer[parameterLength] = 0;
    fixed (byte* parameterBufferPtr = parameterBuffer) {
      var ret = _GetParameterValueFloat(parameterBufferPtr, defaultValue);
      pool.Return(parameterBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<byte*, int> _GetCommandLine;

  public unsafe static string GetCommandLine() {
    var ret = _GetCommandLine(null);
    var pool = ArrayPool<byte>.Shared;
    var retBuffer = pool.Rent(ret + 1);
    fixed (byte* retBufferPtr = retBuffer) {
      ret = _GetCommandLine(retBufferPtr);
      var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
      pool.Return(retBuffer);
      return retString;
    }
  }

  private unsafe static delegate* unmanaged<byte> _HasParameters;

  public unsafe static bool HasParameters() {
    var ret = _HasParameters();
    return ret == 1;
  }
}