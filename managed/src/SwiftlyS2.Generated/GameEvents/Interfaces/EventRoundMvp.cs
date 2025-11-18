using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "round_mvp"
/// </summary>
public interface EventRoundMvp : IGameEvent<EventRoundMvp>
{

    static EventRoundMvp IGameEvent<EventRoundMvp>.Create( nint address ) => new EventRoundMvpImpl(address);

    static string IGameEvent<EventRoundMvp>.GetName() => "round_mvp";

    static uint IGameEvent<EventRoundMvp>.GetHash() => 0xC80E399Du;
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
    public short Reason { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int Value { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int MusickItMvps { get; set; }

    /// <summary>
    /// type: byte
    /// </summary>
    public byte NoMusic { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int MusickItID { get; set; }

}
