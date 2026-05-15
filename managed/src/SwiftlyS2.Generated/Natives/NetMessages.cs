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
        return StringAlloc.CreateCString(name, nameBufferPtr =>
        {
            var ret = _AllocateNetMessageByPartialName((byte*)nameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, void> _DeallocateNetMessage;

    public unsafe static void DeallocateNetMessage(nint netmsg)
    {
        _DeallocateNetMessage(netmsg);
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte> _HasField;

    public unsafe static bool HasField(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _HasField(netmsg, (byte*)fieldNameBufferPtr);
            return ret == 1;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int> _GetInt32;

    public unsafe static int GetInt32(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetInt32(netmsg, (byte*)fieldNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, int> _GetRepeatedInt32;

    public unsafe static int GetRepeatedInt32(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedInt32(netmsg, (byte*)fieldNameBufferPtr, index);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, void> _SetInt32;

    public unsafe static void SetInt32(nint netmsg, string fieldName, int value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetInt32(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, int, void> _SetRepeatedInt32;

    public unsafe static void SetRepeatedInt32(nint netmsg, string fieldName, int index, int value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetRepeatedInt32(netmsg, (byte*)fieldNameBufferPtr, index, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, void> _AddInt32;

    public unsafe static void AddInt32(nint netmsg, string fieldName, int value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _AddInt32(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, long> _GetInt64;

    public unsafe static long GetInt64(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetInt64(netmsg, (byte*)fieldNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, long> _GetRepeatedInt64;

    public unsafe static long GetRepeatedInt64(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedInt64(netmsg, (byte*)fieldNameBufferPtr, index);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, long, void> _SetInt64;

    public unsafe static void SetInt64(nint netmsg, string fieldName, long value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetInt64(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, long, void> _SetRepeatedInt64;

    public unsafe static void SetRepeatedInt64(nint netmsg, string fieldName, int index, long value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetRepeatedInt64(netmsg, (byte*)fieldNameBufferPtr, index, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, long, void> _AddInt64;

    public unsafe static void AddInt64(nint netmsg, string fieldName, long value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _AddInt64(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, uint> _GetUInt32;

    public unsafe static uint GetUInt32(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetUInt32(netmsg, (byte*)fieldNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, uint> _GetRepeatedUInt32;

    public unsafe static uint GetRepeatedUInt32(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedUInt32(netmsg, (byte*)fieldNameBufferPtr, index);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, uint, void> _SetUInt32;

    public unsafe static void SetUInt32(nint netmsg, string fieldName, uint value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetUInt32(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, uint, void> _SetRepeatedUInt32;

    public unsafe static void SetRepeatedUInt32(nint netmsg, string fieldName, int index, uint value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetRepeatedUInt32(netmsg, (byte*)fieldNameBufferPtr, index, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, uint, void> _AddUInt32;

    public unsafe static void AddUInt32(nint netmsg, string fieldName, uint value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _AddUInt32(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, ulong> _GetUInt64;

    public unsafe static ulong GetUInt64(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetUInt64(netmsg, (byte*)fieldNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, ulong> _GetRepeatedUInt64;

    public unsafe static ulong GetRepeatedUInt64(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedUInt64(netmsg, (byte*)fieldNameBufferPtr, index);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, ulong, void> _SetUInt64;

    public unsafe static void SetUInt64(nint netmsg, string fieldName, ulong value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetUInt64(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, ulong, void> _SetRepeatedUInt64;

    public unsafe static void SetRepeatedUInt64(nint netmsg, string fieldName, int index, ulong value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetRepeatedUInt64(netmsg, (byte*)fieldNameBufferPtr, index, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, ulong, void> _AddUInt64;

    public unsafe static void AddUInt64(nint netmsg, string fieldName, ulong value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _AddUInt64(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte> _GetBool;

    public unsafe static bool GetBool(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetBool(netmsg, (byte*)fieldNameBufferPtr);
            return ret == 1;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, byte> _GetRepeatedBool;

    public unsafe static bool GetRepeatedBool(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedBool(netmsg, (byte*)fieldNameBufferPtr, index);
            return ret == 1;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte, void> _SetBool;

    public unsafe static void SetBool(nint netmsg, string fieldName, bool value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetBool(netmsg, (byte*)fieldNameBufferPtr, value ? (byte)1 : (byte)0);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, byte, void> _SetRepeatedBool;

    public unsafe static void SetRepeatedBool(nint netmsg, string fieldName, int index, bool value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetRepeatedBool(netmsg, (byte*)fieldNameBufferPtr, index, value ? (byte)1 : (byte)0);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte, void> _AddBool;

    public unsafe static void AddBool(nint netmsg, string fieldName, bool value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _AddBool(netmsg, (byte*)fieldNameBufferPtr, value ? (byte)1 : (byte)0);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, float> _GetFloat;

    public unsafe static float GetFloat(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetFloat(netmsg, (byte*)fieldNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, float> _GetRepeatedFloat;

    public unsafe static float GetRepeatedFloat(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedFloat(netmsg, (byte*)fieldNameBufferPtr, index);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, float, void> _SetFloat;

    public unsafe static void SetFloat(nint netmsg, string fieldName, float value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetFloat(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, float, void> _SetRepeatedFloat;

    public unsafe static void SetRepeatedFloat(nint netmsg, string fieldName, int index, float value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetRepeatedFloat(netmsg, (byte*)fieldNameBufferPtr, index, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, float, void> _AddFloat;

    public unsafe static void AddFloat(nint netmsg, string fieldName, float value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _AddFloat(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, double> _GetDouble;

    public unsafe static double GetDouble(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetDouble(netmsg, (byte*)fieldNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, double> _GetRepeatedDouble;

    public unsafe static double GetRepeatedDouble(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedDouble(netmsg, (byte*)fieldNameBufferPtr, index);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, double, void> _SetDouble;

    public unsafe static void SetDouble(nint netmsg, string fieldName, double value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetDouble(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, double, void> _SetRepeatedDouble;

    public unsafe static void SetRepeatedDouble(nint netmsg, string fieldName, int index, double value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetRepeatedDouble(netmsg, (byte*)fieldNameBufferPtr, index, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, double, void> _AddDouble;

    public unsafe static void AddDouble(nint netmsg, string fieldName, double value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _AddDouble(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<int*, nint, byte*, byte*> _GetString;

    public unsafe static string GetString(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var length = 0;
            var returnedPtr = _GetString(&length, netmsg, (byte*)fieldNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        });
    }

    private unsafe static delegate* unmanaged<int*, nint, byte*, int, byte*> _GetRepeatedString;

    public unsafe static string GetRepeatedString(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var length = 0;
            var returnedPtr = _GetRepeatedString(&length, netmsg, (byte*)fieldNameBufferPtr, index);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte*, void> _SetString;

    public unsafe static void SetString(nint netmsg, string fieldName, string value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            StringAlloc.CreateCString(value, valueBufferPtr =>
            {
                _SetString(netmsg, (byte*)fieldNameBufferPtr, (byte*)valueBufferPtr);
            });
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, byte*, void> _SetRepeatedString;

    public unsafe static void SetRepeatedString(nint netmsg, string fieldName, int index, string value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            StringAlloc.CreateCString(value, valueBufferPtr =>
            {
                _SetRepeatedString(netmsg, (byte*)fieldNameBufferPtr, index, (byte*)valueBufferPtr);
            });
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte*, void> _AddString;

    public unsafe static void AddString(nint netmsg, string fieldName, string value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            StringAlloc.CreateCString(value, valueBufferPtr =>
            {
                _AddString(netmsg, (byte*)fieldNameBufferPtr, (byte*)valueBufferPtr);
            });
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector2D> _GetVector2D;

    public unsafe static Vector2D GetVector2D(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetVector2D(netmsg, (byte*)fieldNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, Vector2D> _GetRepeatedVector2D;

    public unsafe static Vector2D GetRepeatedVector2D(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedVector2D(netmsg, (byte*)fieldNameBufferPtr, index);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector2D, void> _SetVector2D;

    public unsafe static void SetVector2D(nint netmsg, string fieldName, Vector2D value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetVector2D(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, Vector2D, void> _SetRepeatedVector2D;

    public unsafe static void SetRepeatedVector2D(nint netmsg, string fieldName, int index, Vector2D value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetRepeatedVector2D(netmsg, (byte*)fieldNameBufferPtr, index, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector2D, void> _AddVector2D;

    public unsafe static void AddVector2D(nint netmsg, string fieldName, Vector2D value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _AddVector2D(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector> _GetVector;

    public unsafe static Vector GetVector(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetVector(netmsg, (byte*)fieldNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, Vector> _GetRepeatedVector;

    public unsafe static Vector GetRepeatedVector(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedVector(netmsg, (byte*)fieldNameBufferPtr, index);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector, void> _SetVector;

    public unsafe static void SetVector(nint netmsg, string fieldName, Vector value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetVector(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, Vector, void> _SetRepeatedVector;

    public unsafe static void SetRepeatedVector(nint netmsg, string fieldName, int index, Vector value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetRepeatedVector(netmsg, (byte*)fieldNameBufferPtr, index, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, Vector, void> _AddVector;

    public unsafe static void AddVector(nint netmsg, string fieldName, Vector value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _AddVector(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, Color> _GetColor;

    public unsafe static Color GetColor(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetColor(netmsg, (byte*)fieldNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, Color> _GetRepeatedColor;

    public unsafe static Color GetRepeatedColor(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedColor(netmsg, (byte*)fieldNameBufferPtr, index);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, Color, void> _SetColor;

    public unsafe static void SetColor(nint netmsg, string fieldName, Color value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetColor(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, Color, void> _SetRepeatedColor;

    public unsafe static void SetRepeatedColor(nint netmsg, string fieldName, int index, Color value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetRepeatedColor(netmsg, (byte*)fieldNameBufferPtr, index, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, Color, void> _AddColor;

    public unsafe static void AddColor(nint netmsg, string fieldName, Color value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _AddColor(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, QAngle> _GetQAngle;

    public unsafe static QAngle GetQAngle(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetQAngle(netmsg, (byte*)fieldNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, QAngle> _GetRepeatedQAngle;

    public unsafe static QAngle GetRepeatedQAngle(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedQAngle(netmsg, (byte*)fieldNameBufferPtr, index);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, QAngle, void> _SetQAngle;

    public unsafe static void SetQAngle(nint netmsg, string fieldName, QAngle value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetQAngle(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, QAngle, void> _SetRepeatedQAngle;

    public unsafe static void SetRepeatedQAngle(nint netmsg, string fieldName, int index, QAngle value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _SetRepeatedQAngle(netmsg, (byte*)fieldNameBufferPtr, index, value);
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, QAngle, void> _AddQAngle;

    public unsafe static void AddQAngle(nint netmsg, string fieldName, QAngle value)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _AddQAngle(netmsg, (byte*)fieldNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<byte*, nint, byte*, int> _GetBytes;

    public unsafe static byte[] GetBytes(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetBytes(null, netmsg, (byte*)fieldNameBufferPtr);
            var pool = ArrayPool<byte>.Shared;
            var retBuffer = pool.Rent(ret + 1);
            fixed (byte* retBufferPtr = retBuffer)
            {
                ret = _GetBytes(retBufferPtr, netmsg, (byte*)fieldNameBufferPtr);
                var retBytes = new byte[ret];
                for (int i = 0; i < ret; i++) retBytes[i] = retBufferPtr[i];
                pool.Return(retBuffer);
                return retBytes;
            }
        });
    }

    private unsafe static delegate* unmanaged<byte*, nint, byte*, int, int> _GetRepeatedBytes;

    public unsafe static byte[] GetRepeatedBytes(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedBytes(null, netmsg, (byte*)fieldNameBufferPtr, index);
            var pool = ArrayPool<byte>.Shared;
            var retBuffer = pool.Rent(ret + 1);
            fixed (byte* retBufferPtr = retBuffer)
            {
                ret = _GetRepeatedBytes(retBufferPtr, netmsg, (byte*)fieldNameBufferPtr, index);
                var retBytes = new byte[ret];
                for (int i = 0; i < ret; i++) retBytes[i] = retBufferPtr[i];
                pool.Return(retBuffer);
                return retBytes;
            }
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte*, int, void> _SetBytes;

    public unsafe static void SetBytes(nint netmsg, string fieldName, byte[] value)
    {
        var valueLength = value.Length;
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            fixed (byte* valueBufferPtr = value)
            {
                _SetBytes(netmsg, (byte*)fieldNameBufferPtr, valueBufferPtr, valueLength);
            }
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, byte*, int, void> _SetRepeatedBytes;

    public unsafe static void SetRepeatedBytes(nint netmsg, string fieldName, int index, byte[] value)
    {
        var valueLength = value.Length;
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            fixed (byte* valueBufferPtr = value)
            {
                _SetRepeatedBytes(netmsg, (byte*)fieldNameBufferPtr, index, valueBufferPtr, valueLength);
            }
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, byte*, int, void> _AddBytes;

    public unsafe static void AddBytes(nint netmsg, string fieldName, byte[] value)
    {
        var valueLength = value.Length;
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            fixed (byte* valueBufferPtr = value)
            {
                _AddBytes(netmsg, (byte*)fieldNameBufferPtr, valueBufferPtr, valueLength);
            }
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint> _GetNestedMessage;

    public unsafe static nint GetNestedMessage(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetNestedMessage(netmsg, (byte*)fieldNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int, nint> _GetRepeatedNestedMessage;

    public unsafe static nint GetRepeatedNestedMessage(nint netmsg, string fieldName, int index)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedNestedMessage(netmsg, (byte*)fieldNameBufferPtr, index);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, nint> _AddNestedMessage;

    public unsafe static nint AddNestedMessage(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _AddNestedMessage(netmsg, (byte*)fieldNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, int> _GetRepeatedFieldSize;

    public unsafe static int GetRepeatedFieldSize(nint netmsg, string fieldName)
    {
        return StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            var ret = _GetRepeatedFieldSize(netmsg, (byte*)fieldNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<nint, byte*, void> _ClearRepeatedField;

    public unsafe static void ClearRepeatedField(nint netmsg, string fieldName)
    {
        StringAlloc.CreateCString(fieldName, fieldNameBufferPtr =>
        {
            _ClearRepeatedField(netmsg, (byte*)fieldNameBufferPtr);
        });
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

    private unsafe static delegate* unmanaged<nint, ulong> _AddNetMessageServerHook;

    /// <summary>
    /// the callback should receive the following: uint64* playermask_ptr, int netmessage_id, void* netmsg, return bool (true -> ignored, false -> supercede)
    /// </summary>
    public unsafe static ulong AddNetMessageServerHook(nint callback)
    {
        var ret = _AddNetMessageServerHook(callback);
        return ret;
    }

    private unsafe static delegate* unmanaged<ulong, void> _RemoveNetMessageServerHook;

    public unsafe static void RemoveNetMessageServerHook(ulong callbackID)
    {
        _RemoveNetMessageServerHook(callbackID);
    }

    private unsafe static delegate* unmanaged<nint, ulong> _AddNetMessageClientHook;

    /// <summary>
    /// the callback should receive the following: int32 playerid, int netmessage_id, void* netmsg, return bool (true -> ignored, false -> supercede)
    /// </summary>
    public unsafe static ulong AddNetMessageClientHook(nint callback)
    {
        var ret = _AddNetMessageClientHook(callback);
        return ret;
    }

    private unsafe static delegate* unmanaged<ulong, void> _RemoveNetMessageClientHook;

    public unsafe static void RemoveNetMessageClientHook(ulong callbackID)
    {
        _RemoveNetMessageClientHook(callbackID);
    }

    private unsafe static delegate* unmanaged<nint, ulong> _AddNetMessageServerHookInternal;

    /// <summary>
    /// callback should receive the following: int32 playerid, int netmessage_id, void* netmsg, return bool (true -> ignored, false -> supercede)
    /// </summary>
    public unsafe static ulong AddNetMessageServerHookInternal(nint callback)
    {
        var ret = _AddNetMessageServerHookInternal(callback);
        return ret;
    }

    private unsafe static delegate* unmanaged<ulong, void> _RemoveNetMessageServerHookInternal;

    public unsafe static void RemoveNetMessageServerHookInternal(ulong callbackID)
    {
        _RemoveNetMessageServerHookInternal(callbackID);
    }

    private unsafe static delegate* unmanaged<int*, nint, byte*> _DebugString;

    public unsafe static string DebugString(nint protoMsgPtr)
    {
        var length = 0;
        var returnedPtr = _DebugString(&length, protoMsgPtr);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<nint, nint> _ParseUserCmdFromMove;

    public unsafe static nint ParseUserCmdFromMove(nint moveMsgPtr)
    {
        var ret = _ParseUserCmdFromMove(moveMsgPtr);
        return ret;
    }

    private unsafe static delegate* unmanaged<nint, void> _FreeUserCmd;

    public unsafe static void FreeUserCmd(nint cmdPtr)
    {
        _FreeUserCmd(cmdPtr);
    }

    private unsafe static delegate* unmanaged<int*, nint, byte*> _FormatMoveDebugString;

    public unsafe static string FormatMoveDebugString(nint moveMsgPtr)
    {
        var length = 0;
        var returnedPtr = _FormatMoveDebugString(&length, moveMsgPtr);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<int*, int, byte*> _GetMessageNameById;

    public unsafe static string GetMessageNameById(int msgId)
    {
        var length = 0;
        var returnedPtr = _GetMessageNameById(&length, msgId);
        var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
        NativeAllocator.Free((nint)returnedPtr);
        return outString;
    }

    private unsafe static delegate* unmanaged<int> _GetCNetMessageSize;

    internal unsafe static int GetCNetMessageSize()
    {
        return _GetCNetMessageSize();
    }
}