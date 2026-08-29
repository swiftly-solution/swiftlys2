using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary>
/// Event "show_deathpanel"
/// </summary>
internal class EventShowDeathpanelImpl : GameEvent<EventShowDeathpanel>, EventShowDeathpanel
{

    public EventShowDeathpanelImpl(nint address) : base(address)
    {
    }

    // endindex of the one who was killed
    public CCSPlayerController VictimController
    { get => Accessor.GetPlayerController("victim"); }

    // endindex of the one who was killed
    public CCSPlayerPawn VictimPawn
    { get => Accessor.GetPlayerPawn("victim"); }

    // endindex of the one who was killed
    public IPlayer? VictimPlayer
    { get => Accessor.GetPlayer("victim"); }

    // endindex of the one who was killed
    public int Victim
    { get => Accessor.GetInt32("victim"); set => Accessor.SetInt32("victim", value); }

    // entindex of the killer entity
    public nint Killer
    { get => Accessor.GetPtr("killer"); set => Accessor.SetPtr("killer", value); }

    public CCSPlayerController KillerControllerController
    { get => Accessor.GetPlayerController("killer_controller"); }

    public CCSPlayerPawn KillerControllerPawn
    { get => Accessor.GetPlayerPawn("killer_controller"); }

    public IPlayer? KillerControllerPlayer
    { get => Accessor.GetPlayer("killer_controller"); }

    public int KillerController
    { get => Accessor.GetInt32("killer_controller"); set => Accessor.SetInt32("killer_controller", value); }

    public short HitsTaken
    { get => (short)Accessor.GetInt32("hits_taken"); set => Accessor.SetInt32("hits_taken", value); }

    public short DamageTaken
    { get => (short)Accessor.GetInt32("damage_taken"); set => Accessor.SetInt32("damage_taken", value); }

    public short HitsGiven
    { get => (short)Accessor.GetInt32("hits_given"); set => Accessor.SetInt32("hits_given", value); }

    public short DamageGiven
    { get => (short)Accessor.GetInt32("damage_given"); set => Accessor.SetInt32("damage_given", value); }
}
