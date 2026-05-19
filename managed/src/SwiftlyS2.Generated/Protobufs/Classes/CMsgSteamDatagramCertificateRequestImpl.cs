using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramCertificateRequestImpl : TypedProtobuf<CMsgSteamDatagramCertificateRequest>, CMsgSteamDatagramCertificateRequest
{
    public CMsgSteamDatagramCertificateRequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgSteamDatagramCertificate Cert
    { get => new CMsgSteamDatagramCertificateImpl(NativeNetMessages.GetNestedMessage(Address, "cert"), false); }
}