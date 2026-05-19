using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramRouterPingReply_RouteException : ITypedProtobuf<CMsgSteamDatagramRouterPingReply_RouteException>
{
    static CMsgSteamDatagramRouterPingReply_RouteException ITypedProtobuf<CMsgSteamDatagramRouterPingReply_RouteException>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramRouterPingReply_RouteExceptionImpl(handle, isManuallyAllocated);

    public uint DataCenterId { get; set; }
    public uint Flags { get; set; }
    public uint Penalty { get; set; }
}