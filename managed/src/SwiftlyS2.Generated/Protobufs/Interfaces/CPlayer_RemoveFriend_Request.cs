using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_RemoveFriend_Request : ITypedProtobuf<CPlayer_RemoveFriend_Request>
{
    static CPlayer_RemoveFriend_Request ITypedProtobuf<CPlayer_RemoveFriend_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_RemoveFriend_RequestImpl(handle, isManuallyAllocated);

    public ulong Steamid { get; set; }
}