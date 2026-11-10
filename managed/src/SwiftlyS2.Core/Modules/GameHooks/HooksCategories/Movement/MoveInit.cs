using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class MoveInitMovementHook( GameHooksService owner ) : IMoveInitMovementHook
{
    internal event OnMoveInitMovementPreDelegate? _Pre;
    internal event OnMoveInitMovementPostDelegate? _Post;

    public event OnMoveInitMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.MoveInit, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.MoveInit, owner);
        }
    }

    public event OnMoveInitMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.MoveInit, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.MoveInit, owner);
        }
    }

    public void InvokePre(ref MoveInitMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref MoveInitMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.MoveInit, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.MoveInit, owner);
    }
}
