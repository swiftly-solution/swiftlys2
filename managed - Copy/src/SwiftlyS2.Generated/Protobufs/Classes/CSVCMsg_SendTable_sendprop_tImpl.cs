
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_SendTable_sendprop_tImpl : TypedProtobuf<CSVCMsg_SendTable_sendprop_t>, CSVCMsg_SendTable_sendprop_t
{
  public CSVCMsg_SendTable_sendprop_tImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Type
  { get => Accessor.GetInt32("type"); set => Accessor.SetInt32("type", value); }


  public string VarName
  { get => Accessor.GetString("var_name"); set => Accessor.SetString("var_name", value); }


  public int Flags
  { get => Accessor.GetInt32("flags"); set => Accessor.SetInt32("flags", value); }


  public int Priority
  { get => Accessor.GetInt32("priority"); set => Accessor.SetInt32("priority", value); }


  public string DtName
  { get => Accessor.GetString("dt_name"); set => Accessor.SetString("dt_name", value); }


  public int NumElements
  { get => Accessor.GetInt32("num_elements"); set => Accessor.SetInt32("num_elements", value); }


  public float LowValue
  { get => Accessor.GetFloat("low_value"); set => Accessor.SetFloat("low_value", value); }


  public float HighValue
  { get => Accessor.GetFloat("high_value"); set => Accessor.SetFloat("high_value", value); }


  public int NumBits
  { get => Accessor.GetInt32("num_bits"); set => Accessor.SetInt32("num_bits", value); }

}
