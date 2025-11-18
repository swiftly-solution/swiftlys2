
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSVCMsg_UserMessageImpl : NetMessage<CSVCMsg_UserMessage>, CSVCMsg_UserMessage
{
  public CSVCMsg_UserMessageImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int MsgType
  { get => Accessor.GetInt32("msg_type"); set => Accessor.SetInt32("msg_type", value); }


  public byte[] MsgData
  { get => Accessor.GetBytes("msg_data"); set => Accessor.SetBytes("msg_data", value); }


  public int Passthrough
  { get => Accessor.GetInt32("passthrough"); set => Accessor.SetInt32("passthrough", value); }

}
