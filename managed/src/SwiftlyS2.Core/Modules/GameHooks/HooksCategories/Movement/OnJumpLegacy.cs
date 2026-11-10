using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class OnJumpLegacyMovementHook : IOnJumpLegacyMovementHook
{
    private OnOnJumpLegacyMovementPreDelegate? _Pre;
    private OnOnJumpLegacyMovementPostDelegate? _Post;

    public event OnOnJumpLegacyMovementPreDelegate Pre
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.OnJumpLegacy, ref _Pre, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.OnJumpLegacy, ref _Pre, value, add: false);
        }
    }

    public event OnOnJumpLegacyMovementPostDelegate Post
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.OnJumpLegacy, ref _Post, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.OnJumpLegacy, ref _Post, value, add: false);
        }
    }

    public void InvokePre(ref OnJumpLegacyMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref OnJumpLegacyMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.OnJumpLegacy, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.OnJumpLegacy, ref _Post);
    }
}
