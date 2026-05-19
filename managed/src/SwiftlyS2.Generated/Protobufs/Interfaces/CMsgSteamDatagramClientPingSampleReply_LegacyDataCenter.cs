using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramClientPingSampleReply_LegacyDataCenter : ITypedProtobuf<CMsgSteamDatagramClientPingSampleReply_LegacyDataCenter>
{
    static CMsgSteamDatagramClientPingSampleReply_LegacyDataCenter ITypedProtobuf<CMsgSteamDatagramClientPingSampleReply_LegacyDataCenter>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramClientPingSampleReply_LegacyDataCenterImpl(handle, isManuallyAllocated);

    public uint DataCenterId { get; set; }
    public uint BestDcViaRelayPopId { get; set; }
    public uint BestDcPingMs { get; set; }
}