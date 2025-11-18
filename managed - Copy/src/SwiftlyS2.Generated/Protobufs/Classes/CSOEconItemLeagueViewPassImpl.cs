
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOEconItemLeagueViewPassImpl : TypedProtobuf<CSOEconItemLeagueViewPass>, CSOEconItemLeagueViewPass
{
  public CSOEconItemLeagueViewPassImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint AccountId
  { get => Accessor.GetUInt32("account_id"); set => Accessor.SetUInt32("account_id", value); }


  public uint LeagueId
  { get => Accessor.GetUInt32("league_id"); set => Accessor.SetUInt32("league_id", value); }


  public uint Admin
  { get => Accessor.GetUInt32("admin"); set => Accessor.SetUInt32("admin", value); }


  public uint Itemindex
  { get => Accessor.GetUInt32("itemindex"); set => Accessor.SetUInt32("itemindex", value); }

}
