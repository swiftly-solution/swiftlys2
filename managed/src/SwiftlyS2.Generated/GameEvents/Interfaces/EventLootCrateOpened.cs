using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "loot_crate_opened"
/// </summary>
public interface EventLootCrateOpened : IGameEvent<EventLootCrateOpened>
{

    static EventLootCrateOpened IGameEvent<EventLootCrateOpened>.Create( nint address ) => new EventLootCrateOpenedImpl(address);

    static string IGameEvent<EventLootCrateOpened>.GetName() => "loot_crate_opened";

    static uint IGameEvent<EventLootCrateOpened>.GetHash() => 0x18E203D5u;
    /// <summary>
    /// player entindex
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player entindex
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player entindex
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player entindex
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// type of crate (metal, wood, or paradrop)
    /// <br/>
    /// type: string
    /// </summary>
    public string Type { get; set; }

}
