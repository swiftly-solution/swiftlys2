
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCNameItemNotificationImpl : TypedProtobuf<CMsgGCNameItemNotification>, CMsgGCNameItemNotification
{
  public CMsgGCNameItemNotificationImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong PlayerSteamid
  { get => Accessor.GetUInt64("player_steamid"); set => Accessor.SetUInt64("player_steamid", value); }


  public uint ItemDefIndex
  { get => Accessor.GetUInt32("item_def_index"); set => Accessor.SetUInt32("item_def_index", value); }


  public string ItemNameCustom
  { get => Accessor.GetString("item_name_custom"); set => Accessor.SetString("item_name_custom", value); }

}
