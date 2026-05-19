using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_UpdateSteamAnnouncementLastRead_RequestImpl : TypedProtobuf<CPlayer_UpdateSteamAnnouncementLastRead_Request>, CPlayer_UpdateSteamAnnouncementLastRead_Request
{
    public CPlayer_UpdateSteamAnnouncementLastRead_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong AnnouncementGid
    { get => Accessor.GetUInt64("announcement_gid"); set => Accessor.SetUInt64("announcement_gid", value); }
    public uint TimePosted
    { get => Accessor.GetUInt32("time_posted"); set => Accessor.SetUInt32("time_posted", value); }
}