#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeSchema
{

    private unsafe static delegate* unmanaged<nint, ulong, void> _SetStateChanged;

    public unsafe static void SetStateChanged(nint entity, ulong hash)
    {
        _SetStateChanged(entity, hash);
    }

    private unsafe static delegate* unmanaged<byte*, uint> _FindChainOffset;

    public unsafe static uint FindChainOffset(string className)
    {
        return StringAlloc.CreateCString(className, classNameBufferPtr =>
        {
            var ret = _FindChainOffset((byte*)classNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<ulong, int> _GetOffset;

    public unsafe static int GetOffset(ulong hash)
    {
        var ret = _GetOffset(hash);
        return ret;
    }

    private unsafe static delegate* unmanaged<byte*, byte> _IsStruct;

    public unsafe static bool IsStruct(string className)
    {
        return StringAlloc.CreateCString(className, classNameBufferPtr =>
        {
            var ret = _IsStruct((byte*)classNameBufferPtr);
            return ret == 1;
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte> _IsClassLoaded;

    public unsafe static bool IsClassLoaded(string className)
    {
        return StringAlloc.CreateCString(className, classNameBufferPtr =>
        {
            var ret = _IsClassLoaded((byte*)classNameBufferPtr);
            return ret == 1;
        });
    }

    private unsafe static delegate* unmanaged<nint, ulong, nint> _GetPropPtr;

    public unsafe static nint GetPropPtr(nint entity, ulong hash)
    {
        var ret = _GetPropPtr(entity, hash);
        return ret;
    }

    private unsafe static delegate* unmanaged<nint, ulong, nint, uint, void> _WritePropPtr;

    public unsafe static void WritePropPtr(nint entity, ulong hash, nint value, uint size)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        _WritePropPtr(entity, hash, value, size);
    }

    private unsafe static delegate* unmanaged<nint, nint> _GetVData;

    public unsafe static nint GetVData(nint entity)
    {
        var ret = _GetVData(entity);
        return ret;
    }

    private unsafe static delegate* unmanaged<uint, nint> _GetDatamapFunction;

    public unsafe static nint GetDatamapFunction(uint hash)
    {
        var ret = _GetDatamapFunction(hash);
        return ret;
    }

    private unsafe static delegate* unmanaged<int*, nint, byte*, byte*> _GetEntityFields;

    public unsafe static string GetEntityFields(nint entity, string className)
    {
        return StringAlloc.CreateCString(className, classNameBufferPtr =>
        {
            var length = 0;
            var returnedPtr = _GetEntityFields(&length, entity, (byte*)classNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        });
    }
}