using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_IgnoreFriend_Request : ITypedProtobuf<CPlayer_IgnoreFriend_Request>
{
    static CPlayer_IgnoreFriend_Request ITypedProtobuf<CPlayer_IgnoreFriend_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_IgnoreFriend_RequestImpl(handle, isManuallyAllocated);

    public ulong Steamid { get; set; }
    public bool Unignore { get; set; }
}