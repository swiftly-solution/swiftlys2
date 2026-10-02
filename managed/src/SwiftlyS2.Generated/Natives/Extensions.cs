#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeExtensions
{

    private unsafe static delegate* unmanaged<int*, byte*> _GetExtensions;

    /// <summary>
    /// returns a JSON array with the loaded extensions
    /// </summary>
    public unsafe static string GetExtensions()
    {
        var length = 0;
        var returnedPtr = _GetExtensions(&length);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<byte*, byte> _Load;

    /// <summary>
    /// loads the extension located at extensions/{id}/{id}.(dll|so)
    /// </summary>
    public unsafe static bool Load(string id)
    {
        using var idStr = new ScopedCString(id);
        fixed (byte* idBufferPtr = idStr)
        {
            var ret = _Load(idBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte> _Unload;

    /// <summary>
    /// unloads the extension
    /// </summary>
    public unsafe static bool Unload(string id)
    {
        using var idStr = new ScopedCString(id);
        fixed (byte* idBufferPtr = idStr)
        {
            var ret = _Unload(idBufferPtr);
            return ret == 1;
        }
    }
}