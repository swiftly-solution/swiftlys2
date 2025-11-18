using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.GameEvents;
using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "ugc_file_download_start"
/// </summary>
public interface EventUgcFileDownloadStart : IGameEvent<EventUgcFileDownloadStart> {

  static EventUgcFileDownloadStart IGameEvent<EventUgcFileDownloadStart>.Create(nint address) => new EventUgcFileDownloadStartImpl(address);

  static string IGameEvent<EventUgcFileDownloadStart>.GetName() => "ugc_file_download_start";

  static uint IGameEvent<EventUgcFileDownloadStart>.GetHash() => 0x2E2A0CEDu;
  /// <summary>
  /// id of this specific content (may be image or map)
  /// <br/>
  /// type: uint64
  /// </summary>
  ulong HContent { get; set; }

  /// <summary>
  /// id of the associated content package
  /// <br/>
  /// type: uint64
  /// </summary>
  ulong PublishedFileId { get; set; }

}
