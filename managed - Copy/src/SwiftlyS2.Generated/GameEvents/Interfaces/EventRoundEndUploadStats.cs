using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "round_end_upload_stats"
/// </summary>
public interface EventRoundEndUploadStats : IGameEvent<EventRoundEndUploadStats> {

  static EventRoundEndUploadStats IGameEvent<EventRoundEndUploadStats>.Create(nint address) => new EventRoundEndUploadStatsImpl(address);

  static string IGameEvent<EventRoundEndUploadStats>.GetName() => "round_end_upload_stats";

  static uint IGameEvent<EventRoundEndUploadStats>.GetHash() => 0xD0FB36E3u;
}
