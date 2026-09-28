#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeNetMessages
{

    private unsafe static delegate* unmanaged<int, nint> _AllocateNetMessageByID;

    public unsafe static nint AllocateNetMessageByID(int msgid)
    {
        var ret = _AllocateNetMessageByID(msgid);
        return ret;
    }

    private unsafe static delegate* unmanaged<byte*, nint> _AllocateNetMessageByPartialName;

    public unsafe static nint AllocateNetMessageByPartialName(string name)
    {
        using var nameStr = new ScopedCString(name);
        fixed (byte* nameBufferPtr = nameStr)
        {
            var ret = _AllocateNetMessageByPartialName(nameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, void> _DeallocateNetMessage;

    public unsafe static void DeallocateNetMessage(nint netmsg)
    {
        _DeallocateNetMessage(netmsg);
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte> _HasField;

    public unsafe static bool HasField(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _HasField(netmsg, fieldNameBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int> _GetInt32;

    public unsafe static int GetInt32(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetInt32(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, int> _GetRepeatedInt32;

    public unsafe static int GetRepeatedInt32(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedInt32(netmsg, fieldNameBufferPtr, index);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, void> _SetInt32;

    public unsafe static void SetInt32(nint netmsg, string fieldName, int value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetInt32(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, int, void> _SetRepeatedInt32;

    public unsafe static void SetRepeatedInt32(nint netmsg, string fieldName, int index, int value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetRepeatedInt32(netmsg, fieldNameBufferPtr, index, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, void> _AddInt32;

    public unsafe static void AddInt32(nint netmsg, string fieldName, int value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _AddInt32(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, long> _GetInt64;

    public unsafe static long GetInt64(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetInt64(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, long> _GetRepeatedInt64;

    public unsafe static long GetRepeatedInt64(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedInt64(netmsg, fieldNameBufferPtr, index);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, long, void> _SetInt64;

    public unsafe static void SetInt64(nint netmsg, string fieldName, long value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetInt64(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, long, void> _SetRepeatedInt64;

    public unsafe static void SetRepeatedInt64(nint netmsg, string fieldName, int index, long value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetRepeatedInt64(netmsg, fieldNameBufferPtr, index, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, long, void> _AddInt64;

    public unsafe static void AddInt64(nint netmsg, string fieldName, long value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _AddInt64(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, uint> _GetUInt32;

    public unsafe static uint GetUInt32(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetUInt32(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, uint> _GetRepeatedUInt32;

    public unsafe static uint GetRepeatedUInt32(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedUInt32(netmsg, fieldNameBufferPtr, index);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, uint, void> _SetUInt32;

    public unsafe static void SetUInt32(nint netmsg, string fieldName, uint value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetUInt32(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, uint, void> _SetRepeatedUInt32;

    public unsafe static void SetRepeatedUInt32(nint netmsg, string fieldName, int index, uint value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetRepeatedUInt32(netmsg, fieldNameBufferPtr, index, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, uint, void> _AddUInt32;

    public unsafe static void AddUInt32(nint netmsg, string fieldName, uint value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _AddUInt32(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, ulong> _GetUInt64;

    public unsafe static ulong GetUInt64(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetUInt64(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, ulong> _GetRepeatedUInt64;

    public unsafe static ulong GetRepeatedUInt64(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedUInt64(netmsg, fieldNameBufferPtr, index);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, ulong, void> _SetUInt64;

    public unsafe static void SetUInt64(nint netmsg, string fieldName, ulong value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetUInt64(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, ulong, void> _SetRepeatedUInt64;

    public unsafe static void SetRepeatedUInt64(nint netmsg, string fieldName, int index, ulong value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetRepeatedUInt64(netmsg, fieldNameBufferPtr, index, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, ulong, void> _AddUInt64;

    public unsafe static void AddUInt64(nint netmsg, string fieldName, ulong value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _AddUInt64(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte> _GetBool;

    public unsafe static bool GetBool(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetBool(netmsg, fieldNameBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, byte> _GetRepeatedBool;

    public unsafe static bool GetRepeatedBool(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedBool(netmsg, fieldNameBufferPtr, index);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte, void> _SetBool;

    public unsafe static void SetBool(nint netmsg, string fieldName, bool value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetBool(netmsg, fieldNameBufferPtr, value ? (byte)1 : (byte)0);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, byte, void> _SetRepeatedBool;

    public unsafe static void SetRepeatedBool(nint netmsg, string fieldName, int index, bool value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetRepeatedBool(netmsg, fieldNameBufferPtr, index, value ? (byte)1 : (byte)0);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte, void> _AddBool;

    public unsafe static void AddBool(nint netmsg, string fieldName, bool value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _AddBool(netmsg, fieldNameBufferPtr, value ? (byte)1 : (byte)0);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, float> _GetFloat;

    public unsafe static float GetFloat(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetFloat(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, float> _GetRepeatedFloat;

    public unsafe static float GetRepeatedFloat(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedFloat(netmsg, fieldNameBufferPtr, index);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, float, void> _SetFloat;

    public unsafe static void SetFloat(nint netmsg, string fieldName, float value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetFloat(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, float, void> _SetRepeatedFloat;

    public unsafe static void SetRepeatedFloat(nint netmsg, string fieldName, int index, float value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetRepeatedFloat(netmsg, fieldNameBufferPtr, index, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, float, void> _AddFloat;

    public unsafe static void AddFloat(nint netmsg, string fieldName, float value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _AddFloat(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, double> _GetDouble;

    public unsafe static double GetDouble(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetDouble(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, double> _GetRepeatedDouble;

    public unsafe static double GetRepeatedDouble(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedDouble(netmsg, fieldNameBufferPtr, index);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, double, void> _SetDouble;

    public unsafe static void SetDouble(nint netmsg, string fieldName, double value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetDouble(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, double, void> _SetRepeatedDouble;

    public unsafe static void SetRepeatedDouble(nint netmsg, string fieldName, int index, double value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetRepeatedDouble(netmsg, fieldNameBufferPtr, index, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, double, void> _AddDouble;

    public unsafe static void AddDouble(nint netmsg, string fieldName, double value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _AddDouble(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<int*, nint, byte*, byte*> _GetString;

    public unsafe static string GetString(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var length = 0;
            var returnedPtr = _GetString(&length, netmsg, fieldNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        }
    }

    private unsafe static delegate* unmanaged<int*, nint, byte*, int, byte*> _GetRepeatedString;

    public unsafe static string GetRepeatedString(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var length = 0;
            var returnedPtr = _GetRepeatedString(&length, netmsg, fieldNameBufferPtr, index);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte*, void> _SetString;

    public unsafe static void SetString(nint netmsg, string fieldName, string value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        using var valueStr = new ScopedCString(value);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                _SetString(netmsg, fieldNameBufferPtr, valueBufferPtr);
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, byte*, void> _SetRepeatedString;

    public unsafe static void SetRepeatedString(nint netmsg, string fieldName, int index, string value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        using var valueStr = new ScopedCString(value);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                _SetRepeatedString(netmsg, fieldNameBufferPtr, index, valueBufferPtr);
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte*, void> _AddString;

    public unsafe static void AddString(nint netmsg, string fieldName, string value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        using var valueStr = new ScopedCString(value);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                _AddString(netmsg, fieldNameBufferPtr, valueBufferPtr);
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector2D> _GetVector2D;

    public unsafe static Vector2D GetVector2D(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetVector2D(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, Vector2D> _GetRepeatedVector2D;

    public unsafe static Vector2D GetRepeatedVector2D(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedVector2D(netmsg, fieldNameBufferPtr, index);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector2D, void> _SetVector2D;

    public unsafe static void SetVector2D(nint netmsg, string fieldName, Vector2D value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetVector2D(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, Vector2D, void> _SetRepeatedVector2D;

    public unsafe static void SetRepeatedVector2D(nint netmsg, string fieldName, int index, Vector2D value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetRepeatedVector2D(netmsg, fieldNameBufferPtr, index, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector2D, void> _AddVector2D;

    public unsafe static void AddVector2D(nint netmsg, string fieldName, Vector2D value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _AddVector2D(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector> _GetVector;

    public unsafe static Vector GetVector(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetVector(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, Vector> _GetRepeatedVector;

    public unsafe static Vector GetRepeatedVector(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedVector(netmsg, fieldNameBufferPtr, index);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector, void> _SetVector;

    public unsafe static void SetVector(nint netmsg, string fieldName, Vector value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetVector(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, Vector, void> _SetRepeatedVector;

    public unsafe static void SetRepeatedVector(nint netmsg, string fieldName, int index, Vector value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetRepeatedVector(netmsg, fieldNameBufferPtr, index, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector, void> _AddVector;

    public unsafe static void AddVector(nint netmsg, string fieldName, Vector value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _AddVector(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Color> _GetColor;

    public unsafe static Color GetColor(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetColor(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, Color> _GetRepeatedColor;

    public unsafe static Color GetRepeatedColor(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedColor(netmsg, fieldNameBufferPtr, index);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Color, void> _SetColor;

    public unsafe static void SetColor(nint netmsg, string fieldName, Color value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetColor(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, Color, void> _SetRepeatedColor;

    public unsafe static void SetRepeatedColor(nint netmsg, string fieldName, int index, Color value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetRepeatedColor(netmsg, fieldNameBufferPtr, index, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, Color, void> _AddColor;

    public unsafe static void AddColor(nint netmsg, string fieldName, Color value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _AddColor(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, QAngle> _GetQAngle;

    public unsafe static QAngle GetQAngle(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetQAngle(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, QAngle> _GetRepeatedQAngle;

    public unsafe static QAngle GetRepeatedQAngle(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedQAngle(netmsg, fieldNameBufferPtr, index);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, QAngle, void> _SetQAngle;

    public unsafe static void SetQAngle(nint netmsg, string fieldName, QAngle value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetQAngle(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, QAngle, void> _SetRepeatedQAngle;

    public unsafe static void SetRepeatedQAngle(nint netmsg, string fieldName, int index, QAngle value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _SetRepeatedQAngle(netmsg, fieldNameBufferPtr, index, value);
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, QAngle, void> _AddQAngle;

    public unsafe static void AddQAngle(nint netmsg, string fieldName, QAngle value)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _AddQAngle(netmsg, fieldNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint, byte*, int> _GetBytes;

    public unsafe static byte[] GetBytes(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetBytes(null, netmsg, fieldNameBufferPtr);
            var pool = ArrayPool<byte>.Shared;
            var retBuffer = pool.Rent(ret + 1);
            fixed (byte* retBufferPtr = retBuffer)
            {
                ret = _GetBytes(retBufferPtr, netmsg, fieldNameBufferPtr);
                var retBytes = new byte[ret];
                for (int i = 0; i < ret; i++) retBytes[i] = retBufferPtr[i];
                pool.Return(retBuffer);
                return retBytes;
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint, byte*, int, int> _GetRepeatedBytes;

    public unsafe static byte[] GetRepeatedBytes(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedBytes(null, netmsg, fieldNameBufferPtr, index);
            var pool = ArrayPool<byte>.Shared;
            var retBuffer = pool.Rent(ret + 1);
            fixed (byte* retBufferPtr = retBuffer)
            {
                ret = _GetRepeatedBytes(retBufferPtr, netmsg, fieldNameBufferPtr, index);
                var retBytes = new byte[ret];
                for (int i = 0; i < ret; i++) retBytes[i] = retBufferPtr[i];
                pool.Return(retBuffer);
                return retBytes;
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte*, int, void> _SetBytes;

    public unsafe static void SetBytes(nint netmsg, string fieldName, byte[] value)
    {
        var valueLength = value.Length;
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            fixed (byte* valueBufferPtr = value)
            {
                _SetBytes(netmsg, fieldNameBufferPtr, valueBufferPtr, valueLength);
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, byte*, int, void> _SetRepeatedBytes;

    public unsafe static void SetRepeatedBytes(nint netmsg, string fieldName, int index, byte[] value)
    {
        var valueLength = value.Length;
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            fixed (byte* valueBufferPtr = value)
            {
                _SetRepeatedBytes(netmsg, fieldNameBufferPtr, index, valueBufferPtr, valueLength);
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte*, int, void> _AddBytes;

    public unsafe static void AddBytes(nint netmsg, string fieldName, byte[] value)
    {
        var valueLength = value.Length;
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            fixed (byte* valueBufferPtr = value)
            {
                _AddBytes(netmsg, fieldNameBufferPtr, valueBufferPtr, valueLength);
            }
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint> _GetNestedMessage;

    public unsafe static nint GetNestedMessage(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetNestedMessage(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, nint> _GetRepeatedNestedMessage;

    public unsafe static nint GetRepeatedNestedMessage(nint netmsg, string fieldName, int index)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedNestedMessage(netmsg, fieldNameBufferPtr, index);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint> _AddNestedMessage;

    public unsafe static nint AddNestedMessage(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _AddNestedMessage(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, int> _GetRepeatedFieldSize;

    public unsafe static int GetRepeatedFieldSize(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            var ret = _GetRepeatedFieldSize(netmsg, fieldNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<nint, byte*, void> _ClearRepeatedField;

    public unsafe static void ClearRepeatedField(nint netmsg, string fieldName)
    {
        using var fieldNameStr = new ScopedCString(fieldName);
        fixed (byte* fieldNameBufferPtr = fieldNameStr)
        {
            _ClearRepeatedField(netmsg, fieldNameBufferPtr);
        }
    }

    private unsafe static delegate* unmanaged<nint, void> _Clear;

    public unsafe static void Clear(nint netmsg)
    {
        _Clear(netmsg);
    }

    private unsafe static delegate* unmanaged<nint, int, int, void> _SendMessage;

    public unsafe static void SendMessage(nint netmsg, int msgid, int playerid)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        _SendMessage(netmsg, msgid, playerid);
    }

    private unsafe static delegate* unmanaged<nint, int, ulong, void> _SendMessageToPlayers;

    /// <summary>
    /// each bit in player_mask represents a playerid
    /// </summary>
    public unsafe static void SendMessageToPlayers(nint netmsg, int msgid, ulong playermask)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        _SendMessageToPlayers(netmsg, msgid, playermask);
    }

    private unsafe static delegate* unmanaged<nint, void> _SetNetMessageServerHook;

    /// <summary>
    /// the callback should receive the following: uint64* playermask_ptr, int netmessage_id, void* netmsg, return HookResult int
    /// </summary>
    public unsafe static void SetNetMessageServerHook(nint callback)
    {
        _SetNetMessageServerHook(callback);
    }

    private unsafe static delegate* unmanaged<nint, void> _SetNetMessageClientHook;

    /// <summary>
    /// the callback should receive the following: int32 playerid, int netmessage_id, void* netmsg, return HookResult int
    /// </summary>
    public unsafe static void SetNetMessageClientHook(nint callback)
    {
        _SetNetMessageClientHook(callback);
    }

    private unsafe static delegate* unmanaged<nint, void> _SetNetMessageServerHookInternal;

    /// <summary>
    /// callback should receive the following: int32 playerid, int netmessage_id, void* netmsg, return HookResult int
    /// </summary>
    public unsafe static void SetNetMessageServerHookInternal(nint callback)
    {
        _SetNetMessageServerHookInternal(callback);
    }
}