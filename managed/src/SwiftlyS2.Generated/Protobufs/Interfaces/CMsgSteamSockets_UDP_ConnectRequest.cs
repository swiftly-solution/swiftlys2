using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamSockets_UDP_ConnectRequest : ITypedProtobuf<CMsgSteamSockets_UDP_ConnectRequest>
{
    static CMsgSteamSockets_UDP_ConnectRequest ITypedProtobuf<CMsgSteamSockets_UDP_ConnectRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamSockets_UDP_ConnectRequestImpl(handle, isManuallyAllocated);

    public uint ClientConnectionId { get; set; }
    public ulong Challenge { get; set; }
    public ulong MyTimestamp { get; set; }
    public uint PingEstMs { get; set; }
    public CMsgSteamDatagramSessionCryptInfoSigned Crypt { get; }
    public CMsgSteamDatagramCertificateSigned Cert { get; }
    public uint LegacyProtocolVersion { get; set; }
    public string IdentityString { get; set; }
    public ulong LegacyClientSteamId { get; set; }
    public CMsgSteamNetworkingIdentityLegacyBinary LegacyIdentityBinary { get; }
}