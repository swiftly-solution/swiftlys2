
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCCstrike15_v2_GC2ServerNotifyXPRewarded : ITypedProtobuf<CMsgGCCstrike15_v2_GC2ServerNotifyXPRewarded>
{
  static CMsgGCCstrike15_v2_GC2ServerNotifyXPRewarded ITypedProtobuf<CMsgGCCstrike15_v2_GC2ServerNotifyXPRewarded>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCCstrike15_v2_GC2ServerNotifyXPRewardedImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<XpProgressData> XpProgressData { get; }


  public uint AccountId { get; set; }


  public uint CurrentXp { get; set; }


  public uint CurrentLevel { get; set; }


  public uint UpgradedDefidx { get; set; }


  public uint OperationPointsAwarded { get; set; }


  public uint FreeRewards { get; set; }


  public uint XpTrailRemaining { get; set; }


  public int XpTrailXpNeeded { get; set; }


  public uint XpTrailLevel { get; set; }

}
