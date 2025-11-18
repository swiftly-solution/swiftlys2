using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hltv_changed_mode"
/// </summary>
internal class EventHltvChangedModeImpl : GameEvent<EventHltvChangedMode>, EventHltvChangedMode
{

  public EventHltvChangedModeImpl(nint address) : base(address)
  {
  }

  public int OldMode
  { get => Accessor.GetInt32("oldmode"); set => Accessor.SetInt32("oldmode", value); }

  public int NewMode
  { get => Accessor.GetInt32("newmode"); set => Accessor.SetInt32("newmode", value); }

  public int ObsTarget
  { get => Accessor.GetInt32("obs_target"); set => Accessor.SetInt32("obs_target", value); }
}
