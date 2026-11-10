using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class TakeDamageEntityHook( GameHooksService owner ) : ITakeDamageEntityHook
{
    internal event OnTakeDamageEntityPreDelegate? _Pre;
    internal event OnTakeDamageEntityPostDelegate? _Post;

    public event OnTakeDamageEntityPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.TakeDamage, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.TakeDamage, owner);
        }
    }

    public event OnTakeDamageEntityPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.TakeDamage, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.TakeDamage, owner);
        }
    }

    public void InvokePre( ref TakeDamageEntityPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref TakeDamageEntityPostContext ctx ) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.TakeDamage, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.TakeDamage, owner);
    }
}
