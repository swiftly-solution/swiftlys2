using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "hostage_call_for_help"
/// </summary>
internal class EventHostageCallForHelpImpl : GameEvent<EventHostageCallForHelp>, EventHostageCallForHelp
{

  public EventHostageCallForHelpImpl(nint address) : base(address)
  {
  }

  // hostage entity index
  public short Hostage
  { get => (short)Accessor.GetInt32("hostage"); set => Accessor.SetInt32("hostage", value); }
}
