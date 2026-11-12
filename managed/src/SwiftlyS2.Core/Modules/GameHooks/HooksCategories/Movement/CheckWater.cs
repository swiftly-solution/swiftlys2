using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckWaterMovementHook( GameHooksService owner ) : ICheckWaterMovementHook
{
    internal event OnCheckWaterMovementPreDelegate? _Pre;
    internal event OnCheckWaterMovementPostDelegate? _Post;

    public event OnCheckWaterMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckWater, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckWater, owner);
        }
    }

    public event OnCheckWaterMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckWater, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckWater, owner);
        }
    }

    public void InvokePre(ref CheckWaterMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CheckWaterMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckWater, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckWater, owner);
    }
}
