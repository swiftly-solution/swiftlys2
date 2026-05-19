using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramCertificateSigned : ITypedProtobuf<CMsgSteamDatagramCertificateSigned>
{
    static CMsgSteamDatagramCertificateSigned ITypedProtobuf<CMsgSteamDatagramCertificateSigned>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramCertificateSignedImpl(handle, isManuallyAllocated);

    public byte[] Cert { get; set; }
    public ulong CaKeyId { get; set; }
    public byte[] CaSignature { get; set; }
    public byte[] PrivateKeyData { get; set; }
}