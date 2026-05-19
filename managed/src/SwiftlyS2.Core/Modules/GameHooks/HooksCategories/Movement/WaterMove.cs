using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class WaterMoveMovementData : IWaterMoveMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class WaterMoveMovementEvents : IWaterMoveMovementEvents
{
    internal event OnWaterMoveMovementDelegate? _Pre;
    internal event OnWaterMoveMovementDelegate? _Post;

    public event OnWaterMoveMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.WaterMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.WaterMove);
        }
    }

    public event OnWaterMoveMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.WaterMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.WaterMove);
        }
    }

    public void InvokePre( ref IWaterMoveMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref IWaterMoveMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.WaterMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.WaterMove);
    }
}
