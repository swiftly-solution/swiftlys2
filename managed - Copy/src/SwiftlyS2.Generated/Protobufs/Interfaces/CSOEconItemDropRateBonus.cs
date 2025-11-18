
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOEconItemDropRateBonus : ITypedProtobuf<CSOEconItemDropRateBonus>
{
  static CSOEconItemDropRateBonus ITypedProtobuf<CSOEconItemDropRateBonus>.Wrap(nint handle, bool isManuallyAllocated) => new CSOEconItemDropRateBonusImpl(handle, isManuallyAllocated);


  public uint AccountId { get; set; }


  public uint ExpirationDate { get; set; }


  public float Bonus { get; set; }


  public uint BonusCount { get; set; }


  public ulong ItemId { get; set; }


  public uint DefIndex { get; set; }

}
