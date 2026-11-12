using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CanMovePawnHook( GameHooksService owner ) : ICanMovePawnHook
{
    internal event OnCanMovePawnPreDelegate? _Pre;
    internal event OnCanMovePawnPostDelegate? _Post;

    public event OnCanMovePawnPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CanMove, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CanMove, owner);
        }
    }

    public event OnCanMovePawnPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CanMove, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CanMove, owner);
        }
    }

    public void InvokePre(ref CanMovePawnPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CanMovePawnPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CanMove, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CanMove, owner);
    }
}
