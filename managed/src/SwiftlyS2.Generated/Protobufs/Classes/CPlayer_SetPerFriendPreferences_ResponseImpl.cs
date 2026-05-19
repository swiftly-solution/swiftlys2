using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_SetPerFriendPreferences_ResponseImpl : TypedProtobuf<CPlayer_SetPerFriendPreferences_Response>, CPlayer_SetPerFriendPreferences_Response
{
    public CPlayer_SetPerFriendPreferences_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

}