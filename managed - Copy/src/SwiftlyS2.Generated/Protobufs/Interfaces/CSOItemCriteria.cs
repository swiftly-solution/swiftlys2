
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOItemCriteria : ITypedProtobuf<CSOItemCriteria>
{
  static CSOItemCriteria ITypedProtobuf<CSOItemCriteria>.Wrap(nint handle, bool isManuallyAllocated) => new CSOItemCriteriaImpl(handle, isManuallyAllocated);


  public uint ItemLevel { get; set; }


  public int ItemQuality { get; set; }


  public bool ItemLevelSet { get; set; }


  public bool ItemQualitySet { get; set; }


  public uint InitialInventory { get; set; }


  public uint InitialQuantity { get; set; }


  public bool IgnoreEnabledFlag { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CSOItemCriteriaCondition> Conditions { get; }


  public int ItemRarity { get; set; }


  public bool ItemRaritySet { get; set; }


  public bool RecentOnly { get; set; }

}
