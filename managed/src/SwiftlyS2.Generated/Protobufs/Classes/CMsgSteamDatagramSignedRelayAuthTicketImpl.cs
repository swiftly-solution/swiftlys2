using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramSignedRelayAuthTicketImpl : TypedProtobuf<CMsgSteamDatagramSignedRelayAuthTicket>, CMsgSteamDatagramSignedRelayAuthTicket
{
    public CMsgSteamDatagramSignedRelayAuthTicketImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public ulong ReservedDoNotUse
    { get => Accessor.GetUInt64("reserved_do_not_use"); set => Accessor.SetUInt64("reserved_do_not_use", value); }
    public byte[] Ticket
    { get => Accessor.GetBytes("ticket"); set => Accessor.SetBytes("ticket", value); }
    public byte[] Signature
    { get => Accessor.GetBytes("signature"); set => Accessor.SetBytes("signature", value); }
    public ulong KeyId
    { get => Accessor.GetUInt64("key_id"); set => Accessor.SetUInt64("key_id", value); }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramCertificateSigned> Certs
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramCertificateSigned>(Accessor, "certs"); }
}