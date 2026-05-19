using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface ICheckJumpButtonLegacyMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public HookResult Result { get; set; }
}

public delegate void OnCheckJumpButtonLegacyMovementDelegate( ref ICheckJumpButtonLegacyMovement data );

public interface ICheckJumpButtonLegacyMovementEvents
{
    public event OnCheckJumpButtonLegacyMovementDelegate Pre;
    public event OnCheckJumpButtonLegacyMovementDelegate Post;
}
