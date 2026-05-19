using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CategorizePositionMovementData : ICategorizePositionMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public bool StayOnGround { get; set; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class CategorizePositionMovementEvents : ICategorizePositionMovementEvents
{
    internal event OnCategorizePositionMovementDelegate? _Pre;
    internal event OnCategorizePositionMovementDelegate? _Post;

    public event OnCategorizePositionMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CategorizePosition);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CategorizePosition);
        }
    }

    public event OnCategorizePositionMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CategorizePosition);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CategorizePosition);
        }
    }

    public void InvokePre( ref ICategorizePositionMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref ICategorizePositionMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CategorizePosition);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CategorizePosition);
    }
}
