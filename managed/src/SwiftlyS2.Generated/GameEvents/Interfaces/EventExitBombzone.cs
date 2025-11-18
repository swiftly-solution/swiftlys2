using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "exit_bombzone"
/// </summary>
public interface EventExitBombzone : IGameEvent<EventExitBombzone>
{

    static EventExitBombzone IGameEvent<EventExitBombzone>.Create( nint address ) => new EventExitBombzoneImpl(address);

    static string IGameEvent<EventExitBombzone>.GetName() => "exit_bombzone";

    static uint IGameEvent<EventExitBombzone>.GetHash() => 0xF5ADEBDCu;
    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// type: bool
    /// </summary>
    public bool HasBomb { get; set; }

    /// <summary>
    /// type: bool
    /// </summary>
    public bool IsPlanted { get; set; }

}
