
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_SendTable_sendprop_t : ITypedProtobuf<CSVCMsg_SendTable_sendprop_t>
{
  static CSVCMsg_SendTable_sendprop_t ITypedProtobuf<CSVCMsg_SendTable_sendprop_t>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_SendTable_sendprop_tImpl(handle, isManuallyAllocated);


  public int Type { get; set; }


  public string VarName { get; set; }


  public int Flags { get; set; }


  public int Priority { get; set; }


  public string DtName { get; set; }


  public int NumElements { get; set; }


  public float LowValue { get; set; }


  public float HighValue { get; set; }


  public int NumBits { get; set; }

}
