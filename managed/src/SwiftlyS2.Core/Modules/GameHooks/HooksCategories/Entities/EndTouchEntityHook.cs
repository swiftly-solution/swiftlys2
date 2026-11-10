using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class EndTouchEntityHook : IEndTouchEntityHook
{
    private OnEndTouchEntityPreDelegate? _Pre;
    private OnEndTouchEntityPostDelegate? _Post;

    public event OnEndTouchEntityPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.EndTouch, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.EndTouch, ref _Pre, value, add: false);
    }

    public event OnEndTouchEntityPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.EndTouch, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.EndTouch, ref _Post, value, add: false);
    }

    public void InvokePre( ref EndTouchEntityPreContext ctx ) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost( ref EndTouchEntityPostContext ctx ) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.EndTouch, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.EndTouch, ref _Post);
    }
}
