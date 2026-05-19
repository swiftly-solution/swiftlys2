using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamNetworkingP2PRendezvous_ConnectOKImpl : TypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ConnectOK>, CMsgSteamNetworkingP2PRendezvous_ConnectOK
{
    public CMsgSteamNetworkingP2PRendezvous_ConnectOKImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgSteamDatagramSessionCryptInfoSigned Crypt
    { get => new CMsgSteamDatagramSessionCryptInfoSignedImpl(NativeNetMessages.GetNestedMessage(Address, "crypt"), false); }
    public CMsgSteamDatagramCertificateSigned Cert
    { get => new CMsgSteamDatagramCertificateSignedImpl(NativeNetMessages.GetNestedMessage(Address, "cert"), false); }
}