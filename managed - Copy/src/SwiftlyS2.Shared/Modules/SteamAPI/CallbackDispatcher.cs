using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// Manages Steam callbacks and call results registration/dispatch
/// </summary>
public static class CallbackDispatcher
{
    // Storage for callback dispatchers - keyed by callback ID
    private static readonly ConcurrentDictionary<int, List<ICallbackHandler>> s_callbackHandlers = new();

    // Storage for call result dispatchers - keyed by SteamAPICall handle
    private static readonly ConcurrentDictionary<ulong, ICallResultHandler> s_callResultHandlers = new();

    // Storage for registered CCallbackBase instances per callback ID
    private static readonly ConcurrentDictionary<int, IntPtr> s_registeredCallbacks = new();

    /// <summary>
    /// Register a callback handler
    /// </summary>
    internal static unsafe void RegisterCallback<T>( ICallbackHandler<T> handler ) where T : struct
    {
        var callbackId = CallbackIdentities.GetCallbackIdentity(typeof(T));

        // Add handler to dictionary
        s_callbackHandlers.AddOrUpdate(
            callbackId,
            _ => [handler],
            ( _, list ) => { list.Add(handler); return list; }
        );

        // Register with Steam if this is the first handler for this callback
        if (!s_registeredCallbacks.ContainsKey(callbackId))
        {
            // Allocate CCallbackBase structure
            var pCallback = Marshal.AllocHGlobal(Marshal.SizeOf<CCallbackBase>());
            var callback = (CCallbackBase*)pCallback;

            callback->m_vfptr = CCallbackBaseVTable.CallbackVTablePtr;
            callback->m_nCallbackFlags = 0x02; // k_ECallbackFlagsGameServer
            callback->m_iCallback = callbackId;

            // Register with Steam API
            NativeMethods.SteamAPI_RegisterCallback(pCallback, callbackId);

            // Store to prevent GC
            s_registeredCallbacks[callbackId] = pCallback;
        }
    }

    /// <summary>
    /// Unregister a callback handler
    /// </summary>
    internal static unsafe void UnregisterCallback<T>( ICallbackHandler<T> handler ) where T : struct
    {
        var callbackId = CallbackIdentities.GetCallbackIdentity(typeof(T));

        if (s_callbackHandlers.TryGetValue(callbackId, out var handlers))
        {
            _ = handlers.Remove(handler);

            // If no more handlers, unregister from Steam
            if (handlers.Count == 0)
            {
                _ = s_callbackHandlers.TryRemove(callbackId, out _);

                if (s_registeredCallbacks.TryRemove(callbackId, out var pCallback))
                {
                    NativeMethods.SteamAPI_UnregisterCallback(pCallback);
                    Marshal.FreeHGlobal(pCallback);
                }
            }
        }
    }

    /// <summary>
    /// Register a call result handler
    /// </summary>
    internal static unsafe void RegisterCallResult<T>( ulong hAPICall, ICallResultHandler<T> handler ) where T : struct
    {
        var callbackId = CallbackIdentities.GetCallbackIdentity(typeof(T));

        // Store handler
        s_callResultHandlers[hAPICall] = handler;

        // Allocate CCallbackBase structure
        var pCallback = Marshal.AllocHGlobal(Marshal.SizeOf<CCallbackBase>());
        var callback = (CCallbackBase*)pCallback;

        callback->m_vfptr = CCallbackBaseVTable.CallResultVTablePtr;
        callback->m_nCallbackFlags = 0x02; // k_ECallbackFlagsGameServer
        callback->m_iCallback = callbackId;

        // Register with Steam API
        NativeMethods.SteamAPI_RegisterCallResult(pCallback, hAPICall);

        // Store for cleanup
        handler.SetAPICall(hAPICall, pCallback);
    }

    /// <summary>
    /// Unregister a call result
    /// </summary>
    internal static unsafe void UnregisterCallResult( ulong hAPICall, IntPtr pCallback )
    {
        _ = s_callResultHandlers.TryRemove(hAPICall, out _);

        if (hAPICall != 0 && pCallback != IntPtr.Zero)
        {
            NativeMethods.SteamAPI_UnregisterCallResult(pCallback, hAPICall);
            Marshal.FreeHGlobal(pCallback);
        }
    }

    /// <summary>
    /// Internal method called from VTable Run function
    /// </summary>
    internal static unsafe void DispatchCallback( int callbackId, void* param )
    {
        if (s_callbackHandlers.TryGetValue(callbackId, out var handlers))
        {
            foreach (var handler in handlers.ToArray()) // ToArray to avoid collection modified during iteration
            {
                try
                {
                    handler.Run(param);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error dispatching callback {callbackId}: {ex}");
                }
            }
        }
    }

    /// <summary>
    /// Internal method called from VTable RunCallResult function
    /// </summary>
    internal static unsafe void DispatchCallResult( void* param, bool ioFailure, ulong hAPICall )
    {
        if (s_callResultHandlers.TryRemove(hAPICall, out var handler))
        {
            try
            {
                handler.Run(param, ioFailure);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error dispatching call result: {ex}");
            }
        }
    }
}

/// <summary>
/// Interface for callback handlers
/// </summary>
internal interface ICallbackHandler
{
    internal unsafe void Run( void* param );
}

/// <summary>
/// Generic interface for callback handlers
/// </summary>
internal interface ICallbackHandler<T> : ICallbackHandler where T : struct
{
}

/// <summary>
/// Interface for call result handlers
/// </summary>
internal interface ICallResultHandler
{
    internal unsafe void Run( void* param, bool ioFailure );
    internal void SetAPICall( ulong hAPICall, IntPtr pCallback );
}

/// <summary>
/// Generic interface for call result handlers
/// </summary>
internal interface ICallResultHandler<T> : ICallResultHandler where T : struct
{
}

/// <summary>
/// Represents a Steam callback that automatically manages its lifecycle
/// </summary>
public sealed class Callback<T> : ICallbackHandler<T>, IDisposable where T : struct
{
    private Action<T>? _callback;
    private bool _isRegistered;
    private bool _disposed;

    private Callback( Action<T> callback )
    {
        _callback = callback;
    }

    /// <summary>
    /// Create and register a new callback
    /// </summary>
    public static Callback<T> Create( Action<T> callback )
    {
        var instance = new Callback<T>(callback);
        instance.Register();
        return instance;
    }

    private void Register()
    {
        if (!_isRegistered && !_disposed)
        {
            CallbackDispatcher.RegisterCallback(this);
            _isRegistered = true;
        }
    }

    private void Unregister()
    {
        if (_isRegistered)
        {
            CallbackDispatcher.UnregisterCallback(this);
            _isRegistered = false;
        }
    }

    unsafe void ICallbackHandler.Run( void* param )
    {
        if (_callback != null && !_disposed)
        {
            var data = Marshal.PtrToStructure<T>((IntPtr)param);
            _callback(data);
        }
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            Unregister();
            _callback = null;
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    ~Callback()
    {
        Dispose();
    }
}

/// <summary>
/// Represents a Steam call result that automatically manages its lifecycle
/// </summary>
public sealed class CallResult<T> : ICallResultHandler<T>, IDisposable where T : struct
{
    private Action<T, bool>? _callback;
    private ulong _hAPICall;
    private IntPtr _pCallback;
    private bool _disposed;

    private CallResult( Action<T, bool> callback )
    {
        _callback = callback;
    }

    /// <summary>
    /// Create and register a new call result
    /// </summary>
    public static CallResult<T> Create( ulong hAPICall, Action<T, bool> callback )
    {
        var instance = new CallResult<T>(callback);
        instance.Set(hAPICall);
        return instance;
    }

    /// <summary>
    /// Set or change the API call to wait for
    /// </summary>
    public void Set( ulong hAPICall )
    {
        if (_disposed)
            return;

        // Unregister previous if any
        if (_hAPICall != 0)
        {
            CallbackDispatcher.UnregisterCallResult(_hAPICall, _pCallback);
        }

        _hAPICall = hAPICall;

        if (hAPICall != 0)
        {
            CallbackDispatcher.RegisterCallResult(hAPICall, this);
        }
    }

    void ICallResultHandler.SetAPICall( ulong hAPICall, IntPtr pCallback )
    {
        _hAPICall = hAPICall;
        _pCallback = pCallback;
    }

    unsafe void ICallResultHandler.Run( void* param, bool ioFailure )
    {
        if (_callback != null && !_disposed)
        {
            var data = Marshal.PtrToStructure<T>((IntPtr)param);
            _callback(data, ioFailure);
        }

        // Auto-cleanup after call result fires
        Dispose();
    }

    public void Dispose()
    {
        if (!_disposed)
        {
            if (_hAPICall != 0)
            {
                CallbackDispatcher.UnregisterCallResult(_hAPICall, _pCallback);
                _hAPICall = 0;
                _pCallback = IntPtr.Zero;
            }
            _callback = null;
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }

    ~CallResult()
    {
        Dispose();
    }
}

/// <summary>
/// Native callback structure that matches C++ CCallbackBase layout
/// This structure is passed to SteamAPI_RegisterCallback
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
internal unsafe struct CCallbackBase
{
    public IntPtr m_vfptr;
    public byte m_nCallbackFlags;
    public int m_iCallback;
}

/// <summary>
/// VTable implementation for CCallbackBase
/// </summary>
internal static class CCallbackBaseVTable
{
    public static IntPtr CallbackVTablePtr { get; private set; }
    public static IntPtr CallResultVTablePtr { get; private set; }

    static unsafe CCallbackBaseVTable()
    {
        // Allocate VTable with 3 function pointers
        CallbackVTablePtr = Marshal.AllocHGlobal(IntPtr.Size * 3);
        CallResultVTablePtr = Marshal.AllocHGlobal(IntPtr.Size * 3);
        Span<IntPtr> vtable = new((void*)CallbackVTablePtr, 3);
        Span<IntPtr> callResultVtable = new((void*)CallResultVTablePtr, 3);

        vtable[0] = (IntPtr)(delegate* unmanaged< CCallbackBase*, void*, void >)&RunCallback;
        vtable[1] = (IntPtr)(delegate* unmanaged< CCallbackBase*, void*, byte, ulong, void >)&RunCallbackOverload;
        vtable[2] = (IntPtr)(delegate* unmanaged< CCallbackBase*, int >)&GetCallbackSizeBytes;

        callResultVtable[0] = (IntPtr)(delegate* unmanaged< CCallbackBase*, void*, void >)&RunCallResult;
        callResultVtable[1] = (IntPtr)(delegate* unmanaged< CCallbackBase*, void*, byte, ulong, void >)&RunCallResultOverload;
        callResultVtable[2] = (IntPtr)(delegate* unmanaged< CCallbackBase*, int >)&GetCallbackSizeBytes;
    }

    /// <summary>
    /// Called by Steam when a callback is triggered
    /// </summary>
    [UnmanagedCallersOnly]
    private static unsafe void RunCallback( CCallbackBase* self, void* param )
    {
        try
        {
            var callbackId = self->m_iCallback;
            CallbackDispatcher.DispatchCallback(callbackId, param);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in CCallbackBase.Run: {ex}");
        }
    }


    /// <summary>
    /// Called by Steam when a callback is triggered, overload version
    /// </summary>
    [UnmanagedCallersOnly]
    private static unsafe void RunCallbackOverload( CCallbackBase* self, void* param, byte ioFailure, ulong hAPICall )
    {
        try
        {
            var callbackId = self->m_iCallback;
            CallbackDispatcher.DispatchCallback(callbackId, param);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in CCallbackBase.RunCallResult: {ex}");
        }
    }

    [UnmanagedCallersOnly]
    private static unsafe void RunCallResult( CCallbackBase* self, void* param )
    {
        throw new NotImplementedException("Shouldn't be called.");
    }


    /// <summary>
    /// Called by Steam when a call result is ready
    /// </summary>
    [UnmanagedCallersOnly]
    private static unsafe void RunCallResultOverload( CCallbackBase* self, void* param, byte ioFailure, ulong hAPICall )
    {
        try
        {
            CallbackDispatcher.DispatchCallResult(param, ioFailure == 1, hAPICall);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in CCallbackBase.RunCallResult: {ex}");
        }
    }

    /// <summary>
    /// Returns the size of the callback structure
    /// </summary>
    [UnmanagedCallersOnly]
    private static unsafe int GetCallbackSizeBytes( CCallbackBase* self )
    {
        Console.WriteLine($"GetCallbackSizeBytes");
        try
        {
            int callbackId = self->m_iCallback;

            // Find the callback type by ID
            foreach (var type in typeof(CCallbackBase).Assembly.GetTypes())
            {
                if (type.IsValueType && !type.IsEnum)
                {
                    foreach (var attr in type.GetCustomAttributes(typeof(CallbackIdentityAttribute), false))
                    {
                        if (attr is CallbackIdentityAttribute identity && identity.Identity == callbackId)
                        {
                            return Marshal.SizeOf(type);
                        }
                    }
                }
            }
        }
        catch
        {
            // Fallback
        }

        return 0;
    }
}