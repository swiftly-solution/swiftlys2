using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class RunCommandMovementData : IRunCommandMovement
{
    public required IPlayer Player { get; set; }
    public required CInButtonState ButtonState { get; init; }
    public required CSGOUserCmdPB UserCmdPB { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class RunCommandMovementEvents : IRunCommandMovementEvents
{
    internal event OnRunCommandMovementDelegate? _Pre;
    internal event OnRunCommandMovementDelegate? _Post;

    public event OnRunCommandMovementDelegate Pre {
        add {
            _Pre += value;
        }
        remove {
            _Pre -= value;
        }
    }

    public event OnRunCommandMovementDelegate Post {
        add {
            _Post += value;
        }
        remove {
            _Post -= value;
        }
    }
}
