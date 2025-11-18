using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "inferno_extinguish"
/// </summary>
internal class EventInfernoExtinguishImpl : GameEvent<EventInfernoExtinguish>, EventInfernoExtinguish
{

  public EventInfernoExtinguishImpl(nint address) : base(address)
  {
  }

  public short EntityID
  { get => (short)Accessor.GetInt32("entityid"); set => Accessor.SetInt32("entityid", value); }

  public float X
  { get => Accessor.GetFloat("x"); set => Accessor.SetFloat("x", value); }

  public float Y
  { get => Accessor.GetFloat("y"); set => Accessor.SetFloat("y", value); }

  public float Z
  { get => Accessor.GetFloat("z"); set => Accessor.SetFloat("z", value); }
}
