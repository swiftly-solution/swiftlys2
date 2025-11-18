
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_VoiceMask_PlayerMaskImpl : TypedProtobuf<CCSUsrMsg_VoiceMask_PlayerMask>, CCSUsrMsg_VoiceMask_PlayerMask
{
  public CCSUsrMsg_VoiceMask_PlayerMaskImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int GameRulesMask
  { get => Accessor.GetInt32("game_rules_mask"); set => Accessor.SetInt32("game_rules_mask", value); }


  public int BanMasks
  { get => Accessor.GetInt32("ban_masks"); set => Accessor.SetInt32("ban_masks", value); }

}
