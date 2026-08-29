using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary>
/// Event "player_avenged_teammate"
/// </summary>
internal class EventPlayerAvengedTeammateImpl : GameEvent<EventPlayerAvengedTeammate>, EventPlayerAvengedTeammate
{

    public EventPlayerAvengedTeammateImpl(nint address) : base(address)
    {
    }

    public CCSPlayerController AvengerIdController
    { get => Accessor.GetPlayerController("avenger_id"); }

    public CCSPlayerPawn AvengerIdPawn
    { get => Accessor.GetPlayerPawn("avenger_id"); }

    public IPlayer? AvengerIdPlayer
    { get => Accessor.GetPlayer("avenger_id"); }

    public int AvengerId
    { get => Accessor.GetInt32("avenger_id"); set => Accessor.SetInt32("avenger_id", value); }

    public CCSPlayerController AvengedPlayerIdController
    { get => Accessor.GetPlayerController("avenged_player_id"); }

    public CCSPlayerPawn AvengedPlayerIdPawn
    { get => Accessor.GetPlayerPawn("avenged_player_id"); }

    public IPlayer? AvengedPlayerIdPlayer
    { get => Accessor.GetPlayer("avenged_player_id"); }

    public int AvengedPlayerId
    { get => Accessor.GetInt32("avenged_player_id"); set => Accessor.SetInt32("avenged_player_id", value); }
}
