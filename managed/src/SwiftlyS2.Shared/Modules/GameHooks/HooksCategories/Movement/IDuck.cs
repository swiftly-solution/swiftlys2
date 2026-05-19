using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface IDuckMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public HookResult Result { get; set; }
}

public delegate void OnDuckMovementDelegate( ref IDuckMovement data );

public interface IDuckMovementEvents
{
    public event OnDuckMovementDelegate Pre;
    public event OnDuckMovementDelegate Post;
}
