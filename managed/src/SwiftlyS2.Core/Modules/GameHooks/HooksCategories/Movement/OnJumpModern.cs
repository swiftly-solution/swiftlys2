using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class OnJumpModernMovementHook( GameHooksService owner ) : IOnJumpModernMovementHook
{
    internal event OnOnJumpModernMovementPreDelegate? _Pre;
    internal event OnOnJumpModernMovementPostDelegate? _Post;

    public event OnOnJumpModernMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.OnJumpModern, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpModern, owner);
        }
    }

    public event OnOnJumpModernMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.OnJumpModern, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpModern, owner);
        }
    }

    public void InvokePre(ref OnJumpModernMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref OnJumpModernMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpModern, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.OnJumpModern, owner);
    }
}
