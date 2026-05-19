using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramNoSessionRelayToPeer : ITypedProtobuf<CMsgSteamDatagramNoSessionRelayToPeer>
{
    static CMsgSteamDatagramNoSessionRelayToPeer ITypedProtobuf<CMsgSteamDatagramNoSessionRelayToPeer>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramNoSessionRelayToPeerImpl(handle, isManuallyAllocated);

    public uint LegacyRelaySessionId { get; set; }
    public uint FromRelaySessionId { get; set; }
    public uint FromConnectionId { get; set; }
    public ulong KludgePad { get; set; }
}