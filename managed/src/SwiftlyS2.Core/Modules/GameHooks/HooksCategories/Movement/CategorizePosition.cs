using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CategorizePositionMovementHook : ICategorizePositionMovementHook
{
    private OnCategorizePositionMovementPreDelegate? _Pre;
    private OnCategorizePositionMovementPostDelegate? _Post;

    public event OnCategorizePositionMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CategorizePosition, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CategorizePosition, ref _Pre, value, add: false);
    }

    public event OnCategorizePositionMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CategorizePosition, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CategorizePosition, ref _Post, value, add: false);
    }

    public void InvokePre(ref CategorizePositionMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref CategorizePositionMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.CategorizePosition, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.CategorizePosition, ref _Post);
    }
}
