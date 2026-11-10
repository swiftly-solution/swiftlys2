using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class SetupMoveMovementHook : ISetupMoveMovementHook
{
    private OnSetupMoveMovementPreDelegate? _Pre;
    private OnSetupMoveMovementPostDelegate? _Post;

    public event OnSetupMoveMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.SetupMove, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.SetupMove, ref _Pre, value, add: false);
    }

    public event OnSetupMoveMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.SetupMove, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.SetupMove, ref _Post, value, add: false);
    }

    public void InvokePre(ref SetupMoveMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref SetupMoveMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.SetupMove, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.SetupMove, ref _Post);
    }
}
