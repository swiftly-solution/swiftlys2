using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramRouterPingReplyImpl : TypedProtobuf<CMsgSteamDatagramRouterPingReply>, CMsgSteamDatagramRouterPingReply
{
    public CMsgSteamDatagramRouterPingReplyImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ClientTimestamp
    { get => Accessor.GetUInt32("client_timestamp"); set => Accessor.SetUInt32("client_timestamp", value); }
    public IProtobufRepeatedFieldValueType<uint> LatencyDatacenterIds
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "latency_datacenter_ids"); }
    public IProtobufRepeatedFieldValueType<uint> LatencyPingMs
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "latency_ping_ms"); }
    public IProtobufRepeatedFieldValueType<uint> LatencyDatacenterIdsP2p
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "latency_datacenter_ids_p2p"); }
    public IProtobufRepeatedFieldValueType<uint> LatencyPingMsP2p
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "latency_ping_ms_p2p"); }
    public uint YourPublicIp
    { get => Accessor.GetUInt32("your_public_ip"); set => Accessor.SetUInt32("your_public_ip", value); }
    public uint YourPublicPort
    { get => Accessor.GetUInt32("your_public_port"); set => Accessor.SetUInt32("your_public_port", value); }
    public uint ServerTime
    { get => Accessor.GetUInt32("server_time"); set => Accessor.SetUInt32("server_time", value); }
    public ulong Challenge
    { get => Accessor.GetUInt64("challenge"); set => Accessor.SetUInt64("challenge", value); }
    public uint SecondsUntilShutdown
    { get => Accessor.GetUInt32("seconds_until_shutdown"); set => Accessor.SetUInt32("seconds_until_shutdown", value); }
    public uint ClientCookie
    { get => Accessor.GetUInt32("client_cookie"); set => Accessor.SetUInt32("client_cookie", value); }
    public uint RecvTos
    { get => Accessor.GetUInt32("recv_tos"); set => Accessor.SetUInt32("recv_tos", value); }
    public uint EchoSentTos
    { get => Accessor.GetUInt32("echo_sent_tos"); set => Accessor.SetUInt32("echo_sent_tos", value); }
    public uint SentTos
    { get => Accessor.GetUInt32("sent_tos"); set => Accessor.SetUInt32("sent_tos", value); }
    public uint EchoRequestReplyTos
    { get => Accessor.GetUInt32("echo_request_reply_tos"); set => Accessor.SetUInt32("echo_request_reply_tos", value); }
    public uint ScoringPenaltyRelayCluster
    { get => Accessor.GetUInt32("scoring_penalty_relay_cluster"); set => Accessor.SetUInt32("scoring_penalty_relay_cluster", value); }
    public uint Flags
    { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramRouterPingReply_RouteException> RouteExceptions
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramRouterPingReply_RouteException>(Accessor, "route_exceptions"); }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramRouterPingReply_AltAddress> AltAddresses
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramRouterPingReply_AltAddress>(Accessor, "alt_addresses"); }
    public byte[] DummyPad
    { get => Accessor.GetBytes("dummy_pad"); set => Accessor.SetBytes("dummy_pad", value); }
    public ulong DummyVarint
    { get => Accessor.GetUInt64("dummy_varint"); set => Accessor.SetUInt64("dummy_varint", value); }
}