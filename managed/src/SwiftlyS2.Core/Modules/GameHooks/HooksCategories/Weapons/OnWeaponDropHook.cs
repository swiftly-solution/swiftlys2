using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class WeaponDropHook( GameHooksService owner ) : IWeaponDropHook
{
    internal event OnWeaponDropPreDelegate? _Pre;
    internal event OnWeaponDropPostDelegate? _Post;

    public event OnWeaponDropPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.WeaponDrop, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.WeaponDrop, owner);
        }
    }

    public event OnWeaponDropPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.WeaponDrop, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.WeaponDrop, owner);
        }
    }

    public void InvokePre(ref WeaponDropPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref WeaponDropPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.WeaponDrop, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.WeaponDrop, owner);
    }
}
