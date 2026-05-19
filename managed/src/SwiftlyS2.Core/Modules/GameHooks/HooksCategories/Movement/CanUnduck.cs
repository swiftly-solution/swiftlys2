using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class CanUnduckMovementData : ICanUnduckMovement
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

internal sealed class CanUnduckMovementEvents : ICanUnduckMovementEvents
{
    internal event OnCanUnduckMovementDelegate? _Pre;
    internal event OnCanUnduckMovementDelegate? _Post;

    public event OnCanUnduckMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.CanUnduck);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.CanUnduck);
        }
    }

    public event OnCanUnduckMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.CanUnduck);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.CanUnduck);
        }
    }

    public void InvokePre( ref ICanUnduckMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref ICanUnduckMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.CanUnduck);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.CanUnduck);
    }
}
