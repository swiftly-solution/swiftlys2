using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGameNetworkingUI_ConnectionStateImpl : TypedProtobuf<CGameNetworkingUI_ConnectionState>, CGameNetworkingUI_ConnectionState
{
    public CGameNetworkingUI_ConnectionStateImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public string ConnectionKey
    { get => Accessor.GetString("connection_key"); set => Accessor.SetString("connection_key", value); }
    public uint Appid
    { get => Accessor.GetUInt32("appid"); set => Accessor.SetUInt32("appid", value); }
    public uint ConnectionIdLocal
    { get => Accessor.GetUInt32("connection_id_local"); set => Accessor.SetUInt32("connection_id_local", value); }
    public string IdentityLocal
    { get => Accessor.GetString("identity_local"); set => Accessor.SetString("identity_local", value); }
    public string IdentityRemote
    { get => Accessor.GetString("identity_remote"); set => Accessor.SetString("identity_remote", value); }
    public uint ConnectionState
    { get => Accessor.GetUInt32("connection_state"); set => Accessor.SetUInt32("connection_state", value); }
    public uint StartTime
    { get => Accessor.GetUInt32("start_time"); set => Accessor.SetUInt32("start_time", value); }
    public uint CloseTime
    { get => Accessor.GetUInt32("close_time"); set => Accessor.SetUInt32("close_time", value); }
    public uint CloseReason
    { get => Accessor.GetUInt32("close_reason"); set => Accessor.SetUInt32("close_reason", value); }
    public string CloseMessage
    { get => Accessor.GetString("close_message"); set => Accessor.SetString("close_message", value); }
    public string StatusLocToken
    { get => Accessor.GetString("status_loc_token"); set => Accessor.SetString("status_loc_token", value); }
    public uint TransportKind
    { get => Accessor.GetUInt32("transport_kind"); set => Accessor.SetUInt32("transport_kind", value); }
    public string SdrpopidLocal
    { get => Accessor.GetString("sdrpopid_local"); set => Accessor.SetString("sdrpopid_local", value); }
    public string SdrpopidRemote
    { get => Accessor.GetString("sdrpopid_remote"); set => Accessor.SetString("sdrpopid_remote", value); }
    public string AddressRemote
    { get => Accessor.GetString("address_remote"); set => Accessor.SetString("address_remote", value); }
    public CMsgSteamDatagramP2PRoutingSummary P2pRouting
    { get => new CMsgSteamDatagramP2PRoutingSummaryImpl(NativeNetMessages.GetNestedMessage(Address, "p2p_routing"), false); }
    public uint PingInterior
    { get => Accessor.GetUInt32("ping_interior"); set => Accessor.SetUInt32("ping_interior", value); }
    public uint PingRemoteFront
    { get => Accessor.GetUInt32("ping_remote_front"); set => Accessor.SetUInt32("ping_remote_front", value); }
    public uint PingDefaultInternetRoute
    { get => Accessor.GetUInt32("ping_default_internet_route"); set => Accessor.SetUInt32("ping_default_internet_route", value); }
    public CMsgSteamDatagramConnectionQuality E2eQualityLocal
    { get => new CMsgSteamDatagramConnectionQualityImpl(NativeNetMessages.GetNestedMessage(Address, "e2e_quality_local"), false); }
    public CMsgSteamDatagramConnectionQuality E2eQualityRemote
    { get => new CMsgSteamDatagramConnectionQualityImpl(NativeNetMessages.GetNestedMessage(Address, "e2e_quality_remote"), false); }
    public ulong E2eQualityRemoteInstantaneousTime
    { get => Accessor.GetUInt64("e2e_quality_remote_instantaneous_time"); set => Accessor.SetUInt64("e2e_quality_remote_instantaneous_time", value); }
    public ulong E2eQualityRemoteLifetimeTime
    { get => Accessor.GetUInt64("e2e_quality_remote_lifetime_time"); set => Accessor.SetUInt64("e2e_quality_remote_lifetime_time", value); }
    public CMsgSteamDatagramConnectionQuality FrontQualityLocal
    { get => new CMsgSteamDatagramConnectionQualityImpl(NativeNetMessages.GetNestedMessage(Address, "front_quality_local"), false); }
    public CMsgSteamDatagramConnectionQuality FrontQualityRemote
    { get => new CMsgSteamDatagramConnectionQualityImpl(NativeNetMessages.GetNestedMessage(Address, "front_quality_remote"), false); }
    public ulong FrontQualityRemoteInstantaneousTime
    { get => Accessor.GetUInt64("front_quality_remote_instantaneous_time"); set => Accessor.SetUInt64("front_quality_remote_instantaneous_time", value); }
    public ulong FrontQualityRemoteLifetimeTime
    { get => Accessor.GetUInt64("front_quality_remote_lifetime_time"); set => Accessor.SetUInt64("front_quality_remote_lifetime_time", value); }
}