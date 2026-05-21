using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckJumpButtonLegacyMovementHook : ICheckJumpButtonLegacyMovementHook
{
    internal event OnCheckJumpButtonLegacyMovementPreDelegate? _Pre;
    internal event OnCheckJumpButtonLegacyMovementPostDelegate? _Post;

    public event OnCheckJumpButtonLegacyMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckJumpButtonLegacy);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonLegacy);
        }
    }

    public event OnCheckJumpButtonLegacyMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckJumpButtonLegacy);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonLegacy);
        }
    }

    public void InvokePre( ref CheckJumpButtonLegacyMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref CheckJumpButtonLegacyMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonLegacy);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonLegacy);
    }
}
