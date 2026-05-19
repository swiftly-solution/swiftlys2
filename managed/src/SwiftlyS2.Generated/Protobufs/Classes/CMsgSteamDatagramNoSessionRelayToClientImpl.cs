using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramNoSessionRelayToClientImpl : TypedProtobuf<CMsgSteamDatagramNoSessionRelayToClient>, CMsgSteamDatagramNoSessionRelayToClient
{
    public CMsgSteamDatagramNoSessionRelayToClientImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint ConnectionId
    { get => Accessor.GetUInt32("connection_id"); set => Accessor.SetUInt32("connection_id", value); }
    public uint YourPublicIp
    { get => Accessor.GetUInt32("your_public_ip"); set => Accessor.SetUInt32("your_public_ip", value); }
    public uint YourPublicPort
    { get => Accessor.GetUInt32("your_public_port"); set => Accessor.SetUInt32("your_public_port", value); }
    public uint ServerTime
    { get => Accessor.GetUInt32("server_time"); set => Accessor.SetUInt32("server_time", value); }
    public ulong Challenge
    { get => Accessor.GetUInt64("challenge"); set => Accessor.SetUInt64("challenge", value); }
    public uint SecondsUntilShutdown
    { get => Accessor.GetUInt32("seconds_until_shutdown"); set => Accessor.SetUInt32("seconds_until_shutdown", value); }
}