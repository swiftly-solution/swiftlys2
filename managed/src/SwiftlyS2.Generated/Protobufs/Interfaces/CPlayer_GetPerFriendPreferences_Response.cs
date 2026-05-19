using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetPerFriendPreferences_Response : ITypedProtobuf<CPlayer_GetPerFriendPreferences_Response>
{
    static CPlayer_GetPerFriendPreferences_Response ITypedProtobuf<CPlayer_GetPerFriendPreferences_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetPerFriendPreferences_ResponseImpl(handle, isManuallyAllocated);

    public IProtobufRepeatedFieldSubMessageType<PerFriendPreferences> Preferences { get; }
}