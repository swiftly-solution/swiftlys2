using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramSignedRelayAuthTicket : ITypedProtobuf<CMsgSteamDatagramSignedRelayAuthTicket>
{
    static CMsgSteamDatagramSignedRelayAuthTicket ITypedProtobuf<CMsgSteamDatagramSignedRelayAuthTicket>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramSignedRelayAuthTicketImpl(handle, isManuallyAllocated);

    public ulong ReservedDoNotUse { get; set; }
    public byte[] Ticket { get; set; }
    public byte[] Signature { get; set; }
    public ulong KeyId { get; set; }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramCertificateSigned> Certs { get; }
}