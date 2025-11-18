using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "entity_visible"
/// </summary>
internal class EventEntityVisibleImpl : GameEvent<EventEntityVisible>, EventEntityVisible
{

  public EventEntityVisibleImpl(nint address) : base(address)
  {
  }

  // The player who sees the entity
  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  // The player who sees the entity
  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  // The player who sees the entity
  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  // The player who sees the entity
  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // Entindex of the entity they see
  public int Subject
  { get => Accessor.GetInt32("subject"); set => Accessor.SetInt32("subject", value); }

  // Classname of the entity they see
  public string ClassName
  { get => Accessor.GetString("classname"); set => Accessor.SetString("classname", value); }

  // name of the entity they see
  public string EntityName
  { get => Accessor.GetString("entityname"); set => Accessor.SetString("entityname", value); }
}
