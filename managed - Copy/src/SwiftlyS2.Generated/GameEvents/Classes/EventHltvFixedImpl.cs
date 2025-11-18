using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hltv_fixed"
/// show from fixed view
/// </summary>
internal class EventHltvFixedImpl : GameEvent<EventHltvFixed>, EventHltvFixed
{

  public EventHltvFixedImpl(nint address) : base(address)
  {
  }

  // camera position in world
  public int PosX
  { get => Accessor.GetInt32("posx"); set => Accessor.SetInt32("posx", value); }

  public int Posy
  { get => Accessor.GetInt32("posy"); set => Accessor.SetInt32("posy", value); }

  public int PosZ
  { get => Accessor.GetInt32("posz"); set => Accessor.SetInt32("posz", value); }

  // camera angles
  public short Theta
  { get => (short)Accessor.GetInt32("theta"); set => Accessor.SetInt32("theta", value); }

  public short Phi
  { get => (short)Accessor.GetInt32("phi"); set => Accessor.SetInt32("phi", value); }

  public short Offset
  { get => (short)Accessor.GetInt32("offset"); set => Accessor.SetInt32("offset", value); }

  public float FOv
  { get => Accessor.GetFloat("fov"); set => Accessor.SetFloat("fov", value); }

  // follow this player
  public int Target
  { get => Accessor.GetPlayerSlot("target"); set => Accessor.SetPlayerSlot("target", value); }
}
