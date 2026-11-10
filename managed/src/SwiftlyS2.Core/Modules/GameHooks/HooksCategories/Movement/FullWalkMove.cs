using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class FullWalkMoveMovementHook : IFullWalkMoveMovementHook
{
    private OnFullWalkMoveMovementPreDelegate? _Pre;
    private OnFullWalkMoveMovementPostDelegate? _Post;

    public event OnFullWalkMoveMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.FullWalkMove, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.FullWalkMove, ref _Pre, value, add: false);
    }

    public event OnFullWalkMoveMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.FullWalkMove, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.FullWalkMove, ref _Post, value, add: false);
    }

    public void InvokePre(ref FullWalkMoveMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref FullWalkMoveMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.FullWalkMove, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.FullWalkMove, ref _Post);
    }
}
