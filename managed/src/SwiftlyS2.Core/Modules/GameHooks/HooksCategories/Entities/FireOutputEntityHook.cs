using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class FireOutputEntityHook : IFireOutputEntityHook
{
    private OnFireOutputEntityPreDelegate? _Pre;
    private OnFireOutputEntityPostDelegate? _Post;

    public event OnFireOutputEntityPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.FireOutput, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.FireOutput, ref _Pre, value, add: false);
    }

    public event OnFireOutputEntityPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.FireOutput, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.FireOutput, ref _Post, value, add: false);
    }

    public void InvokePre( ref FireOutputEntityPreContext ctx ) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost( ref FireOutputEntityPostContext ctx ) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.FireOutput, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.FireOutput, ref _Post);
    }
}
