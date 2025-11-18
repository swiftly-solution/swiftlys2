
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CBidirMsg_RebroadcastSourceImpl : TypedProtobuf<CBidirMsg_RebroadcastSource>, CBidirMsg_RebroadcastSource
{
  public CBidirMsg_RebroadcastSourceImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int Eventsource
  { get => Accessor.GetInt32("eventsource"); set => Accessor.SetInt32("eventsource", value); }

}
