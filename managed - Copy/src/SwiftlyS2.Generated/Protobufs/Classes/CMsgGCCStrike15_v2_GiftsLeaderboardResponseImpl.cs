
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCCStrike15_v2_GiftsLeaderboardResponseImpl : TypedProtobuf<CMsgGCCStrike15_v2_GiftsLeaderboardResponse>, CMsgGCCStrike15_v2_GiftsLeaderboardResponse
{
  public CMsgGCCStrike15_v2_GiftsLeaderboardResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint Servertime
  { get => Accessor.GetUInt32("servertime"); set => Accessor.SetUInt32("servertime", value); }


  public uint TimePeriodSeconds
  { get => Accessor.GetUInt32("time_period_seconds"); set => Accessor.SetUInt32("time_period_seconds", value); }


  public uint TotalGiftsGiven
  { get => Accessor.GetUInt32("total_gifts_given"); set => Accessor.SetUInt32("total_gifts_given", value); }


  public uint TotalGivers
  { get => Accessor.GetUInt32("total_givers"); set => Accessor.SetUInt32("total_givers", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_GiftsLeaderboardResponse_GiftLeaderboardEntry> Entries
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_GiftsLeaderboardResponse_GiftLeaderboardEntry>(Accessor, "entries"); }

}
