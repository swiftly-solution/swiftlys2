using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramP2PSessionRequestBody_EncryptedDataImpl : TypedProtobuf<CMsgSteamDatagramP2PSessionRequestBody_EncryptedData>, CMsgSteamDatagramP2PSessionRequestBody_EncryptedData
{
    public CMsgSteamDatagramP2PSessionRequestBody_EncryptedDataImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string PeerIdentityString
    { get => Accessor.GetString("peer_identity_string"); set => Accessor.SetString("peer_identity_string", value); }
}