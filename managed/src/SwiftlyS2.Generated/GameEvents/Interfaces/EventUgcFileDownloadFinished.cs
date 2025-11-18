using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "ugc_file_download_finished"
/// </summary>
public interface EventUgcFileDownloadFinished : IGameEvent<EventUgcFileDownloadFinished>
{

    static EventUgcFileDownloadFinished IGameEvent<EventUgcFileDownloadFinished>.Create( nint address ) => new EventUgcFileDownloadFinishedImpl(address);

    static string IGameEvent<EventUgcFileDownloadFinished>.GetName() => "ugc_file_download_finished";

    static uint IGameEvent<EventUgcFileDownloadFinished>.GetHash() => 0x2800BC41u;
    /// <summary>
    /// id of this specific content (may be image or map)
    /// <br/>
    /// type: uint64
    /// </summary>
    public ulong HContent { get; set; }

}
