using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_SetPerFriendPreferences_Request : ITypedProtobuf<CPlayer_SetPerFriendPreferences_Request>
{
    static CPlayer_SetPerFriendPreferences_Request ITypedProtobuf<CPlayer_SetPerFriendPreferences_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_SetPerFriendPreferences_RequestImpl(handle, isManuallyAllocated);

    public PerFriendPreferences Preferences { get; }
}