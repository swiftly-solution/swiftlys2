using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramClientPingSampleReply_POP : ITypedProtobuf<CMsgSteamDatagramClientPingSampleReply_POP>
{
    static CMsgSteamDatagramClientPingSampleReply_POP ITypedProtobuf<CMsgSteamDatagramClientPingSampleReply_POP>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramClientPingSampleReply_POPImpl(handle, isManuallyAllocated);

    public uint PopId { get; set; }
    public uint DefaultFrontPingMs { get; set; }
    public uint ClusterPenalty { get; set; }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramClientPingSampleReply_POP_AltAddress> AltAddresses { get; }
    public uint DefaultE2ePingMs { get; set; }
    public uint DefaultE2eScore { get; set; }
    public uint P2pViaPeerRelayPopId { get; set; }
    public uint BestDcPingMs { get; set; }
    public uint BestDcScore { get; set; }
    public uint BestDcViaRelayPopId { get; set; }
    public uint DefaultDcPingMs { get; set; }
    public uint DefaultDcScore { get; set; }
    public uint DefaultDcViaRelayPopId { get; set; }
    public uint TestDcPingMs { get; set; }
    public uint TestDcScore { get; set; }
    public uint TestDcViaRelayPopId { get; set; }
}