using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramClientPingSampleReply_LegacyDataCenterImpl : TypedProtobuf<CMsgSteamDatagramClientPingSampleReply_LegacyDataCenter>, CMsgSteamDatagramClientPingSampleReply_LegacyDataCenter
{
    public CMsgSteamDatagramClientPingSampleReply_LegacyDataCenterImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint DataCenterId
    { get => Accessor.GetUInt32("data_center_id"); set => Accessor.SetUInt32("data_center_id", value); }
    public uint BestDcViaRelayPopId
    { get => Accessor.GetUInt32("best_dc_via_relay_pop_id"); set => Accessor.SetUInt32("best_dc_via_relay_pop_id", value); }
    public uint BestDcPingMs
    { get => Accessor.GetUInt32("best_dc_ping_ms"); set => Accessor.SetUInt32("best_dc_ping_ms", value); }
}