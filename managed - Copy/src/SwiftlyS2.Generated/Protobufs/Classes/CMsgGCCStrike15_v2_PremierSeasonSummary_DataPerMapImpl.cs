
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerMapImpl : TypedProtobuf<CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerMap>, CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerMap
{
  public CMsgGCCStrike15_v2_PremierSeasonSummary_DataPerMapImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint MapId
  { get => Accessor.GetUInt32("map_id"); set => Accessor.SetUInt32("map_id", value); }


  public uint Wins
  { get => Accessor.GetUInt32("wins"); set => Accessor.SetUInt32("wins", value); }


  public uint Ties
  { get => Accessor.GetUInt32("ties"); set => Accessor.SetUInt32("ties", value); }


  public uint Losses
  { get => Accessor.GetUInt32("losses"); set => Accessor.SetUInt32("losses", value); }


  public uint Rounds
  { get => Accessor.GetUInt32("rounds"); set => Accessor.SetUInt32("rounds", value); }


  public uint Kills
  { get => Accessor.GetUInt32("kills"); set => Accessor.SetUInt32("kills", value); }


  public uint Headshots
  { get => Accessor.GetUInt32("headshots"); set => Accessor.SetUInt32("headshots", value); }


  public uint Assists
  { get => Accessor.GetUInt32("assists"); set => Accessor.SetUInt32("assists", value); }


  public uint Deaths
  { get => Accessor.GetUInt32("deaths"); set => Accessor.SetUInt32("deaths", value); }


  public uint Mvps
  { get => Accessor.GetUInt32("mvps"); set => Accessor.SetUInt32("mvps", value); }


  public uint Rounds3k
  { get => Accessor.GetUInt32("rounds_3k"); set => Accessor.SetUInt32("rounds_3k", value); }


  public uint Rounds4k
  { get => Accessor.GetUInt32("rounds_4k"); set => Accessor.SetUInt32("rounds_4k", value); }


  public uint Rounds5k
  { get => Accessor.GetUInt32("rounds_5k"); set => Accessor.SetUInt32("rounds_5k", value); }

}
