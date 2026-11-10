using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class SimulateUserCommandsHook( GameHooksService owner ) : ISimulateUserCommandsHook
{
    private event OnSimulateUserCommandsPreDelegate? _Pre;
    private event OnSimulateUserCommandsPostDelegate? _Post;

    public event OnSimulateUserCommandsPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.SimulateUserCommands, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.SimulateUserCommands, owner);
        }
    }

    public event OnSimulateUserCommandsPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.SimulateUserCommands, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.SimulateUserCommands, owner);
        }
    }

    public void InvokePre(ref SimulateUserCommandsPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref SimulateUserCommandsPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.SimulateUserCommands, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.SimulateUserCommands, owner);
    }
}
