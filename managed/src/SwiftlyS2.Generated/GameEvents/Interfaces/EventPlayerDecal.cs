using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "player_decal"
/// </summary>
public interface EventPlayerDecal : IGameEvent<EventPlayerDecal>
{

    static EventPlayerDecal IGameEvent<EventPlayerDecal>.Create( nint address ) => new EventPlayerDecalImpl(address);

    static string IGameEvent<EventPlayerDecal>.GetName() => "player_decal";

    static uint IGameEvent<EventPlayerDecal>.GetHash() => 0xC7978ED6u;
    /// <summary>
    /// <br/>
    /// type: player_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// <br/>
    /// type: player_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// <br/>
    /// type: player_pawn
    /// </summary>
    public int UserId { get; set; }

}
