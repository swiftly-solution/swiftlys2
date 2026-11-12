using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckJumpButtonModernMovementHook( GameHooksService owner ) : ICheckJumpButtonModernMovementHook
{
    internal event OnCheckJumpButtonModernMovementPreDelegate? _Pre;
    internal event OnCheckJumpButtonModernMovementPostDelegate? _Post;

    public event OnCheckJumpButtonModernMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckJumpButtonModern, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonModern, owner);
        }
    }

    public event OnCheckJumpButtonModernMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckJumpButtonModern, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonModern, owner);
        }
    }

    public void InvokePre(ref CheckJumpButtonModernMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CheckJumpButtonModernMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonModern, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckJumpButtonModern, owner);
    }
}
