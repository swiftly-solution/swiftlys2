using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameHooks;

public interface IRunCommandMovement
{
    /// <summary>
    /// The player who dropped the weapon.
    /// </summary>
    public IPlayer Player { get; set; }

    /// <summary>
    /// The user command.
    /// </summary>
    public IUserCmd UserCmd { get; }

    /// <summary>
    /// The result of the hook. Can be used to prevent the drop by returning <see cref="HookResult.Stop"/> or <see cref="HookResult.CancelOriginal"/> .
    /// </summary>
    public HookResult Result { get; set; }
}

public delegate void OnRunCommandMovementDelegate( ref IRunCommandMovement postThink );

public interface IRunCommandMovementEvents
{
    /// <summary>
    /// Event triggered before a player movement tick is processed.
    /// </summary>
    public event OnRunCommandMovementDelegate Pre;

    /// <summary>
    /// Event triggered after a player movement tick is processed.
    /// </summary>
    public event OnRunCommandMovementDelegate Post;
}
