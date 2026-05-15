#pragma warning disable CS0649
#pragma warning disable CS0169

using System.Buffers;
using System.Text;
using System.Threading;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.Natives;

internal static class NativeConvars
{

    private unsafe static delegate* unmanaged<int, byte*, void> _QueryClientConvar;

    public unsafe static void QueryClientConvar(int playerid, string cvarName)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            _QueryClientConvar(playerid, (byte*)cvarNameBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<nint, int> _AddQueryClientCvarCallback;

    /// <summary>
    /// the callback should receive the following: int32 playerid, string cvarName, string cvarValue
    /// </summary>
    public unsafe static int AddQueryClientCvarCallback(nint callback)
    {
        var ret = _AddQueryClientCvarCallback(callback);
        return ret;
    }

    private unsafe static delegate* unmanaged<int, void> _RemoveQueryClientCvarCallback;

    public unsafe static void RemoveQueryClientCvarCallback(int callbackID)
    {
        _RemoveQueryClientCvarCallback(callbackID);
    }

    private unsafe static delegate* unmanaged<nint, ulong> _AddGlobalChangeListener;

    /// <summary>
    /// the callback should receive the following: string convarName, int playerid, string newValue, string oldValue
    /// </summary>
    public unsafe static ulong AddGlobalChangeListener(nint callback)
    {
        var ret = _AddGlobalChangeListener(callback);
        return ret;
    }

    private unsafe static delegate* unmanaged<ulong, void> _RemoveGlobalChangeListener;

    public unsafe static void RemoveGlobalChangeListener(ulong callbackID)
    {
        _RemoveGlobalChangeListener(callbackID);
    }

    private unsafe static delegate* unmanaged<nint, ulong> _AddConvarCreatedListener;

    /// <summary>
    /// the callback should receive the following: string convarName
    /// </summary>
    public unsafe static ulong AddConvarCreatedListener(nint callback)
    {
        var ret = _AddConvarCreatedListener(callback);
        return ret;
    }

    private unsafe static delegate* unmanaged<ulong, void> _RemoveConvarCreatedListener;

    public unsafe static void RemoveConvarCreatedListener(ulong callbackID)
    {
        _RemoveConvarCreatedListener(callbackID);
    }

    private unsafe static delegate* unmanaged<nint, ulong> _AddConCommandCreatedListener;

    /// <summary>
    /// the callback should receive the following: string commandName
    /// </summary>
    public unsafe static ulong AddConCommandCreatedListener(nint callback)
    {
        var ret = _AddConCommandCreatedListener(callback);
        return ret;
    }

    private unsafe static delegate* unmanaged<ulong, void> _RemoveConCommandCreatedListener;

    public unsafe static void RemoveConCommandCreatedListener(ulong callbackID)
    {
        _RemoveConCommandCreatedListener(callbackID);
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, short, nint, nint, void> _CreateConvarInt16;

    public unsafe static void CreateConvarInt16(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, short defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarInt16((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, ushort, nint, nint, void> _CreateConvarUInt16;

    public unsafe static void CreateConvarUInt16(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, ushort defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarUInt16((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, int, nint, nint, void> _CreateConvarInt32;

    public unsafe static void CreateConvarInt32(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, int defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarInt32((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, uint, nint, nint, void> _CreateConvarUInt32;

    public unsafe static void CreateConvarUInt32(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, uint defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarUInt32((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, long, nint, nint, void> _CreateConvarInt64;

    public unsafe static void CreateConvarInt64(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, long defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarInt64((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, ulong, nint, nint, void> _CreateConvarUInt64;

    public unsafe static void CreateConvarUInt64(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, ulong defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarUInt64((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, byte, nint, nint, void> _CreateConvarBool;

    public unsafe static void CreateConvarBool(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, bool defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarBool((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue ? (byte)1 : (byte)0, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, float, nint, nint, void> _CreateConvarFloat;

    public unsafe static void CreateConvarFloat(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, float defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarFloat((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, double, nint, nint, void> _CreateConvarDouble;

    public unsafe static void CreateConvarDouble(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, double defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarDouble((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, Color, nint, nint, void> _CreateConvarColor;

    public unsafe static void CreateConvarColor(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, Color defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarColor((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, Vector2D, nint, nint, void> _CreateConvarVector2D;

    public unsafe static void CreateConvarVector2D(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, Vector2D defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarVector2D((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, Vector, nint, nint, void> _CreateConvarVector;

    public unsafe static void CreateConvarVector(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, Vector defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarVector((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, Vector4D, nint, nint, void> _CreateConvarVector4D;

    public unsafe static void CreateConvarVector4D(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, Vector4D defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarVector4D((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, QAngle, nint, nint, void> _CreateConvarQAngle;

    public unsafe static void CreateConvarQAngle(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, QAngle defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                _CreateConvarQAngle((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, defaultValue, minValue, maxValue);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, byte*, nint, nint, void> _CreateConvarString;

    public unsafe static void CreateConvarString(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, string defaultValue, nint minValue, nint maxValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(helpMessage, helpMessageBufferPtr =>
            {
                StringAlloc.CreateCString(defaultValue, defaultValueBufferPtr =>
                {
                    _CreateConvarString((byte*)cvarNameBufferPtr, cvarType, cvarFlags, (byte*)helpMessageBufferPtr, (byte*)defaultValueBufferPtr, minValue, maxValue);
                });
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, void> _DeleteConvar;

    public unsafe static void DeleteConvar(string cvarName)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            _DeleteConvar((byte*)cvarNameBufferPtr);
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte> _ExistsConvar;

    public unsafe static bool ExistsConvar(string cvarName)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            var ret = _ExistsConvar((byte*)cvarNameBufferPtr);
            return ret == 1;
        });
    }

    private unsafe static delegate* unmanaged<byte*, int> _GetConvarType;

    public unsafe static int GetConvarType(string cvarName)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            var ret = _GetConvarType((byte*)cvarNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<byte*, ulong> _GetFlags;

    public unsafe static ulong GetFlags(string cvarName)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            var ret = _GetFlags((byte*)cvarNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<byte*, ulong, void> _SetFlags;

    public unsafe static void SetFlags(string cvarName, ulong flags)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            _SetFlags((byte*)cvarNameBufferPtr, flags);
        });
    }

    private unsafe static delegate* unmanaged<byte*, nint> _GetMinValuePtrPtr;

    public unsafe static nint GetMinValuePtrPtr(string cvarName)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            var ret = _GetMinValuePtrPtr((byte*)cvarNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<byte*, nint> _GetMaxValuePtrPtr;

    public unsafe static nint GetMaxValuePtrPtr(string cvarName)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            var ret = _GetMaxValuePtrPtr((byte*)cvarNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte> _HasDefaultValue;

    public unsafe static bool HasDefaultValue(string cvarName)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            var ret = _HasDefaultValue((byte*)cvarNameBufferPtr);
            return ret == 1;
        });
    }

    private unsafe static delegate* unmanaged<byte*, nint> _GetDefaultValuePtr;

    public unsafe static nint GetDefaultValuePtr(string cvarName)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            var ret = _GetDefaultValuePtr((byte*)cvarNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<byte*, nint, void> _SetDefaultValue;

    public unsafe static void SetDefaultValue(string cvarName, nint defaultValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            _SetDefaultValue((byte*)cvarNameBufferPtr, defaultValue);
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte*, void> _SetDefaultValueString;

    public unsafe static void SetDefaultValueString(string cvarName, string defaultValue)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(defaultValue, defaultValueBufferPtr =>
            {
                _SetDefaultValueString((byte*)cvarNameBufferPtr, (byte*)defaultValueBufferPtr);
            });
        });
    }

    private unsafe static delegate* unmanaged<byte*, nint> _GetValuePtr;

    public unsafe static nint GetValuePtr(string cvarName)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            var ret = _GetValuePtr((byte*)cvarNameBufferPtr);
            return ret;
        });
    }

    private unsafe static delegate* unmanaged<byte*, nint, void> _SetValuePtr;

    public unsafe static void SetValuePtr(string cvarName, nint value)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            _SetValuePtr((byte*)cvarNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<byte*, nint, void> _SetValueInternalPtr;

    public unsafe static void SetValueInternalPtr(string cvarName, nint value)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            _SetValueInternalPtr((byte*)cvarNameBufferPtr, value);
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte> _SetValueAsString;

    public unsafe static bool SetValueAsString(string cvarName, string value)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            return StringAlloc.CreateCString(value, valueBufferPtr =>
            {
                var ret = _SetValueAsString((byte*)cvarNameBufferPtr, (byte*)valueBufferPtr);
                return ret == 1;
            });
        });
    }

    private unsafe static delegate* unmanaged<int*, byte*, byte*> _GetValueAsString;

    public unsafe static string GetValueAsString(string cvarName)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            var length = 0;
            var returnedPtr = _GetValueAsString(&length, (byte*)cvarNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte> _SetDefaultValueAsString;

    public unsafe static bool SetDefaultValueAsString(string cvarName, string value)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            return StringAlloc.CreateCString(value, valueBufferPtr =>
            {
                var ret = _SetDefaultValueAsString((byte*)cvarNameBufferPtr, (byte*)valueBufferPtr);
                return ret == 1;
            });
        });
    }

    private unsafe static delegate* unmanaged<int*, byte*, byte*> _GetDefaultValueAsString;

    public unsafe static string GetDefaultValueAsString(string cvarName)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            var length = 0;
            var returnedPtr = _GetDefaultValueAsString(&length, (byte*)cvarNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte> _SetMinValueAsString;

    public unsafe static bool SetMinValueAsString(string cvarName, string value)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            return StringAlloc.CreateCString(value, valueBufferPtr =>
            {
                var ret = _SetMinValueAsString((byte*)cvarNameBufferPtr, (byte*)valueBufferPtr);
                return ret == 1;
            });
        });
    }

    private unsafe static delegate* unmanaged<int*, byte*, byte*> _GetMinValueAsString;

    public unsafe static string GetMinValueAsString(string cvarName)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            var length = 0;
            var returnedPtr = _GetMinValueAsString(&length, (byte*)cvarNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte> _SetMaxValueAsString;

    public unsafe static bool SetMaxValueAsString(string cvarName, string value)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            return StringAlloc.CreateCString(value, valueBufferPtr =>
            {
                var ret = _SetMaxValueAsString((byte*)cvarNameBufferPtr, (byte*)valueBufferPtr);
                return ret == 1;
            });
        });
    }

    private unsafe static delegate* unmanaged<int*, byte*, byte*> _GetMaxValueAsString;

    public unsafe static string GetMaxValueAsString(string cvarName)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            var length = 0;
            var returnedPtr = _GetMaxValueAsString(&length, (byte*)cvarNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        });
    }

    private unsafe static delegate* unmanaged<byte*, byte*, void> _SetValueInternalAsString;

    public unsafe static void SetValueInternalAsString(string cvarName, string value)
    {
        StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            StringAlloc.CreateCString(value, valueBufferPtr =>
            {
                _SetValueInternalAsString((byte*)cvarNameBufferPtr, (byte*)valueBufferPtr);
            });
        });
    }

    private unsafe static delegate* unmanaged<int*, byte*, byte*> _GetDescription;

    public unsafe static string GetDescription(string cvarName)
    {
        return StringAlloc.CreateCString(cvarName, cvarNameBufferPtr =>
        {
            var length = 0;
            var returnedPtr = _GetDescription(&length, (byte*)cvarNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        });
    }

    private unsafe static delegate* unmanaged<int*, nint> _EnumerateAll;

    public unsafe static (nint ptr, int count) EnumerateAll()
    {
        var count = 0;
        var ptr = _EnumerateAll(&count);
        return (ptr, count);
    }

    private unsafe static delegate* unmanaged<ulong, int> _UnlockAll;

    public unsafe static int UnlockAll(ulong flagsToRemove)
    {
        var ret = _UnlockAll(flagsToRemove);
        return ret;
    }
}