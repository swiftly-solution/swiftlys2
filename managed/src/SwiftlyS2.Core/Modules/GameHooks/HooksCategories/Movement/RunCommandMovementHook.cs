using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class RunCommandMovementHook( GameHooksService owner ) : IRunCommandMovementHook
{
    internal event OnRunCommandMovementPreDelegate? _Pre;
    internal event OnRunCommandMovementPostDelegate? _Post;

    public event OnRunCommandMovementPreDelegate Pre
    {
        add
        {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.RunCommand, owner);
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand, owner);
        }
    }

    public event OnRunCommandMovementPostDelegate Post
    {
        add
        {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.RunCommand, owner);
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand, owner);
        }
    }

    public void InvokePre(ref RunCommandMovementPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref RunCommandMovementPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand, owner);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand, owner);
    }
}
