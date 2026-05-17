using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class ProcessUsercmdsController : IProcessUsercmdsController
{
    public required IPlayer Player { get; set; }

    public required List<CSGOUserCmdPB> Usercmds { get; init; }

    public required bool Paused { get; init; }

    public required float Margin { get; init; }

    public required HookResult Result { get; set; } = HookResult.Continue;

}

internal sealed class ProcessUsercmdsEvents : IProcessUsercmdsEvents
{
    private event OnProcessUsercmdsDelegate? _Pre;
    private event OnProcessUsercmdsDelegate? _Post;

    public event OnProcessUsercmdsDelegate Pre {
        add {
            _Pre += value;
        }
        remove {
            _Pre -= value;
        }
    }

    public event OnProcessUsercmdsDelegate Post {
        add {
            _Post += value;
        }
        remove {
            _Post -= value;
        }
    }
}
