using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetLastPlayedTimes_Request : ITypedProtobuf<CPlayer_GetLastPlayedTimes_Request>
{
    static CPlayer_GetLastPlayedTimes_Request ITypedProtobuf<CPlayer_GetLastPlayedTimes_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetLastPlayedTimes_RequestImpl(handle, isManuallyAllocated);

    public uint MinLastPlayed { get; set; }
}