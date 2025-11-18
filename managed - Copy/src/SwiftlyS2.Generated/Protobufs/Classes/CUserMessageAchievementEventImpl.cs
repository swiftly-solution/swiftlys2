
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageAchievementEventImpl : NetMessage<CUserMessageAchievementEvent>, CUserMessageAchievementEvent
{
  public CUserMessageAchievementEventImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Achievement
  { get => Accessor.GetUInt32("achievement"); set => Accessor.SetUInt32("achievement", value); }

}
