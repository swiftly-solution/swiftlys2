using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Hooks;
using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Shared.Memory;

namespace SwiftlyS2.Core.Memory;

internal class UnmanagedMemory : NativeHandle, IUnmanagedMemory, IDisposable
{
    public new nint Address { get; private set; }
    private HookManager _HookManager { get; set; }
    private ILogger<UnmanagedMemory> _Logger { get; set; }
    public List<Guid> Hooks { get; } = new();

    public UnmanagedMemory( nint address, HookManager hookManager, ILoggerFactory loggerFactory ) : base(address)
    {
        Address = address;
        _HookManager = hookManager;
        _Logger = loggerFactory.CreateLogger<UnmanagedMemory>();
    }

    public Guid AddHook( MidHookDelegate callback )
    {
        try
        {
            var id = _HookManager.AddMidHook(Address, callback);
            Hooks.Add(id);
            return id;
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return Guid.Empty;
            _Logger.LogError(e, "Failed to add midhook to function {0}.", Address);
            return Guid.Empty;
        }
    }

    public void Dispose()
    {
        _HookManager.Remove(Hooks);
        Hooks.Clear();
    }

    public void RemoveHook( Guid id )
    {
        try
        {
            _HookManager.RemoveMidHook(new List<Guid> { id });
            Hooks.Remove(id);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(e)) return;
            _Logger.LogError(e, "Failed to remove midhook {0} from function {1}.", id, Address);
        }
    }
}