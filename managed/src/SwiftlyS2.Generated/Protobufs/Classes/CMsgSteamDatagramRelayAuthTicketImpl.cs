using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramRelayAuthTicketImpl : TypedProtobuf<CMsgSteamDatagramRelayAuthTicket>, CMsgSteamDatagramRelayAuthTicket
{
    public CMsgSteamDatagramRelayAuthTicketImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint TimeExpiry
    { get => Accessor.GetUInt32("time_expiry"); set => Accessor.SetUInt32("time_expiry", value); }
    public string AuthorizedClientIdentityString
    { get => Accessor.GetString("authorized_client_identity_string"); set => Accessor.SetString("authorized_client_identity_string", value); }
    public string GameserverIdentityString
    { get => Accessor.GetString("gameserver_identity_string"); set => Accessor.SetString("gameserver_identity_string", value); }
    public uint AuthorizedPublicIp
    { get => Accessor.GetUInt32("authorized_public_ip"); set => Accessor.SetUInt32("authorized_public_ip", value); }
    public byte[] GameserverAddress
    { get => Accessor.GetBytes("gameserver_address"); set => Accessor.SetBytes("gameserver_address", value); }
    public uint AppId
    { get => Accessor.GetUInt32("app_id"); set => Accessor.SetUInt32("app_id", value); }
    public uint VirtualPort
    { get => Accessor.GetUInt32("virtual_port"); set => Accessor.SetUInt32("virtual_port", value); }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramRelayAuthTicket_ExtraField> ExtraFields
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSteamDatagramRelayAuthTicket_ExtraField>(Accessor, "extra_fields"); }
    public ulong LegacyAuthorizedSteamId
    { get => Accessor.GetUInt64("legacy_authorized_steam_id"); set => Accessor.SetUInt64("legacy_authorized_steam_id", value); }
    public ulong LegacyGameserverSteamId
    { get => Accessor.GetUInt64("legacy_gameserver_steam_id"); set => Accessor.SetUInt64("legacy_gameserver_steam_id", value); }
    public uint LegacyGameserverPopId
    { get => Accessor.GetUInt32("legacy_gameserver_pop_id"); set => Accessor.SetUInt32("legacy_gameserver_pop_id", value); }
    public byte[] LegacyAuthorizedClientIdentityBinary
    { get => Accessor.GetBytes("legacy_authorized_client_identity_binary"); set => Accessor.SetBytes("legacy_authorized_client_identity_binary", value); }
    public byte[] LegacyGameserverIdentityBinary
    { get => Accessor.GetBytes("legacy_gameserver_identity_binary"); set => Accessor.SetBytes("legacy_gameserver_identity_binary", value); }
}