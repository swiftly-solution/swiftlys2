using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class DuckMovementHook : IDuckMovementHook
{
    private OnDuckMovementPreDelegate? _Pre;
    private OnDuckMovementPostDelegate? _Post;

    public event OnDuckMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.Duck, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.Duck, ref _Pre, value, add: false);
    }

    public event OnDuckMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.Duck, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.Duck, ref _Post, value, add: false);
    }

    public void InvokePre(ref DuckMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref DuckMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.Duck, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.Duck, ref _Post);
    }
}
