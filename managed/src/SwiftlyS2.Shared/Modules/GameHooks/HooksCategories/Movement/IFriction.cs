using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface IFrictionMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public HookResult Result { get; set; }
}

public delegate void OnFrictionMovementDelegate( ref IFrictionMovement data );

public interface IFrictionMovementEvents
{
    public event OnFrictionMovementDelegate Pre;
    public event OnFrictionMovementDelegate Post;
}
