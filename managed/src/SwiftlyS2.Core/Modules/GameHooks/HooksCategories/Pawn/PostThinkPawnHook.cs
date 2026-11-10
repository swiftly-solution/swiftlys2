using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class PostThinkPawnHook : IPostThinkPawnHook
{
    private OnPostThinkPawnPreDelegate? _Pre;
    private OnPostThinkPawnPostDelegate? _Post;

    public event OnPostThinkPawnPreDelegate Pre
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.PostThink, ref _Pre, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.PostThink, ref _Pre, value, add: false);
        }
    }

    public event OnPostThinkPawnPostDelegate Post
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.PostThink, ref _Post, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.PostThink, ref _Post, value, add: false);
        }
    }

    public void InvokePre(ref PostThinkPawnPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref PostThinkPawnPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.PostThink, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.PostThink, ref _Post);
    }
}
