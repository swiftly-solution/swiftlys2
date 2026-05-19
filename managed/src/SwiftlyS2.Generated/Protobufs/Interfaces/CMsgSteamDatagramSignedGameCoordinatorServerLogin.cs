using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramSignedGameCoordinatorServerLogin : ITypedProtobuf<CMsgSteamDatagramSignedGameCoordinatorServerLogin>
{
    static CMsgSteamDatagramSignedGameCoordinatorServerLogin ITypedProtobuf<CMsgSteamDatagramSignedGameCoordinatorServerLogin>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramSignedGameCoordinatorServerLoginImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramCertificateSigned Cert { get; }
    public byte[] Login { get; set; }
    public byte[] Signature { get; set; }
}