using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface IOnJumpModernMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public HookResult Result { get; set; }
}

public delegate void OnOnJumpModernMovementDelegate( ref IOnJumpModernMovement data );

public interface IOnJumpModernMovementEvents
{
    public event OnOnJumpModernMovementDelegate Pre;
    public event OnOnJumpModernMovementDelegate Post;
}
