using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramConnectionStatsServerToRouter : ITypedProtobuf<CMsgSteamDatagramConnectionStatsServerToRouter>
{
    static CMsgSteamDatagramConnectionStatsServerToRouter ITypedProtobuf<CMsgSteamDatagramConnectionStatsServerToRouter>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramConnectionStatsServerToRouterImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramConnectionQuality QualityRelay { get; }
    public CMsgSteamDatagramConnectionQuality QualityE2e { get; }
    public IProtobufRepeatedFieldValueType<uint> AckRelay { get; }
    public IProtobufRepeatedFieldValueType<uint> LegacyAckE2e { get; }
    public uint Flags { get; set; }
    public uint SeqNumS2r { get; set; }
    public uint SeqNumE2e { get; set; }
    public uint RelaySessionId { get; set; }
    public uint ClientConnectionId { get; set; }
    public uint ServerConnectionId { get; set; }
}