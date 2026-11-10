using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class AcceptInputEntityHook( GameHooksService owner ) : IAcceptInputEntityHook
{
    internal event OnAcceptInputEntityPreDelegate? _Pre;
    internal event OnAcceptInputEntityPostDelegate? _Post;

    public event OnAcceptInputEntityPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.AcceptInput, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.AcceptInput, owner);
        }
    }

    public event OnAcceptInputEntityPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.AcceptInput, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.AcceptInput, owner);
        }
    }

    public void InvokePre( ref AcceptInputEntityPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref AcceptInputEntityPostContext ctx ) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.AcceptInput, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.AcceptInput, owner);
    }
}
