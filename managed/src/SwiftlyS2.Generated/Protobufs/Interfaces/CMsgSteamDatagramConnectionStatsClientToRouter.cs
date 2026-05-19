using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramConnectionStatsClientToRouter : ITypedProtobuf<CMsgSteamDatagramConnectionStatsClientToRouter>
{
    static CMsgSteamDatagramConnectionStatsClientToRouter ITypedProtobuf<CMsgSteamDatagramConnectionStatsClientToRouter>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramConnectionStatsClientToRouterImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramConnectionQuality QualityRelay { get; }
    public CMsgSteamDatagramConnectionQuality QualityE2e { get; }
    public IProtobufRepeatedFieldValueType<uint> AckRelay { get; }
    public IProtobufRepeatedFieldValueType<uint> LegacyAckE2e { get; }
    public uint Flags { get; set; }
    public uint ClientConnectionId { get; set; }
    public uint SeqNumC2r { get; set; }
    public uint SeqNumE2e { get; set; }
}