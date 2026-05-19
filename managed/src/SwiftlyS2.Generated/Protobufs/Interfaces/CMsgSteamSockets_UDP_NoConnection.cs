using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamSockets_UDP_NoConnection : ITypedProtobuf<CMsgSteamSockets_UDP_NoConnection>
{
    static CMsgSteamSockets_UDP_NoConnection ITypedProtobuf<CMsgSteamSockets_UDP_NoConnection>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamSockets_UDP_NoConnectionImpl(handle, isManuallyAllocated);

    public uint FromConnectionId { get; set; }
    public uint ToConnectionId { get; set; }
}