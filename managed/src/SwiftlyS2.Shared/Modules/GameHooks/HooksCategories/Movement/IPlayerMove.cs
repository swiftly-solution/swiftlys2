using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface IPlayerMoveMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public bool OriginalResult { get; }
    public void SetResult( bool result );
    public bool Intercepted { get; set; }
    public HookResult Result { get; set; }
}

public delegate void OnPlayerMoveMovementDelegate( ref IPlayerMoveMovement data );

public interface IPlayerMoveMovementEvents
{
    public event OnPlayerMoveMovementDelegate Pre;
    public event OnPlayerMoveMovementDelegate Post;
}
