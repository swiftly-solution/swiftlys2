using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckJumpButtonLegacyMovementHook : ICheckJumpButtonLegacyMovementHook
{
    private OnCheckJumpButtonLegacyMovementPreDelegate? _Pre;
    private OnCheckJumpButtonLegacyMovementPostDelegate? _Post;

    public event OnCheckJumpButtonLegacyMovementPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CheckJumpButtonLegacy, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CheckJumpButtonLegacy, ref _Pre, value, add: false);
    }

    public event OnCheckJumpButtonLegacyMovementPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.CheckJumpButtonLegacy, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.CheckJumpButtonLegacy, ref _Post, value, add: false);
    }

    public void InvokePre(ref CheckJumpButtonLegacyMovementPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref CheckJumpButtonLegacyMovementPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.CheckJumpButtonLegacy, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.CheckJumpButtonLegacy, ref _Post);
    }
}
