using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_UpdateSteamAnnouncementLastRead_Request : ITypedProtobuf<CPlayer_UpdateSteamAnnouncementLastRead_Request>
{
    static CPlayer_UpdateSteamAnnouncementLastRead_Request ITypedProtobuf<CPlayer_UpdateSteamAnnouncementLastRead_Request>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_UpdateSteamAnnouncementLastRead_RequestImpl(handle, isManuallyAllocated);

    public ulong AnnouncementGid { get; set; }
    public uint TimePosted { get; set; }
}