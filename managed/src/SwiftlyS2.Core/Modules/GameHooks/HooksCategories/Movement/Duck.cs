using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class DuckMovementData : IDuckMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;
}

internal sealed class DuckMovementEvents : IDuckMovementEvents
{
    internal event OnDuckMovementDelegate? _Pre;
    internal event OnDuckMovementDelegate? _Post;

    public event OnDuckMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.Duck);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.Duck);
        }
    }

    public event OnDuckMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.Duck);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.Duck);
        }
    }

    public void InvokePre( ref IDuckMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref IDuckMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.Duck);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.Duck);
    }
}
