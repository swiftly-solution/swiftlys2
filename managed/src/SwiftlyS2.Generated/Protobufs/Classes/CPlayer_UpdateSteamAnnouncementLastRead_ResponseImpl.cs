using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_UpdateSteamAnnouncementLastRead_ResponseImpl : TypedProtobuf<CPlayer_UpdateSteamAnnouncementLastRead_Response>, CPlayer_UpdateSteamAnnouncementLastRead_Response
{
    public CPlayer_UpdateSteamAnnouncementLastRead_ResponseImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

}