
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_VoiceMask_PlayerMask : ITypedProtobuf<CCSUsrMsg_VoiceMask_PlayerMask>
{
  static CCSUsrMsg_VoiceMask_PlayerMask ITypedProtobuf<CCSUsrMsg_VoiceMask_PlayerMask>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_VoiceMask_PlayerMaskImpl(handle, isManuallyAllocated);


  public int GameRulesMask { get; set; }


  public int BanMasks { get; set; }

}
