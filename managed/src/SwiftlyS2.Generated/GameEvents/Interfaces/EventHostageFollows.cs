using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "hostage_follows"
/// </summary>
public interface EventHostageFollows : IGameEvent<EventHostageFollows>
{

    static EventHostageFollows IGameEvent<EventHostageFollows>.Create( nint address ) => new EventHostageFollowsImpl(address);

    static string IGameEvent<EventHostageFollows>.GetName() => "hostage_follows";

    static uint IGameEvent<EventHostageFollows>.GetHash() => 0xEC398ED7u;
    /// <summary>
    /// player who touched the hostage
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerController UserIdController { get; }

    /// <summary>
    /// player who touched the hostage
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public CCSPlayerPawn UserIdPawn { get; }


    // player who touched the hostage
    public IPlayer UserIdPlayer { get => Accessor.GetPlayer("userid"); }
    /// <summary>
    /// player who touched the hostage
    /// <br/>
    /// type: player_controller_and_pawn
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// hostage entity index
    /// <br/>
    /// type: short
    /// </summary>
    public short Hostage { get; set; }

}
