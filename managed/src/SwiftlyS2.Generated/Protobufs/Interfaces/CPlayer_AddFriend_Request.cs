using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_AddFriend_Request : ITypedProtobuf<CPlayer_AddFriend_Request>
{
    static CPlayer_AddFriend_Request ITypedProtobuf<CPlayer_AddFriend_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_AddFriend_RequestImpl(handle, isManuallyAllocated);

    public ulong Steamid { get; set; }
}