
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSystemBroadcastImpl : TypedProtobuf<CMsgSystemBroadcast>, CMsgSystemBroadcast
{
  public CMsgSystemBroadcastImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Message
  { get => Accessor.GetString("message"); set => Accessor.SetString("message", value); }

}
