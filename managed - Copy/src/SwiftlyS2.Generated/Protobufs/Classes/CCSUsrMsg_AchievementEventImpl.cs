
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_AchievementEventImpl : NetMessage<CCSUsrMsg_AchievementEvent>, CCSUsrMsg_AchievementEvent
{
  public CCSUsrMsg_AchievementEventImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public int Achievement
  { get => Accessor.GetInt32("achievement"); set => Accessor.SetInt32("achievement", value); }


  public int Count
  { get => Accessor.GetInt32("count"); set => Accessor.SetInt32("count", value); }


  public int UserId
  { get => Accessor.GetInt32("user_id"); set => Accessor.SetInt32("user_id", value); }

}
