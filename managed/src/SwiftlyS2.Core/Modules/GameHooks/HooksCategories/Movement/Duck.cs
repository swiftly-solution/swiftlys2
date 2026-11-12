using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class DuckMovementHook( GameHooksService owner ) : IDuckMovementHook
{
    internal event OnDuckMovementPreDelegate? _Pre;
    internal event OnDuckMovementPostDelegate? _Post;

    public event OnDuckMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.Duck, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.Duck, owner);
        }
    }

    public event OnDuckMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.Duck, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.Duck, owner);
        }
    }

    public void InvokePre(ref DuckMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref DuckMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.Duck, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.Duck, owner);
    }
}
