using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CanAcquireItemHook : ICanAcquireItemHook
{
    private OnCanAcquireItemPreDelegate? _Pre;
    private OnCanAcquireItemPostDelegate? _Post;

    public event OnCanAcquireItemPreDelegate Pre
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.CanAcquire, ref _Pre, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.CanAcquire, ref _Pre, value, add: false);
        }
    }

    public event OnCanAcquireItemPostDelegate Post
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.CanAcquire, ref _Post, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.CanAcquire, ref _Post, value, add: false);
        }
    }

    public void InvokePre(ref CanAcquireItemPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref CanAcquireItemPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.CanAcquire, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.CanAcquire, ref _Post);
    }
}
