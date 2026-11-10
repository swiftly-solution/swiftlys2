using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckFallingMovementHook : ICheckFallingMovementHook
{
    private OnCheckFallingMovementPreDelegate? _Pre;
    private OnCheckFallingMovementPostDelegate? _Post;

    public event OnCheckFallingMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CheckFalling, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CheckFalling, ref _Pre, value, add: false);
    }

    public event OnCheckFallingMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CheckFalling, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CheckFalling, ref _Post, value, add: false);
    }

    public void InvokePre(ref CheckFallingMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref CheckFallingMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.CheckFalling, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.CheckFalling, ref _Post);
    }
}
