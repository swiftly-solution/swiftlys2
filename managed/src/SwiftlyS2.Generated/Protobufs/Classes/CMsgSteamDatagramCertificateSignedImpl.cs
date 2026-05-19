using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramCertificateSignedImpl : TypedProtobuf<CMsgSteamDatagramCertificateSigned>, CMsgSteamDatagramCertificateSigned
{
    public CMsgSteamDatagramCertificateSignedImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public byte[] Cert
    { get => Accessor.GetBytes("cert"); set => Accessor.SetBytes("cert", value); }
    public ulong CaKeyId
    { get => Accessor.GetUInt64("ca_key_id"); set => Accessor.SetUInt64("ca_key_id", value); }
    public byte[] CaSignature
    { get => Accessor.GetBytes("ca_signature"); set => Accessor.SetBytes("ca_signature", value); }
    public byte[] PrivateKeyData
    { get => Accessor.GetBytes("private_key_data"); set => Accessor.SetBytes("private_key_data", value); }
}