using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramSignedMessageGenericImpl : TypedProtobuf<CMsgSteamDatagramSignedMessageGeneric>, CMsgSteamDatagramSignedMessageGeneric
{
    public CMsgSteamDatagramSignedMessageGenericImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgSteamDatagramCertificateSigned Cert
    { get => new CMsgSteamDatagramCertificateSignedImpl(NativeNetMessages.GetNestedMessage(Address, "cert"), false); }
    public byte[] SignedData
    { get => Accessor.GetBytes("signed_data"); set => Accessor.SetBytes("signed_data", value); }
    public byte[] Signature
    { get => Accessor.GetBytes("signature"); set => Accessor.SetBytes("signature", value); }
    public byte[] DummyPad
    { get => Accessor.GetBytes("dummy_pad"); set => Accessor.SetBytes("dummy_pad", value); }
}