
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessage_NotifyResponseFoundImpl : NetMessage<CUserMessage_NotifyResponseFound>, CUserMessage_NotifyResponseFound
{
  public CUserMessage_NotifyResponseFoundImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int EntIndex
  { get => Accessor.GetInt32("ent_index"); set => Accessor.SetInt32("ent_index", value); }


  public string RuleName
  { get => Accessor.GetString("rule_name"); set => Accessor.SetString("rule_name", value); }


  public string ResponseValue
  { get => Accessor.GetString("response_value"); set => Accessor.SetString("response_value", value); }


  public string ResponseConcept
  { get => Accessor.GetString("response_concept"); set => Accessor.SetString("response_concept", value); }


  public IProtobufRepeatedFieldSubMessageType<CUserMessage_NotifyResponseFound_Criteria> Criteria
  { get => new ProtobufRepeatedFieldSubMessageType<CUserMessage_NotifyResponseFound_Criteria>(Accessor, "criteria"); }


  public IProtobufRepeatedFieldValueType<uint> IntCriteriaNames
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "int_criteria_names"); }


  public IProtobufRepeatedFieldValueType<int> IntCriteriaValues
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "int_criteria_values"); }


  public IProtobufRepeatedFieldValueType<uint> FloatCriteriaNames
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "float_criteria_names"); }


  public IProtobufRepeatedFieldValueType<float> FloatCriteriaValues
  { get => new ProtobufRepeatedFieldValueType<float>(Accessor, "float_criteria_values"); }


  public IProtobufRepeatedFieldValueType<uint> SymbolCriteriaNames
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "symbol_criteria_names"); }


  public IProtobufRepeatedFieldValueType<uint> SymbolCriteriaValues
  { get => new ProtobufRepeatedFieldValueType<uint>(Accessor, "symbol_criteria_values"); }


  public int SpeakResult
  { get => Accessor.GetInt32("speak_result"); set => Accessor.SetInt32("speak_result", value); }

}
