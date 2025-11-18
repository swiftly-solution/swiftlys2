
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CBidirMsg_RebroadcastGameEventImpl : TypedProtobuf<CBidirMsg_RebroadcastGameEvent>, CBidirMsg_RebroadcastGameEvent
{
  public CBidirMsg_RebroadcastGameEventImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool Posttoserver
  { get => Accessor.GetBool("posttoserver"); set => Accessor.SetBool("posttoserver", value); }


  public int Buftype
  { get => Accessor.GetInt32("buftype"); set => Accessor.SetInt32("buftype", value); }


  public uint Clientbitcount
  { get => Accessor.GetUInt32("clientbitcount"); set => Accessor.SetUInt32("clientbitcount", value); }


  public ulong Receivingclients
  { get => Accessor.GetUInt64("receivingclients"); set => Accessor.SetUInt64("receivingclients", value); }

}
