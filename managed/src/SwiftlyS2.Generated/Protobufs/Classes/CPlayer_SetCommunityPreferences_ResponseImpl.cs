using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_SetCommunityPreferences_ResponseImpl : TypedProtobuf<CPlayer_SetCommunityPreferences_Response>, CPlayer_SetCommunityPreferences_Response
{
    public CPlayer_SetCommunityPreferences_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

}