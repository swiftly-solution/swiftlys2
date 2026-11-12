using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class WaterMoveMovementHook( GameHooksService owner ) : IWaterMoveMovementHook
{
    internal event OnWaterMoveMovementPreDelegate? _Pre;
    internal event OnWaterMoveMovementPostDelegate? _Post;

    public event OnWaterMoveMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.WaterMove, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.WaterMove, owner);
        }
    }

    public event OnWaterMoveMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.WaterMove, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.WaterMove, owner);
        }
    }

    public void InvokePre(ref WaterMoveMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref WaterMoveMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.WaterMove, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.WaterMove, owner);
    }
}
