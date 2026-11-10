using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class TouchEntityHook : ITouchEntityHook
{
    private OnTouchEntityPreDelegate? _Pre;
    private OnTouchEntityPostDelegate? _Post;

    public event OnTouchEntityPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.Touch, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.Touch, ref _Pre, value, add: false);
    }

    public event OnTouchEntityPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.Touch, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.Touch, ref _Post, value, add: false);
    }

    public void InvokePre( ref TouchEntityPreContext ctx ) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost( ref TouchEntityPostContext ctx ) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.Touch, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.Touch, ref _Post);
    }
}
