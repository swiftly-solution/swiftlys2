using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_info"
/// a player changed his name
/// </summary>
public interface EventPlayerInfo : IGameEvent<EventPlayerInfo>
{

    static EventPlayerInfo IGameEvent<EventPlayerInfo>.Create( nint address ) => new EventPlayerInfoImpl(address);

    static string IGameEvent<EventPlayerInfo>.GetName() => "player_info";

    static uint IGameEvent<EventPlayerInfo>.GetHash() => 0x0A0BAFFDu;
    /// <summary>
    /// player name
    /// <br/>
    /// type: string
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// user ID on server (unique on server)
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// user ID on server (unique on server)
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // user ID on server (unique on server)
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// user ID on server (unique on server)
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// player network (i.e steam) id
    /// <br/>
    /// type: uint64
    /// </summary>
    public ulong SteamID { get; set; }

    /// <summary>
    /// true if player is a AI bot
    /// <br/>
    /// type: bool
    /// </summary>
    public bool Bot { get; set; }

}
