using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CGameNetworkingUI_ConnectionState : ITypedProtobuf<CGameNetworkingUI_ConnectionState>
{
    static CGameNetworkingUI_ConnectionState ITypedProtobuf<CGameNetworkingUI_ConnectionState>.Wrap(nint handle, bool isManuallyAllocated) => new CGameNetworkingUI_ConnectionStateImpl(handle, isManuallyAllocated);

    public string ConnectionKey { get; set; }
    public uint Appid { get; set; }
    public uint ConnectionIdLocal { get; set; }
    public string IdentityLocal { get; set; }
    public string IdentityRemote { get; set; }
    public uint ConnectionState { get; set; }
    public uint StartTime { get; set; }
    public uint CloseTime { get; set; }
    public uint CloseReason { get; set; }
    public string CloseMessage { get; set; }
    public string StatusLocToken { get; set; }
    public uint TransportKind { get; set; }
    public string SdrpopidLocal { get; set; }
    public string SdrpopidRemote { get; set; }
    public string AddressRemote { get; set; }
    public CMsgSteamDatagramP2PRoutingSummary P2pRouting { get; }
    public uint PingInterior { get; set; }
    public uint PingRemoteFront { get; set; }
    public uint PingDefaultInternetRoute { get; set; }
    public CMsgSteamDatagramConnectionQuality E2eQualityLocal { get; }
    public CMsgSteamDatagramConnectionQuality E2eQualityRemote { get; }
    public ulong E2eQualityRemoteInstantaneousTime { get; set; }
    public ulong E2eQualityRemoteLifetimeTime { get; set; }
    public CMsgSteamDatagramConnectionQuality FrontQualityLocal { get; }
    public CMsgSteamDatagramConnectionQuality FrontQualityRemote { get; }
    public ulong FrontQualityRemoteInstantaneousTime { get; set; }
    public ulong FrontQualityRemoteLifetimeTime { get; set; }
}