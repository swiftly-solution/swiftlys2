
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOItemCriteriaImpl : TypedProtobuf<CSOItemCriteria>, CSOItemCriteria
{
  public CSOItemCriteriaImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint ItemLevel
  { get => Accessor.GetUInt32("item_level"); set => Accessor.SetUInt32("item_level", value); }


  public int ItemQuality
  { get => Accessor.GetInt32("item_quality"); set => Accessor.SetInt32("item_quality", value); }


  public bool ItemLevelSet
  { get => Accessor.GetBool("item_level_set"); set => Accessor.SetBool("item_level_set", value); }


  public bool ItemQualitySet
  { get => Accessor.GetBool("item_quality_set"); set => Accessor.SetBool("item_quality_set", value); }


  public uint InitialInventory
  { get => Accessor.GetUInt32("initial_inventory"); set => Accessor.SetUInt32("initial_inventory", value); }


  public uint InitialQuantity
  { get => Accessor.GetUInt32("initial_quantity"); set => Accessor.SetUInt32("initial_quantity", value); }


  public bool IgnoreEnabledFlag
  { get => Accessor.GetBool("ignore_enabled_flag"); set => Accessor.SetBool("ignore_enabled_flag", value); }


  public IProtobufRepeatedFieldSubMessageType<CSOItemCriteriaCondition> Conditions
  { get => new ProtobufRepeatedFieldSubMessageType<CSOItemCriteriaCondition>(Accessor, "conditions"); }


  public int ItemRarity
  { get => Accessor.GetInt32("item_rarity"); set => Accessor.SetInt32("item_rarity", value); }


  public bool ItemRaritySet
  { get => Accessor.GetBool("item_rarity_set"); set => Accessor.SetBool("item_rarity_set", value); }


  public bool RecentOnly
  { get => Accessor.GetBool("recent_only"); set => Accessor.SetBool("recent_only", value); }

}
