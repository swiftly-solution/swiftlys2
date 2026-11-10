using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckJumpButtonLegacyMovementHook( GameHooksService owner ) : ICheckJumpButtonLegacyMovementHook
{
    internal event OnCheckJumpButtonLegacyMovementPreDelegate? _Pre;
    internal event OnCheckJumpButtonLegacyMovementPostDelegate? _Post;

    public event OnCheckJumpButtonLegacyMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckJumpButtonLegacy, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonLegacy, owner);
        }
    }

    public event OnCheckJumpButtonLegacyMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckJumpButtonLegacy, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonLegacy, owner);
        }
    }

    public void InvokePre(ref CheckJumpButtonLegacyMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CheckJumpButtonLegacyMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonLegacy, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonLegacy, owner);
    }
}
