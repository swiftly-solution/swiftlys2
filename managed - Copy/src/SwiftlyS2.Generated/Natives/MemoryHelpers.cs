#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeMemoryHelpers {
  private static int _MainThreadID;

  private unsafe static delegate* unmanaged<byte*, nint> _FetchInterfaceByName;

  /// <summary>
  /// supports both internal interface system, but also valve interface system
  /// </summary>
  public unsafe static nint FetchInterfaceByName(string ifaceName) {
    var pool = ArrayPool<byte>.Shared;
    var ifaceNameLength = Encoding.UTF8.GetByteCount(ifaceName);
    var ifaceNameBuffer = pool.Rent(ifaceNameLength + 1);
    Encoding.UTF8.GetBytes(ifaceName, ifaceNameBuffer);
    ifaceNameBuffer[ifaceNameLength] = 0;
    fixed (byte* ifaceNameBufferPtr = ifaceNameBuffer) {
      var ret = _FetchInterfaceByName(ifaceNameBufferPtr);
      pool.Return(ifaceNameBuffer);
      return ret;
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte*, nint> _GetVirtualTableAddress;

  public unsafe static nint GetVirtualTableAddress(string library, string vtableName) {
    var pool = ArrayPool<byte>.Shared;
    var libraryLength = Encoding.UTF8.GetByteCount(library);
    var libraryBuffer = pool.Rent(libraryLength + 1);
    Encoding.UTF8.GetBytes(library, libraryBuffer);
    libraryBuffer[libraryLength] = 0;
    var vtableNameLength = Encoding.UTF8.GetByteCount(vtableName);
    var vtableNameBuffer = pool.Rent(vtableNameLength + 1);
    Encoding.UTF8.GetBytes(vtableName, vtableNameBuffer);
    vtableNameBuffer[vtableNameLength] = 0;
    fixed (byte* libraryBufferPtr = libraryBuffer) {
      fixed (byte* vtableNameBufferPtr = vtableNameBuffer) {
        var ret = _GetVirtualTableAddress(libraryBufferPtr, vtableNameBufferPtr);
        pool.Return(libraryBuffer);
        pool.Return(vtableNameBuffer);
        return ret;
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, byte*, int, byte, nint> _GetAddressBySignature;

  public unsafe static nint GetAddressBySignature(string library, string sig, int len, bool rawBytes) {
    var pool = ArrayPool<byte>.Shared;
    var libraryLength = Encoding.UTF8.GetByteCount(library);
    var libraryBuffer = pool.Rent(libraryLength + 1);
    Encoding.UTF8.GetBytes(library, libraryBuffer);
    libraryBuffer[libraryLength] = 0;
    var sigLength = Encoding.UTF8.GetByteCount(sig);
    var sigBuffer = pool.Rent(sigLength + 1);
    Encoding.UTF8.GetBytes(sig, sigBuffer);
    sigBuffer[sigLength] = 0;
    fixed (byte* libraryBufferPtr = libraryBuffer) {
      fixed (byte* sigBufferPtr = sigBuffer) {
        var ret = _GetAddressBySignature(libraryBufferPtr, sigBufferPtr, len, rawBytes ? (byte)1 : (byte)0);
        pool.Return(libraryBuffer);
        pool.Return(sigBuffer);
        return ret;
      }
    }
  }

  private unsafe static delegate* unmanaged<byte*, nint, int> _GetObjectPtrVtableName;

  public unsafe static string GetObjectPtrVtableName(nint objptr) {
    var ret = _GetObjectPtrVtableName(null, objptr);
    var pool = ArrayPool<byte>.Shared;
    var retBuffer = pool.Rent(ret + 1);
    fixed (byte* retBufferPtr = retBuffer) {
      ret = _GetObjectPtrVtableName(retBufferPtr, objptr);
      var retString = Encoding.UTF8.GetString(retBufferPtr, ret);
      pool.Return(retBuffer);
      return retString;
    }
  }

  private unsafe static delegate* unmanaged<nint, byte> _ObjectPtrHasVtable;

  public unsafe static bool ObjectPtrHasVtable(nint objptr) {
    var ret = _ObjectPtrHasVtable(objptr);
    return ret == 1;
  }

  private unsafe static delegate* unmanaged<nint, byte*, byte> _ObjectPtrHasBaseClass;

  public unsafe static bool ObjectPtrHasBaseClass(nint objptr, string baseClassName) {
    var pool = ArrayPool<byte>.Shared;
    var baseClassNameLength = Encoding.UTF8.GetByteCount(baseClassName);
    var baseClassNameBuffer = pool.Rent(baseClassNameLength + 1);
    Encoding.UTF8.GetBytes(baseClassName, baseClassNameBuffer);
    baseClassNameBuffer[baseClassNameLength] = 0;
    fixed (byte* baseClassNameBufferPtr = baseClassNameBuffer) {
      var ret = _ObjectPtrHasBaseClass(objptr, baseClassNameBufferPtr);
      pool.Return(baseClassNameBuffer);
      return ret == 1;
    }
  }
}