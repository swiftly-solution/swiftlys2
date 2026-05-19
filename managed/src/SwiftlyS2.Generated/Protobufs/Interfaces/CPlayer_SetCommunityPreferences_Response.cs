using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_SetCommunityPreferences_Response : ITypedProtobuf<CPlayer_SetCommunityPreferences_Response>
{
    static CPlayer_SetCommunityPreferences_Response ITypedProtobuf<CPlayer_SetCommunityPreferences_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_SetCommunityPreferences_ResponseImpl(handle, isManuallyAllocated);

}