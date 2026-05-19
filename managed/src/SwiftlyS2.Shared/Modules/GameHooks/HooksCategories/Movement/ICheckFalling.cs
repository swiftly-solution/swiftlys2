using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface ICheckFallingMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public HookResult Result { get; set; }
}

public delegate void OnCheckFallingMovementDelegate( ref ICheckFallingMovement data );

public interface ICheckFallingMovementEvents
{
    public event OnCheckFallingMovementDelegate Pre;
    public event OnCheckFallingMovementDelegate Post;
}
