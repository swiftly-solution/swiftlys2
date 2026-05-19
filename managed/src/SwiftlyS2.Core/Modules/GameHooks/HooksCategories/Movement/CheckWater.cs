using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CheckWaterMovementData : ICheckWaterMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public required bool OriginalResult { get; set; }

    public void SetResult( bool result )
    {
        OriginalResult = result;
        Intercepted = true;
    }

    public bool Intercepted { get; set; } = false;
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class CheckWaterMovementEvents : ICheckWaterMovementEvents
{
    internal event OnCheckWaterMovementDelegate? _Pre;
    internal event OnCheckWaterMovementDelegate? _Post;

    public event OnCheckWaterMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CheckWater);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckWater);
        }
    }

    public event OnCheckWaterMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CheckWater);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CheckWater);
        }
    }

    public void InvokePre( ref ICheckWaterMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref ICheckWaterMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckWater);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CheckWater);
    }
}
