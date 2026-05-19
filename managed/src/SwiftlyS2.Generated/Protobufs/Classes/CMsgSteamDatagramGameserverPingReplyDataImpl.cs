using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramGameserverPingReplyDataImpl : TypedProtobuf<CMsgSteamDatagramGameserverPingReplyData>, CMsgSteamDatagramGameserverPingReplyData
{
    public CMsgSteamDatagramGameserverPingReplyDataImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint EchoRelayUnixTime
    { get => Accessor.GetUInt32("echo_relay_unix_time"); set => Accessor.SetUInt32("echo_relay_unix_time", value); }
    public byte[] Echo
    { get => Accessor.GetBytes("echo"); set => Accessor.SetBytes("echo", value); }
    public ulong LegacyChallenge
    { get => Accessor.GetUInt64("legacy_challenge"); set => Accessor.SetUInt64("legacy_challenge", value); }
    public uint LegacyRouterTimestamp
    { get => Accessor.GetUInt32("legacy_router_timestamp"); set => Accessor.SetUInt32("legacy_router_timestamp", value); }
    public uint DataCenterId
    { get => Accessor.GetUInt32("data_center_id"); set => Accessor.SetUInt32("data_center_id", value); }
    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public uint ProtocolVersion
    { get => Accessor.GetUInt32("protocol_version"); set => Accessor.SetUInt32("protocol_version", value); }
    public string Build
    { get => Accessor.GetString("build"); set => Accessor.SetString("build", value); }
    public ulong NetworkConfigVersion
    { get => Accessor.GetUInt64("network_config_version"); set => Accessor.SetUInt64("network_config_version", value); }
    public uint MyUnixTime
    { get => Accessor.GetUInt32("my_unix_time"); set => Accessor.SetUInt32("my_unix_time", value); }
    public byte[] RoutingBlob
    { get => Accessor.GetBytes("routing_blob"); set => Accessor.SetBytes("routing_blob", value); }
}