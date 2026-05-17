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
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.PostThink);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.PostThink);
        }
    }

    public event OnPostThinkPawnDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.PostThink);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.PostThink);
        }
    }

    public void InvokePre( ref IPostThinkPawn data )
    {
        _Pre?.Invoke(ref data);
    }

    public void InvokePost( ref IPostThinkPawn data )
    {
        _Post?.Invoke(ref data);
    }

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.PostThink);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.PostThink);
    }
}
