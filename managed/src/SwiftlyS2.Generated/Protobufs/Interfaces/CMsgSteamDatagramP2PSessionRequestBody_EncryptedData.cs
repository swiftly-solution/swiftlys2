using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramP2PSessionRequestBody_EncryptedData : ITypedProtobuf<CMsgSteamDatagramP2PSessionRequestBody_EncryptedData>
{
    static CMsgSteamDatagramP2PSessionRequestBody_EncryptedData ITypedProtobuf<CMsgSteamDatagramP2PSessionRequestBody_EncryptedData>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramP2PSessionRequestBody_EncryptedDataImpl(handle, isManuallyAllocated);

    public string PeerIdentityString { get; set; }
}