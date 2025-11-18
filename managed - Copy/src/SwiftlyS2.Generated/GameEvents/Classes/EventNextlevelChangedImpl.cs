using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "nextlevel_changed"
/// a game event, name may be 32 characters long
/// </summary>
internal class EventNextlevelChangedImpl : GameEvent<EventNextlevelChanged>, EventNextlevelChanged
{

  public EventNextlevelChangedImpl(nint address) : base(address)
  {
  }

  public string NextLevel
  { get => Accessor.GetString("nextlevel"); set => Accessor.SetString("nextlevel", value); }

  public string MapGroup
  { get => Accessor.GetString("mapgroup"); set => Accessor.SetString("mapgroup", value); }

  public string SkirmishMode
  { get => Accessor.GetString("skirmishmode"); set => Accessor.SetString("skirmishmode", value); }
}
