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

    private unsafe static delegate* unmanaged<byte*, byte> _LoadFromPath;

    /// <summary>
    /// loads an extension from an absolute library path
    /// </summary>
    public unsafe static bool LoadFromPath(string path)
    {
        using var pathStr = new ScopedCString(path);
        fixed (byte* pathBufferPtr = pathStr)
        {
            var ret = _LoadFromPath(pathBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte> _UnloadFromPath;

    /// <summary>
    /// unloads an extension by its absolute library path
    /// </summary>
    public unsafe static bool UnloadFromPath(string path)
    {
        using var pathStr = new ScopedCString(path);
        fixed (byte* pathBufferPtr = pathStr)
        {
            var ret = _UnloadFromPath(pathBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint> _GetSharedPointer;

    /// <summary>
    /// gets a shared pointer, or zero if the key does not exist
    /// </summary>
    public unsafe static nint GetSharedPointer(string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetSharedPointer(keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint, void> _SetSharedPointer;

    /// <summary>
    /// stores or replaces a shared pointer, which can be zero
    /// </summary>
    public unsafe static void SetSharedPointer(string key, nint pointer)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetSharedPointer(keyBufferPtr, pointer);
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte> _HasSharedPointer;

    /// <summary>
    /// checks whether a shared pointer key exists, including zero values
    /// </summary>
    public unsafe static bool HasSharedPointer(string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _HasSharedPointer(keyBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<byte*, void> _RemoveSharedPointer;

    /// <summary>
    /// removes a shared pointer key without freeing its value
    /// </summary>
    public unsafe static void RemoveSharedPointer(string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _RemoveSharedPointer(keyBufferPtr);
        }
    }
}