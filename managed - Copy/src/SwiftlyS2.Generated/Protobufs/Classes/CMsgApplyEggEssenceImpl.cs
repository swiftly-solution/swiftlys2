
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgApplyEggEssenceImpl : TypedProtobuf<CMsgApplyEggEssence>, CMsgApplyEggEssence
{
  public CMsgApplyEggEssenceImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong EssenceItemId
  { get => Accessor.GetUInt64("essence_item_id"); set => Accessor.SetUInt64("essence_item_id", value); }


  public ulong EggItemId
  { get => Accessor.GetUInt64("egg_item_id"); set => Accessor.SetUInt64("egg_item_id", value); }

}
