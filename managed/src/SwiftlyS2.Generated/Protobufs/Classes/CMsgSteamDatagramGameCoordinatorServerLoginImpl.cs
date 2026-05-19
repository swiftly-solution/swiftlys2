using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramGameCoordinatorServerLoginImpl : TypedProtobuf<CMsgSteamDatagramGameCoordinatorServerLogin>, CMsgSteamDatagramGameCoordinatorServerLogin
{
    public CMsgSteamDatagramGameCoordinatorServerLoginImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint TimeGenerated
    { get => Accessor.GetUInt32("time_generated"); set => Accessor.SetUInt32("time_generated", value); }
    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public byte[] Routing
    { get => Accessor.GetBytes("routing"); set => Accessor.SetBytes("routing", value); }
    public byte[] Appdata
    { get => Accessor.GetBytes("appdata"); set => Accessor.SetBytes("appdata", value); }
    public byte[] LegacyIdentityBinary
    { get => Accessor.GetBytes("legacy_identity_binary"); set => Accessor.SetBytes("legacy_identity_binary", value); }
    public string IdentityString
    { get => Accessor.GetString("identity_string"); set => Accessor.SetString("identity_string", value); }
    public ulong DummySteamId
    { get => Accessor.GetUInt64("dummy_steam_id"); set => Accessor.SetUInt64("dummy_steam_id", value); }
}