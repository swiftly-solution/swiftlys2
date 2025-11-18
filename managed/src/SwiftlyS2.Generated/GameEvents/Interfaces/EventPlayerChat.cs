using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_chat"
/// a public player chat
/// </summary>
public interface EventPlayerChat : IGameEvent<EventPlayerChat>
{

    static EventPlayerChat IGameEvent<EventPlayerChat>.Create( nint address ) => new EventPlayerChatImpl(address);

    static string IGameEvent<EventPlayerChat>.GetName() => "player_chat";

    static uint IGameEvent<EventPlayerChat>.GetHash() => 0xA2C21BE3u;
    /// <summary>
    /// true if team only chat
    /// <br/>
    /// type: bool
    /// </summary>
    public bool TeamOnly { get; set; }

    /// <summary>
    /// chatting player
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// chatting player
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // chatting player
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// chatting player
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// chatting player ID
    /// <br/>
    /// type: short
    /// </summary>
    public short Playerid { get; set; }

    /// <summary>
    /// chat text
    /// <br/>
    /// type: string
    /// </summary>
    public string Text { get; set; }

}
