using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class StartTouchEntityHook( GameHooksService owner ) : IStartTouchEntityHook
{
    internal event OnStartTouchEntityPreDelegate? _Pre;
    internal event OnStartTouchEntityPostDelegate? _Post;

    public event OnStartTouchEntityPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.StartTouch, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.StartTouch, owner);
        }
    }

    public event OnStartTouchEntityPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.StartTouch, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.StartTouch, owner);
        }
    }

    public void InvokePre( ref StartTouchEntityPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref StartTouchEntityPostContext ctx ) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.StartTouch, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.StartTouch, owner);
    }
}
