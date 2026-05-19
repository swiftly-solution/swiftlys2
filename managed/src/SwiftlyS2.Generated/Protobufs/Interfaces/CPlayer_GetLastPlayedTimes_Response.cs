using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetLastPlayedTimes_Response : ITypedProtobuf<CPlayer_GetLastPlayedTimes_Response>
{
    static CPlayer_GetLastPlayedTimes_Response ITypedProtobuf<CPlayer_GetLastPlayedTimes_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetLastPlayedTimes_ResponseImpl(handle, isManuallyAllocated);

    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetLastPlayedTimes_Response_Game> Games { get; }
}