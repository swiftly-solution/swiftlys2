using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class TouchEntityHook( GameHooksService owner ) : ITouchEntityHook
{
    internal event OnTouchEntityPreDelegate? _Pre;
    internal event OnTouchEntityPostDelegate? _Post;

    public event OnTouchEntityPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.Touch, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.Touch, owner);
        }
    }

    public event OnTouchEntityPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.Touch, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.Touch, owner);
        }
    }

    public void InvokePre( ref TouchEntityPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref TouchEntityPostContext ctx ) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.Touch, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.Touch, owner);
    }
}
