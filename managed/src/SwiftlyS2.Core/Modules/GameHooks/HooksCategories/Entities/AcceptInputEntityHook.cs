using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class AcceptInputEntityHook : IAcceptInputEntityHook
{
    private OnAcceptInputEntityPreDelegate? _Pre;
    private OnAcceptInputEntityPostDelegate? _Post;

    public event OnAcceptInputEntityPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.AcceptInput, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.AcceptInput, ref _Pre, value, add: false);
    }

    public event OnAcceptInputEntityPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.AcceptInput, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.AcceptInput, ref _Post, value, add: false);
    }

    public void InvokePre( ref AcceptInputEntityPreContext ctx ) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost( ref AcceptInputEntityPostContext ctx ) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.AcceptInput, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.AcceptInput, ref _Post);
    }
}
