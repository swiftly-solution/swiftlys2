using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface IOnJumpLegacyMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public HookResult Result { get; set; }
}

public delegate void OnOnJumpLegacyMovementDelegate( ref IOnJumpLegacyMovement data );

public interface IOnJumpLegacyMovementEvents
{
    public event OnOnJumpLegacyMovementDelegate Pre;
    public event OnOnJumpLegacyMovementDelegate Post;
}
