using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CanUseWeaponData : ICanUseWeapon
{
    public required IPlayer Player { get; set; }
    public required CCSWeaponBase Weapon { get; init; }
    public required bool OriginalResult { get; set; }

    private bool _intercepted;

    public void SetResult( bool result )
    {
        OriginalResult = result;
        _intercepted = true;
    }

    public bool Intercepted => _intercepted;
}

internal sealed class CanUseWeaponEvents : ICanUseWeaponEvents
{
    internal event OnCanUseWeaponDelegate? _Pre;
    internal event OnCanUseWeaponDelegate? _Post;

    public event OnCanUseWeaponDelegate Pre {
        add {
            _Pre += value;
        }
        remove {
            _Pre -= value;
        }
    }

    public event OnCanUseWeaponDelegate Post {
        add {
            _Post += value;
        }
        remove {
            _Post -= value;
        }
    }
}
