using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class TryPlayerMoveMovementHook( GameHooksService owner ) : ITryPlayerMoveMovementHook
{
    internal event OnTryPlayerMoveMovementPreDelegate? _Pre;
    internal event OnTryPlayerMoveMovementPostDelegate? _Post;

    public event OnTryPlayerMoveMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.TryPlayerMove, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.TryPlayerMove, owner);
        }
    }

    public event OnTryPlayerMoveMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.TryPlayerMove, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.TryPlayerMove, owner);
        }
    }

    public void InvokePre(ref TryPlayerMoveMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref TryPlayerMoveMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.TryPlayerMove, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.TryPlayerMove, owner);
    }
}
