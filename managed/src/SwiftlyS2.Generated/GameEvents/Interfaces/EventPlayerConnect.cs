using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_connect"
/// a new client connected
/// </summary>
public interface EventPlayerConnect : IGameEvent<EventPlayerConnect>
{

    static EventPlayerConnect IGameEvent<EventPlayerConnect>.Create( nint address ) => new EventPlayerConnectImpl(address);

    static string IGameEvent<EventPlayerConnect>.GetName() => "player_connect";

    static uint IGameEvent<EventPlayerConnect>.GetHash() => 0x721B9701u;
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
    /// type: string
    /// </summary>
    public string NetworkID { get; set; }

    /// <summary>
    /// steam id
    /// <br/>
    /// type: uint64
    /// </summary>
    public ulong XuID { get; set; }

    /// <summary>
    /// type: bool
    /// </summary>
    public bool Bot { get; set; }

}
