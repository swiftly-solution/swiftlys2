using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CanMovePawnHook : ICanMovePawnHook
{
    private OnCanMovePawnPreDelegate? _Pre;
    private OnCanMovePawnPostDelegate? _Post;

    public event OnCanMovePawnPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CanMove, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CanMove, ref _Pre, value, add: false);
    }

    public event OnCanMovePawnPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CanMove, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CanMove, ref _Post, value, add: false);
    }

    public void InvokePre(ref CanMovePawnPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref CanMovePawnPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.CanMove, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.CanMove, ref _Post);
    }
}
