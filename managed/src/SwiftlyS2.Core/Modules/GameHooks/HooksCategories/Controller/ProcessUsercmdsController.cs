using SwiftlyS2.Shared.GameHooks;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class ProcessUsercmdsHook : IProcessUsercmdsHook
{
    private event OnProcessUsercmdsPreDelegate? _Pre;
    private event OnProcessUsercmdsPostDelegate? _Post;

    public event OnProcessUsercmdsPreDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.ProcessUsercmds);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds);
        }
    }

    public event OnProcessUsercmdsPostDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.ProcessUsercmds);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds);
        }
    }

    public void InvokePre( ref ProcessUsercmdsPreContext ctx ) => _Pre?.Invoke(ref ctx);
    public void InvokePost( ref ProcessUsercmdsPostContext ctx ) => _Post?.Invoke(ref ctx);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds);
    }
}
