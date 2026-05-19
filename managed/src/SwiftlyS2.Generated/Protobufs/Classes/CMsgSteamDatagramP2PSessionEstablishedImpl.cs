using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramP2PSessionEstablishedImpl : TypedProtobuf<CMsgSteamDatagramP2PSessionEstablished>, CMsgSteamDatagramP2PSessionEstablished
{
    public CMsgSteamDatagramP2PSessionEstablishedImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ConnectionId
    { get => Accessor.GetUInt32("connection_id"); set => Accessor.SetUInt32("connection_id", value); }
    public uint SecondsUntilShutdown
    { get => Accessor.GetUInt32("seconds_until_shutdown"); set => Accessor.SetUInt32("seconds_until_shutdown", value); }
    public byte[] RelayRoutingToken
    { get => Accessor.GetBytes("relay_routing_token"); set => Accessor.SetBytes("relay_routing_token", value); }
    public uint SeqNumR2c
    { get => Accessor.GetUInt32("seq_num_r2c"); set => Accessor.SetUInt32("seq_num_r2c", value); }
}