#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeMemoryHelpers
{

    private unsafe static delegate* unmanaged<byte*, nint> _FetchInterfaceByName;

    /// <summary>
    /// supports both internal interface system, but also valve interface system
    /// </summary>
    public unsafe static nint FetchInterfaceByName(string ifaceName)
    {
        using var ifaceNameStr = new ScopedCString(ifaceName);
        fixed (byte* ifaceNameBufferPtr = ifaceNameStr)
        {
            var ret = _FetchInterfaceByName(ifaceNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, nint> _GetVirtualTableAddress;

    public unsafe static nint GetVirtualTableAddress(string library, string vtableName)
    {
        using var libraryStr = new ScopedCString(library);
        using var vtableNameStr = new ScopedCString(vtableName);
        fixed (byte* libraryBufferPtr = libraryStr)
        {
            fixed (byte* vtableNameBufferPtr = vtableNameStr)
            {
                var ret = _GetVirtualTableAddress(libraryBufferPtr, vtableNameBufferPtr);
                return ret;
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte*, nint> _GetVirtualTableAddressNested2;

    public unsafe static nint GetVirtualTableAddressNested2(string library, string class1, string class2)
    {
        using var libraryStr = new ScopedCString(library);
        using var class1Str = new ScopedCString(class1);
        using var class2Str = new ScopedCString(class2);
        fixed (byte* libraryBufferPtr = libraryStr)
        {
            fixed (byte* class1BufferPtr = class1Str)
            {
                fixed (byte* class2BufferPtr = class2Str)
                {
                    var ret = _GetVirtualTableAddressNested2(libraryBufferPtr, class1BufferPtr, class2BufferPtr);
                    return ret;
                }
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, int, byte, nint> _GetAddressBySignature;

    public unsafe static nint GetAddressBySignature(string library, string sig, int len, bool rawBytes)
    {
        using var libraryStr = new ScopedCString(library);
        using var sigStr = new ScopedCString(sig);
        fixed (byte* libraryBufferPtr = libraryStr)
        {
            fixed (byte* sigBufferPtr = sigStr)
            {
                var ret = _GetAddressBySignature(libraryBufferPtr, sigBufferPtr, len, rawBytes ? (byte)1 : (byte)0);
                return ret;
            }
        }
    }

    private unsafe static delegate* unmanaged<int*, nint, byte*> _GetObjectPtrVtableName;

    public unsafe static string GetObjectPtrVtableName(nint objptr)
    {
        var length = 0;
        var returnedPtr = _GetObjectPtrVtableName(&length, objptr);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<nint, byte> _ObjectPtrHasVtable;

    public unsafe static bool ObjectPtrHasVtable(nint objptr)
    {
        var ret = _ObjectPtrHasVtable(objptr);
        return ret == 1;
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte> _ObjectPtrHasBaseClass;

    public unsafe static bool ObjectPtrHasBaseClass(nint objptr, string baseClassName)
    {
        using var baseClassNameStr = new ScopedCString(baseClassName);
        fixed (byte* baseClassNameBufferPtr = baseClassNameStr)
        {
            var ret = _ObjectPtrHasBaseClass(objptr, baseClassNameBufferPtr);
            return ret == 1;
        }
    }
}