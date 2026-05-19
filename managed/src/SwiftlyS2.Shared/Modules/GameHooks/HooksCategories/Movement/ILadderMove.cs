using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface ILadderMoveMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public bool OriginalResult { get; }
    public void SetResult( bool result );
    public bool Intercepted { get; set; }
    public HookResult Result { get; set; }
}

public delegate void OnLadderMoveMovementDelegate( ref ILadderMoveMovement data );

public interface ILadderMoveMovementEvents
{
    public event OnLadderMoveMovementDelegate Pre;
    public event OnLadderMoveMovementDelegate Post;
}
