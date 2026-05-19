using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface ICheckParametersMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public HookResult Result { get; set; }
}

public delegate void OnCheckParametersMovementDelegate( ref ICheckParametersMovement data );

public interface ICheckParametersMovementEvents
{
    public event OnCheckParametersMovementDelegate Pre;
    public event OnCheckParametersMovementDelegate Post;
}
