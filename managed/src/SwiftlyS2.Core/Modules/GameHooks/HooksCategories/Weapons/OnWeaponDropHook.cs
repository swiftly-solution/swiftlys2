using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class OnWeaponDropData : IOnWeaponDrop
{
    public required IPlayer Player { get; set; }
    public required CBasePlayerWeapon? Weapon { get; init; }
    public required bool SwappingWeapon { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class OnWeaponDropEvents : IOnWeaponDropEvents
{
    internal event OnWeaponDropDelegate? _Pre;
    internal event OnWeaponDropDelegate? _Post;

    public event OnWeaponDropDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.WeaponDrop);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.WeaponDrop);
        }
    }

    public event OnWeaponDropDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.WeaponDrop);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.WeaponDrop);
        }
    }

    public void InvokePre( ref IOnWeaponDrop data )
    {
        _Pre?.Invoke(ref data);
    }

    public void InvokePost( ref IOnWeaponDrop data )
    {
        _Post?.Invoke(ref data);
    }

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.WeaponDrop);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.WeaponDrop);
    }
}
