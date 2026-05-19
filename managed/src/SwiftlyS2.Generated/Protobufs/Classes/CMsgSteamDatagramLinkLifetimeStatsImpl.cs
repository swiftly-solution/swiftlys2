using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramLinkLifetimeStatsImpl : TypedProtobuf<CMsgSteamDatagramLinkLifetimeStats>, CMsgSteamDatagramLinkLifetimeStats
{
    public CMsgSteamDatagramLinkLifetimeStatsImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ConnectedSeconds
    { get => Accessor.GetUInt32("connected_seconds"); set => Accessor.SetUInt32("connected_seconds", value); }
    public ulong PacketsSent
    { get => Accessor.GetUInt64("packets_sent"); set => Accessor.SetUInt64("packets_sent", value); }
    public ulong KbSent
    { get => Accessor.GetUInt64("kb_sent"); set => Accessor.SetUInt64("kb_sent", value); }
    public ulong PacketsRecv
    { get => Accessor.GetUInt64("packets_recv"); set => Accessor.SetUInt64("packets_recv", value); }
    public ulong KbRecv
    { get => Accessor.GetUInt64("kb_recv"); set => Accessor.SetUInt64("kb_recv", value); }
    public ulong PacketsRecvSequenced
    { get => Accessor.GetUInt64("packets_recv_sequenced"); set => Accessor.SetUInt64("packets_recv_sequenced", value); }
    public ulong PacketsRecvDropped
    { get => Accessor.GetUInt64("packets_recv_dropped"); set => Accessor.SetUInt64("packets_recv_dropped", value); }
    public ulong PacketsRecvOutOfOrder
    { get => Accessor.GetUInt64("packets_recv_out_of_order"); set => Accessor.SetUInt64("packets_recv_out_of_order", value); }
    public ulong PacketsRecvOutOfOrderCorrected
    { get => Accessor.GetUInt64("packets_recv_out_of_order_corrected"); set => Accessor.SetUInt64("packets_recv_out_of_order_corrected", value); }
    public ulong PacketsRecvDuplicate
    { get => Accessor.GetUInt64("packets_recv_duplicate"); set => Accessor.SetUInt64("packets_recv_duplicate", value); }
    public ulong PacketsRecvLurch
    { get => Accessor.GetUInt64("packets_recv_lurch"); set => Accessor.SetUInt64("packets_recv_lurch", value); }
    public IProtobufRepeatedFieldValueType<ulong> MultipathPacketsRecvSequenced
    { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "multipath_packets_recv_sequenced"); }
    public IProtobufRepeatedFieldValueType<ulong> MultipathPacketsRecvLater
    { get => new ProtobufRepeatedFieldValueType<ulong>(Accessor, "multipath_packets_recv_later"); }
    public uint MultipathSendEnabled
    { get => Accessor.GetUInt32("multipath_send_enabled"); set => Accessor.SetUInt32("multipath_send_enabled", value); }
    public uint QualityHistogram100
    { get => Accessor.GetUInt32("quality_histogram_100"); set => Accessor.SetUInt32("quality_histogram_100", value); }
    public uint QualityHistogram99
    { get => Accessor.GetUInt32("quality_histogram_99"); set => Accessor.SetUInt32("quality_histogram_99", value); }
    public uint QualityHistogram97
    { get => Accessor.GetUInt32("quality_histogram_97"); set => Accessor.SetUInt32("quality_histogram_97", value); }
    public uint QualityHistogram95
    { get => Accessor.GetUInt32("quality_histogram_95"); set => Accessor.SetUInt32("quality_histogram_95", value); }
    public uint QualityHistogram90
    { get => Accessor.GetUInt32("quality_histogram_90"); set => Accessor.SetUInt32("quality_histogram_90", value); }
    public uint QualityHistogram75
    { get => Accessor.GetUInt32("quality_histogram_75"); set => Accessor.SetUInt32("quality_histogram_75", value); }
    public uint QualityHistogram50
    { get => Accessor.GetUInt32("quality_histogram_50"); set => Accessor.SetUInt32("quality_histogram_50", value); }
    public uint QualityHistogram1
    { get => Accessor.GetUInt32("quality_histogram_1"); set => Accessor.SetUInt32("quality_histogram_1", value); }
    public uint QualityHistogramDead
    { get => Accessor.GetUInt32("quality_histogram_dead"); set => Accessor.SetUInt32("quality_histogram_dead", value); }
    public uint QualityNtile2nd
    { get => Accessor.GetUInt32("quality_ntile_2nd"); set => Accessor.SetUInt32("quality_ntile_2nd", value); }
    public uint QualityNtile5th
    { get => Accessor.GetUInt32("quality_ntile_5th"); set => Accessor.SetUInt32("quality_ntile_5th", value); }
    public uint QualityNtile25th
    { get => Accessor.GetUInt32("quality_ntile_25th"); set => Accessor.SetUInt32("quality_ntile_25th", value); }
    public uint QualityNtile50th
    { get => Accessor.GetUInt32("quality_ntile_50th"); set => Accessor.SetUInt32("quality_ntile_50th", value); }
    public uint PingHistogram25
    { get => Accessor.GetUInt32("ping_histogram_25"); set => Accessor.SetUInt32("ping_histogram_25", value); }
    public uint PingHistogram50
    { get => Accessor.GetUInt32("ping_histogram_50"); set => Accessor.SetUInt32("ping_histogram_50", value); }
    public uint PingHistogram75
    { get => Accessor.GetUInt32("ping_histogram_75"); set => Accessor.SetUInt32("ping_histogram_75", value); }
    public uint PingHistogram100
    { get => Accessor.GetUInt32("ping_histogram_100"); set => Accessor.SetUInt32("ping_histogram_100", value); }
    public uint PingHistogram125
    { get => Accessor.GetUInt32("ping_histogram_125"); set => Accessor.SetUInt32("ping_histogram_125", value); }
    public uint PingHistogram150
    { get => Accessor.GetUInt32("ping_histogram_150"); set => Accessor.SetUInt32("ping_histogram_150", value); }
    public uint PingHistogram200
    { get => Accessor.GetUInt32("ping_histogram_200"); set => Accessor.SetUInt32("ping_histogram_200", value); }
    public uint PingHistogram300
    { get => Accessor.GetUInt32("ping_histogram_300"); set => Accessor.SetUInt32("ping_histogram_300", value); }
    public uint PingHistogramMax
    { get => Accessor.GetUInt32("ping_histogram_max"); set => Accessor.SetUInt32("ping_histogram_max", value); }
    public uint PingNtile5th
    { get => Accessor.GetUInt32("ping_ntile_5th"); set => Accessor.SetUInt32("ping_ntile_5th", value); }
    public uint PingNtile50th
    { get => Accessor.GetUInt32("ping_ntile_50th"); set => Accessor.SetUInt32("ping_ntile_50th", value); }
    public uint PingNtile75th
    { get => Accessor.GetUInt32("ping_ntile_75th"); set => Accessor.SetUInt32("ping_ntile_75th", value); }
    public uint PingNtile95th
    { get => Accessor.GetUInt32("ping_ntile_95th"); set => Accessor.SetUInt32("ping_ntile_95th", value); }
    public uint PingNtile98th
    { get => Accessor.GetUInt32("ping_ntile_98th"); set => Accessor.SetUInt32("ping_ntile_98th", value); }
    public uint JitterHistogramNegligible
    { get => Accessor.GetUInt32("jitter_histogram_negligible"); set => Accessor.SetUInt32("jitter_histogram_negligible", value); }
    public uint JitterHistogram1
    { get => Accessor.GetUInt32("jitter_histogram_1"); set => Accessor.SetUInt32("jitter_histogram_1", value); }
    public uint JitterHistogram2
    { get => Accessor.GetUInt32("jitter_histogram_2"); set => Accessor.SetUInt32("jitter_histogram_2", value); }
    public uint JitterHistogram5
    { get => Accessor.GetUInt32("jitter_histogram_5"); set => Accessor.SetUInt32("jitter_histogram_5", value); }
    public uint JitterHistogram10
    { get => Accessor.GetUInt32("jitter_histogram_10"); set => Accessor.SetUInt32("jitter_histogram_10", value); }
    public uint JitterHistogram20
    { get => Accessor.GetUInt32("jitter_histogram_20"); set => Accessor.SetUInt32("jitter_histogram_20", value); }
}