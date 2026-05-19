using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramGameserverPingRequestBodyImpl : TypedProtobuf<CMsgSteamDatagramGameserverPingRequestBody>, CMsgSteamDatagramGameserverPingRequestBody
{
    public CMsgSteamDatagramGameserverPingRequestBodyImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint RelayPopid
    { get => Accessor.GetUInt32("relay_popid"); set => Accessor.SetUInt32("relay_popid", value); }
    public CMsgSteamNetworkingIPAddress YourPublicIp
    { get => new CMsgSteamNetworkingIPAddressImpl(NativeNetMessages.GetNestedMessage(Address, "your_public_ip"), false); }
    public uint YourPublicPort
    { get => Accessor.GetUInt32("your_public_port"); set => Accessor.SetUInt32("your_public_port", value); }
    public ulong RelayUnixTime
    { get => Accessor.GetUInt64("relay_unix_time"); set => Accessor.SetUInt64("relay_unix_time", value); }
    public ulong RoutingSecret
    { get => Accessor.GetUInt64("routing_secret"); set => Accessor.SetUInt64("routing_secret", value); }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamNetworkingIPAddress> MyIps
    { get => new ProtobufRepeatedFieldSubMessageType<CMsgSteamNetworkingIPAddress>(Accessor, "my_ips"); }
    public byte[] Echo
    { get => Accessor.GetBytes("echo"); set => Accessor.SetBytes("echo", value); }
}