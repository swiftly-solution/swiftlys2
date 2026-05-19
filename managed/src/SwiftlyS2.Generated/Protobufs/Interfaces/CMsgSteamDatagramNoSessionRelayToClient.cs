using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramNoSessionRelayToClient : ITypedProtobuf<CMsgSteamDatagramNoSessionRelayToClient>
{
    static CMsgSteamDatagramNoSessionRelayToClient ITypedProtobuf<CMsgSteamDatagramNoSessionRelayToClient>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramNoSessionRelayToClientImpl(handle, isManuallyAllocated);

    public uint ConnectionId { get; set; }
    public uint YourPublicIp { get; set; }
    public uint YourPublicPort { get; set; }
    public uint ServerTime { get; set; }
    public ulong Challenge { get; set; }
    public uint SecondsUntilShutdown { get; set; }
}