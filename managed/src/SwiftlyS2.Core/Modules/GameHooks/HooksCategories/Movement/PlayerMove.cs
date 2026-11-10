using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class PlayerMoveMovementHook : IPlayerMoveMovementHook
{
    private OnPlayerMoveMovementPreDelegate? _Pre;
    private OnPlayerMoveMovementPostDelegate? _Post;

    public event OnPlayerMoveMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.PlayerMove, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.PlayerMove, ref _Pre, value, add: false);
    }

    public event OnPlayerMoveMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.PlayerMove, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.PlayerMove, ref _Post, value, add: false);
    }

    public void InvokePre(ref PlayerMoveMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref PlayerMoveMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.PlayerMove, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.PlayerMove, ref _Post);
    }
}
