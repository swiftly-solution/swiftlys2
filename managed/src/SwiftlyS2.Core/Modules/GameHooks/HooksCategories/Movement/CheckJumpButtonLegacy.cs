using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckJumpButtonLegacyMovementData : ICheckJumpButtonLegacyMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class CheckJumpButtonLegacyMovementEvents : ICheckJumpButtonLegacyMovementEvents
{
    internal event OnCheckJumpButtonLegacyMovementDelegate? _Pre;
    internal event OnCheckJumpButtonLegacyMovementDelegate? _Post;

    public event OnCheckJumpButtonLegacyMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckJumpButtonLegacy);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonLegacy);
        }
    }

    public event OnCheckJumpButtonLegacyMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckJumpButtonLegacy);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonLegacy);
        }
    }

    public void InvokePre( ref ICheckJumpButtonLegacyMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref ICheckJumpButtonLegacyMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonLegacy);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonLegacy);
    }
}
