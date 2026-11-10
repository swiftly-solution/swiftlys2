using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class WalkMoveMovementHook : IWalkMoveMovementHook
{
    private OnWalkMoveMovementPreDelegate? _Pre;
    private OnWalkMoveMovementPostDelegate? _Post;

    public event OnWalkMoveMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.WalkMove, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.WalkMove, ref _Pre, value, add: false);
    }

    public event OnWalkMoveMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.WalkMove, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.WalkMove, ref _Post, value, add: false);
    }

    public void InvokePre(ref WalkMoveMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref WalkMoveMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.WalkMove, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.WalkMove, ref _Post);
    }
}
