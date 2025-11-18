
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOItemCriteriaCondition : ITypedProtobuf<CSOItemCriteriaCondition>
{
  static CSOItemCriteriaCondition ITypedProtobuf<CSOItemCriteriaCondition>.Wrap(nint handle, bool isManuallyAllocated) => new CSOItemCriteriaConditionImpl(handle, isManuallyAllocated);


  public int Op { get; set; }


  public string Field { get; set; }


  public bool Required { get; set; }


  public float FloatValue { get; set; }


  public string StringValue { get; set; }

}
