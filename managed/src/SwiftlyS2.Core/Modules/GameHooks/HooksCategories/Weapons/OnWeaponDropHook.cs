using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class WeaponDropHook : IWeaponDropHook
{
    private OnWeaponDropPreDelegate? _Pre;
    private OnWeaponDropPostDelegate? _Post;

    public event OnWeaponDropPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.WeaponDrop, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.WeaponDrop, ref _Pre, value, add: false);
    }

    public event OnWeaponDropPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.WeaponDrop, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.WeaponDrop, ref _Post, value, add: false);
    }

    public void InvokePre(ref WeaponDropPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref WeaponDropPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.WeaponDrop, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.WeaponDrop, ref _Post);
    }
}
