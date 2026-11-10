using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class SimulateUserCommandsHook : ISimulateUserCommandsHook
{
    private OnSimulateUserCommandsPreDelegate? _Pre;
    private OnSimulateUserCommandsPostDelegate? _Post;

    public event OnSimulateUserCommandsPreDelegate Pre
    {
        add => GameHooksPublisher.UpdateListener(HookListener.SimulateUserCommands, ref _Pre, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.SimulateUserCommands, ref _Pre, value, add: false);
    }

    public event OnSimulateUserCommandsPostDelegate Post
    {
        add => GameHooksPublisher.UpdateListener(HookListener.SimulateUserCommands, ref _Post, value, add: true);
        remove => GameHooksPublisher.UpdateListener(HookListener.SimulateUserCommands, ref _Post, value, add: false);
    }

    public void InvokePre(ref SimulateUserCommandsPreContext ctx) => Volatile.Read(ref _Pre)?.Invoke(ref ctx);
    public void InvokePost(ref SimulateUserCommandsPostContext ctx) => Volatile.Read(ref _Post)?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        GameHooksPublisher.ClearListeners(HookListener.SimulateUserCommands, ref _Pre);
        GameHooksPublisher.ClearListeners(HookListener.SimulateUserCommands, ref _Post);
    }
}
