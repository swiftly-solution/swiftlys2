using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramP2PRoutesImpl : TypedProtobuf<CMsgSteamDatagramP2PRoutes>, CMsgSteamDatagramP2PRoutes
{
    public CMsgSteamDatagramP2PRoutesImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramP2PRoutes_RelayCluster> RelayClusters
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramP2PRoutes_RelayCluster>(Accessor, "relay_clusters"); }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramP2PRoutes_Route> Routes
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramP2PRoutes_Route>(Accessor, "routes"); }
    public uint Revision
    { get => Accessor.GetUInt32("revision"); set => Accessor.SetUInt32("revision", value); }
}