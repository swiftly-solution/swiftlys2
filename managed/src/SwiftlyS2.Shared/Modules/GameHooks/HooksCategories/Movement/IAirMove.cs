using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface IAirMoveMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public HookResult Result { get; set; }
}

public delegate void OnAirMoveMovementDelegate( ref IAirMoveMovement data );

public interface IAirMoveMovementEvents
{
    public event OnAirMoveMovementDelegate Pre;
    public event OnAirMoveMovementDelegate Post;
}
