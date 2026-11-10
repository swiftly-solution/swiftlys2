using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GroundAccelerateMovementHook : IGroundAccelerateMovementHook
{
    private OnGroundAccelerateMovementPreDelegate? _Pre;
    private OnGroundAccelerateMovementPostDelegate? _Post;

    public event OnGroundAccelerateMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.GroundAccelerate, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.GroundAccelerate, ref _Pre, value, add: false);
    }

    public event OnGroundAccelerateMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.GroundAccelerate, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.GroundAccelerate, ref _Post, value, add: false);
    }

    public void InvokePre(ref GroundAccelerateMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref GroundAccelerateMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.GroundAccelerate, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.GroundAccelerate, ref _Post);
    }
}
