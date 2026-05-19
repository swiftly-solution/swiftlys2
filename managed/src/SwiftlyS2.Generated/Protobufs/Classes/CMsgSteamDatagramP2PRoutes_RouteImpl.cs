using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramP2PRoutes_RouteImpl : TypedProtobuf<CMsgSteamDatagramP2PRoutes_Route>, CMsgSteamDatagramP2PRoutes_Route
{
    public CMsgSteamDatagramP2PRoutes_RouteImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint MyPopId
    { get => Accessor.GetUInt32("my_pop_id"); set => Accessor.SetUInt32("my_pop_id", value); }
    public uint YourPopId
    { get => Accessor.GetUInt32("your_pop_id"); set => Accessor.SetUInt32("your_pop_id", value); }
    public uint LegacyScore
    { get => Accessor.GetUInt32("legacy_score"); set => Accessor.SetUInt32("legacy_score", value); }
    public uint InteriorScore
    { get => Accessor.GetUInt32("interior_score"); set => Accessor.SetUInt32("interior_score", value); }
}