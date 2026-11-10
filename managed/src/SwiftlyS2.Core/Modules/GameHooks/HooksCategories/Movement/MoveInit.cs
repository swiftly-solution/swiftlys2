using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class MoveInitMovementHook : IMoveInitMovementHook
{
    private OnMoveInitMovementPreDelegate? _Pre;
    private OnMoveInitMovementPostDelegate? _Post;

    public event OnMoveInitMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.MoveInit, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.MoveInit, ref _Pre, value, add: false);
    }

    public event OnMoveInitMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.MoveInit, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.MoveInit, ref _Post, value, add: false);
    }

    public void InvokePre(ref MoveInitMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref MoveInitMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.MoveInit, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.MoveInit, ref _Post);
    }
}
