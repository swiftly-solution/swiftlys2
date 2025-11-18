
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgConsumableExhaustedImpl : TypedProtobuf<CMsgConsumableExhausted>, CMsgConsumableExhausted
{
  public CMsgConsumableExhaustedImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int ItemDefId
  { get => Accessor.GetInt32("item_def_id"); set => Accessor.SetInt32("item_def_id", value); }

}
