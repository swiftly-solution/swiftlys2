using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class StartTouchEntityHook : IStartTouchEntityHook
{
    private OnStartTouchEntityPreDelegate? _Pre;
    private OnStartTouchEntityPostDelegate? _Post;

    public event OnStartTouchEntityPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.StartTouch, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.StartTouch, ref _Pre, value, add: false);
    }

    public event OnStartTouchEntityPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.StartTouch, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.StartTouch, ref _Post, value, add: false);
    }

    public void InvokePre( ref StartTouchEntityPreContext ctx ) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost( ref StartTouchEntityPostContext ctx ) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.StartTouch, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.StartTouch, ref _Post);
    }
}
