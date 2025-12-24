#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeDatabase {

  private unsafe static delegate* unmanaged<byte*, int> _GetDefaultDriver;

  public unsafe static string GetDefaultDriver() {
    var ret = _GetDefaultDriver(null);
    var pool = ArrayPool<byte>.Shared;
    var retBuffer = pool.Rent(ret + 1);
    fixed (byte* retBufferPtr = retBuffer) {
      ret = _GetDefaultDriver(retBufferPtr);
      var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
      pool.Return(retBuffer);
      return retString;
    }
  }

  private unsafe static delegate* unmanaged<byte*, int> _GetDefaultConnectionName;

  public unsafe static string GetDefaultConnectionName() {
    var ret = _GetDefaultConnectionName(null);
    var pool = ArrayPool<byte>.Shared;
    var retBuffer = pool.Rent(ret + 1);
    fixed (byte* retBufferPtr = retBuffer) {
      ret = _GetDefaultConnectionName(retBufferPtr);
      var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
      pool.Return(retBuffer);
      return retString;
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte*, int> _GetConnectionDriver;

  public unsafe static string GetConnectionDriver(string connectionName) {
    var pool = ArrayPool<byte>.Shared;
    var connectionNameLength = Encoding.UTF8.GetByteCount(connectionName);
    var connectionNameBuffer = pool.Rent(connectionNameLength + 1);
    Encoding.UTF8.GetBytes(connectionName, connectionNameBuffer);
    connectionNameBuffer[connectionNameLength] = 0;
    fixed (byte* connectionNameBufferPtr = connectionNameBuffer) {
      var ret = _GetConnectionDriver(null, connectionNameBufferPtr);
      var retBuffer = pool.Rent(ret + 1);
      fixed (byte* retBufferPtr = retBuffer) {
        ret = _GetConnectionDriver(retBufferPtr, connectionNameBufferPtr);
        var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
        pool.Return(retBuffer);
        pool.Return(connectionNameBuffer);
        return retString;
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte*, int> _GetConnectionHost;

  public unsafe static string GetConnectionHost(string connectionName) {
    var pool = ArrayPool<byte>.Shared;
    var connectionNameLength = Encoding.UTF8.GetByteCount(connectionName);
    var connectionNameBuffer = pool.Rent(connectionNameLength + 1);
    Encoding.UTF8.GetBytes(connectionName, connectionNameBuffer);
    connectionNameBuffer[connectionNameLength] = 0;
    fixed (byte* connectionNameBufferPtr = connectionNameBuffer) {
      var ret = _GetConnectionHost(null, connectionNameBufferPtr);
      var retBuffer = pool.Rent(ret + 1);
      fixed (byte* retBufferPtr = retBuffer) {
        ret = _GetConnectionHost(retBufferPtr, connectionNameBufferPtr);
        var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
        pool.Return(retBuffer);
        pool.Return(connectionNameBuffer);
        return retString;
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte*, int> _GetConnectionDatabase;

  public unsafe static string GetConnectionDatabase(string connectionName) {
    var pool = ArrayPool<byte>.Shared;
    var connectionNameLength = Encoding.UTF8.GetByteCount(connectionName);
    var connectionNameBuffer = pool.Rent(connectionNameLength + 1);
    Encoding.UTF8.GetBytes(connectionName, connectionNameBuffer);
    connectionNameBuffer[connectionNameLength] = 0;
    fixed (byte* connectionNameBufferPtr = connectionNameBuffer) {
      var ret = _GetConnectionDatabase(null, connectionNameBufferPtr);
      var retBuffer = pool.Rent(ret + 1);
      fixed (byte* retBufferPtr = retBuffer) {
        ret = _GetConnectionDatabase(retBufferPtr, connectionNameBufferPtr);
        var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
        pool.Return(retBuffer);
        pool.Return(connectionNameBuffer);
        return retString;
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte*, int> _GetConnectionUser;

  public unsafe static string GetConnectionUser(string connectionName) {
    var pool = ArrayPool<byte>.Shared;
    var connectionNameLength = Encoding.UTF8.GetByteCount(connectionName);
    var connectionNameBuffer = pool.Rent(connectionNameLength + 1);
    Encoding.UTF8.GetBytes(connectionName, connectionNameBuffer);
    connectionNameBuffer[connectionNameLength] = 0;
    fixed (byte* connectionNameBufferPtr = connectionNameBuffer) {
      var ret = _GetConnectionUser(null, connectionNameBufferPtr);
      var retBuffer = pool.Rent(ret + 1);
      fixed (byte* retBufferPtr = retBuffer) {
        ret = _GetConnectionUser(retBufferPtr, connectionNameBufferPtr);
        var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
        pool.Return(retBuffer);
        pool.Return(connectionNameBuffer);
        return retString;
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte*, int> _GetConnectionPass;

  public unsafe static string GetConnectionPass(string connectionName) {
    var pool = ArrayPool<byte>.Shared;
    var connectionNameLength = Encoding.UTF8.GetByteCount(connectionName);
    var connectionNameBuffer = pool.Rent(connectionNameLength + 1);
    Encoding.UTF8.GetBytes(connectionName, connectionNameBuffer);
    connectionNameBuffer[connectionNameLength] = 0;
    fixed (byte* connectionNameBufferPtr = connectionNameBuffer) {
      var ret = _GetConnectionPass(null, connectionNameBufferPtr);
      var retBuffer = pool.Rent(ret + 1);
      fixed (byte* retBufferPtr = retBuffer) {
        ret = _GetConnectionPass(retBufferPtr, connectionNameBufferPtr);
        var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
        pool.Return(retBuffer);
        pool.Return(connectionNameBuffer);
        return retString;
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, uint> _GetConnectionTimeout;

  public unsafe static uint GetConnectionTimeout(string connectionName) {
    var pool = ArrayPool<byte>.Shared;
    var connectionNameLength = Encoding.UTF8.GetByteCount(connectionName);
    var connectionNameBuffer = pool.Rent(connectionNameLength + 1);
    Encoding.UTF8.GetBytes(connectionName, connectionNameBuffer);
    connectionNameBuffer[connectionNameLength] = 0;
    fixed (byte* connectionNameBufferPtr = connectionNameBuffer) {
      var ret = _GetConnectionTimeout(connectionNameBufferPtr);
      pool.Return(connectionNameBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<byte*, ushort> _GetConnectionPort;

  public unsafe static ushort GetConnectionPort(string connectionName) {
    var pool = ArrayPool<byte>.Shared;
    var connectionNameLength = Encoding.UTF8.GetByteCount(connectionName);
    var connectionNameBuffer = pool.Rent(connectionNameLength + 1);
    Encoding.UTF8.GetBytes(connectionName, connectionNameBuffer);
    connectionNameBuffer[connectionNameLength] = 0;
    fixed (byte* connectionNameBufferPtr = connectionNameBuffer) {
      var ret = _GetConnectionPort(connectionNameBufferPtr);
      pool.Return(connectionNameBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte*, int> _GetConnectionRawUri;

  public unsafe static string GetConnectionRawUri(string connectionName) {
    var pool = ArrayPool<byte>.Shared;
    var connectionNameLength = Encoding.UTF8.GetByteCount(connectionName);
    var connectionNameBuffer = pool.Rent(connectionNameLength + 1);
    Encoding.UTF8.GetBytes(connectionName, connectionNameBuffer);
    connectionNameBuffer[connectionNameLength] = 0;
    fixed (byte* connectionNameBufferPtr = connectionNameBuffer) {
      var ret = _GetConnectionRawUri(null, connectionNameBufferPtr);
      var retBuffer = pool.Rent(ret + 1);
      fixed (byte* retBufferPtr = retBuffer) {
        ret = _GetConnectionRawUri(retBufferPtr, connectionNameBufferPtr);
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