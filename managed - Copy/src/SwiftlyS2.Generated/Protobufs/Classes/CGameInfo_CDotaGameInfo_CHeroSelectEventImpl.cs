
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CGameInfo_CDotaGameInfo_CHeroSelectEventImpl : TypedProtobuf<CGameInfo_CDotaGameInfo_CHeroSelectEvent>, CGameInfo_CDotaGameInfo_CHeroSelectEvent
{
  public CGameInfo_CDotaGameInfo_CHeroSelectEventImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool IsPick
  { get => Accessor.GetBool("is_pick"); set => Accessor.SetBool("is_pick", value); }


  public uint Team
  { get => Accessor.GetUInt32("team"); set => Accessor.SetUInt32("team", value); }


  public int HeroId
  { get => Accessor.GetInt32("hero_id"); set => Accessor.SetInt32("hero_id", value); }

}
