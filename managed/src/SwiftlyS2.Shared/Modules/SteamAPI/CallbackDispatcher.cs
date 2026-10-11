using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Extensions;

namespace SwiftlyS2.Shared.SteamAPI;

/// <summary>
/// Manages Steam callbacks and call results registration/dispatch
/// </summary>
internal static class CallbackDispatcher
{
    /// <summary>k_ECallbackFlagsGameServer</summary>
    private const byte GameServerCallbackFlags = 0x02;

    /// <summary>Callback handlers, keyed by callback ID.</summary>
    private static readonly ConcurrentDictionary<int, List<ICallbackHandler>> callbackHandlers = new();

    /// <summary>Call result handlers, keyed by SteamAPICall handle.</summary>
    private static readonly ConcurrentDictionary<ulong, ICallResultHandler> callResultHandlers = new();

    /// <summary>Native CCallbackBase instances registered with Steam, keyed by callback ID.</summary>
    private static readonly ConcurrentDictionary<int, nint> registeredCallbacks = new();

    /// <summary>
    /// Register a callback handler
    /// </summary>
    internal static unsafe void RegisterCallback<T>( ICallbackHandler<T> handler ) where T : struct
    {
        var callbackId = CallbackIdentities.GetCallbackIdentity(typeof(T));

        _ = callbackHandlers.AddOrUpdate(
            callbackId,
            _ => [handler],
            ( _, list ) =>
            {
                list.Add(handler);
                return list;
            }
        );

        // Register with Steam only for the first handler of this callback
        if (registeredCallbacks.ContainsKey(callbackId))
        {
            return;
        }

        // Kept in the dictionary so the native memory outlives the registration
        registeredCallbacks[callbackId] = AllocateAndRegister(
            CCallbackBaseVTable.CallbackVTablePtr,
            callbackId,
            static ( ptr, id ) => NativeMethods.SteamAPI_RegisterCallback(ptr, id)
        );
    }

    /// <summary>
    /// Unregister a callback handler
    /// </summary>
    internal static void UnregisterCallback<T>( ICallbackHandler<T> handler ) where T : struct
    {
        var callbackId = CallbackIdentities.GetCallbackIdentity(typeof(T));

        if (!callbackHandlers.TryGetValue(callbackId, out var handlers))
        {
            return;
        }

        _ = handlers.Remove(handler);

        if (handlers.Count != 0)
        {
            return;
        }

        // No handlers left, unregister from Steam
        _ = callbackHandlers.TryRemove(callbackId, out _);

        if (registeredCallbacks.TryRemove(callbackId, out var pCallback))
        {
            NativeMethods.SteamAPI_UnregisterCallback(pCallback);
            Marshal.FreeHGlobal(pCallback);
        }
    }

    /// <summary>
    /// Register a call result handler
    /// </summary>
    internal static unsafe void RegisterCallResult<T>( ulong hAPICall, ICallResultHandler<T> handler ) where T : struct
    {
        var callbackId = CallbackIdentities.GetCallbackIdentity(typeof(T));

        callResultHandlers[hAPICall] = handler;

        var pCallback = AllocateAndRegister(
            CCallbackBaseVTable.CallResultVTablePtr,
            callbackId,
            ( ptr, _ ) => NativeMethods.SteamAPI_RegisterCallResult(ptr, hAPICall)
        );

        // Handler keeps the pointer for cleanup
        handler.SetAPICall(hAPICall, pCallback);
    }

    /// <summary>
    /// Unregister a call result
    /// </summary>
    internal static void UnregisterCallResult( ulong hAPICall, nint pCallback )
    {
        _ = callResultHandlers.TryRemove(hAPICall, out _);

        if (hAPICall != 0 && pCallback != nint.Zero)
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
        if (!callbackHandlers.TryGetValue(callbackId, out var handlers))
        {
            return;
        }

        // Snapshot so handlers may (un)register while being dispatched
        foreach (var handler in handlers.ToArray())
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

    /// <summary>
    /// Internal method called from VTable RunCallResult function
    /// </summary>
    internal static unsafe void DispatchCallResult( void* param, bool ioFailure, ulong hAPICall )
    {
        if (!callResultHandlers.TryRemove(hAPICall, out var handler))
        {
            return;
        }

        try
        {
            handler.Run(param, ioFailure);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error dispatching call result: {ex}");
        }
    }

    /// <summary>
    /// Allocates a native CCallbackBase, fills it in and hands it to <paramref name="register"/>.
    /// </summary>
    private static unsafe nint AllocateAndRegister( nint vtable, int callbackId, Action<nint, int> register )
    {
        var pCallback = Marshal.AllocHGlobal(sizeof(CCallbackBase));

        *(CCallbackBase*)pCallback = new CCallbackBase {
            m_vfptr = vtable,
            m_nCallbackFlags = GameServerCallbackFlags,
            m_iCallback = callbackId
        };

        register(pCallback, callbackId);
        return pCallback;
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
internal interface ICallbackHandler<T> : ICallbackHandler where T : struct;

/// <summary>
/// Interface for call result handlers
/// </summary>
internal interface ICallResultHandler
{
    internal unsafe void Run( void* param, bool ioFailure );
    internal void SetAPICall( ulong hAPICall, nint pCallback );
}

/// <summary>
/// Generic interface for call result handlers
/// </summary>
internal interface ICallResultHandler<T> : ICallResultHandler where T : struct;

/// <summary>
/// Reads callback structs from native memory. Types without references are copied raw,
/// anything else goes through marshalling. The choice is made once per <typeparamref name="T"/>.
/// </summary>
internal static class CallbackStructReader<T> where T : struct
{
    private static readonly bool useMarshalling = RuntimeHelpers.IsReferenceOrContainsReferences<T>();

    internal static unsafe T Read( void* param ) =>
        useMarshalling ? Marshal.PtrToStructure<T>((nint)param) : Unsafe.Read<T>(param);
}

/// <summary>
/// Represents a Steam callback that automatically manages its lifecycle
/// </summary>
public sealed class Callback<T> : ICallbackHandler<T>, IDisposable where T : struct
{
    private Action<T>? callback;
    private bool isRegistered;
    private bool disposed;

    private Callback( Action<T> callback )
    {
        this.callback = callback;
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
        if (isRegistered || disposed)
        {
            return;
        }

        CallbackDispatcher.RegisterCallback(this);
        isRegistered = true;
    }

    private void Unregister()
    {
        if (!isRegistered)
        {
            return;
        }

        CallbackDispatcher.UnregisterCallback(this);
        isRegistered = false;
    }

    unsafe void ICallbackHandler.Run( void* param )
    {
        if (callback is not null && !disposed)
        {
            callback(CallbackStructReader<T>.Read(param));
        }
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        Unregister();
        callback = null;
        disposed = true;
        GC.SuppressFinalize(this);
    }

    ~Callback() => Dispose();
}

/// <summary>
/// Represents a Steam call result that automatically manages its lifecycle
/// </summary>
public sealed class CallResult<T> : ICallResultHandler<T>, IDisposable where T : struct
{
    private Action<T, bool>? callback;
    private ulong apiCall;
    private nint nativeCallback;
    private bool disposed;

    private CallResult( Action<T, bool> callback )
    {
        this.callback = callback;
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
        if (disposed)
        {
            return;
        }

        // Unregister previous if any
        if (apiCall != 0)
        {
            CallbackDispatcher.UnregisterCallResult(apiCall, nativeCallback);
        }

        apiCall = hAPICall;

        if (hAPICall != 0)
        {
            CallbackDispatcher.RegisterCallResult(hAPICall, this);
        }
    }

    void ICallResultHandler.SetAPICall( ulong hAPICall, nint pCallback )
    {
        apiCall = hAPICall;
        nativeCallback = pCallback;
    }

    unsafe void ICallResultHandler.Run( void* param, bool ioFailure )
    {
        if (callback is not null && !disposed)
        {
            callback(CallbackStructReader<T>.Read(param), ioFailure);
        }

        // Auto-cleanup after call result fires
        Dispose();
    }

    public void Dispose()
    {
        if (disposed)
        {
            return;
        }

        if (apiCall != 0)
        {
            CallbackDispatcher.UnregisterCallResult(apiCall, nativeCallback);
            apiCall = 0;
            nativeCallback = nint.Zero;
        }

        callback = null;
        disposed = true;
        GC.SuppressFinalize(this);
    }

    ~CallResult() => Dispose();
}

/// <summary>
/// Native callback structure that matches C++ CCallbackBase layout
/// This structure is passed to SteamAPI_RegisterCallback
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
internal struct CCallbackBase
{
    public nint m_vfptr;
    public byte m_nCallbackFlags;
    public int m_iCallback;
}

/// <summary>
/// VTable implementation for CCallbackBase
/// </summary>
internal static unsafe class CCallbackBaseVTable
{
    private const int VTableSlots = 3;

    public static nint CallbackVTablePtr { get; }
    public static nint CallResultVTablePtr { get; }

    static CCallbackBaseVTable()
    {
        CallbackVTablePtr = Marshal.AllocHGlobal(nint.Size * VTableSlots);
        CallResultVTablePtr = Marshal.AllocHGlobal(nint.Size * VTableSlots);

        var callbackVtable = new Span<nint>((void*)CallbackVTablePtr, VTableSlots);
        var callResultVtable = new Span<nint>((void*)CallResultVTablePtr, VTableSlots);

        callbackVtable[0] = (nint)(delegate* unmanaged<CCallbackBase*, void*, void>)&RunCallback;
        callbackVtable[1] = (nint)(delegate* unmanaged<CCallbackBase*, void*, byte, ulong, void>)&RunCallbackOverload;
        callbackVtable[2] = (nint)(delegate* unmanaged<CCallbackBase*, int>)&GetCallbackSizeBytes;

        callResultVtable[0] = (nint)(delegate* unmanaged<CCallbackBase*, void*, void>)&RunCallResult;
        callResultVtable[1] = (nint)(delegate* unmanaged<CCallbackBase*, void*, byte, ulong, void>)&RunCallResultOverload;
        callResultVtable[2] = (nint)(delegate* unmanaged<CCallbackBase*, int>)&GetCallbackSizeBytes;
    }

    /// <summary>
    /// Called by Steam when a callback is triggered
    /// </summary>
    [UnmanagedCallersOnly]
    private static void RunCallback( CCallbackBase* self, void* param )
    {
        try
        {
            CallbackDispatcher.DispatchCallback(self->m_iCallback, param);
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
    private static void RunCallbackOverload( CCallbackBase* self, void* param, byte ioFailure, ulong hAPICall )
    {
        try
        {
            CallbackDispatcher.DispatchCallback(self->m_iCallback, param);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception in CCallbackBase.RunCallResult: {ex}");
        }
    }

    [UnmanagedCallersOnly]
    private static void RunCallResult( CCallbackBase* self, void* param )
    {
        throw new NotImplementedException("Shouldn't be called.");
    }

    /// <summary>
    /// Called by Steam when a call result is ready
    /// </summary>
    [UnmanagedCallersOnly]
    private static void RunCallResultOverload( CCallbackBase* self, void* param, byte ioFailure, ulong hAPICall )
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
    private static int GetCallbackSizeBytes( CCallbackBase* self )
    {
        try
        {
            var callbackId = self->m_iCallback;

            // Find the callback type by ID
            foreach (var type in typeof(CCallbackBase).Assembly.GetTypes())
            {
                if (!type.IsValueType || type.IsEnum)
                {
                    continue;
                }

                foreach (var attr in type.GetCustomAttributes(typeof(CallbackIdentityAttribute), false))
                {
                    if (attr is CallbackIdentityAttribute identity && identity.Identity == callbackId)
                    {
                        return Marshal.SizeOf(type);
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
