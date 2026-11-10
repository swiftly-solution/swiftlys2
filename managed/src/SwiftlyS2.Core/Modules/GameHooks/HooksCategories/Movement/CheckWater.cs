using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckWaterMovementHook : ICheckWaterMovementHook
{
    private OnCheckWaterMovementPreDelegate? _Pre;
    private OnCheckWaterMovementPostDelegate? _Post;

    public event OnCheckWaterMovementPreDelegate Pre
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.CheckWater, ref _Pre, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.CheckWater, ref _Pre, value, add: false);
        }
    }

    public event OnCheckWaterMovementPostDelegate Post
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.CheckWater, ref _Post, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.CheckWater, ref _Post, value, add: false);
        }
    }

    public void InvokePre(ref CheckWaterMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref CheckWaterMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.CheckWater, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.CheckWater, ref _Post);
    }
}
