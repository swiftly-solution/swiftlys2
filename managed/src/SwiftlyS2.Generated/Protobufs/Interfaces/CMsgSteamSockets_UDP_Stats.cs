using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamSockets_UDP_Stats : ITypedProtobuf<CMsgSteamSockets_UDP_Stats>
{
    static CMsgSteamSockets_UDP_Stats ITypedProtobuf<CMsgSteamSockets_UDP_Stats>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamSockets_UDP_StatsImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramConnectionQuality Stats { get; }
    public uint Flags { get; set; }
}