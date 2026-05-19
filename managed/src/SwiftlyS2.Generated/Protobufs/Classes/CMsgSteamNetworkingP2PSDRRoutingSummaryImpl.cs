using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSteamNetworkingP2PSDRRoutingSummaryImpl : TypedProtobuf<CMsgSteamNetworkingP2PSDRRoutingSummary>, CMsgSteamNetworkingP2PSDRRoutingSummary
{
    public CMsgSteamNetworkingP2PSDRRoutingSummaryImpl(nint handle, bool isManuallyAllocated) : base(handle)
    {
    }

    public uint InitialPing
    { get => Accessor.GetUInt32("initial_ping"); set => Accessor.SetUInt32("initial_ping", value); }
    public uint InitialPingFrontLocal
    { get => Accessor.GetUInt32("initial_ping_front_local"); set => Accessor.SetUInt32("initial_ping_front_local", value); }
    public uint InitialPingFrontRemote
    { get => Accessor.GetUInt32("initial_ping_front_remote"); set => Accessor.SetUInt32("initial_ping_front_remote", value); }
    public uint InitialScore
    { get => Accessor.GetUInt32("initial_score"); set => Accessor.SetUInt32("initial_score", value); }
    public uint InitialPopLocal
    { get => Accessor.GetUInt32("initial_pop_local"); set => Accessor.SetUInt32("initial_pop_local", value); }
    public uint InitialPopRemote
    { get => Accessor.GetUInt32("initial_pop_remote"); set => Accessor.SetUInt32("initial_pop_remote", value); }
    public uint BestPing
    { get => Accessor.GetUInt32("best_ping"); set => Accessor.SetUInt32("best_ping", value); }
    public uint BestPingFrontLocal
    { get => Accessor.GetUInt32("best_ping_front_local"); set => Accessor.SetUInt32("best_ping_front_local", value); }
    public uint BestPingFrontRemote
    { get => Accessor.GetUInt32("best_ping_front_remote"); set => Accessor.SetUInt32("best_ping_front_remote", value); }
    public uint BestScore
    { get => Accessor.GetUInt32("best_score"); set => Accessor.SetUInt32("best_score", value); }
    public uint BestPopLocal
    { get => Accessor.GetUInt32("best_pop_local"); set => Accessor.SetUInt32("best_pop_local", value); }
    public uint BestPopRemote
    { get => Accessor.GetUInt32("best_pop_remote"); set => Accessor.SetUInt32("best_pop_remote", value); }
    public uint BestTime
    { get => Accessor.GetUInt32("best_time"); set => Accessor.SetUInt32("best_time", value); }
    public uint NegotiationMs
    { get => Accessor.GetUInt32("negotiation_ms"); set => Accessor.SetUInt32("negotiation_ms", value); }
    public uint SelectedSeconds
    { get => Accessor.GetUInt32("selected_seconds"); set => Accessor.SetUInt32("selected_seconds", value); }
}