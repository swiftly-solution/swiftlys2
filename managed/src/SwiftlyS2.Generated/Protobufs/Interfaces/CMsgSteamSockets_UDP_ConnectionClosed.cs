using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamSockets_UDP_ConnectionClosed : ITypedProtobuf<CMsgSteamSockets_UDP_ConnectionClosed>
{
    static CMsgSteamSockets_UDP_ConnectionClosed ITypedProtobuf<CMsgSteamSockets_UDP_ConnectionClosed>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamSockets_UDP_ConnectionClosedImpl(handle, isManuallyAllocated);

    public uint ToConnectionId { get; set; }
    public uint FromConnectionId { get; set; }
    public string Debug { get; set; }
    public uint ReasonCode { get; set; }
}