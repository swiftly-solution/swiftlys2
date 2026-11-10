using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class ProcessMovementMovementHook : IProcessMovementMovementHook
{
    private OnProcessMovementMovementPreDelegate? _Pre;
    private OnProcessMovementMovementPostDelegate? _Post;

    public event OnProcessMovementMovementPreDelegate Pre
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.ProcessMovement, ref _Pre, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.ProcessMovement, ref _Pre, value, add: false);
        }
    }

    public event OnProcessMovementMovementPostDelegate Post
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.ProcessMovement, ref _Post, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.ProcessMovement, ref _Post, value, add: false);
        }
    }

    public void InvokePre(ref ProcessMovementMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref ProcessMovementMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.ProcessMovement, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.ProcessMovement, ref _Post);
    }
}
