using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "enter_bombzone"
/// </summary>
public interface EventEnterBombzone : IGameEvent<EventEnterBombzone>
{

    static EventEnterBombzone IGameEvent<EventEnterBombzone>.Create( nint address ) => new EventEnterBombzoneImpl(address);

    static string IGameEvent<EventEnterBombzone>.GetName() => "enter_bombzone";

    static uint IGameEvent<EventEnterBombzone>.GetHash() => 0x9175DF94u;
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
