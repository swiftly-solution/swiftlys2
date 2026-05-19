using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetCommunityPreferences_RequestImpl : TypedProtobuf<CPlayer_GetCommunityPreferences_Request>, CPlayer_GetCommunityPreferences_Request
{
    public CPlayer_GetCommunityPreferences_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

}