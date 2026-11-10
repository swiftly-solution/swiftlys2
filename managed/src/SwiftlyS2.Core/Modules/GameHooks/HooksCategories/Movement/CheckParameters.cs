using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckParametersMovementHook : ICheckParametersMovementHook
{
    private OnCheckParametersMovementPreDelegate? _Pre;
    private OnCheckParametersMovementPostDelegate? _Post;

    public event OnCheckParametersMovementPreDelegate Pre
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.CheckParameters, ref _Pre, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.CheckParameters, ref _Pre, value, add: false);
        }
    }

    public event OnCheckParametersMovementPostDelegate Post
    {
        add
        {
            GameHooksPublisher.UpdateListener(HookListener.CheckParameters, ref _Post, value, add: true);
        }
        remove
        {
            GameHooksPublisher.UpdateListener(HookListener.CheckParameters, ref _Post, value, add: false);
        }
    }

    public void InvokePre(ref CheckParametersMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref CheckParametersMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.CheckParameters, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.CheckParameters, ref _Post);
    }
}
