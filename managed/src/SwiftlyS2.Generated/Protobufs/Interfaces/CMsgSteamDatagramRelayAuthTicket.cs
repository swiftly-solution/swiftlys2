using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramRelayAuthTicket : ITypedProtobuf<CMsgSteamDatagramRelayAuthTicket>
{
    static CMsgSteamDatagramRelayAuthTicket ITypedProtobuf<CMsgSteamDatagramRelayAuthTicket>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramRelayAuthTicketImpl(handle, isManuallyAllocated);

    public uint TimeExpiry { get; set; }
    public string AuthorizedClientIdentityString { get; set; }
    public string GameserverIdentityString { get; set; }
    public uint AuthorizedPublicIp { get; set; }
    public byte[] GameserverAddress { get; set; }
    public uint AppId { get; set; }
    public uint VirtualPort { get; set; }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramRelayAuthTicket_ExtraField> ExtraFields { get; }
    public ulong LegacyAuthorizedSteamId { get; set; }
    public ulong LegacyGameserverSteamId { get; set; }
    public uint LegacyGameserverPopId { get; set; }
    public byte[] LegacyAuthorizedClientIdentityBinary { get; set; }
    public byte[] LegacyGameserverIdentityBinary { get; set; }
}