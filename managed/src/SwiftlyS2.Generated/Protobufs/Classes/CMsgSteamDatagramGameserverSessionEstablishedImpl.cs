using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramGameserverSessionEstablishedImpl : TypedProtobuf<CMsgSteamDatagramGameserverSessionEstablished>, CMsgSteamDatagramGameserverSessionEstablished
{
    public CMsgSteamDatagramGameserverSessionEstablishedImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ConnectionId
    { get => Accessor.GetUInt32("connection_id"); set => Accessor.SetUInt32("connection_id", value); }
    public string GameserverIdentityString
    { get => Accessor.GetString("gameserver_identity_string"); set => Accessor.SetString("gameserver_identity_string", value); }
    public uint SecondsUntilShutdown
    { get => Accessor.GetUInt32("seconds_until_shutdown"); set => Accessor.SetUInt32("seconds_until_shutdown", value); }
    public uint SeqNumR2c
    { get => Accessor.GetUInt32("seq_num_r2c"); set => Accessor.SetUInt32("seq_num_r2c", value); }
    public byte[] DummyLegacyIdentityBinary
    { get => Accessor.GetBytes("dummy_legacy_identity_binary"); set => Accessor.SetBytes("dummy_legacy_identity_binary", value); }
    public ulong LegacyGameserverSteamid
    { get => Accessor.GetUInt64("legacy_gameserver_steamid"); set => Accessor.SetUInt64("legacy_gameserver_steamid", value); }
}