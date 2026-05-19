using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramSignedGameCoordinatorServerLoginImpl : TypedProtobuf<CMsgSteamDatagramSignedGameCoordinatorServerLogin>, CMsgSteamDatagramSignedGameCoordinatorServerLogin
{
    public CMsgSteamDatagramSignedGameCoordinatorServerLoginImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgSteamDatagramCertificateSigned Cert
    { get => new CMsgSteamDatagramCertificateSignedImpl(NativeNetMessages.GetNestedMessage(Address, "cert"), false); }
    public byte[] Login
    { get => Accessor.GetBytes("login"); set => Accessor.SetBytes("login", value); }
    public byte[] Signature
    { get => Accessor.GetBytes("signature"); set => Accessor.SetBytes("signature", value); }
}