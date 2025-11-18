using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hltv_chase"
/// shot of a single entity
/// </summary>
internal class EventHltvChaseImpl : GameEvent<EventHltvChase>, EventHltvChase
{

  public EventHltvChaseImpl(nint address) : base(address)
  {
  }

  // primary traget index
  public int Target1
  { get => Accessor.GetPlayerSlot("target1"); set => Accessor.SetPlayerSlot("target1", value); }

  // secondary traget index or 0
  public int Target2
  { get => Accessor.GetPlayerSlot("target2"); set => Accessor.SetPlayerSlot("target2", value); }

  // camera distance
  public short Distance
  { get => (short)Accessor.GetInt32("distance"); set => Accessor.SetInt32("distance", value); }

  // view angle horizontal
  public short Theta
  { get => (short)Accessor.GetInt32("theta"); set => Accessor.SetInt32("theta", value); }

  // view angle vertical
  public short Phi
  { get => (short)Accessor.GetInt32("phi"); set => Accessor.SetInt32("phi", value); }

  // camera inertia
  public byte Inertia
  { get => (byte)Accessor.GetInt32("inertia"); set => Accessor.SetInt32("inertia", value); }

  // diretcor suggests to show ineye
  public byte InEye
  { get => (byte)Accessor.GetInt32("ineye"); set => Accessor.SetInt32("ineye", value); }
}
