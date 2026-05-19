using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamNetworkingP2PRendezvous_ApplicationMessageImpl : TypedProtobuf<CMsgSteamNetworkingP2PRendezvous_ApplicationMessage>, CMsgSteamNetworkingP2PRendezvous_ApplicationMessage
{
    public CMsgSteamNetworkingP2PRendezvous_ApplicationMessageImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public byte[] Data
    { get => Accessor.GetBytes("data"); set => Accessor.SetBytes("data", value); }
    public ulong MsgNum
    { get => Accessor.GetUInt64("msg_num"); set => Accessor.SetUInt64("msg_num", value); }
    public uint Flags
    { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }
    public uint LaneIdx
    { get => Accessor.GetUInt32("lane_idx"); set => Accessor.SetUInt32("lane_idx", value); }
}