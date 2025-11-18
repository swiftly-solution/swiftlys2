using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "molotov_detonate"
/// </summary>
public interface EventMolotovDetonate : IGameEvent<EventMolotovDetonate>
{

    static EventMolotovDetonate IGameEvent<EventMolotovDetonate>.Create( nint address ) => new EventMolotovDetonateImpl(address);

    static string IGameEvent<EventMolotovDetonate>.GetName() => "molotov_detonate";

    static uint IGameEvent<EventMolotovDetonate>.GetHash() => 0xD541EE9Au;
    /// <summary>
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float X { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float Y { get; set; }

    /// <summary>
    /// type: float
    /// </summary>
    public float Z { get; set; }

}
