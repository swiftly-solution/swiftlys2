using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramConnectOK : ITypedProtobuf<CMsgSteamDatagramConnectOK>
{
    static CMsgSteamDatagramConnectOK ITypedProtobuf<CMsgSteamDatagramConnectOK>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramConnectOKImpl(handle, isManuallyAllocated);

    public uint ClientConnectionId { get; set; }
    public uint ServerConnectionId { get; set; }
    public ulong YourTimestamp { get; set; }
    public uint DelayTimeUsec { get; set; }
    public uint GameserverRelaySessionId { get; set; }
    public CMsgSteamDatagramSessionCryptInfoSigned Crypt { get; }
    public CMsgSteamDatagramCertificateSigned Cert { get; }
}