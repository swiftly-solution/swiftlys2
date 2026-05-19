using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramP2PSessionEstablished : ITypedProtobuf<CMsgSteamDatagramP2PSessionEstablished>
{
    static CMsgSteamDatagramP2PSessionEstablished ITypedProtobuf<CMsgSteamDatagramP2PSessionEstablished>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramP2PSessionEstablishedImpl(handle, isManuallyAllocated);

    public uint ConnectionId { get; set; }
    public uint SecondsUntilShutdown { get; set; }
    public byte[] RelayRoutingToken { get; set; }
    public uint SeqNumR2c { get; set; }
}