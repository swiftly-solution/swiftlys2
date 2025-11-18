
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_MarkAchievementImpl : NetMessage<CCSUsrMsg_MarkAchievement>, CCSUsrMsg_MarkAchievement
{
  public CCSUsrMsg_MarkAchievementImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public string Achievement
  { get => Accessor.GetString("achievement"); set => Accessor.SetString("achievement", value); }

}
