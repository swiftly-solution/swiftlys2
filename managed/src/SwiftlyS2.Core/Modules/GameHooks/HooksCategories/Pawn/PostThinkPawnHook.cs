using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class PostThinkPawnData : IPostThinkPawn
{
    public required IPlayer Player { get; set; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class PostThinkPawnEvents : IPostThinkPawnEvents
{
    internal event OnPostThinkPawnDelegate? _Pre;
    internal event OnPostThinkPawnDelegate? _Post;

    public event OnPostThinkPawnDelegate Pre {
        add {
            _Pre += value;
        }
        remove {
            _Pre -= value;
        }
    }

    public event OnPostThinkPawnDelegate Post {
        add {
            _Post += value;
        }
        remove {
            _Post -= value;
        }
    }
}
