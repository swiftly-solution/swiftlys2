
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_VoiceMaskImpl : NetMessage<CCSUsrMsg_VoiceMask>, CCSUsrMsg_VoiceMask
{
  public CCSUsrMsg_VoiceMaskImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CCSUsrMsg_VoiceMask_PlayerMask> PlayerMasks
  { get => new ProtobufRepeatedFieldSubMessageType<CCSUsrMsg_VoiceMask_PlayerMask>(Accessor, "player_masks"); }


  public bool PlayerModEnable
  { get => Accessor.GetBool("player_mod_enable"); set => Accessor.SetBool("player_mod_enable", value); }

}
