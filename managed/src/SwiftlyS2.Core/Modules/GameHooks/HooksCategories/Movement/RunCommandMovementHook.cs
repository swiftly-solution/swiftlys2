using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class RunCommandMovementHook : IRunCommandMovementHook
{
    internal event OnRunCommandMovementPreDelegate? _Pre;
    internal event OnRunCommandMovementPostDelegate? _Post;

    public event OnRunCommandMovementPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.RunCommand);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand);
        }
    }

    public event OnRunCommandMovementPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.RunCommand);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand);
        }
    }

    public void InvokePre( ref RunCommandMovementPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref RunCommandMovementPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.RunCommand);
    }
}
