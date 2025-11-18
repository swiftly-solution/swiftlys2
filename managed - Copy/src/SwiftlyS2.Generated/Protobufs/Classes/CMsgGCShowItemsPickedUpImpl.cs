
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCShowItemsPickedUpImpl : TypedProtobuf<CMsgGCShowItemsPickedUp>, CMsgGCShowItemsPickedUp
{
  public CMsgGCShowItemsPickedUpImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong PlayerSteamid
  { get => Accessor.GetUInt64("player_steamid"); set => Accessor.SetUInt64("player_steamid", value); }

}
