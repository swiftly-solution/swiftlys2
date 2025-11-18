
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGameServers_AggregationQuery_Response_GroupImpl : TypedProtobuf<CGameServers_AggregationQuery_Response_Group>, CGameServers_AggregationQuery_Response_Group
{
  public CGameServers_AggregationQuery_Response_GroupImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldValueType<string> GroupValues
  { get => new ProtobufRepeatedFieldValueType<string>(Accessor, "group_values"); }


  public uint ServersEmpty
  { get => Accessor.GetUInt32("servers_empty"); set => Accessor.SetUInt32("servers_empty", value); }


  public uint ServersFull
  { get => Accessor.GetUInt32("servers_full"); set => Accessor.SetUInt32("servers_full", value); }


  public uint ServersTotal
  { get => Accessor.GetUInt32("servers_total"); set => Accessor.SetUInt32("servers_total", value); }


  public uint PlayersHumans
  { get => Accessor.GetUInt32("players_humans"); set => Accessor.SetUInt32("players_humans", value); }


  public uint PlayersBots
  { get => Accessor.GetUInt32("players_bots"); set => Accessor.SetUInt32("players_bots", value); }


  public uint PlayerCapacity
  { get => Accessor.GetUInt32("player_capacity"); set => Accessor.SetUInt32("player_capacity", value); }

}
