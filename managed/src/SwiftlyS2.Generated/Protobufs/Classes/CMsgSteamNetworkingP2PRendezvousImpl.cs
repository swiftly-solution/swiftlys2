using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamNetworkingP2PRendezvousImpl : TypedProtobuf<CMsgSteamNetworkingP2PRendezvous>, CMsgSteamNetworkingP2PRendezvous
{
    public CMsgSteamNetworkingP2PRendezvousImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string FromIdentity
    { get => Accessor.GetString("from_identity"); set => Accessor.SetString("from_identity", value); }
    public uint FromConnectionId
    { get => Accessor.GetUInt32("from_connection_id"); set => Accessor.SetUInt32("from_connection_id", value); }
    public string ToIdentity
    { get => Accessor.GetString("to_identity"); set => Accessor.SetString("to_identity", value); }
    public uint ToConnectionId
    { get => Accessor.GetUInt32("to_connection_id"); set => Accessor.SetUInt32("to_connection_id", value); }
    public byte[] SdrRoutes
    { get => Accessor.GetBytes("sdr_routes"); set => Accessor.SetBytes("sdr_routes", value); }
    public uint AckPeerRoutesRevision
    { get => Accessor.GetUInt32("ack_peer_routes_revision"); set => Accessor.SetUInt32("ack_peer_routes_revision", value); }
    public bool IceEnabled
    { get => Accessor.GetBool("ice_enabled"); set => Accessor.SetBool("ice_enabled", value); }
    public byte[] HostedServerTicket
    { get => Accessor.GetBytes("hosted_server_ticket"); set => Accessor.SetBytes("hosted_server_ticket", value); }
    public CMsgSteamNetworkingP2PRendezvous_ConnectRequest ConnectRequest
    { get => new CMsgSteamNetworkingP2PRendezvous_ConnectRequestImpl(NativeNetMessages.GetNestedMessage(Address, "connect_request"), false); }
    public CMsgSteamNetworkingP2PRendezvous_ConnectOK ConnectOk
    { get => new CMsgSteamNetworkingP2PRendezvous_ConnectOKImpl(NativeNetMessages.GetNestedMessage(Address, "connect_ok"), false); }
    public CMsgSteamNetworkingP2PRendezvous_ConnectionClosed ConnectionClosed
    { get => new CMsgSteamNetworkingP2PRendezvous_ConnectionClosedImpl(NativeNetMessages.GetNestedMessage(Address, "connection_closed"), false); }
    public uint AckReliableMsg
    { get => Accessor.GetUInt32("ack_reliable_msg"); set => Accessor.SetUInt32("ack_reliable_msg", value); }
    public uint FirstReliableMsg
    { get => Accessor.GetUInt32("first_reliable_msg"); set => Accessor.SetUInt32("first_reliable_msg", value); }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamNetworkingP2PRendezvous_ReliableMessage> ReliableMessages
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSteamNetworkingP2PRendezvous_ReliableMessage>(Accessor, "reliable_messages"); }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamNetworkingP2PRendezvous_ApplicationMessage> ApplicationMessages
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSteamNetworkingP2PRendezvous_ApplicationMessage>(Accessor, "application_messages"); }
}