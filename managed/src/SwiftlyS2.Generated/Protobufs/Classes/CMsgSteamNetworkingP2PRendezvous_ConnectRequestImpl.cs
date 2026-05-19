using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamNetworkingP2PRendezvous_ConnectRequestImpl : TypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ConnectRequest>, CMsgSteamNetworkingP2PRendezvous_ConnectRequest
{
    public CMsgSteamNetworkingP2PRendezvous_ConnectRequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgSteamDatagramSessionCryptInfoSigned Crypt
    { get => new CMsgSteamDatagramSessionCryptInfoSignedImpl(NativeNetMessages.GetNestedMessage(Address, "crypt"), false); }
    public CMsgSteamDatagramCertificateSigned Cert
    { get => new CMsgSteamDatagramCertificateSignedImpl(NativeNetMessages.GetNestedMessage(Address, "cert"), false); }
    public uint ToVirtualPort
    { get => Accessor.GetUInt32("to_virtual_port"); set => Accessor.SetUInt32("to_virtual_port", value); }
    public uint FromVirtualPort
    { get => Accessor.GetUInt32("from_virtual_port"); set => Accessor.SetUInt32("from_virtual_port", value); }
    public string FromFakeip
    { get => Accessor.GetString("from_fakeip"); set => Accessor.SetString("from_fakeip", value); }
}