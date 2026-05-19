using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface IFullWalkMoveMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public bool Ground { get; set; }
    public HookResult Result { get; set; }
}

public delegate void OnFullWalkMoveMovementDelegate( ref IFullWalkMoveMovement data );

public interface IFullWalkMoveMovementEvents
{
    public event OnFullWalkMoveMovementDelegate Pre;
    public event OnFullWalkMoveMovementDelegate Post;
}
