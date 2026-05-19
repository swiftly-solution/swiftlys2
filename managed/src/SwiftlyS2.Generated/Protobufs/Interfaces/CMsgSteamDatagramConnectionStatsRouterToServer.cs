using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramConnectionStatsRouterToServer : ITypedProtobuf<CMsgSteamDatagramConnectionStatsRouterToServer>
{
    static CMsgSteamDatagramConnectionStatsRouterToServer ITypedProtobuf<CMsgSteamDatagramConnectionStatsRouterToServer>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramConnectionStatsRouterToServerImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramConnectionQuality QualityRelay { get; }
    public CMsgSteamDatagramConnectionQuality QualityE2e { get; }
    public IProtobufRepeatedFieldValueType<uint> AckRelay { get; }
    public IProtobufRepeatedFieldValueType<uint> LegacyAckE2e { get; }
    public uint Flags { get; set; }
    public uint SeqNumR2s { get; set; }
    public uint SeqNumE2e { get; set; }
    public string ClientIdentityString { get; set; }
    public ulong LegacyClientSteamId { get; set; }
    public uint RelaySessionId { get; set; }
    public uint ClientConnectionId { get; set; }
    public uint ServerConnectionId { get; set; }
    public ulong RoutingSecret { get; set; }
}