using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class OnJumpLegacyMovementHook( GameHooksService owner ) : IOnJumpLegacyMovementHook
{
    internal event OnOnJumpLegacyMovementPreDelegate? _Pre;
    internal event OnOnJumpLegacyMovementPostDelegate? _Post;

    public event OnOnJumpLegacyMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.OnJumpLegacy, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpLegacy, owner);
        }
    }

    public event OnOnJumpLegacyMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.OnJumpLegacy, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpLegacy, owner);
        }
    }

    public void InvokePre(ref OnJumpLegacyMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref OnJumpLegacyMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpLegacy, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpLegacy, owner);
    }
}
