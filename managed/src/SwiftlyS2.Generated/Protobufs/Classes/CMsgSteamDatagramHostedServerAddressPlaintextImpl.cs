using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramHostedServerAddressPlaintextImpl : TypedProtobuf<CMsgSteamDatagramHostedServerAddressPlaintext>, CMsgSteamDatagramHostedServerAddressPlaintext
{
    public CMsgSteamDatagramHostedServerAddressPlaintextImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Ipv4
    { get => Accessor.GetUInt32("ipv4"); set => Accessor.SetUInt32("ipv4", value); }
    public byte[] Ipv6
    { get => Accessor.GetBytes("ipv6"); set => Accessor.SetBytes("ipv6", value); }
    public uint Port
    { get => Accessor.GetUInt32("port"); set => Accessor.SetUInt32("port", value); }
    public ulong RoutingSecret
    { get => Accessor.GetUInt64("routing_secret"); set => Accessor.SetUInt64("routing_secret", value); }
    public uint ProtocolVersion
    { get => Accessor.GetUInt32("protocol_version"); set => Accessor.SetUInt32("protocol_version", value); }
}