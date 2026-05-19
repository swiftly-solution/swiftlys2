using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CPlayer_UpdateSteamAnnouncementLastRead_Response : ITypedProtobuf<CPlayer_UpdateSteamAnnouncementLastRead_Response>
{
    static CPlayer_UpdateSteamAnnouncementLastRead_Response ITypedProtobuf<CPlayer_UpdateSteamAnnouncementLastRead_Response>.Wrap(nint handle, bool isManuallyAllocated) => new CPlayer_UpdateSteamAnnouncementLastRead_ResponseImpl(handle, isManuallyAllocated);

}