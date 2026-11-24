using SwiftlyS2.Core.Hooks;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Core.Natives.NativeObjects;

namespace SwiftlyS2.Core.Memory;

internal class UnmanagedMemory : NativeHandle, IUnmanagedMemory, IDisposable
{
    public new nint Address { get; private set; }
    public List<Guid> Hooks { get; init; }
    private readonly Lock hooksLock;
    private readonly HookManager hookManager;

    public UnmanagedMemory( nint address, HookManager hookManager ) : base(address)
    {
        this.Address = address;
        this.Hooks = [];

        this.hooksLock = new();
        this.hookManager = hookManager;
    }

    public Guid AddHook( MidHookDelegate callback )
    {
        lock (hooksLock)
        {
            var id = hookManager.AddMidHook(Address, callback);
            Hooks.Add(id);
            return id;
        }
    }

    public void Dispose()
    {
        lock (hooksLock)
        {
            hookManager.RemoveMidHook(Hooks);
            Hooks.Clear();
        }
    }

    public void RemoveHook( Guid id )
    {
        lock (hooksLock)
        {
            hookManager.RemoveMidHook([id]);
            _ = Hooks.Remove(id);
        }
    }
}
