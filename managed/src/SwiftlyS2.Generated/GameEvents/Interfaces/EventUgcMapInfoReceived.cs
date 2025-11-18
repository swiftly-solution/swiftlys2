using SwiftlyS2.Core.GameEventDefinitions;
using SwiftlyS2.Shared.GameEvents;

namespace SwiftlyS2.Shared.GameEventDefinitions;

/// <summary> 
/// Event "ugc_map_info_received"
/// </summary>
public interface EventUgcMapInfoReceived : IGameEvent<EventUgcMapInfoReceived>
{

    static EventUgcMapInfoReceived IGameEvent<EventUgcMapInfoReceived>.Create( nint address ) => new EventUgcMapInfoReceivedImpl(address);

    static string IGameEvent<EventUgcMapInfoReceived>.GetName() => "ugc_map_info_received";

    static uint IGameEvent<EventUgcMapInfoReceived>.GetHash() => 0x723494B6u;
    /// <summary>
    /// type: uint64
    /// </summary>
    public ulong PublishedFileId { get; set; }

}
