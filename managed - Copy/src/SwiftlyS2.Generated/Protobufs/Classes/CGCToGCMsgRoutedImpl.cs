
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGCToGCMsgRoutedImpl : TypedProtobuf<CGCToGCMsgRouted>, CGCToGCMsgRouted
{
  public CGCToGCMsgRoutedImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint MsgType
  { get => Accessor.GetUInt32("msg_type"); set => Accessor.SetUInt32("msg_type", value); }


  public ulong SenderId
  { get => Accessor.GetUInt64("sender_id"); set => Accessor.SetUInt64("sender_id", value); }


  public byte[] NetMessage
  { get => Accessor.GetBytes("net_message"); set => Accessor.SetBytes("net_message", value); }


  public uint Ip
  { get => Accessor.GetUInt32("ip"); set => Accessor.SetUInt32("ip", value); }

}
