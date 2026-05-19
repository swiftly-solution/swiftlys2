using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface ICheckVelocityMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public HookResult Result { get; set; }
}

public delegate void OnCheckVelocityMovementDelegate( ref ICheckVelocityMovement data );

public interface ICheckVelocityMovementEvents
{
    public event OnCheckVelocityMovementDelegate Pre;
    public event OnCheckVelocityMovementDelegate Post;
}
