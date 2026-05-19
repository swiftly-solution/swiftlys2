using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramRouterPingReply : ITypedProtobuf<CMsgSteamDatagramRouterPingReply>
{
    static CMsgSteamDatagramRouterPingReply ITypedProtobuf<CMsgSteamDatagramRouterPingReply>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramRouterPingReplyImpl(handle, isManuallyAllocated);

    public uint ClientTimestamp { get; set; }
    public IProtobufRepeatedFieldValueType<uint> LatencyDatacenterIds { get; }
    public IProtobufRepeatedFieldValueType<uint> LatencyPingMs { get; }
    public IProtobufRepeatedFieldValueType<uint> LatencyDatacenterIdsP2p { get; }
    public IProtobufRepeatedFieldValueType<uint> LatencyPingMsP2p { get; }
    public uint YourPublicIp { get; set; }
    public uint YourPublicPort { get; set; }
    public uint ServerTime { get; set; }
    public ulong Challenge { get; set; }
    public uint SecondsUntilShutdown { get; set; }
    public uint ClientCookie { get; set; }
    public uint RecvTos { get; set; }
    public uint EchoSentTos { get; set; }
    public uint SentTos { get; set; }
    public uint EchoRequestReplyTos { get; set; }
    public uint ScoringPenaltyRelayCluster { get; set; }
    public uint Flags { get; set; }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramRouterPingReply_RouteException> RouteExceptions { get; }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramRouterPingReply_AltAddress> AltAddresses { get; }
    public byte[] DummyPad { get; set; }
    public ulong DummyVarint { get; set; }
}