using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class OnJumpModernMovementHook : IOnJumpModernMovementHook
{
    private OnOnJumpModernMovementPreDelegate? _Pre;
    private OnOnJumpModernMovementPostDelegate? _Post;

    public event OnOnJumpModernMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.OnJumpModern, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.OnJumpModern, ref _Pre, value, add: false);
    }

    public event OnOnJumpModernMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.OnJumpModern, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.OnJumpModern, ref _Post, value, add: false);
    }

    public void InvokePre(ref OnJumpModernMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref OnJumpModernMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.OnJumpModern, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.OnJumpModern, ref _Post);
    }
}
