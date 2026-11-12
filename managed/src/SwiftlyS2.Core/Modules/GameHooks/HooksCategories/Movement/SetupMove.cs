using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class SetupMoveMovementHook( GameHooksService owner ) : ISetupMoveMovementHook
{
    internal event OnSetupMoveMovementPreDelegate? _Pre;
    internal event OnSetupMoveMovementPostDelegate? _Post;

    public event OnSetupMoveMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.SetupMove, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.SetupMove, owner);
        }
    }

    public event OnSetupMoveMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.SetupMove, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.SetupMove, owner);
        }
    }

    public void InvokePre(ref SetupMoveMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref SetupMoveMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.SetupMove, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.SetupMove, owner);
    }
}
