using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckParametersMovementData : ICheckParametersMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class CheckParametersMovementEvents : ICheckParametersMovementEvents
{
    internal event OnCheckParametersMovementDelegate? _Pre;
    internal event OnCheckParametersMovementDelegate? _Post;

    public event OnCheckParametersMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckParameters);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckParameters);
        }
    }

    public event OnCheckParametersMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckParameters);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckParameters);
        }
    }

    public void InvokePre( ref ICheckParametersMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref ICheckParametersMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckParameters);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckParameters);
    }
}
