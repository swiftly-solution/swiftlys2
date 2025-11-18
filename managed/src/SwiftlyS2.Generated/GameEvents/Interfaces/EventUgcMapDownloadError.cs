using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "ugc_map_download_error"
/// </summary>
public interface EventUgcMapDownloadError : IGameEvent<EventUgcMapDownloadError>
{

    static EventUgcMapDownloadError IGameEvent<EventUgcMapDownloadError>.Create( nint address ) => new EventUgcMapDownloadErrorImpl(address);

    static string IGameEvent<EventUgcMapDownloadError>.GetName() => "ugc_map_download_error";

    static uint IGameEvent<EventUgcMapDownloadError>.GetHash() => 0xAAE6E991u;
    /// <summary>
    /// type: uint64
    /// </summary>
    public ulong PublishedFileId { get; set; }

    /// <summary>
    /// type: long
    /// </summary>
    public int ErrorCode { get; set; }

}
