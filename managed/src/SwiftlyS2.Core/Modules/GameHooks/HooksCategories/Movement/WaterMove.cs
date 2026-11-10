using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class WaterMoveMovementHook : IWaterMoveMovementHook
{
    private OnWaterMoveMovementPreDelegate? _Pre;
    private OnWaterMoveMovementPostDelegate? _Post;

    public event OnWaterMoveMovementPreDelegate Pre
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.WaterMove, ref _Pre, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.WaterMove, ref _Pre, value, add: false);
        }
    }

    public event OnWaterMoveMovementPostDelegate Post
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.WaterMove, ref _Post, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.WaterMove, ref _Post, value, add: false);
        }
    }

    public void InvokePre(ref WaterMoveMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref WaterMoveMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.WaterMove, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.WaterMove, ref _Post);
    }
}
