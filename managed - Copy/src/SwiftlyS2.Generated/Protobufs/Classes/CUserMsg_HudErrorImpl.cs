
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_HudErrorImpl : TypedProtobuf<CUserMsg_HudError>, CUserMsg_HudError
{
  public CUserMsg_HudErrorImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int OrderId
  { get => Accessor.GetInt32("order_id"); set => Accessor.SetInt32("order_id", value); }

}
