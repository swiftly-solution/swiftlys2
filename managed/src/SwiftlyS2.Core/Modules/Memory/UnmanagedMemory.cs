using Microsoft.Extensions.Logging;
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
    private readonly ILogger<UnmanagedMemory> logger;

    public UnmanagedMemory( nint address, HookManager hookManager, ILoggerFactory loggerFactory ) : base(address)
    {
        this.Address = address;
        this.Hooks = [];

        this.hooksLock = new();
        this.hookManager = hookManager;
        this.logger = loggerFactory.CreateLogger<UnmanagedMemory>();
    }

    public Guid AddHook( MidHookDelegate callback )
    {
        try
        {
            lock (hooksLock)
            {
                var id = hookManager.AddMidHook(Address, callback);
                Hooks.Add(id);
                return id;
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return Guid.Empty;
            logger.LogError(e, "Failed to add midhook to function {Address}.", Address);
            return Guid.Empty;
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
        try
        {
            lock (hooksLock)
            {
                hookManager.RemoveMidHook([id]);
                _ = Hooks.Remove(id);
            }
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            logger.LogError(e, "Failed to remove midhook {Id} from function {Address}.", id, Address);
        }
    }
}