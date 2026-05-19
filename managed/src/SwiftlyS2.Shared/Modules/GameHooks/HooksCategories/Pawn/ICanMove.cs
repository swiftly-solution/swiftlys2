using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface ICanMovePawn
{
    public IPlayer Player { get; set; }
    public bool OriginalResult { get; }
    public void SetResult( bool result );
    public bool Intercepted { get; set; }
    public HookResult Result { get; set; }
}

public delegate void OnCanMovePawnDelegate( ref ICanMovePawn data );

public interface ICanMovePawnEvents
{
    public event OnCanMovePawnDelegate Pre;
    public event OnCanMovePawnDelegate Post;
}
