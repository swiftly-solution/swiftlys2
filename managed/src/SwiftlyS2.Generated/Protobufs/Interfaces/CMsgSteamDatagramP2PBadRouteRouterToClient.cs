using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramP2PBadRouteRouterToClient : ITypedProtobuf<CMsgSteamDatagramP2PBadRouteRouterToClient>
{
    static CMsgSteamDatagramP2PBadRouteRouterToClient ITypedProtobuf<CMsgSteamDatagramP2PBadRouteRouterToClient>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramP2PBadRouteRouterToClientImpl(handle, isManuallyAllocated);

    public uint ConnectionId { get; set; }
    public byte[] FailedRelayRoutingToken { get; set; }
    public uint AckForwardTargetRevision { get; set; }
    public ulong KludgePad { get; set; }
}