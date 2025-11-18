
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGCToGCRequestPassportItemGrantImpl : TypedProtobuf<CMsgGCToGCRequestPassportItemGrant>, CMsgGCToGCRequestPassportItemGrant
{
  public CMsgGCToGCRequestPassportItemGrantImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong SteamId
  { get => Accessor.GetUInt64("steam_id"); set => Accessor.SetUInt64("steam_id", value); }


  public uint LeagueId
  { get => Accessor.GetUInt32("league_id"); set => Accessor.SetUInt32("league_id", value); }


  public int RewardFlag
  { get => Accessor.GetInt32("reward_flag"); set => Accessor.SetInt32("reward_flag", value); }

}
