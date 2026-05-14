using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Events;

/// <summary>
/// Called when the player pawn post think hook is triggered.
/// </summary>
[Obsolete("This event is deprecated and will be removed in future versions. Use GameHooks instead.")]
public interface IOnPlayerPawnPostThinkHookEvent
{
    /// <summary>
    /// The player pawn.
    /// </summary>
    public CCSPlayerPawn PlayerPawn { get; }
}