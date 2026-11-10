using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckVelocityMovementHook : ICheckVelocityMovementHook
{
    private OnCheckVelocityMovementPreDelegate? _Pre;
    private OnCheckVelocityMovementPostDelegate? _Post;

    public event OnCheckVelocityMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CheckVelocity, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CheckVelocity, ref _Pre, value, add: false);
    }

    public event OnCheckVelocityMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CheckVelocity, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CheckVelocity, ref _Post, value, add: false);
    }

    public void InvokePre(ref CheckVelocityMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref CheckVelocityMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.CheckVelocity, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.CheckVelocity, ref _Post);
    }
}
