using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class OnJumpLegacyMovementData : IOnJumpLegacyMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class OnJumpLegacyMovementEvents : IOnJumpLegacyMovementEvents
{
    internal event OnOnJumpLegacyMovementDelegate? _Pre;
    internal event OnOnJumpLegacyMovementDelegate? _Post;

    public event OnOnJumpLegacyMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.OnJumpLegacy);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpLegacy);
        }
    }

    public event OnOnJumpLegacyMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.OnJumpLegacy);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpLegacy);
        }
    }

    public void InvokePre( ref IOnJumpLegacyMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref IOnJumpLegacyMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpLegacy);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpLegacy);
    }
}
