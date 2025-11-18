
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgAcknowledgeRentalExpirationImpl : TypedProtobuf<CMsgAcknowledgeRentalExpiration>, CMsgAcknowledgeRentalExpiration
{
  public CMsgAcknowledgeRentalExpirationImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong CrateItemId
  { get => Accessor.GetUInt64("crate_item_id"); set => Accessor.SetUInt64("crate_item_id", value); }

}
