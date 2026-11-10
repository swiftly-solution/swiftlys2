using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class TryPlayerMoveMovementHook : ITryPlayerMoveMovementHook
{
    private OnTryPlayerMoveMovementPreDelegate? _Pre;
    private OnTryPlayerMoveMovementPostDelegate? _Post;

    public event OnTryPlayerMoveMovementPreDelegate Pre
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.TryPlayerMove, ref _Pre, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.TryPlayerMove, ref _Pre, value, add: false);
        }
    }

    public event OnTryPlayerMoveMovementPostDelegate Post
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.TryPlayerMove, ref _Post, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.TryPlayerMove, ref _Post, value, add: false);
        }
    }

    public void InvokePre(ref TryPlayerMoveMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref TryPlayerMoveMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.TryPlayerMove, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.TryPlayerMove, ref _Post);
    }
}
