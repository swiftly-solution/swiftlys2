using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSteamDatagramGameserverPingRequestBody : ITypedProtobuf<CMsgSteamDatagramGameserverPingRequestBody>
{
    static CMsgSteamDatagramGameserverPingRequestBody ITypedProtobuf<CMsgSteamDatagramGameserverPingRequestBody>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSteamDatagramGameserverPingRequestBodyImpl(handle, isManuallyAllocated);

    public uint RelayPopid { get; set; }
    public CMsgSteamNetworkingIPAddress YourPublicIp { get; }
    public uint YourPublicPort { get; set; }
    public ulong RelayUnixTime { get; set; }
    public ulong RoutingSecret { get; set; }
    public IProtobufRepeatedFieldSubMessageType<CMsgSteamNetworkingIPAddress> MyIps { get; }
    public byte[] Echo { get; set; }
}