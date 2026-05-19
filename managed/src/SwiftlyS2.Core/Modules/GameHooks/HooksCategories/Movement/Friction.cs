using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class FrictionMovementData : IFrictionMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class FrictionMovementEvents : IFrictionMovementEvents
{
    internal event OnFrictionMovementDelegate? _Pre;
    internal event OnFrictionMovementDelegate? _Post;

    public event OnFrictionMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.Friction);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.Friction);
        }
    }

    public event OnFrictionMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.Friction);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.Friction);
        }
    }

    public void InvokePre( ref IFrictionMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref IFrictionMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.Friction);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.Friction);
    }
}
