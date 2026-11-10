using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckJumpButtonModernMovementHook : ICheckJumpButtonModernMovementHook
{
    private OnCheckJumpButtonModernMovementPreDelegate? _Pre;
    private OnCheckJumpButtonModernMovementPostDelegate? _Post;

    public event OnCheckJumpButtonModernMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CheckJumpButtonModern, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CheckJumpButtonModern, ref _Pre, value, add: false);
    }

    public event OnCheckJumpButtonModernMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CheckJumpButtonModern, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CheckJumpButtonModern, ref _Post, value, add: false);
    }

    public void InvokePre(ref CheckJumpButtonModernMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref CheckJumpButtonModernMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public bool HasPreListeners => Volatile.Read(ref _Pre) != null;
    public bool HasPostListeners => Volatile.Read(ref _Post) != null;

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.CheckJumpButtonModern, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.CheckJumpButtonModern, ref _Post);
    }
}
