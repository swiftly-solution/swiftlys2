using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class ProcessUsercmdsControllerData : IProcessUsercmdsController
{
    public required IPlayer Player { get; set; }

    public required List<IUserCmd> Usercmds { get; init; }

    public required bool Paused { get; init; }

    public required float Margin { get; init; }

    public required HookResult Result { get; set; } = HookResult.Continue;

}

internal sealed class ProcessUsercmdsEvents : IProcessUsercmdsEvents
{
    private event OnProcessUsercmdsDelegate? _Pre;
    private event OnProcessUsercmdsDelegate? _Post;

    public event OnProcessUsercmdsDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.ProcessUsercmds);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds);
        }
    }

    public event OnProcessUsercmdsDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.ProcessUsercmds);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds);
        }
    }

    public void InvokePre( ref IProcessUsercmdsController data )
    {
        _Pre?.Invoke(ref data);
    }

    public void InvokePost( ref IProcessUsercmdsController data )
    {
        _Post?.Invoke(ref data);
    }

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.ProcessUsercmds);
    }
}
