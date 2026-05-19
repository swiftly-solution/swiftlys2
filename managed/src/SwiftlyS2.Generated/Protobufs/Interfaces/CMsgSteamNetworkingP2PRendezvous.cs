using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamNetworkingP2PRendezvous : ITypedProtobuf<CMsgSteamNetworkingP2PRendezvous>
{
    static CMsgSteamNetworkingP2PRendezvous ITypedProtobuf<CMsgSteamNetworkingP2PRendezvous>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamNetworkingP2PRendezvousImpl(handle, isManuallyAllocated);

    public string FromIdentity { get; set; }
    public uint FromConnectionId { get; set; }
    public string ToIdentity { get; set; }
    public uint ToConnectionId { get; set; }
    public byte[] SdrRoutes { get; set; }
    public uint AckPeerRoutesRevision { get; set; }
    public bool IceEnabled { get; set; }
    public byte[] HostedServerTicket { get; set; }
    public CMsgSteamNetworkingP2PRendezvous_ConnectRequest ConnectRequest { get; }
    public CMsgSteamNetworkingP2PRendezvous_ConnectOK ConnectOk { get; }
    public CMsgSteamNetworkingP2PRendezvous_ConnectionClosed ConnectionClosed { get; }
    public uint AckReliableMsg { get; set; }
    public uint FirstReliableMsg { get; set; }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamNetworkingP2PRendezvous_ReliableMessage> ReliableMessages { get; }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamNetworkingP2PRendezvous_ApplicationMessage> ApplicationMessages { get; }
}