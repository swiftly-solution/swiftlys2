using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamSockets_UDP_ChallengeRequest : ITypedProtobuf<CMsgSteamSockets_UDP_ChallengeRequest>
{
    static CMsgSteamSockets_UDP_ChallengeRequest ITypedProtobuf<CMsgSteamSockets_UDP_ChallengeRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamSockets_UDP_ChallengeRequestImpl(handle, isManuallyAllocated);

    public uint ConnectionId { get; set; }
    public ulong MyTimestamp { get; set; }
    public uint ProtocolVersion { get; set; }
}