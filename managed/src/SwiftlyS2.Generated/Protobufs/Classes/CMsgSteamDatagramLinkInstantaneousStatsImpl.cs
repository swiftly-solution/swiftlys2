using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramLinkInstantaneousStatsImpl : TypedProtobuf<CMsgSteamDatagramLinkInstantaneousStats>, CMsgSteamDatagramLinkInstantaneousStats
{
    public CMsgSteamDatagramLinkInstantaneousStatsImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint OutPacketsPerSecX10
    { get => Accessor.GetUInt32("out_packets_per_sec_x10"); set => Accessor.SetUInt32("out_packets_per_sec_x10", value); }
    public uint OutBytesPerSec
    { get => Accessor.GetUInt32("out_bytes_per_sec"); set => Accessor.SetUInt32("out_bytes_per_sec", value); }
    public uint InPacketsPerSecX10
    { get => Accessor.GetUInt32("in_packets_per_sec_x10"); set => Accessor.SetUInt32("in_packets_per_sec_x10", value); }
    public uint InBytesPerSec
    { get => Accessor.GetUInt32("in_bytes_per_sec"); set => Accessor.SetUInt32("in_bytes_per_sec", value); }
    public uint PingMs
    { get => Accessor.GetUInt32("ping_ms"); set => Accessor.SetUInt32("ping_ms", value); }
    public uint PacketsDroppedPct
    { get => Accessor.GetUInt32("packets_dropped_pct"); set => Accessor.SetUInt32("packets_dropped_pct", value); }
    public uint PacketsWeirdSequencePct
    { get => Accessor.GetUInt32("packets_weird_sequence_pct"); set => Accessor.SetUInt32("packets_weird_sequence_pct", value); }
    public uint PeakJitterUsec
    { get => Accessor.GetUInt32("peak_jitter_usec"); set => Accessor.SetUInt32("peak_jitter_usec", value); }
}