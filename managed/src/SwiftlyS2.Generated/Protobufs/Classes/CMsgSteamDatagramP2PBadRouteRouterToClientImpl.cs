using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramP2PBadRouteRouterToClientImpl : TypedProtobuf<CMsgSteamDatagramP2PBadRouteRouterToClient>, CMsgSteamDatagramP2PBadRouteRouterToClient
{
    public CMsgSteamDatagramP2PBadRouteRouterToClientImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ConnectionId
    { get => Accessor.GetUInt32("connection_id"); set => Accessor.SetUInt32("connection_id", value); }
    public byte[] FailedRelayRoutingToken
    { get => Accessor.GetBytes("failed_relay_routing_token"); set => Accessor.SetBytes("failed_relay_routing_token", value); }
    public uint AckForwardTargetRevision
    { get => Accessor.GetUInt32("ack_forward_target_revision"); set => Accessor.SetUInt32("ack_forward_target_revision", value); }
    public ulong KludgePad
    { get => Accessor.GetUInt64("kludge_pad"); set => Accessor.SetUInt64("kludge_pad", value); }
}