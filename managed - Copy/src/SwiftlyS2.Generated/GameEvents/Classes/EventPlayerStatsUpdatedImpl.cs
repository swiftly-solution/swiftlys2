using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "player_stats_updated"
/// </summary>
internal class EventPlayerStatsUpdatedImpl : GameEvent<EventPlayerStatsUpdated>, EventPlayerStatsUpdated
{

  public EventPlayerStatsUpdatedImpl(nint address) : base(address)
  {
  }

  public bool ForceUpload
  { get => Accessor.GetBool("forceupload"); set => Accessor.SetBool("forceupload", value); }
}
