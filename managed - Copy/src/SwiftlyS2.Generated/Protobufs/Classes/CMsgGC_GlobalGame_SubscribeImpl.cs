
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGC_GlobalGame_SubscribeImpl : TypedProtobuf<CMsgGC_GlobalGame_Subscribe>, CMsgGC_GlobalGame_Subscribe
{
  public CMsgGC_GlobalGame_SubscribeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Ticket
  { get => Accessor.GetUInt64("ticket"); set => Accessor.SetUInt64("ticket", value); }

}
