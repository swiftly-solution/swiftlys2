using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGameNetworkingUI_ConnectionSummaryImpl : TypedProtobuf<CGameNetworkingUI_ConnectionSummary>, CGameNetworkingUI_ConnectionSummary
{
    public CGameNetworkingUI_ConnectionSummaryImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint TransportKind
    { get => Accessor.GetUInt32("transport_kind"); set => Accessor.SetUInt32("transport_kind", value); }
    public uint ConnectionState
    { get => Accessor.GetUInt32("connection_state"); set => Accessor.SetUInt32("connection_state", value); }
    public string SdrpopLocal
    { get => Accessor.GetString("sdrpop_local"); set => Accessor.SetString("sdrpop_local", value); }
    public string SdrpopRemote
    { get => Accessor.GetString("sdrpop_remote"); set => Accessor.SetString("sdrpop_remote", value); }
    public uint PingMs
    { get => Accessor.GetUInt32("ping_ms"); set => Accessor.SetUInt32("ping_ms", value); }
    public float PacketLoss
    { get => Accessor.GetFloat("packet_loss"); set => Accessor.SetFloat("packet_loss", value); }
    public uint PingDefaultInternetRoute
    { get => Accessor.GetUInt32("ping_default_internet_route"); set => Accessor.SetUInt32("ping_default_internet_route", value); }
    public bool IpWasShared
    { get => Accessor.GetBool("ip_was_shared"); set => Accessor.SetBool("ip_was_shared", value); }
}