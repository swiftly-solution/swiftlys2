using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramCertificateImpl : TypedProtobuf<CMsgSteamDatagramCertificate>, CMsgSteamDatagramCertificate
{
    public CMsgSteamDatagramCertificateImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public CMsgSteamDatagramCertificate_EKeyType KeyType
    { get => (CMsgSteamDatagramCertificate_EKeyType)Accessor.GetInt32("key_type"); set => Accessor.SetInt32("key_type", (int)value); }
    public byte[] KeyData
    { get => Accessor.GetBytes("key_data"); set => Accessor.SetBytes("key_data", value); }
    public ulong LegacySteamId
    { get => Accessor.GetUInt64("legacy_steam_id"); set => Accessor.SetUInt64("legacy_steam_id", value); }
    public CMsgSteamNetworkingIdentityLegacyBinary LegacyIdentityBinary
    { get => new CMsgSteamNetworkingIdentityLegacyBinaryImpl(NativeNetMessages.GetNestedMessage(Address, "legacy_identity_binary"), false); }
    public string IdentityString
    { get => Accessor.GetString("identity_string"); set => Accessor.SetString("identity_string", value); }
    public IProtobufRepeatedFieldValueType<uint> GameserverDatacenterIds
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "gameserver_datacenter_ids"); }
    public uint TimeCreated
    { get => Accessor.GetUInt32("time_created"); set => Accessor.SetUInt32("time_created", value); }
    public uint TimeExpiry
    { get => Accessor.GetUInt32("time_expiry"); set => Accessor.SetUInt32("time_expiry", value); }
    public IProtobufRepeatedFieldValueType<uint> AppIds
    { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "app_ids"); }
    public IProtobufRepeatedFieldValueType<string> IpAddresses
    { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "ip_addresses"); }
}