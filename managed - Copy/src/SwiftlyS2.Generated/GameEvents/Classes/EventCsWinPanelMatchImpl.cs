using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "cs_win_panel_match"
/// </summary>
internal class EventCsWinPanelMatchImpl : GameEvent<EventCsWinPanelMatch>, EventCsWinPanelMatch
{

  public EventCsWinPanelMatchImpl(nint address) : base(address)
  {
  }
}
