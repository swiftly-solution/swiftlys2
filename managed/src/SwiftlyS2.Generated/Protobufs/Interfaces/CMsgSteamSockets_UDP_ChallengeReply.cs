using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamSockets_UDP_ChallengeReply : ITypedProtobuf<CMsgSteamSockets_UDP_ChallengeReply>
{
    static CMsgSteamSockets_UDP_ChallengeReply ITypedProtobuf<CMsgSteamSockets_UDP_ChallengeReply>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamSockets_UDP_ChallengeReplyImpl(handle, isManuallyAllocated);

    public uint ConnectionId { get; set; }
    public ulong Challenge { get; set; }
    public ulong YourTimestamp { get; set; }
    public uint ProtocolVersion { get; set; }
}