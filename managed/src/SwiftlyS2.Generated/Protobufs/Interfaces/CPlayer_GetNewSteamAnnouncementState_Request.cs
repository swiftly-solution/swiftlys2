using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetNewSteamAnnouncementState_Request : ITypedProtobuf<CPlayer_GetNewSteamAnnouncementState_Request>
{
    static CPlayer_GetNewSteamAnnouncementState_Request ITypedProtobuf<CPlayer_GetNewSteamAnnouncementState_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetNewSteamAnnouncementState_RequestImpl(handle, isManuallyAllocated);

    public int Language { get; set; }
}