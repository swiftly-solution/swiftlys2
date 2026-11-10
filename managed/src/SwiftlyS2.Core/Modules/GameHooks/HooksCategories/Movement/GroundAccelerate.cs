using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class GroundAccelerateMovementHook( GameHooksService owner ) : IGroundAccelerateMovementHook
{
    internal event OnGroundAccelerateMovementPreDelegate? _Pre;
    internal event OnGroundAccelerateMovementPostDelegate? _Post;

    public event OnGroundAccelerateMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.GroundAccelerate, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.GroundAccelerate, owner);
        }
    }

    public event OnGroundAccelerateMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.GroundAccelerate, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.GroundAccelerate, owner);
        }
    }

    public void InvokePre(ref GroundAccelerateMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref GroundAccelerateMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.GroundAccelerate, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.GroundAccelerate, owner);
    }
}
