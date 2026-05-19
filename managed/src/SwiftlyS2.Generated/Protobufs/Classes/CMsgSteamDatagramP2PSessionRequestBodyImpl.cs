using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramP2PSessionRequestBodyImpl : TypedProtobuf<CMsgSteamDatagramP2PSessionRequestBody>, CMsgSteamDatagramP2PSessionRequestBody
{
    public CMsgSteamDatagramP2PSessionRequestBodyImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ChallengeTime
    { get => Accessor.GetUInt32("challenge_time"); set => Accessor.SetUInt32("challenge_time", value); }
    public ulong Challenge
    { get => Accessor.GetUInt64("challenge"); set => Accessor.SetUInt64("challenge", value); }
    public uint ClientConnectionId
    { get => Accessor.GetUInt32("client_connection_id"); set => Accessor.SetUInt32("client_connection_id", value); }
    public ulong LegacyPeerSteamId
    { get => Accessor.GetUInt64("legacy_peer_steam_id"); set => Accessor.SetUInt64("legacy_peer_steam_id", value); }
    public string PeerIdentityString
    { get => Accessor.GetString("peer_identity_string"); set => Accessor.SetString("peer_identity_string", value); }
    public uint PeerConnectionId
    { get => Accessor.GetUInt32("peer_connection_id"); set => Accessor.SetUInt32("peer_connection_id", value); }
    public byte[] EncryptedData
    { get => Accessor.GetBytes("encrypted_data"); set => Accessor.SetBytes("encrypted_data", value); }
    public uint EncryptionYourPublicKeyLeadByte
    { get => Accessor.GetUInt32("encryption_your_public_key_lead_byte"); set => Accessor.SetUInt32("encryption_your_public_key_lead_byte", value); }
    public byte[] EncryptionMyEphemeralPublicKey
    { get => Accessor.GetBytes("encryption_my_ephemeral_public_key"); set => Accessor.SetBytes("encryption_my_ephemeral_public_key", value); }
    public uint ProtocolVersion
    { get => Accessor.GetUInt32("protocol_version"); set => Accessor.SetUInt32("protocol_version", value); }
    public ulong NetworkConfigVersion
    { get => Accessor.GetUInt64("network_config_version"); set => Accessor.SetUInt64("network_config_version", value); }
    public string Platform
    { get => Accessor.GetString("platform"); set => Accessor.SetString("platform", value); }
    public string Build
    { get => Accessor.GetString("build"); set => Accessor.SetString("build", value); }
}