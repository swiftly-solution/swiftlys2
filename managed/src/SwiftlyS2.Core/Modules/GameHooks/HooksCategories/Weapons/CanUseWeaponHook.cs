using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CanUseWeaponData : ICanUseWeapon
{
    public required IPlayer Player { get; set; }
    public required CCSWeaponBase Weapon { get; init; }
    public required bool OriginalResult { get; set; }

    public void SetResult( bool result )
    {
        OriginalResult = result;
        Intercepted = true;
    }

    public bool Intercepted { get; set; } = false;
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class CanUseWeaponEvents : ICanUseWeaponEvents
{
    internal event OnCanUseWeaponDelegate? _Pre;
    internal event OnCanUseWeaponDelegate? _Post;

    public event OnCanUseWeaponDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CanUse);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CanUse);
        }
    }

    public event OnCanUseWeaponDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CanUse);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CanUse);
        }
    }

    public void InvokePre( ref ICanUseWeapon data )
    {
        _Pre?.Invoke(ref data);
    }

    public void InvokePost( ref ICanUseWeapon data )
    {
        _Post?.Invoke(ref data);
    }

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CanUse);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CanUse);
    }
}
