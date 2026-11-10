using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class LadderMoveMovementHook : ILadderMoveMovementHook
{
    private OnLadderMoveMovementPreDelegate? _Pre;
    private OnLadderMoveMovementPostDelegate? _Post;

    public event OnLadderMoveMovementPreDelegate Pre
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.LadderMove, ref _Pre, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.LadderMove, ref _Pre, value, add: false);
        }
    }

    public event OnLadderMoveMovementPostDelegate Post
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.LadderMove, ref _Post, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.LadderMove, ref _Post, value, add: false);
        }
    }

    public void InvokePre(ref LadderMoveMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref LadderMoveMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.LadderMove, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.LadderMove, ref _Post);
    }
}
