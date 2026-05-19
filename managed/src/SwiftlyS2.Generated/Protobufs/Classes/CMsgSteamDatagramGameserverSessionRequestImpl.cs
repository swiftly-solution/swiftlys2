using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramGameserverSessionRequestImpl : TypedProtobuf<CMsgSteamDatagramGameserverSessionRequest>, CMsgSteamDatagramGameserverSessionRequest
{
    public CMsgSteamDatagramGameserverSessionRequestImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public byte[] Ticket
    { get => Accessor.GetBytes("ticket"); set => Accessor.SetBytes("ticket", value); }
    public uint ChallengeTime
    { get => Accessor.GetUInt32("challenge_time"); set => Accessor.SetUInt32("challenge_time", value); }
    public ulong Challenge
    { get => Accessor.GetUInt64("challenge"); set => Accessor.SetUInt64("challenge", value); }
    public uint ClientConnectionId
    { get => Accessor.GetUInt32("client_connection_id"); set => Accessor.SetUInt32("client_connection_id", value); }
    public uint ServerConnectionId
    { get => Accessor.GetUInt32("server_connection_id"); set => Accessor.SetUInt32("server_connection_id", value); }
    public ulong NetworkConfigVersion
    { get => Accessor.GetUInt64("network_config_version"); set => Accessor.SetUInt64("network_config_version", value); }
    public uint ProtocolVersion
    { get => Accessor.GetUInt32("protocol_version"); set => Accessor.SetUInt32("protocol_version", value); }
    public string Platform
    { get => Accessor.GetString("platform"); set => Accessor.SetString("platform", value); }
    public string Build
    { get => Accessor.GetString("build"); set => Accessor.SetString("build", value); }
    public string DevGameserverIdentity
    { get => Accessor.GetString("dev_gameserver_identity"); set => Accessor.SetString("dev_gameserver_identity", value); }
    public CMsgSteamDatagramCertificateSigned DevClientCert
    { get => new CMsgSteamDatagramCertificateSignedImpl(NativeNetMessages.GetNestedMessage(Address, "dev_client_cert"), false); }
}