using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface IProcessMovementMovement
{
    /// <summary>
    /// The player who dropped the weapon.
    /// </summary>
    public IPlayer Player { get; set; }

    /// <summary>
    /// The movement data.
    /// </summary>
    public IMoveData MoveData { get; }

    /// <summary>
    /// The result of the hook. Can be used to prevent the drop by returning <see cref="HookResult.Stop"/> or <see cref="HookResult.CancelOriginal"/> .
    /// </summary>
    public HookResult Result { get; set; }
}

public delegate void OnProcessMovementMovementDelegate( ref IProcessMovementMovement postThink );

public interface IProcessMovementMovementEvents
{
    public event OnProcessMovementMovementDelegate Pre;

    public event OnProcessMovementMovementDelegate Post;
}
