
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCIncrementKillCountResponseImpl : TypedProtobuf<CMsgGCIncrementKillCountResponse>, CMsgGCIncrementKillCountResponse
{
  public CMsgGCIncrementKillCountResponseImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint KillerAccountId
  { get => Accessor.GetUInt32("killer_account_id"); set => Accessor.SetUInt32("killer_account_id", value); }


  public uint NumKills
  { get => Accessor.GetUInt32("num_kills"); set => Accessor.SetUInt32("num_kills", value); }


  public uint ItemDef
  { get => Accessor.GetUInt32("item_def"); set => Accessor.SetUInt32("item_def", value); }


  public uint LevelType
  { get => Accessor.GetUInt32("level_type"); set => Accessor.SetUInt32("level_type", value); }

}
