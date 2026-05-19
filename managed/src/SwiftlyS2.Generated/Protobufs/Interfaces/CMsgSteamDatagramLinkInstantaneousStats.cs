using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramLinkInstantaneousStats : ITypedProtobuf<CMsgSteamDatagramLinkInstantaneousStats>
{
    static CMsgSteamDatagramLinkInstantaneousStats ITypedProtobuf<CMsgSteamDatagramLinkInstantaneousStats>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramLinkInstantaneousStatsImpl(handle, isManuallyAllocated);

    public uint OutPacketsPerSecX10 { get; set; }
    public uint OutBytesPerSec { get; set; }
    public uint InPacketsPerSecX10 { get; set; }
    public uint InBytesPerSec { get; set; }
    public uint PingMs { get; set; }
    public uint PacketsDroppedPct { get; set; }
    public uint PacketsWeirdSequencePct { get; set; }
    public uint PeakJitterUsec { get; set; }
}