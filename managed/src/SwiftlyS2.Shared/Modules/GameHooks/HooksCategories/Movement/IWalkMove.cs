using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface IWalkMoveMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public HookResult Result { get; set; }
}

public delegate void OnWalkMoveMovementDelegate( ref IWalkMoveMovement data );

public interface IWalkMoveMovementEvents
{
    public event OnWalkMoveMovementDelegate Pre;
    public event OnWalkMoveMovementDelegate Post;
}
