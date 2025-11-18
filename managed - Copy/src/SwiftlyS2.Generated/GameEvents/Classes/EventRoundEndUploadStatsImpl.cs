using SwiftlyS2.Core.GameEvents;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.GameEventDefinitions;

// generated
/// <summary> 
/// Event "round_end_upload_stats"
/// </summary>
internal class EventRoundEndUploadStatsImpl : GameEvent<EventRoundEndUploadStats>, EventRoundEndUploadStats
{

  public EventRoundEndUploadStatsImpl(nint address) : base(address)
  {
  }
}
