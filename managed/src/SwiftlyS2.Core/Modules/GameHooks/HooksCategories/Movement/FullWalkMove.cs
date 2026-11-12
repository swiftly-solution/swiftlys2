using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class FullWalkMoveMovementHook( GameHooksService owner ) : IFullWalkMoveMovementHook
{
    internal event OnFullWalkMoveMovementPreDelegate? _Pre;
    internal event OnFullWalkMoveMovementPostDelegate? _Post;

    public event OnFullWalkMoveMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.FullWalkMove, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.FullWalkMove, owner);
        }
    }

    public event OnFullWalkMoveMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.FullWalkMove, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.FullWalkMove, owner);
        }
    }

    public void InvokePre(ref FullWalkMoveMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref FullWalkMoveMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.FullWalkMove, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.FullWalkMove, owner);
    }
}
