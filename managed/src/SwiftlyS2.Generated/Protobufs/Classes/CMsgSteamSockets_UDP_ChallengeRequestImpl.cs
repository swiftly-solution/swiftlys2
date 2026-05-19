using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamSockets_UDP_ChallengeRequestImpl : TypedProtobuf<CMsgSteamSockets_UDP_ChallengeRequest>, CMsgSteamSockets_UDP_ChallengeRequest
{
    public CMsgSteamSockets_UDP_ChallengeRequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ConnectionId
    { get => Accessor.GetUInt32("connection_id"); set => Accessor.SetUInt32("connection_id", value); }
    public ulong MyTimestamp
    { get => Accessor.GetUInt64("my_timestamp"); set => Accessor.SetUInt64("my_timestamp", value); }
    public uint ProtocolVersion
    { get => Accessor.GetUInt32("protocol_version"); set => Accessor.SetUInt32("protocol_version", value); }
}