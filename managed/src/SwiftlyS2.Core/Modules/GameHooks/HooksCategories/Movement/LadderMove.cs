using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class LadderMoveMovementHook( GameHooksService owner ) : ILadderMoveMovementHook
{
    internal event OnLadderMoveMovementPreDelegate? _Pre;
    internal event OnLadderMoveMovementPostDelegate? _Post;

    public event OnLadderMoveMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.LadderMove, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.LadderMove, owner);
        }
    }

    public event OnLadderMoveMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.LadderMove, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.LadderMove, owner);
        }
    }

    public void InvokePre(ref LadderMoveMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref LadderMoveMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.LadderMove, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.LadderMove, owner);
    }
}
