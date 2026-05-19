using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramP2PRoutes : ITypedProtobuf<CMsgSteamDatagramP2PRoutes>
{
    static CMsgSteamDatagramP2PRoutes ITypedProtobuf<CMsgSteamDatagramP2PRoutes>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramP2PRoutesImpl(handle, isManuallyAllocated);

    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramP2PRoutes_RelayCluster> RelayClusters { get; }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramP2PRoutes_Route> Routes { get; }
    public uint Revision { get; set; }
}