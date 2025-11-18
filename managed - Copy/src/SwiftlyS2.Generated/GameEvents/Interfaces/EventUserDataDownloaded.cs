using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "user_data_downloaded"
/// fired when achievements/stats are downloaded from Steam or XBox Live
/// </summary>
public interface EventUserDataDownloaded : IGameEvent<EventUserDataDownloaded> {

  static EventUserDataDownloaded IGameEvent<EventUserDataDownloaded>.Create(nint address) => new EventUserDataDownloadedImpl(address);

  static string IGameEvent<EventUserDataDownloaded>.GetName() => "user_data_downloaded";

  static uint IGameEvent<EventUserDataDownloaded>.GetHash() => 0xA7AE5F51u;
}
