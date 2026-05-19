using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface ICategorizePositionMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public bool StayOnGround { get; set; }
    public HookResult Result { get; set; }
}

public delegate void OnCategorizePositionMovementDelegate( ref ICategorizePositionMovement data );

public interface ICategorizePositionMovementEvents
{
    public event OnCategorizePositionMovementDelegate Pre;
    public event OnCategorizePositionMovementDelegate Post;
}
