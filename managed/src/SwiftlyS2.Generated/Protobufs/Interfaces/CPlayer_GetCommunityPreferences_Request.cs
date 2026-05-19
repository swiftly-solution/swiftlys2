using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetCommunityPreferences_Request : ITypedProtobuf<CPlayer_GetCommunityPreferences_Request>
{
    static CPlayer_GetCommunityPreferences_Request ITypedProtobuf<CPlayer_GetCommunityPreferences_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetCommunityPreferences_RequestImpl(handle, isManuallyAllocated);

}