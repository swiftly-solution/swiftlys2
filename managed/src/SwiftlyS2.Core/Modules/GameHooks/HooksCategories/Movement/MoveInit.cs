using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class MoveInitMovementData : IMoveInitMovement
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

internal sealed class MoveInitMovementEvents : IMoveInitMovementEvents
{
    internal event OnMoveInitMovementDelegate? _Pre;
    internal event OnMoveInitMovementDelegate? _Post;

    public event OnMoveInitMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.MoveInit);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.MoveInit);
        }
    }

    public event OnMoveInitMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.MoveInit);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.MoveInit);
        }
    }

    public void InvokePre( ref IMoveInitMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref IMoveInitMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.MoveInit);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.MoveInit);
    }
}
