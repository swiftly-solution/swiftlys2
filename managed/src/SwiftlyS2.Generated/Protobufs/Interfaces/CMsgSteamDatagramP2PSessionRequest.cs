using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramP2PSessionRequest : ITypedProtobuf<CMsgSteamDatagramP2PSessionRequest>
{
    static CMsgSteamDatagramP2PSessionRequest ITypedProtobuf<CMsgSteamDatagramP2PSessionRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramP2PSessionRequestImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramCertificateSigned Cert { get; }
    public byte[] Body { get; set; }
    public byte[] Signature { get; set; }
}