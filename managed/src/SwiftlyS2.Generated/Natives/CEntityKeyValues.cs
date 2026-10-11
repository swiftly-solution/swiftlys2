#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeCEntityKeyValues
{

    private unsafe static delegate* unmanaged<nint> _Allocate;

    public unsafe static nint Allocate()
    {
        var ret = _Allocate();
        return ret;
    }

    private unsafe static delegate* unmanaged<nint, void> _Deallocate;

    public unsafe static void Deallocate(nint keyvalues)
    {
        _Deallocate(keyvalues);
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, nint, void> _GetValue;

    public unsafe static void GetValue(nint keyvalues, string key, int kind, nint value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _GetValue(keyvalues, keyBufferPtr, kind, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, nint, void> _SetValue;

    public unsafe static void SetValue(nint keyvalues, string key, int kind, nint value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetValue(keyvalues, keyBufferPtr, kind, value);
        }
    }

    private unsafe static delegate* unmanaged<int*, nint, byte*, byte*> _GetString;

    public unsafe static string GetString(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var length = 0;
            var returnedPtr = _GetString(&length, keyvalues, keyBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte*, void> _SetString;

    public unsafe static void SetString(nint keyvalues, string key, string value)
    {
        using var keyStr = new ScopedCString(key);
        using var valueStr = new ScopedCString(value);
        fixed (byte* keyBufferPtr = keyStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                _SetString(keyvalues, keyBufferPtr, valueBufferPtr);
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte> _HasKey;

    public unsafe static bool HasKey(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _HasKey(keyvalues, keyBufferPtr);
            return ret == 1;
        }
    }
}