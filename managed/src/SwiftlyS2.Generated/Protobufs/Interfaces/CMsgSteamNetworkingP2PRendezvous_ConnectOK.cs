using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamNetworkingP2PRendezvous_ConnectOK : ITypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ConnectOK>
{
    static CMsgSteamNetworkingP2PRendezvous_ConnectOK ITypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ConnectOK>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamNetworkingP2PRendezvous_ConnectOKImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramSessionCryptInfoSigned Crypt { get; }
    public CMsgSteamDatagramCertificateSigned Cert { get; }
}