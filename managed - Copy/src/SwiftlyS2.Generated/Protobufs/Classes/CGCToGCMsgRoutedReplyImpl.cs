
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGCToGCMsgRoutedReplyImpl : TypedProtobuf<CGCToGCMsgRoutedReply>, CGCToGCMsgRoutedReply
{
  public CGCToGCMsgRoutedReplyImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint MsgType
  { get => Accessor.GetUInt32("msg_type"); set => Accessor.SetUInt32("msg_type", value); }


  public byte[] NetMessage
  { get => Accessor.GetBytes("net_message"); set => Accessor.SetBytes("net_message", value); }

}
