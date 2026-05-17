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
            _Pre += value;
        }
        remove {
            _Pre -= value;
        }
    }

    public event OnWeaponDropDelegate Post {
        add {
            _Post += value;
        }
        remove {
            _Post -= value;
        }
    }
}
