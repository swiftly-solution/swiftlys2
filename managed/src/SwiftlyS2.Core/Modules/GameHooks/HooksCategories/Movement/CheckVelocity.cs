using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckVelocityMovementHook( GameHooksService owner ) : ICheckVelocityMovementHook
{
    internal event OnCheckVelocityMovementPreDelegate? _Pre;
    internal event OnCheckVelocityMovementPostDelegate? _Post;

    public event OnCheckVelocityMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckVelocity, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckVelocity, owner);
        }
    }

    public event OnCheckVelocityMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckVelocity, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckVelocity, owner);
        }
    }

    public void InvokePre(ref CheckVelocityMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CheckVelocityMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckVelocity, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckVelocity, owner);
    }
}
