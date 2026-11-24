using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using SwiftlyS2.Core.Hooks;
using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Shared.Memory;

namespace SwiftlyS2.Core.Memory;

internal static class DelegateCache<TDelegate> where TDelegate : Delegate
{
    private static readonly ConcurrentDictionary<nint, TDelegate> Cache = new();

    public static TDelegate Get( nint ptr ) =>
        Cache.GetOrAdd(ptr, static p => Marshal.GetDelegateForFunctionPointer<TDelegate>(p));
}

internal abstract class UnmanagedFunction : NativeHandle, IDisposable
{
    public UnmanagedFunction( nint address ) : base(address)
    {
    }

    public Type? DelegateType { get; init; }

    public abstract void Dispose();
}

internal class UnmanagedFunction<TDelegate> : UnmanagedFunction, IUnmanagedFunction<TDelegate>, IDisposable where TDelegate : Delegate
{

    public new nint Address { get; private set; }

    public TDelegate CallOriginal {
        get {
            if (_HookManager.IsHooked(Address))
            {
                var original = _HookManager.GetOriginal(Address);
                if (original != nint.Zero)
                {
                    return DelegateCache<TDelegate>.Get(original);
                }
            }
            return Call;
        }
    }

    public TDelegate Call { get; private set; }

    public List<Guid> Hooks { get; } = [];

    private HookManager _HookManager { get; set; }

    public UnmanagedFunction( nint address, HookManager hookManager ) : base(address)
    {
        _HookManager = hookManager;
        DelegateType = typeof(TDelegate);

        Address = address;

        Call = DelegateCache<TDelegate>.Get(address);
    }

    public Guid AddHook( Func<Func<TDelegate>, TDelegate> callbackBuilder )
    {
        var id = _HookManager.AddHook(Address, ( builder ) => callbackBuilder(() => DelegateCache<TDelegate>.Get(builder())));
        Hooks.Add(id);
        return id;
    }

    public void RemoveHook( Guid id )
    {
        _HookManager.Remove([id]);
        _ = Hooks.Remove(id);
    }

    public override void Dispose()
    {
        _HookManager.Remove(Hooks);
        Hooks.Clear();
    }
}