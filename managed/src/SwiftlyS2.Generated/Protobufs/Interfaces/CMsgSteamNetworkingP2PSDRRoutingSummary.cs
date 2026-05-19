using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamNetworkingP2PSDRRoutingSummary : ITypedProtobuf<CMsgSteamNetworkingP2PSDRRoutingSummary>
{
    static CMsgSteamNetworkingP2PSDRRoutingSummary ITypedProtobuf<CMsgSteamNetworkingP2PSDRRoutingSummary>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamNetworkingP2PSDRRoutingSummaryImpl(handle, isManuallyAllocated);

    public uint InitialPing { get; set; }
    public uint InitialPingFrontLocal { get; set; }
    public uint InitialPingFrontRemote { get; set; }
    public uint InitialScore { get; set; }
    public uint InitialPopLocal { get; set; }
    public uint InitialPopRemote { get; set; }
    public uint BestPing { get; set; }
    public uint BestPingFrontLocal { get; set; }
    public uint BestPingFrontRemote { get; set; }
    public uint BestScore { get; set; }
    public uint BestPopLocal { get; set; }
    public uint BestPopRemote { get; set; }
    public uint BestTime { get; set; }
    public uint NegotiationMs { get; set; }
    public uint SelectedSeconds { get; set; }
}