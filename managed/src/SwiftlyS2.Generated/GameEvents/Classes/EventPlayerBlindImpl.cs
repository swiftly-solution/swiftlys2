using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary>
/// Event "player_blind"
/// </summary>
internal class EventPlayerBlindImpl : GameEvent<EventPlayerBlind>, EventPlayerBlind
{

    public EventPlayerBlindImpl(nint address) : base(address)
    {
    }

    public CCSPlayerController UserIdController
    { get => Accessor.GetPlayerController("userid"); }

    public CCSPlayerPawn UserIdPawn
    { get => Accessor.GetPlayerPawn("userid"); }

    public IPlayer? UserIdPlayer
    { get => Accessor.GetPlayer("userid"); }

    public int UserId
    { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

    // user ID who threw the flash
    public CCSPlayerController AttackerController
    { get => Accessor.GetPlayerController("attacker"); }

    // user ID who threw the flash
    public CCSPlayerPawn AttackerPawn
    { get => Accessor.GetPlayerPawn("attacker"); }

    // user ID who threw the flash
    public IPlayer? AttackerPlayer
    { get => Accessor.GetPlayer("attacker"); }

    // user ID who threw the flash
    public int Attacker
    { get => Accessor.GetInt32("attacker"); set => Accessor.SetInt32("attacker", value); }

    // the flashbang going off
    public short EntityID
    { get => (short)Accessor.GetInt32("entityid"); set => Accessor.SetInt32("entityid", value); }

    public float BlindDuration
    { get => Accessor.GetFloat("blind_duration"); set => Accessor.SetFloat("blind_duration", value); }
}
