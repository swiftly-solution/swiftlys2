using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramLinkLifetimeStats : ITypedProtobuf<CMsgSteamDatagramLinkLifetimeStats>
{
    static CMsgSteamDatagramLinkLifetimeStats ITypedProtobuf<CMsgSteamDatagramLinkLifetimeStats>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramLinkLifetimeStatsImpl(handle, isManuallyAllocated);

    public uint ConnectedSeconds { get; set; }
    public ulong PacketsSent { get; set; }
    public ulong KbSent { get; set; }
    public ulong PacketsRecv { get; set; }
    public ulong KbRecv { get; set; }
    public ulong PacketsRecvSequenced { get; set; }
    public ulong PacketsRecvDropped { get; set; }
    public ulong PacketsRecvOutOfOrder { get; set; }
    public ulong PacketsRecvOutOfOrderCorrected { get; set; }
    public ulong PacketsRecvDuplicate { get; set; }
    public ulong PacketsRecvLurch { get; set; }
    public IProtobufRepeatedFieldValueType<ulong> MultipathPacketsRecvSequenced { get; }
    public IProtobufRepeatedFieldValueType<ulong> MultipathPacketsRecvLater { get; }
    public uint MultipathSendEnabled { get; set; }
    public uint QualityHistogram100 { get; set; }
    public uint QualityHistogram99 { get; set; }
    public uint QualityHistogram97 { get; set; }
    public uint QualityHistogram95 { get; set; }
    public uint QualityHistogram90 { get; set; }
    public uint QualityHistogram75 { get; set; }
    public uint QualityHistogram50 { get; set; }
    public uint QualityHistogram1 { get; set; }
    public uint QualityHistogramDead { get; set; }
    public uint QualityNtile2nd { get; set; }
    public uint QualityNtile5th { get; set; }
    public uint QualityNtile25th { get; set; }
    public uint QualityNtile50th { get; set; }
    public uint PingHistogram25 { get; set; }
    public uint PingHistogram50 { get; set; }
    public uint PingHistogram75 { get; set; }
    public uint PingHistogram100 { get; set; }
    public uint PingHistogram125 { get; set; }
    public uint PingHistogram150 { get; set; }
    public uint PingHistogram200 { get; set; }
    public uint PingHistogram300 { get; set; }
    public uint PingHistogramMax { get; set; }
    public uint PingNtile5th { get; set; }
    public uint PingNtile50th { get; set; }
    public uint PingNtile75th { get; set; }
    public uint PingNtile95th { get; set; }
    public uint PingNtile98th { get; set; }
    public uint JitterHistogramNegligible { get; set; }
    public uint JitterHistogram1 { get; set; }
    public uint JitterHistogram2 { get; set; }
    public uint JitterHistogram5 { get; set; }
    public uint JitterHistogram10 { get; set; }
    public uint JitterHistogram20 { get; set; }
}