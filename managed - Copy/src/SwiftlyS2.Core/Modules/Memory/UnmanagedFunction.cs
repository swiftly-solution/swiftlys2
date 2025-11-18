using System.Runtime.InteropServices;
using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Hooks;
using SwiftlyS2.Core.Natives.NativeObjects;
using SwiftlyS2.Shared.Memory;

namespace SwiftlyS2.Core.Memory;

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
          return Marshal.GetDelegateForFunctionPointer<TDelegate>(original);
        }
      }
      return Call;
    }
  }

  public TDelegate Call { get; private set; }

  public List<Guid> Hooks { get; } = new();

  private HookManager _HookManager { get; set; }

  private ILogger<UnmanagedFunction<TDelegate>> _Logger { get; set; }

  public UnmanagedFunction( nint address, HookManager hookManager, ILoggerFactory loggerFactory ) : base(address)
  {
    _Logger = loggerFactory.CreateLogger<UnmanagedFunction<TDelegate>>();
    _HookManager = hookManager;
    DelegateType = typeof(TDelegate);

    Address = address;

    Call = Marshal.GetDelegateForFunctionPointer<TDelegate>(address);
  }

  public Guid AddHook( Func<Func<TDelegate>, TDelegate> callbackBuilder )
  {
    try
    {
      var id = _HookManager.AddHook(Address, ( builder ) => callbackBuilder(() => Marshal.GetDelegateForFunctionPointer<TDelegate>(builder())));
      Hooks.Add(id);
      return id;
    }
    catch (Exception e)
    {
      if (!GlobalExceptionHandler.Handle(e)) return Guid.Empty;
      _Logger.LogError(e, "Failed to add hook to function {0}.", Address);
      return Guid.Empty;
    }
  }

  public void RemoveHook( Guid id )
  {
    try
    {
      _HookManager.Remove(new List<Guid> { id });
      Hooks.Remove(id);
    }
    catch (Exception e)
    {
      if (!GlobalExceptionHandler.Handle(e)) return;
      _Logger.LogError(e, "Failed to remove hook {0} from function {1}.", id, Address);
    }
  }

  public override void Dispose()
  {
    _HookManager.Remove(Hooks);
    Hooks.Clear();
  }






}