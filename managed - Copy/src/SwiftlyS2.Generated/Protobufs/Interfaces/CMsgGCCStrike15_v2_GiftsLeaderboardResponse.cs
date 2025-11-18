
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCStrike15_v2_GiftsLeaderboardResponse : ITypedProtobuf<CMsgGCCStrike15_v2_GiftsLeaderboardResponse>
{
  static CMsgGCCStrike15_v2_GiftsLeaderboardResponse ITypedProtobuf<CMsgGCCStrike15_v2_GiftsLeaderboardResponse>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCStrike15_v2_GiftsLeaderboardResponseImpl(handle, isManuallyAllocated);


  public uint Servertime { get; set; }


  public uint TimePeriodSeconds { get; set; }


  public uint TotalGiftsGiven { get; set; }


  public uint TotalGivers { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgGCCStrike15_v2_GiftsLeaderboardResponse_GiftLeaderboardEntry> Entries { get; }

}
