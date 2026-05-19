using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetLastPlayedTimes_ResponseImpl : TypedProtobuf<CPlayer_GetLastPlayedTimes_Response>, CPlayer_GetLastPlayedTimes_Response
{
    public CPlayer_GetLastPlayedTimes_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public IProtobufRepeatedFieldSubMessageType<CPlayer_GetLastPlayedTimes_Response_Game> Games
    { get => new ProtobufRepeatedFieldSubMessageType<CPlayer_GetLastPlayedTimes_Response_Game>(Accessor, "games"); }
}