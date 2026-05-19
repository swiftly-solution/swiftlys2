using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramClientSwitchedPrimaryImpl : TypedProtobuf<CMsgSteamDatagramClientSwitchedPrimary>, CMsgSteamDatagramClientSwitchedPrimary
{
    public CMsgSteamDatagramClientSwitchedPrimaryImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ConnectionId
    { get => Accessor.GetUInt32("connection_id"); set => Accessor.SetUInt32("connection_id", value); }
    public uint FromIp
    { get => Accessor.GetUInt32("from_ip"); set => Accessor.SetUInt32("from_ip", value); }
    public uint FromPort
    { get => Accessor.GetUInt32("from_port"); set => Accessor.SetUInt32("from_port", value); }
    public uint FromRouterCluster
    { get => Accessor.GetUInt32("from_router_cluster"); set => Accessor.SetUInt32("from_router_cluster", value); }
    public uint FromActiveTime
    { get => Accessor.GetUInt32("from_active_time"); set => Accessor.SetUInt32("from_active_time", value); }
    public uint FromActivePacketsRecv
    { get => Accessor.GetUInt32("from_active_packets_recv"); set => Accessor.SetUInt32("from_active_packets_recv", value); }
    public string FromDroppedReason
    { get => Accessor.GetString("from_dropped_reason"); set => Accessor.SetString("from_dropped_reason", value); }
    public uint GapMs
    { get => Accessor.GetUInt32("gap_ms"); set => Accessor.SetUInt32("gap_ms", value); }
    public CMsgSteamDatagramClientSwitchedPrimary_RouterQuality FromQualityNow
    { get => new CMsgSteamDatagramClientSwitchedPrimary_RouterQualityImpl(NativeNetMessages.GetNestedMessage(Address, "from_quality_now"), false); }
    public CMsgSteamDatagramClientSwitchedPrimary_RouterQuality ToQualityNow
    { get => new CMsgSteamDatagramClientSwitchedPrimary_RouterQualityImpl(NativeNetMessages.GetNestedMessage(Address, "to_quality_now"), false); }
    public CMsgSteamDatagramClientSwitchedPrimary_RouterQuality FromQualityThen
    { get => new CMsgSteamDatagramClientSwitchedPrimary_RouterQualityImpl(NativeNetMessages.GetNestedMessage(Address, "from_quality_then"), false); }
    public CMsgSteamDatagramClientSwitchedPrimary_RouterQuality ToQualityThen
    { get => new CMsgSteamDatagramClientSwitchedPrimary_RouterQualityImpl(NativeNetMessages.GetNestedMessage(Address, "to_quality_then"), false); }
}