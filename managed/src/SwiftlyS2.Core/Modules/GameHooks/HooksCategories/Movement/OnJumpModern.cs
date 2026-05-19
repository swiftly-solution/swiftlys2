using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class OnJumpModernMovementData : IOnJumpModernMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class OnJumpModernMovementEvents : IOnJumpModernMovementEvents
{
    internal event OnOnJumpModernMovementDelegate? _Pre;
    internal event OnOnJumpModernMovementDelegate? _Post;

    public event OnOnJumpModernMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.OnJumpModern);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpModern);
        }
    }

    public event OnOnJumpModernMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.OnJumpModern);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpModern);
        }
    }

    public void InvokePre( ref IOnJumpModernMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref IOnJumpModernMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpModern);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpModern);
    }
}
