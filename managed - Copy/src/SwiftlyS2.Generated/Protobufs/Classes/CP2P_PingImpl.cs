
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CP2P_PingImpl : TypedProtobuf<CP2P_Ping>, CP2P_Ping
{
  public CP2P_PingImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong SendTime
  { get => Accessor.GetUInt64("send_time"); set => Accessor.SetUInt64("send_time", value); }


  public bool IsReply
  { get => Accessor.GetBool("is_reply"); set => Accessor.SetBool("is_reply", value); }

}
