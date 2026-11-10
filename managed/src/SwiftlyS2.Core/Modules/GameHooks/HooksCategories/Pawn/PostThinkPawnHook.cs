using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class PostThinkPawnHook( GameHooksService owner ) : IPostThinkPawnHook
{
    internal event OnPostThinkPawnPreDelegate? _Pre;
    internal event OnPostThinkPawnPostDelegate? _Post;

    public event OnPostThinkPawnPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.PostThink, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.PostThink, owner);
        }
    }

    public event OnPostThinkPawnPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.PostThink, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.PostThink, owner);
        }
    }

    public void InvokePre(ref PostThinkPawnPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref PostThinkPawnPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.PostThink, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.PostThink, owner);
    }
}
