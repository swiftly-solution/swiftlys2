using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramClientPingSampleReply_POP_AltAddress : ITypedProtobuf<CMsgSteamDatagramClientPingSampleReply_POP_AltAddress>
{
    static CMsgSteamDatagramClientPingSampleReply_POP_AltAddress ITypedProtobuf<CMsgSteamDatagramClientPingSampleReply_POP_AltAddress>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramClientPingSampleReply_POP_AltAddressImpl(handle, isManuallyAllocated);

    public string Id { get; set; }
    public uint FrontPingMs { get; set; }
    public uint Penalty { get; set; }
}