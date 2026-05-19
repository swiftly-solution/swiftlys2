using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class PlayerMoveMovementData : IPlayerMoveMovement
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

internal sealed class PlayerMoveMovementEvents : IPlayerMoveMovementEvents
{
    internal event OnPlayerMoveMovementDelegate? _Pre;
    internal event OnPlayerMoveMovementDelegate? _Post;

    public event OnPlayerMoveMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.PlayerMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.PlayerMove);
        }
    }

    public event OnPlayerMoveMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.PlayerMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.PlayerMove);
        }
    }

    public void InvokePre( ref IPlayerMoveMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref IPlayerMoveMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.PlayerMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.PlayerMove);
    }
}
