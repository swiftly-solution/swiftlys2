using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "break_breakable"
/// </summary>
internal class EventBreakBreakableImpl : GameEvent<EventBreakBreakable>, EventBreakBreakable
{

  public EventBreakBreakableImpl(nint address) : base(address)
  {
  }

  public int EntIndex
  { get => Accessor.GetInt32("entindex"); set => Accessor.SetInt32("entindex", value); }

  public CCSPlayerController UserIdController
  { get => Accessor.GetPlayerController("userid"); }

  public CCSPlayerPawn UserIdPawn
  { get => Accessor.GetPlayerPawn("userid"); }

  public IPlayer UserIdPlayer
  { get => Accessor.GetPlayer("userid"); }

  public int UserId
  { get => Accessor.GetInt32("userid"); set => Accessor.SetInt32("userid", value); }

  // BREAK_GLASS, BREAK_WOOD, etc
  public byte Material
  { get => (byte)Accessor.GetInt32("material"); set => Accessor.SetInt32("material", value); }
}
