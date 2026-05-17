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

    public bool Intercepted { get; private set; } = false;
}

internal sealed class CanAcquireItemEvents : ICanAcquireItemEvents
{
    private event OnCanAcquireItemDelegate? _Pre;
    private event OnCanAcquireItemDelegate? _Post;

    public event OnCanAcquireItemDelegate Pre {
        add {
            _Pre += value;
        }
        remove {
            _Pre -= value;
        }
    }

    public event OnCanAcquireItemDelegate Post {
        add {
            _Post += value;
        }
        remove {
            _Post -= value;
        }
    }
}
