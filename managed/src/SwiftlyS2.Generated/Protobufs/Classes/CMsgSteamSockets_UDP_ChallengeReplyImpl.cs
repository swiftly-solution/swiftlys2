using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamSockets_UDP_ChallengeReplyImpl : TypedProtobuf<CMsgSteamSockets_UDP_ChallengeReply>, CMsgSteamSockets_UDP_ChallengeReply
{
    public CMsgSteamSockets_UDP_ChallengeReplyImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ConnectionId
    { get => Accessor.GetUInt32("connection_id"); set => Accessor.SetUInt32("connection_id", value); }
    public ulong Challenge
    { get => Accessor.GetUInt64("challenge"); set => Accessor.SetUInt64("challenge", value); }
    public ulong YourTimestamp
    { get => Accessor.GetUInt64("your_timestamp"); set => Accessor.SetUInt64("your_timestamp", value); }
    public uint ProtocolVersion
    { get => Accessor.GetUInt32("protocol_version"); set => Accessor.SetUInt32("protocol_version", value); }
}