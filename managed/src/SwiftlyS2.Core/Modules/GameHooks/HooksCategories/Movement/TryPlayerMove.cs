using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.Trace;

namespace SwiftlyS2.Core.GameHooks;

internal sealed class TryPlayerMoveMovementData : ITryPlayerMoveMovement
{
    public required IPlayer Player { get; set; }
    public required IMoveData MoveData { get; init; }
    public required nint FirstDestPtr { get; init; }
    public required TraceResult FirstTrace { get; init; }
    public required nint IsSurfingPtr { get; init; }
    public HookResult Result { get; set; } = HookResult.Continue;

    public unsafe Vector FirstDest
    {
        get => *(Vector*)FirstDestPtr;
        set => *(Vector*)FirstDestPtr = value;
    }

    public unsafe bool IsSurfing
    {
        get => *(byte*)IsSurfingPtr != 0;
        set => *(byte*)IsSurfingPtr = value ? (byte)1 : (byte)0;
    }
}

internal sealed class TryPlayerMoveMovementEvents : ITryPlayerMoveMovementEvents
{
    internal event OnTryPlayerMoveMovementDelegate? _Pre;
    internal event OnTryPlayerMoveMovementDelegate? _Post;

    public event OnTryPlayerMoveMovementDelegate Pre {
        add {
            if (_Pre == null) GameHooksPublisher.AddHookListener(HookListener.TryPlayerMove);
            _Pre += value;
        }
        remove {
            _Pre -= value;
            if (_Pre == null) GameHooksPublisher.RemoveHookListener(HookListener.TryPlayerMove);
        }
    }

    public event OnTryPlayerMoveMovementDelegate Post {
        add {
            if (_Post == null) GameHooksPublisher.AddHookListener(HookListener.TryPlayerMove);
            _Post += value;
        }
        remove {
            _Post -= value;
            if (_Post == null) GameHooksPublisher.RemoveHookListener(HookListener.TryPlayerMove);
        }
    }

    public void InvokePre( ref ITryPlayerMoveMovement data ) => _Pre?.Invoke(ref data);
    public void InvokePost( ref ITryPlayerMoveMovement data ) => _Post?.Invoke(ref data);

    public void UnregisterListeners()
    {
        if (_Pre != null) GameHooksPublisher.RemoveHookListener(HookListener.TryPlayerMove);
        if (_Post != null) GameHooksPublisher.RemoveHookListener(HookListener.TryPlayerMove);
    }
}
