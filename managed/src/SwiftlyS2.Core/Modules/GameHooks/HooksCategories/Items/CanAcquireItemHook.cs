using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CanAcquireItemData : ICanAcquireItem
{
    public required IPlayer Player { get; set; }
    public required CEconItemView EconItemView { get; init; }
    public required CCSWeaponBaseVData? WeaponVData { get; init; }
    public required AcquireMethod AcquireMethod { get; init; }
    public required AcquireResult OriginalResult { get; set; }

    public void SetAcquireResult( AcquireResult result )
    {
        OriginalResult = result;
        Intercepted = true;
    }

    public bool Intercepted { get; set; } = false;
}

internal sealed class CanAcquireItemEvents : ICanAcquireItemEvents
{
    internal event OnCanAcquireItemDelegate? _Pre;
    internal event OnCanAcquireItemDelegate? _Post;

    public event OnCanAcquireItemDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CanAcquire);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CanAcquire);
        }
    }

    public event OnCanAcquireItemDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CanAcquire);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CanAcquire);
        }
    }

    public void InvokePre( ref ICanAcquireItem data )
    {
        _Pre?.Invoke(ref data);
    }

    public void InvokePost( ref ICanAcquireItem data )
    {
        _Post?.Invoke(ref data);
    }

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CanAcquire);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CanAcquire);
    }
}
