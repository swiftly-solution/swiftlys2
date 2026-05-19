using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramClientPingSampleRequest : ITypedProtobuf<CMsgSteamDatagramClientPingSampleRequest>
{
    static CMsgSteamDatagramClientPingSampleRequest ITypedProtobuf<CMsgSteamDatagramClientPingSampleRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramClientPingSampleRequestImpl(handle, isManuallyAllocated);

    public uint ConnectionId { get; set; }
}