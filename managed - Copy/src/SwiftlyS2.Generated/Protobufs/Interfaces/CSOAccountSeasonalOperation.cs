
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOAccountSeasonalOperation : ITypedProtobuf<CSOAccountSeasonalOperation>
{
  static CSOAccountSeasonalOperation ITypedProtobuf<CSOAccountSeasonalOperation>.Wrap(nint handle, bool isManuallyAllocated) => new CSOAccountSeasonalOperationImpl(handle, isManuallyAllocated);


  public uint SeasonValue { get; set; }


  public uint TierUnlocked { get; set; }


  public uint PremiumTiers { get; set; }


  public uint MissionId { get; set; }


  public uint MissionsCompleted { get; set; }


  public uint RedeemableBalance { get; set; }


  public uint SeasonPassTime { get; set; }

}
