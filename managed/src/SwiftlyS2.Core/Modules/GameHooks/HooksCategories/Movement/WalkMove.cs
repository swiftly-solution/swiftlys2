using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class WalkMoveMovementHook( GameHooksService owner ) : IWalkMoveMovementHook
{
    internal event OnWalkMoveMovementPreDelegate? _Pre;
    internal event OnWalkMoveMovementPostDelegate? _Post;

    public event OnWalkMoveMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.WalkMove, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.WalkMove, owner);
        }
    }

    public event OnWalkMoveMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.WalkMove, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.WalkMove, owner);
        }
    }

    public void InvokePre(ref WalkMoveMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref WalkMoveMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.WalkMove, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.WalkMove, owner);
    }
}
