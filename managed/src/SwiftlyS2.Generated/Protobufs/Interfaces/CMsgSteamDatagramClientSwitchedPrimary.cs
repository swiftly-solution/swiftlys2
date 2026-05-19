using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramClientSwitchedPrimary : ITypedProtobuf<CMsgSteamDatagramClientSwitchedPrimary>
{
    static CMsgSteamDatagramClientSwitchedPrimary ITypedProtobuf<CMsgSteamDatagramClientSwitchedPrimary>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramClientSwitchedPrimaryImpl(handle, isManuallyAllocated);

    public uint ConnectionId { get; set; }
    public uint FromIp { get; set; }
    public uint FromPort { get; set; }
    public uint FromRouterCluster { get; set; }
    public uint FromActiveTime { get; set; }
    public uint FromActivePacketsRecv { get; set; }
    public string FromDroppedReason { get; set; }
    public uint GapMs { get; set; }
    public CMsgSteamDatagramClientSwitchedPrimary_RouterQuality FromQualityNow { get; }
    public CMsgSteamDatagramClientSwitchedPrimary_RouterQuality ToQualityNow { get; }
    public CMsgSteamDatagramClientSwitchedPrimary_RouterQuality FromQualityThen { get; }
    public CMsgSteamDatagramClientSwitchedPrimary_RouterQuality ToQualityThen { get; }
}