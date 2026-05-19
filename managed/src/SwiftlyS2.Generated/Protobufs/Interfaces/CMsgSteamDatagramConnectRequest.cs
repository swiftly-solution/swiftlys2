using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramConnectRequest : ITypedProtobuf<CMsgSteamDatagramConnectRequest>
{
    static CMsgSteamDatagramConnectRequest ITypedProtobuf<CMsgSteamDatagramConnectRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramConnectRequestImpl(handle, isManuallyAllocated);

    public uint ConnectionId { get; set; }
    public ulong MyTimestamp { get; set; }
    public uint PingEstMs { get; set; }
    public uint VirtualPort { get; set; }
    public uint GameserverRelaySessionId { get; set; }
    public CMsgSteamDatagramSessionCryptInfoSigned Crypt { get; }
    public CMsgSteamDatagramCertificateSigned Cert { get; }
    public ulong RoutingSecret { get; set; }
    public ulong LegacyClientSteamId { get; set; }
}