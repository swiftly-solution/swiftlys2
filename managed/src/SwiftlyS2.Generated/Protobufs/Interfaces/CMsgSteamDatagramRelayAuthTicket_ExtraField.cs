using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramRelayAuthTicket_ExtraField : ITypedProtobuf<CMsgSteamDatagramRelayAuthTicket_ExtraField>
{
    static CMsgSteamDatagramRelayAuthTicket_ExtraField ITypedProtobuf<CMsgSteamDatagramRelayAuthTicket_ExtraField>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramRelayAuthTicket_ExtraFieldImpl(handle, isManuallyAllocated);

    public string Name { get; set; }
    public string StringValue { get; set; }
    public long Int64Value { get; set; }
    public ulong Fixed64Value { get; set; }
}