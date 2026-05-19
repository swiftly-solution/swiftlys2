using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class LadderMoveMovementData : ILadderMoveMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public required bool OriginalResult { get; set; }

    public void SetResult( bool result )
    {
        OriginalResult = result;
        Intercepted = true;
    }

    public bool Intercepted { get; set; } = false;
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class LadderMoveMovementEvents : ILadderMoveMovementEvents
{
    internal event OnLadderMoveMovementDelegate? _Pre;
    internal event OnLadderMoveMovementDelegate? _Post;

    public event OnLadderMoveMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.LadderMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.LadderMove);
        }
    }

    public event OnLadderMoveMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.LadderMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.LadderMove);
        }
    }

    public void InvokePre( ref ILadderMoveMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref ILadderMoveMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.LadderMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.LadderMove);
    }
}
