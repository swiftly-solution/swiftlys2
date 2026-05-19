using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamSockets_UDP_ConnectOK : ITypedProtobuf<CMsgSteamSockets_UDP_ConnectOK>
{
    static CMsgSteamSockets_UDP_ConnectOK ITypedProtobuf<CMsgSteamSockets_UDP_ConnectOK>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamSockets_UDP_ConnectOKImpl(handle, isManuallyAllocated);

    public uint ClientConnectionId { get; set; }
    public uint ServerConnectionId { get; set; }
    public ulong YourTimestamp { get; set; }
    public uint DelayTimeUsec { get; set; }
    public CMsgSteamDatagramSessionCryptInfoSigned Crypt { get; }
    public CMsgSteamDatagramCertificateSigned Cert { get; }
    public string IdentityString { get; set; }
    public ulong LegacyServerSteamId { get; set; }
    public CMsgSteamNetworkingIdentityLegacyBinary LegacyIdentityBinary { get; }
}