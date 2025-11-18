using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "flashbang_detonate"
/// </summary>
internal class EventFlashbangDetonateImpl : GameEvent<EventFlashbangDetonate>, EventFlashbangDetonate
{

  public EventFlashbangDetonateImpl(nint address) : base(address)
  {
  }

  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  public short EntityID
  { get => (short)Accessor.GetInt32("entityid"); set => Accessor.SetInt32("entityid", value); }

  public float X
  { get => Accessor.GetFloat("x"); set => Accessor.SetFloat("x", value); }

  public float Y
  { get => Accessor.GetFloat("y"); set => Accessor.SetFloat("y", value); }

  public float Z
  { get => Accessor.GetFloat("z"); set => Accessor.SetFloat("z", value); }
}
