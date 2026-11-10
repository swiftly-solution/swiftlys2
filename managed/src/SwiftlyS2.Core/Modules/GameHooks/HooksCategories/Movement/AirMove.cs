using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class AirMoveMovementHook : IAirMoveMovementHook
{
    private OnAirMoveMovementPreDelegate? _Pre;
    private OnAirMoveMovementPostDelegate? _Post;

    public event OnAirMoveMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.AirMove, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.AirMove, ref _Pre, value, add: false);
    }

    public event OnAirMoveMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.AirMove, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.AirMove, ref _Post, value, add: false);
    }

    public void InvokePre(ref AirMoveMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref AirMoveMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.AirMove, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.AirMove, ref _Post);
    }
}
