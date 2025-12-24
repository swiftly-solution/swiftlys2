#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeKeyValuesSystem {

  private unsafe static delegate* unmanaged<byte*, uint> _GetSymbolForString;

  public unsafe static uint GetSymbolForString(string str) {
    var pool = ArrayPool<byte>.Shared;
    var strLength = Encoding.UTF8.GetByteCount(str);
    var strBuffer = pool.Rent(strLength + 1);
    Encoding.UTF8.GetBytes(str, strBuffer);
    strBuffer[strLength] = 0;
    fixed (byte* strBufferPtr = strBuffer) {
      var ret = _GetSymbolForString(strBufferPtr);
      pool.Return(strBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<byte*, uint, int> _GetStringForSymbol;

  public unsafe static string GetStringForSymbol(uint symbol) {
    var ret = _GetStringForSymbol(null, symbol);
    var pool = ArrayPool<byte>.Shared;
    var retBuffer = pool.Rent(ret + 1);
    fixed (byte* retBufferPtr = retBuffer) {
      ret = _GetStringForSymbol(retBufferPtr, symbol);
      var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
      pool.Return(retBuffer);
      return retString;
    }
  }
}