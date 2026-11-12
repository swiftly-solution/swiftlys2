using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class EndTouchEntityHook( GameHooksService owner ) : IEndTouchEntityHook
{
    internal event OnEndTouchEntityPreDelegate? _Pre;
    internal event OnEndTouchEntityPostDelegate? _Post;

    public event OnEndTouchEntityPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.EndTouch, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.EndTouch, owner);
        }
    }

    public event OnEndTouchEntityPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.EndTouch, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.EndTouch, owner);
        }
    }

    public void InvokePre( ref EndTouchEntityPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref EndTouchEntityPostContext ctx ) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.EndTouch, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.EndTouch, owner);
    }
}
