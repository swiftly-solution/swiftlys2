using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamNetworkingP2PRendezvous_ConnectRequest : ITypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ConnectRequest>
{
    static CMsgSteamNetworkingP2PRendezvous_ConnectRequest ITypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ConnectRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamNetworkingP2PRendezvous_ConnectRequestImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramSessionCryptInfoSigned Crypt { get; }
    public CMsgSteamDatagramCertificateSigned Cert { get; }
    public uint ToVirtualPort { get; set; }
    public uint FromVirtualPort { get; set; }
    public string FromFakeip { get; set; }
}