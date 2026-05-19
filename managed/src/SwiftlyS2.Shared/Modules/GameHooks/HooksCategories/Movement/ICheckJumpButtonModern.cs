using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface ICheckJumpButtonModernMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public HookResult Result { get; set; }
}

public delegate void OnCheckJumpButtonModernMovementDelegate( ref ICheckJumpButtonModernMovement data );

public interface ICheckJumpButtonModernMovementEvents
{
    public event OnCheckJumpButtonModernMovementDelegate Pre;
    public event OnCheckJumpButtonModernMovementDelegate Post;
}
