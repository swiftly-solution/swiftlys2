using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class AirAccelerateMovementHook : IAirAccelerateMovementHook
{
    private OnAirAccelerateMovementPreDelegate? _Pre;
    private OnAirAccelerateMovementPostDelegate? _Post;

    public event OnAirAccelerateMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.AirAccelerate, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.AirAccelerate, ref _Pre, value, add: false);
    }

    public event OnAirAccelerateMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.AirAccelerate, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.AirAccelerate, ref _Post, value, add: false);
    }

    public void InvokePre(ref AirAccelerateMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref AirAccelerateMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.AirAccelerate, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.AirAccelerate, ref _Post);
    }
}
