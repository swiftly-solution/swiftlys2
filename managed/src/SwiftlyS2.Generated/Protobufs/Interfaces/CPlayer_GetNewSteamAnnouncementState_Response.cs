using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_GetNewSteamAnnouncementState_Response : ITypedProtobuf<CPlayer_GetNewSteamAnnouncementState_Response>
{
    static CPlayer_GetNewSteamAnnouncementState_Response ITypedProtobuf<CPlayer_GetNewSteamAnnouncementState_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_GetNewSteamAnnouncementState_ResponseImpl(handle, isManuallyAllocated);

    public int State { get; set; }
    public string AnnouncementHeadline { get; set; }
    public string AnnouncementUrl { get; set; }
    public uint TimePosted { get; set; }
    public ulong AnnouncementGid { get; set; }
}