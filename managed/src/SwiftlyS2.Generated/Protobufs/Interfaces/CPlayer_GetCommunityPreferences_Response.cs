using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetCommunityPreferences_Response : ITypedProtobuf<CPlayer_GetCommunityPreferences_Response>
{
    static CPlayer_GetCommunityPreferences_Response ITypedProtobuf<CPlayer_GetCommunityPreferences_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetCommunityPreferences_ResponseImpl(handle, isManuallyAllocated);

    public CPlayer_CommunityPreferences Preferences { get; }
}