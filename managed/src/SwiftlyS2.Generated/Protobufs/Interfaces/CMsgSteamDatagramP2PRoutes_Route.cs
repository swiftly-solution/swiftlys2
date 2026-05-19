using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramP2PRoutes_Route : ITypedProtobuf<CMsgSteamDatagramP2PRoutes_Route>
{
    static CMsgSteamDatagramP2PRoutes_Route ITypedProtobuf<CMsgSteamDatagramP2PRoutes_Route>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramP2PRoutes_RouteImpl(handle, isManuallyAllocated);

    public uint MyPopId { get; set; }
    public uint YourPopId { get; set; }
    public uint LegacyScore { get; set; }
    public uint InteriorScore { get; set; }
}