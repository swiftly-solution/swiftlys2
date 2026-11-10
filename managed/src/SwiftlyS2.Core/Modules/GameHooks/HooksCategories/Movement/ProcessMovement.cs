using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class ProcessMovementMovementHook( GameHooksService owner ) : IProcessMovementMovementHook
{
    internal event OnProcessMovementMovementPreDelegate? _Pre;
    internal event OnProcessMovementMovementPostDelegate? _Post;

    public event OnProcessMovementMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.ProcessMovement, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessMovement, owner);
        }
    }

    public event OnProcessMovementMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.ProcessMovement, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessMovement, owner);
        }
    }

    public void InvokePre(ref ProcessMovementMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref ProcessMovementMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessMovement, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessMovement, owner);
    }
}
