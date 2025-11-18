
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSOItemCriteriaConditionImpl : TypedProtobuf<CSOItemCriteriaCondition>, CSOItemCriteriaCondition
{
  public CSOItemCriteriaConditionImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Op
  { get => Accessor.GetInt32("op"); set => Accessor.SetInt32("op", value); }


  public string Field
  { get => Accessor.GetString("field"); set => Accessor.SetString("field", value); }


  public bool Required
  { get => Accessor.GetBool("required"); set => Accessor.SetBool("required", value); }


  public float FloatValue
  { get => Accessor.GetFloat("float_value"); set => Accessor.SetFloat("float_value", value); }


  public string StringValue
  { get => Accessor.GetString("string_value"); set => Accessor.SetString("string_value", value); }

}
