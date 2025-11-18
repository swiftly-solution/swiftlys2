
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOItemRecipe : ITypedProtobuf<CSOItemRecipe>
{
  static CSOItemRecipe ITypedProtobuf<CSOItemRecipe>.Wrap(nint handle, bool isManuallyAllocated) => new CSOItemRecipeImpl(handle, isManuallyAllocated);


  public uint DefIndex { get; set; }


  public string Name { get; set; }


  public string NA { get; set; }


  public string DescInputs { get; set; }


  public string DescOutputs { get; set; }


  public string DiA { get; set; }


  public string DiB { get; set; }


  public string DiC { get; set; }


  public string DoA { get; set; }


  public string DoB { get; set; }


  public string DoC { get; set; }


  public bool RequiresAllSameClass { get; set; }


  public bool RequiresAllSameSlot { get; set; }


  public int ClassUsageForOutput { get; set; }


  public int SlotUsageForOutput { get; set; }


  public int SetForOutput { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CSOItemCriteria> InputItemsCriteria { get; }


  public IProtobufRepeatedFieldSubMessageType<CSOItemCriteria> OutputItemsCriteria { get; }


  public IProtobufRepeatedFieldValueType<uint> InputItemDupeCounts { get; }

}
