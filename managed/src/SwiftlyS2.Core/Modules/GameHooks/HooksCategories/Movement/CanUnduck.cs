using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CanUnduckMovementHook : ICanUnduckMovementHook
{
    private OnCanUnduckMovementPreDelegate? _Pre;
    private OnCanUnduckMovementPostDelegate? _Post;

    public event OnCanUnduckMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CanUnduck, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CanUnduck, ref _Pre, value, add: false);
    }

    public event OnCanUnduckMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CanUnduck, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CanUnduck, ref _Post, value, add: false);
    }

    public void InvokePre(ref CanUnduckMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref CanUnduckMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.CanUnduck, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.CanUnduck, ref _Post);
    }
}
