
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgApplyPennantUpgradeImpl : TypedProtobuf<CMsgApplyPennantUpgrade>, CMsgApplyPennantUpgrade
{
  public CMsgApplyPennantUpgradeImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong UpgradeItemId
  { get => Accessor.GetUInt64("upgrade_item_id"); set => Accessor.SetUInt64("upgrade_item_id", value); }


  public ulong PennantItemId
  { get => Accessor.GetUInt64("pennant_item_id"); set => Accessor.SetUInt64("pennant_item_id", value); }

}
