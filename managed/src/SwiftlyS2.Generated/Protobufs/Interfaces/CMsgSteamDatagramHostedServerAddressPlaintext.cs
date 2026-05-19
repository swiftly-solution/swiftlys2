using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramHostedServerAddressPlaintext : ITypedProtobuf<CMsgSteamDatagramHostedServerAddressPlaintext>
{
    static CMsgSteamDatagramHostedServerAddressPlaintext ITypedProtobuf<CMsgSteamDatagramHostedServerAddressPlaintext>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramHostedServerAddressPlaintextImpl(handle, isManuallyAllocated);

    public uint Ipv4 { get; set; }
    public byte[] Ipv6 { get; set; }
    public uint Port { get; set; }
    public ulong RoutingSecret { get; set; }
    public uint ProtocolVersion { get; set; }
}