using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class OnJumpLegacyMovementHook : IOnJumpLegacyMovementHook
{
    internal event OnOnJumpLegacyMovementPreDelegate? _Pre;
    internal event OnOnJumpLegacyMovementPostDelegate? _Post;

    public event OnOnJumpLegacyMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.OnJumpLegacy);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpLegacy);
        }
    }

    public event OnOnJumpLegacyMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.OnJumpLegacy);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpLegacy);
        }
    }

    public void InvokePre( ref OnJumpLegacyMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref OnJumpLegacyMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpLegacy);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpLegacy);
    }
}
