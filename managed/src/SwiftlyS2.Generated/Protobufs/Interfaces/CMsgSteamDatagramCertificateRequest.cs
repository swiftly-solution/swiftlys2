using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramCertificateRequest : ITypedProtobuf<CMsgSteamDatagramCertificateRequest>
{
    static CMsgSteamDatagramCertificateRequest ITypedProtobuf<CMsgSteamDatagramCertificateRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramCertificateRequestImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramCertificate Cert { get; }
}