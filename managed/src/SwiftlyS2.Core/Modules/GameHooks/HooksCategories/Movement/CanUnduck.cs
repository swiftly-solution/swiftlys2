using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CanUnduckMovementHook( GameHooksService owner ) : ICanUnduckMovementHook
{
    internal event OnCanUnduckMovementPreDelegate? _Pre;
    internal event OnCanUnduckMovementPostDelegate? _Post;

    public event OnCanUnduckMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CanUnduck, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CanUnduck, owner);
        }
    }

    public event OnCanUnduckMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CanUnduck, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CanUnduck, owner);
        }
    }

    public void InvokePre(ref CanUnduckMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CanUnduckMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CanUnduck, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CanUnduck, owner);
    }
}
