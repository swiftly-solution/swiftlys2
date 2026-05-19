using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CPlayer_GetLastPlayedTimes_RequestImpl : TypedProtobuf<CPlayer_GetLastPlayedTimes_Request>, CPlayer_GetLastPlayedTimes_Request
{
    public CPlayer_GetLastPlayedTimes_RequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint MinLastPlayed
    { get => Accessor.GetUInt32("min_last_played"); set => Accessor.SetUInt32("min_last_played", value); }
}