using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetNewSteamAnnouncementState_RequestImpl : TypedProtobuf<CPlayer_GetNewSteamAnnouncementState_Request>, CPlayer_GetNewSteamAnnouncementState_Request
{
    public CPlayer_GetNewSteamAnnouncementState_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public int Language
    { get => Accessor.GetInt32("language"); set => Accessor.SetInt32("language", value); }
}