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

    private unsafe static delegate* unmanaged<nint, byte*, byte> _GetBool;

    public unsafe static bool GetBool(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetBool(keyvalues, keyBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int> _GetInt;

    public unsafe static int GetInt(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetInt(keyvalues, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, uint> _GetUint;

    public unsafe static uint GetUint(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetUint(keyvalues, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, long> _GetInt64;

    public unsafe static long GetInt64(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetInt64(keyvalues, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, ulong> _GetUint64;

    public unsafe static ulong GetUint64(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetUint64(keyvalues, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, float> _GetFloat;

    public unsafe static float GetFloat(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetFloat(keyvalues, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, double> _GetDouble;

    public unsafe static double GetDouble(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetDouble(keyvalues, keyBufferPtr);
            return ret;
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

    private unsafe static delegate* unmanaged<nint, byte*, nint> _GetPtr;

    public unsafe static nint GetPtr(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetPtr(keyvalues, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, CUtlStringToken> _GetStringToken;

    public unsafe static CUtlStringToken GetStringToken(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetStringToken(keyvalues, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Color> _GetColor;

    public unsafe static Color GetColor(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetColor(keyvalues, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector> _GetVector;

    public unsafe static Vector GetVector(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetVector(keyvalues, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector2D> _GetVector2D;

    public unsafe static Vector2D GetVector2D(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetVector2D(keyvalues, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector4D> _GetVector4D;

    public unsafe static Vector4D GetVector4D(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetVector4D(keyvalues, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, QAngle> _GetQAngle;

    public unsafe static QAngle GetQAngle(nint keyvalues, string key)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            var ret = _GetQAngle(keyvalues, keyBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte, void> _SetBool;

    public unsafe static void SetBool(nint keyvalues, string key, bool value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetBool(keyvalues, keyBufferPtr, value ? (byte)1 : (byte)0);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, void> _SetInt;

    public unsafe static void SetInt(nint keyvalues, string key, int value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetInt(keyvalues, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, uint, void> _SetUint;

    public unsafe static void SetUint(nint keyvalues, string key, uint value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetUint(keyvalues, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, long, void> _SetInt64;

    public unsafe static void SetInt64(nint keyvalues, string key, long value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetInt64(keyvalues, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, ulong, void> _SetUint64;

    public unsafe static void SetUint64(nint keyvalues, string key, ulong value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetUint64(keyvalues, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, float, void> _SetFloat;

    public unsafe static void SetFloat(nint keyvalues, string key, float value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetFloat(keyvalues, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, double, void> _SetDouble;

    public unsafe static void SetDouble(nint keyvalues, string key, double value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetDouble(keyvalues, keyBufferPtr, value);
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

    private unsafe static delegate* unmanaged<nint, byte*, nint, void> _SetPtr;

    public unsafe static void SetPtr(nint keyvalues, string key, nint value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetPtr(keyvalues, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, CUtlStringToken, void> _SetStringToken;

    public unsafe static void SetStringToken(nint keyvalues, string key, CUtlStringToken value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetStringToken(keyvalues, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Color, void> _SetColor;

    public unsafe static void SetColor(nint keyvalues, string key, Color value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetColor(keyvalues, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector, void> _SetVector;

    public unsafe static void SetVector(nint keyvalues, string key, Vector value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetVector(keyvalues, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector2D, void> _SetVector2D;

    public unsafe static void SetVector2D(nint keyvalues, string key, Vector2D value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetVector2D(keyvalues, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector4D, void> _SetVector4D;

    public unsafe static void SetVector4D(nint keyvalues, string key, Vector4D value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetVector4D(keyvalues, keyBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, QAngle, void> _SetQAngle;

    public unsafe static void SetQAngle(nint keyvalues, string key, QAngle value)
    {
        using var keyStr = new ScopedCString(key);
        fixed (byte* keyBufferPtr = keyStr)
        {
            _SetQAngle(keyvalues, keyBufferPtr, value);
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