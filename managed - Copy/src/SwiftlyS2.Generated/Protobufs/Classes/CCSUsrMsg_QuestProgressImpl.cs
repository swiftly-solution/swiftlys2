
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_QuestProgressImpl : NetMessage<CCSUsrMsg_QuestProgress>, CCSUsrMsg_QuestProgress
{
  public CCSUsrMsg_QuestProgressImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint QuestId
  { get => Accessor.GetUInt32("quest_id"); set => Accessor.SetUInt32("quest_id", value); }


  public uint NormalPoints
  { get => Accessor.GetUInt32("normal_points"); set => Accessor.SetUInt32("normal_points", value); }


  public uint BonusPoints
  { get => Accessor.GetUInt32("bonus_points"); set => Accessor.SetUInt32("bonus_points", value); }


  public bool IsEventQuest
  { get => Accessor.GetBool("is_event_quest"); set => Accessor.SetBool("is_event_quest", value); }

}
