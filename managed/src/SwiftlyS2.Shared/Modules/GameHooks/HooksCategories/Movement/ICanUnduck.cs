using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface ICanUnduckMovement
{
    public IPlayer Player { get; set; }
    public IMoveData MoveData { get; }
    public bool OriginalResult { get; }
    public void SetResult( bool result );
    public bool Intercepted { get; set; }
    public HookResult Result { get; set; }
}

public delegate void OnCanUnduckMovementDelegate( ref ICanUnduckMovement data );

public interface ICanUnduckMovementEvents
{
    public event OnCanUnduckMovementDelegate Pre;
    public event OnCanUnduckMovementDelegate Post;
}
