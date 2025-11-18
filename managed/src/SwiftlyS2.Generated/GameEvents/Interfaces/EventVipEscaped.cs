using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "vip_escaped"
/// </summary>
public interface EventVipEscaped : IGameEvent<EventVipEscaped>
{

    static EventVipEscaped IGameEvent<EventVipEscaped>.Create( nint address ) => new EventVipEscapedImpl(address);

    static string IGameEvent<EventVipEscaped>.GetName() => "vip_escaped";

    static uint IGameEvent<EventVipEscaped>.GetHash() => 0x30143B6Eu;
    /// <summary>
    /// player who was the VIP
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player who was the VIP
    /// <br/>
    /// type: player_controller
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player who was the VIP
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player who was the VIP
    /// <br/>
    /// type: player_controller
    /// </summary>
    public int UserId { get; set; }

}
