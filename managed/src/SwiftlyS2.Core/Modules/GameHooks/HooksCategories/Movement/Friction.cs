using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class FrictionMovementHook : IFrictionMovementHook
{
    private OnFrictionMovementPreDelegate? _Pre;
    private OnFrictionMovementPostDelegate? _Post;

    public event OnFrictionMovementPreDelegate Pre
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.Friction, ref _Pre, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.Friction, ref _Pre, value, add: false);
        }
    }

    public event OnFrictionMovementPostDelegate Post
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.Friction, ref _Post, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.Friction, ref _Post, value, add: false);
        }
    }

    public void InvokePre(ref FrictionMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref FrictionMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.Friction, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.Friction, ref _Post);
    }
}
