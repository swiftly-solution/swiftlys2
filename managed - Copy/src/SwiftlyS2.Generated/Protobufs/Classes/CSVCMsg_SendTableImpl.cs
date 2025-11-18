
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_SendTableImpl : TypedProtobuf<CSVCMsg_SendTable>, CSVCMsg_SendTable
{
  public CSVCMsg_SendTableImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool IsEnd
  { get => Accessor.GetBool("is_end"); set => Accessor.SetBool("is_end", value); }


  public string NetTableName
  { get => Accessor.GetString("net_table_name"); set => Accessor.SetString("net_table_name", value); }


  public bool NeedsDecoder
  { get => Accessor.GetBool("needs_decoder"); set => Accessor.SetBool("needs_decoder", value); }


  public IProtobufRepeatedFieldSubMessageType<CSVCMsg_SendTable_sendprop_t> Props
  { get => new ProtobufRepeatedFieldSubMessageType<CSVCMsg_SendTable_sendprop_t>(Accessor, "props"); }

}
