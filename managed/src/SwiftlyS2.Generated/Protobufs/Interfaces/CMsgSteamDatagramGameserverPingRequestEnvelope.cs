using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramGameserverPingRequestEnvelope : ITypedProtobuf<CMsgSteamDatagramGameserverPingRequestEnvelope>
{
    static CMsgSteamDatagramGameserverPingRequestEnvelope ITypedProtobuf<CMsgSteamDatagramGameserverPingRequestEnvelope>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramGameserverPingRequestEnvelopeImpl(handle, isManuallyAllocated);

    public CMsgSteamDatagramCertificateSigned Cert { get; }
    public byte[] SignedData { get; set; }
    public byte[] Signature { get; set; }
    public uint LegacyYourPublicIp { get; set; }
    public uint LegacyYourPublicPort { get; set; }
    public uint LegacyRelayUnixTime { get; set; }
    public ulong LegacyChallenge { get; set; }
    public uint LegacyRouterTimestamp { get; set; }
    public byte[] DummyPad { get; set; }
}