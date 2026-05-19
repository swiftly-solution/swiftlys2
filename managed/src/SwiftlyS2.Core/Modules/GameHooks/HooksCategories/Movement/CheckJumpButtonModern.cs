using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckJumpButtonModernMovementData : ICheckJumpButtonModernMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class CheckJumpButtonModernMovementEvents : ICheckJumpButtonModernMovementEvents
{
    internal event OnCheckJumpButtonModernMovementDelegate? _Pre;
    internal event OnCheckJumpButtonModernMovementDelegate? _Post;

    public event OnCheckJumpButtonModernMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckJumpButtonModern);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonModern);
        }
    }

    public event OnCheckJumpButtonModernMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckJumpButtonModern);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonModern);
        }
    }

    public void InvokePre( ref ICheckJumpButtonModernMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref ICheckJumpButtonModernMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonModern);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonModern);
    }
}
