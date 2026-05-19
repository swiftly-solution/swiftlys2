using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class SetupMoveMovementData : ISetupMoveMovement
{
    public required IPlayer Player { get; set; }
    public required IUserCmd UserCmd { get; init; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class SetupMoveMovementEvents : ISetupMoveMovementEvents
{
    internal event OnSetupMoveMovementDelegate? _Pre;
    internal event OnSetupMoveMovementDelegate? _Post;

    public event OnSetupMoveMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.SetupMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.SetupMove);
        }
    }

    public event OnSetupMoveMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.SetupMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.SetupMove);
        }
    }

    public void InvokePre( ref ISetupMoveMovement data )
    {
        _Pre?.Invoke(ref data);
    }

    public void InvokePost( ref ISetupMoveMovement data )
    {
        _Post?.Invoke(ref data);
    }

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.SetupMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.SetupMove);
    }
}
