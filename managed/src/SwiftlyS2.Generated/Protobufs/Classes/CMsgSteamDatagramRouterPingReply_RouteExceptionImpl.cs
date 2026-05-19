using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramRouterPingReply_RouteExceptionImpl : TypedProtobuf<CMsgSteamDatagramRouterPingReply_RouteException>, CMsgSteamDatagramRouterPingReply_RouteException
{
    public CMsgSteamDatagramRouterPingReply_RouteExceptionImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint DataCenterId
    { get => Accessor.GetUInt32("data_center_id"); set => Accessor.SetUInt32("data_center_id", value); }
    public uint Flags
    { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }
    public uint Penalty
    { get => Accessor.GetUInt32("penalty"); set => Accessor.SetUInt32("penalty", value); }
}