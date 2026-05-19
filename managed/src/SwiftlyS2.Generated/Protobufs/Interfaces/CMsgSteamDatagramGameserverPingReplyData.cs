using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramGameserverPingReplyData : ITypedProtobuf<CMsgSteamDatagramGameserverPingReplyData>
{
    static CMsgSteamDatagramGameserverPingReplyData ITypedProtobuf<CMsgSteamDatagramGameserverPingReplyData>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramGameserverPingReplyDataImpl(handle, isManuallyAllocated);

    public uint EchoRelayUnixTime { get; set; }
    public byte[] Echo { get; set; }
    public ulong LegacyChallenge { get; set; }
    public uint LegacyRouterTimestamp { get; set; }
    public uint DataCenterId { get; set; }
    public uint Appid { get; set; }
    public uint ProtocolVersion { get; set; }
    public string Build { get; set; }
    public ulong NetworkConfigVersion { get; set; }
    public uint MyUnixTime { get; set; }
    public byte[] RoutingBlob { get; set; }
}