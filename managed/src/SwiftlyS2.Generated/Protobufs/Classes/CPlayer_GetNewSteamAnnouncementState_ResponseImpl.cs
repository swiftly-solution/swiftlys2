using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetNewSteamAnnouncementState_ResponseImpl : TypedProtobuf<CPlayer_GetNewSteamAnnouncementState_Response>, CPlayer_GetNewSteamAnnouncementState_Response
{
    public CPlayer_GetNewSteamAnnouncementState_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public int State
    { get => Accessor.GetInt32("state"); set => Accessor.SetInt32("state", value); }
    public string AnnouncementHeadline
    { get => Accessor.GetString("announcement_headline"); set => Accessor.SetString("announcement_headline", value); }
    public string AnnouncementUrl
    { get => Accessor.GetString("announcement_url"); set => Accessor.SetString("announcement_url", value); }
    public uint TimePosted
    { get => Accessor.GetUInt32("time_posted"); set => Accessor.SetUInt32("time_posted", value); }
    public ulong AnnouncementGid
    { get => Accessor.GetUInt64("announcement_gid"); set => Accessor.SetUInt64("announcement_gid", value); }
}