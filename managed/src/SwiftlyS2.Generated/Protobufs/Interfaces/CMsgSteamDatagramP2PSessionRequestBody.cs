using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramP2PSessionRequestBody : ITypedProtobuf<CMsgSteamDatagramP2PSessionRequestBody>
{
    static CMsgSteamDatagramP2PSessionRequestBody ITypedProtobuf<CMsgSteamDatagramP2PSessionRequestBody>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramP2PSessionRequestBodyImpl(handle, isManuallyAllocated);

    public uint ChallengeTime { get; set; }
    public ulong Challenge { get; set; }
    public uint ClientConnectionId { get; set; }
    public ulong LegacyPeerSteamId { get; set; }
    public string PeerIdentityString { get; set; }
    public uint PeerConnectionId { get; set; }
    public byte[] EncryptedData { get; set; }
    public uint EncryptionYourPublicKeyLeadByte { get; set; }
    public byte[] EncryptionMyEphemeralPublicKey { get; set; }
    public uint ProtocolVersion { get; set; }
    public ulong NetworkConfigVersion { get; set; }
    public string Platform { get; set; }
    public string Build { get; set; }
}