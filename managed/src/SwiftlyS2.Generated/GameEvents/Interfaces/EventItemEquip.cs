using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "item_equip"
/// </summary>
public interface EventItemEquip : IGameEvent<EventItemEquip>
{

    static EventItemEquip IGameEvent<EventItemEquip>.Create( nint address ) => new EventItemEquipImpl(address);

    static string IGameEvent<EventItemEquip>.GetName() => "item_equip";

    static uint IGameEvent<EventItemEquip>.GetHash() => 0x3D5F333Du;
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
    /// either a weapon such as 'tmp' or 'hegrenade', or an item such as 'nvgs'
    /// <br/>
    /// type: string
    /// </summary>
    public string Item { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int DefIndex { get; set; }

    /// <summary>
    /// type: bool
    /// </summary>
    public bool CanZoom { get; set; }

    /// <summary>
    /// type: bool
    /// </summary>
    public bool HasSilencer { get; set; }

    /// <summary>
    /// type: bool
    /// </summary>
    public bool IsSilenced { get; set; }

    /// <summary>
    /// type: bool
    /// </summary>
    public bool HasTracers { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short WepType { get; set; }

    /// <summary>
    /// type: bool
    /// </summary>
    public bool IsPainted { get; set; }

}
