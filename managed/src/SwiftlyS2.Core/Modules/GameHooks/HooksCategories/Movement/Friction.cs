using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class FrictionMovementHook( GameHooksService owner ) : IFrictionMovementHook
{
    internal event OnFrictionMovementPreDelegate? _Pre;
    internal event OnFrictionMovementPostDelegate? _Post;

    public event OnFrictionMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.Friction, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.Friction, owner);
        }
    }

    public event OnFrictionMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.Friction, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.Friction, owner);
        }
    }

    public void InvokePre(ref FrictionMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref FrictionMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.Friction, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.Friction, owner);
    }
}
