using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramGameserverSessionRequest : ITypedProtobuf<CMsgSteamDatagramGameserverSessionRequest>
{
    static CMsgSteamDatagramGameserverSessionRequest ITypedProtobuf<CMsgSteamDatagramGameserverSessionRequest>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramGameserverSessionRequestImpl(handle, isManuallyAllocated);

    public byte[] Ticket { get; set; }
    public uint ChallengeTime { get; set; }
    public ulong Challenge { get; set; }
    public uint ClientConnectionId { get; set; }
    public uint ServerConnectionId { get; set; }
    public ulong NetworkConfigVersion { get; set; }
    public uint ProtocolVersion { get; set; }
    public string Platform { get; set; }
    public string Build { get; set; }
    public string DevGameserverIdentity { get; set; }
    public CMsgSteamDatagramCertificateSigned DevClientCert { get; }
}