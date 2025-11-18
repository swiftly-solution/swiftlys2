using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "item_purchase"
/// </summary>
public interface EventItemPurchase : IGameEvent<EventItemPurchase>
{

    static EventItemPurchase IGameEvent<EventItemPurchase>.Create( nint address ) => new EventItemPurchaseImpl(address);

    static string IGameEvent<EventItemPurchase>.GetName() => "item_purchase";

    static uint IGameEvent<EventItemPurchase>.GetHash() => 0x4400FB1Cu;
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
    /// type: short
    /// </summary>
    public short Team { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short LoadOut { get; set; }

    /// <summary>
    /// type: string
    /// </summary>
    public string Weapon { get; set; }

}
