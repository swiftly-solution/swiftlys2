
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSVCMsg_SendTable : ITypedProtobuf<CSVCMsg_SendTable>
{
  static CSVCMsg_SendTable ITypedProtobuf<CSVCMsg_SendTable>.Wrap(nint handle, bool isManuallyAllocated) => new CSVCMsg_SendTableImpl(handle, isManuallyAllocated);


  public bool IsEnd { get; set; }


  public string NetTableName { get; set; }


  public bool NeedsDecoder { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CSVCMsg_SendTable_sendprop_t> Props { get; }

}
