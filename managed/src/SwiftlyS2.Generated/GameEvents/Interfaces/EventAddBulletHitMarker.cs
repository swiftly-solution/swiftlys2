using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "add_bullet_hit_marker"
/// </summary>
public interface EventAddBulletHitMarker : IGameEvent<EventAddBulletHitMarker>
{

    static EventAddBulletHitMarker IGameEvent<EventAddBulletHitMarker>.Create( nint address ) => new EventAddBulletHitMarkerImpl(address);

    static string IGameEvent<EventAddBulletHitMarker>.GetName() => "add_bullet_hit_marker";

    static uint IGameEvent<EventAddBulletHitMarker>.GetHash() => 0x6CB6A2A2u;
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
    public short Bone { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short PosX { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short PosY { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short PosZ { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short AngX { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short AngY { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short AngZ { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short StartX { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short StartY { get; set; }

    /// <summary>
    /// type: short
    /// </summary>
    public short StartZ { get; set; }

    /// <summary>
    /// type: bool
    /// </summary>
    public bool Hit { get; set; }

}
