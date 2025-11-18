
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOItemRecipeImpl : TypedProtobuf<CSOItemRecipe>, CSOItemRecipe
{
  public CSOItemRecipeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint DefIndex
  { get => Accessor.GetUInt32("def_index"); set => Accessor.SetUInt32("def_index", value); }


  public string Name
  { get => Accessor.GetString("name"); set => Accessor.SetString("name", value); }


  public string NA
  { get => Accessor.GetString("n_a"); set => Accessor.SetString("n_a", value); }


  public string DescInputs
  { get => Accessor.GetString("desc_inputs"); set => Accessor.SetString("desc_inputs", value); }


  public string DescOutputs
  { get => Accessor.GetString("desc_outputs"); set => Accessor.SetString("desc_outputs", value); }


  public string DiA
  { get => Accessor.GetString("di_a"); set => Accessor.SetString("di_a", value); }


  public string DiB
  { get => Accessor.GetString("di_b"); set => Accessor.SetString("di_b", value); }


  public string DiC
  { get => Accessor.GetString("di_c"); set => Accessor.SetString("di_c", value); }


  public string DoA
  { get => Accessor.GetString("do_a"); set => Accessor.SetString("do_a", value); }


  public string DoB
  { get => Accessor.GetString("do_b"); set => Accessor.SetString("do_b", value); }


  public string DoC
  { get => Accessor.GetString("do_c"); set => Accessor.SetString("do_c", value); }


  public bool RequiresAllSameClass
  { get => Accessor.GetBool("requires_all_same_class"); set => Accessor.SetBool("requires_all_same_class", value); }


  public bool RequiresAllSameSlot
  { get => Accessor.GetBool("requires_all_same_slot"); set => Accessor.SetBool("requires_all_same_slot", value); }


  public int ClassUsageForOutput
  { get => Accessor.GetInt32("class_usage_for_output"); set => Accessor.SetInt32("class_usage_for_output", value); }


  public int SlotUsageForOutput
  { get => Accessor.GetInt32("slot_usage_for_output"); set => Accessor.SetInt32("slot_usage_for_output", value); }


  public int SetForOutput
  { get => Accessor.GetInt32("set_for_output"); set => Accessor.SetInt32("set_for_output", value); }


  public IProtobufRepeatedFieldSubMessageType<CSOItemCriteria> InputItemsCriteria
  { get => new ProtobufRepeatedFieldSubMessageType<CSOItemCriteria>(Accessor, "input_items_criteria"); }


  public IProtobufRepeatedFieldSubMessageType<CSOItemCriteria> OutputItemsCriteria
  { get => new ProtobufRepeatedFieldSubMessageType<CSOItemCriteria>(Accessor, "output_items_criteria"); }


  public IProtobufRepeatedFieldValueType<uint> InputItemDupeCounts
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "input_item_dupe_counts"); }

}
