using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CanUseWeaponHook : ICanUseWeaponHook
{
    private OnCanUseWeaponPreDelegate? _Pre;
    private OnCanUseWeaponPostDelegate? _Post;

    public event OnCanUseWeaponPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CanUse, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CanUse, ref _Pre, value, add: false);
    }

    public event OnCanUseWeaponPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CanUse, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CanUse, ref _Post, value, add: false);
    }

    public void InvokePre(ref CanUseWeaponPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref CanUseWeaponPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.CanUse, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.CanUse, ref _Post);
    }
}
