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

    private unsafe static delegate* unmanaged<ulong, int> _GetOffset;

    public unsafe static int GetOffset(ulong hash)
    {
        var ret = _GetOffset(hash);
        return ret;
    }

    private unsafe static delegate* unmanaged<nint, nint> _GetVData;

    public unsafe static nint GetVData(nint entity)
    {
        var ret = _GetVData(entity);
        return ret;
    }

    private unsafe static delegate* unmanaged<byte*, byte*, nint> _GetDatamapFunction;

    public unsafe static nint GetDatamapFunction(string className, string functionName)
    {
        return StringAlloc.CreateCString(className, classNameBufferPtr =>
        {
            return StringAlloc.CreateCString(functionName, functionNameBufferPtr =>
            {
                var ret = _GetDatamapFunction((byte*)classNameBufferPtr, (byte*)functionNameBufferPtr);
                return ret;
            });
        });
    }
}