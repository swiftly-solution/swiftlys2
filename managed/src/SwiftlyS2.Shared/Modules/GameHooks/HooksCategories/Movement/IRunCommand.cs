using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameHooks;

public interface IRunCommandMovement
{
    /// <summary>
    /// The player who dropped the weapon.
    /// </summary>
    public IPlayer Player { get; set; }

    /// <summary>
    /// The button state.
    /// </summary>
    public CInButtonState ButtonState { get; }
    /// <summary>
    /// The user command protobuf.
    /// </summary>
    public CSGOUserCmdPB UserCmdPB { get; }

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