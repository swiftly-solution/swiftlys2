using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CanMovePawnData : ICanMovePawn
{
    public required IPlayer Player { get; set; }
    public required bool OriginalResult { get; set; }

    public void SetResult( bool result )
    {
        OriginalResult = result;
        Intercepted = true;
    }

    public bool Intercepted { get; set; } = false;
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class CanMovePawnEvents : ICanMovePawnEvents
{
    internal event OnCanMovePawnDelegate? _Pre;
    internal event OnCanMovePawnDelegate? _Post;

    public event OnCanMovePawnDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CanMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CanMove);
        }
    }

    public event OnCanMovePawnDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CanMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CanMove);
        }
    }

    public void InvokePre( ref ICanMovePawn data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref ICanMovePawn data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CanMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CanMove);
    }
}
