using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamDatagramRouterPingReply_AltAddressImpl : TypedProtobuf<CMsgSteamDatagramRouterPingReply_AltAddress>, CMsgSteamDatagramRouterPingReply_AltAddress
{
    public CMsgSteamDatagramRouterPingReply_AltAddressImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint Ipv4
    { get => Accessor.GetUInt32("ipv4"); set => Accessor.SetUInt32("ipv4", value); }
    public uint Port
    { get => Accessor.GetUInt32("port"); set => Accessor.SetUInt32("port", value); }
    public uint Penalty
    { get => Accessor.GetUInt32("penalty"); set => Accessor.SetUInt32("penalty", value); }
    public CMsgSteamDatagramRouterPingReply_AltAddress_Protocol Protocol
    { get => (CMsgSteamDatagramRouterPingReply_AltAddress_Protocol)Accessor.GetInt32("protocol"); set => Accessor.SetInt32("protocol", (int)value); }
    public string Id
    { get => Accessor.GetString("id"); set => Accessor.SetString("id", value); }
}