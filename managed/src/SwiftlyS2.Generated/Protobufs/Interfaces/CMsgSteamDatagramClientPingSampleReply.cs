using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramClientPingSampleReply : ITypedProtobuf<CMsgSteamDatagramClientPingSampleReply>
{
    static CMsgSteamDatagramClientPingSampleReply ITypedProtobuf<CMsgSteamDatagramClientPingSampleReply>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramClientPingSampleReplyImpl(handle, isManuallyAllocated);

    public uint ConnectionId { get; set; }
    public bool RelayOverrideActive { get; set; }
    public CMsgTOSTreatment Tos { get; }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramClientPingSampleReply_POP> Pops { get; }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramClientPingSampleReply_LegacyDataCenter> LegacyDataCenters { get; }
}