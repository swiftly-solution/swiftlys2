#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeDatabase {
  private static int _MainThreadID;

  private unsafe static delegate* unmanaged<byte*, int> _GetDefaultConnection;

  public unsafe static string GetDefaultConnection() {
    var ret = _GetDefaultConnection(null);
    var pool = ArrayPool<byte>.Shared;
    var retBuffer = pool.Rent(ret + 1);
    fixed (byte* retBufferPtr = retBuffer) {
      ret = _GetDefaultConnection(retBufferPtr);
      var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
      pool.Return(retBuffer);
      return retString;
    }
  }

  private unsafe static delegate* unmanaged<byte*, int> _GetDefaultConnectionCredentials;

  public unsafe static string GetDefaultConnectionCredentials() {
    var ret = _GetDefaultConnectionCredentials(null);
    var pool = ArrayPool<byte>.Shared;
    var retBuffer = pool.Rent(ret + 1);
    fixed (byte* retBufferPtr = retBuffer) {
      ret = _GetDefaultConnectionCredentials(retBufferPtr);
      var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
      pool.Return(retBuffer);
      return retString;
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte*, int> _GetCredentials;

  public unsafe static string GetCredentials(string connectionName) {
    var pool = ArrayPool<byte>.Shared;
    var connectionNameLength = Encoding.UTF8.GetByteCount(connectionName);
    var connectionNameBuffer = pool.Rent(connectionNameLength + 1);
    Encoding.UTF8.GetBytes(connectionName, connectionNameBuffer);
    connectionNameBuffer[connectionNameLength] = 0;
    fixed (byte* connectionNameBufferPtr = connectionNameBuffer) {
      var ret = _GetCredentials(null, connectionNameBufferPtr);
      var retBuffer = pool.Rent(ret + 1);
      fixed (byte* retBufferPtr = retBuffer) {
        ret = _GetCredentials(retBufferPtr, connectionNameBufferPtr);
        var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
        pool.Return(retBuffer);
        pool.Return(connectionNameBuffer);
        return retString;
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte> _ConnectionExists;

  public unsafe static bool ConnectionExists(string connectionName) {
    var pool = ArrayPool<byte>.Shared;
    var connectionNameLength = Encoding.UTF8.GetByteCount(connectionName);
    var connectionNameBuffer = pool.Rent(connectionNameLength + 1);
    Encoding.UTF8.GetBytes(connectionName, connectionNameBuffer);
    connectionNameBuffer[connectionNameLength] = 0;
    fixed (byte* connectionNameBufferPtr = connectionNameBuffer) {
      var ret = _ConnectionExists(connectionNameBufferPtr);
      pool.Return(connectionNameBuffer);
      return ret == 1;
    }
  }
}