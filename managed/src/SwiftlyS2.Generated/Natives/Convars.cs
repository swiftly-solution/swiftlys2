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
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            _QueryClientConvar(playerid, cvarNameBufferPtr);
        }
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

    private unsafe static delegate* unmanaged<byte*, int, ulong, byte*, nint, nint, nint, void> _CreateConvar;

    /// <summary>
    /// the pointers point to a value of the convar type, or to a null terminated string for string convars. minValue and maxValue are optional and only used by numeric types
    /// </summary>
    public unsafe static void CreateConvar(string cvarName, int cvarType, ulong cvarFlags, string helpMessage, nint defaultValue, nint minValue, nint maxValue)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        using var helpMessageStr = new ScopedCString(helpMessage);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            fixed (byte* helpMessageBufferPtr = helpMessageStr)
            {
                _CreateConvar(cvarNameBufferPtr, cvarType, cvarFlags, helpMessageBufferPtr, defaultValue, minValue, maxValue);
            }
        }
    }

    private unsafe static delegate* unmanaged<byte*, void> _DeleteConvar;

    public unsafe static void DeleteConvar(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            _DeleteConvar(cvarNameBufferPtr);
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte> _ExistsConvar;

    public unsafe static bool ExistsConvar(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            var ret = _ExistsConvar(cvarNameBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<byte*, int> _GetConvarType;

    public unsafe static int GetConvarType(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            var ret = _GetConvarType(cvarNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<byte*, ulong> _GetFlags;

    public unsafe static ulong GetFlags(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            var ret = _GetFlags(cvarNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<byte*, ulong, void> _SetFlags;

    public unsafe static void SetFlags(string cvarName, ulong flags)
    {
        if (!NativeBinding.IsMainThread)
        {
            throw new InvalidOperationException("This method can only be called from the main thread.");
        }
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            _SetFlags(cvarNameBufferPtr, flags);
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint> _GetMinValuePtrPtr;

    public unsafe static nint GetMinValuePtrPtr(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            var ret = _GetMinValuePtrPtr(cvarNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint> _GetMaxValuePtrPtr;

    public unsafe static nint GetMaxValuePtrPtr(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            var ret = _GetMaxValuePtrPtr(cvarNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte> _HasDefaultValue;

    public unsafe static bool HasDefaultValue(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            var ret = _HasDefaultValue(cvarNameBufferPtr);
            return ret == 1;
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint> _GetDefaultValuePtr;

    public unsafe static nint GetDefaultValuePtr(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            var ret = _GetDefaultValuePtr(cvarNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint> _GetValuePtr;

    public unsafe static nint GetValuePtr(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            var ret = _GetValuePtr(cvarNameBufferPtr);
            return ret;
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint, void> _SetValuePtr;

    public unsafe static void SetValuePtr(string cvarName, nint value)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            _SetValuePtr(cvarNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<byte*, nint, void> _SetValueInternalPtr;

    public unsafe static void SetValueInternalPtr(string cvarName, nint value)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            _SetValueInternalPtr(cvarNameBufferPtr, value);
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte> _SetValueAsString;

    public unsafe static bool SetValueAsString(string cvarName, string value)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        using var valueStr = new ScopedCString(value);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _SetValueAsString(cvarNameBufferPtr, valueBufferPtr);
                return ret == 1;
            }
        }
    }

    private unsafe static delegate* unmanaged<int*, byte*, byte*> _GetValueAsString;

    public unsafe static string GetValueAsString(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            var length = 0;
            var returnedPtr = _GetValueAsString(&length, cvarNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte> _SetDefaultValueAsString;

    public unsafe static bool SetDefaultValueAsString(string cvarName, string value)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        using var valueStr = new ScopedCString(value);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _SetDefaultValueAsString(cvarNameBufferPtr, valueBufferPtr);
                return ret == 1;
            }
        }
    }

    private unsafe static delegate* unmanaged<int*, byte*, byte*> _GetDefaultValueAsString;

    public unsafe static string GetDefaultValueAsString(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            var length = 0;
            var returnedPtr = _GetDefaultValueAsString(&length, cvarNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte> _SetMinValueAsString;

    public unsafe static bool SetMinValueAsString(string cvarName, string value)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        using var valueStr = new ScopedCString(value);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _SetMinValueAsString(cvarNameBufferPtr, valueBufferPtr);
                return ret == 1;
            }
        }
    }

    private unsafe static delegate* unmanaged<int*, byte*, byte*> _GetMinValueAsString;

    public unsafe static string GetMinValueAsString(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            var length = 0;
            var returnedPtr = _GetMinValueAsString(&length, cvarNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, byte> _SetMaxValueAsString;

    public unsafe static bool SetMaxValueAsString(string cvarName, string value)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        using var valueStr = new ScopedCString(value);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                var ret = _SetMaxValueAsString(cvarNameBufferPtr, valueBufferPtr);
                return ret == 1;
            }
        }
    }

    private unsafe static delegate* unmanaged<int*, byte*, byte*> _GetMaxValueAsString;

    public unsafe static string GetMaxValueAsString(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            var length = 0;
            var returnedPtr = _GetMaxValueAsString(&length, cvarNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        }
    }

    private unsafe static delegate* unmanaged<byte*, byte*, void> _SetValueInternalAsString;

    public unsafe static void SetValueInternalAsString(string cvarName, string value)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        using var valueStr = new ScopedCString(value);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            fixed (byte* valueBufferPtr = valueStr)
            {
                _SetValueInternalAsString(cvarNameBufferPtr, valueBufferPtr);
            }
        }
    }

    private unsafe static delegate* unmanaged<int*, byte*, byte*> _GetDescription;

    public unsafe static string GetDescription(string cvarName)
    {
        using var cvarNameStr = new ScopedCString(cvarName);
        fixed (byte* cvarNameBufferPtr = cvarNameStr)
        {
            var length = 0;
            var returnedPtr = _GetDescription(&length, cvarNameBufferPtr);
            var outString = StringAlloc.CreateCSharpString((nint)returnedPtr, length);
            NativeAllocator.Free((nint)returnedPtr);
            return outString;
        }
    }
}