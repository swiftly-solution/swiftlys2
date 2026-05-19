using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_SetPerFriendPreferences_Response : ITypedProtobuf<CPlayer_SetPerFriendPreferences_Response>
{
    static CPlayer_SetPerFriendPreferences_Response ITypedProtobuf<CPlayer_SetPerFriendPreferences_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_SetPerFriendPreferences_ResponseImpl(handle, isManuallyAllocated);

}