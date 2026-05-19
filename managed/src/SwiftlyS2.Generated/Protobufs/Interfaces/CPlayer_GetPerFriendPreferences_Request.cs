using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetPerFriendPreferences_Request : ITypedProtobuf<CPlayer_GetPerFriendPreferences_Request>
{
    static CPlayer_GetPerFriendPreferences_Request ITypedProtobuf<CPlayer_GetPerFriendPreferences_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetPerFriendPreferences_RequestImpl(handle, isManuallyAllocated);

}