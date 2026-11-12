using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class AirAccelerateMovementHook( GameHooksService owner ) : IAirAccelerateMovementHook
{
    internal event OnAirAccelerateMovementPreDelegate? _Pre;
    internal event OnAirAccelerateMovementPostDelegate? _Post;

    public event OnAirAccelerateMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.AirAccelerate, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.AirAccelerate, owner);
        }
    }

    public event OnAirAccelerateMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.AirAccelerate, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.AirAccelerate, owner);
        }
    }

    public void InvokePre(ref AirAccelerateMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref AirAccelerateMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.AirAccelerate, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.AirAccelerate, owner);
    }
}
