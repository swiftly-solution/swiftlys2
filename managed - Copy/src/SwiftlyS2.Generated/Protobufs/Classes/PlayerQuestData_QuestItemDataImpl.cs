
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class PlayerQuestData_QuestItemDataImpl : TypedProtobuf<PlayerQuestData_QuestItemData>, PlayerQuestData_QuestItemData
{
  public PlayerQuestData_QuestItemDataImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong QuestId
  { get => Accessor.GetUInt64("quest_id"); set => Accessor.SetUInt64("quest_id", value); }


  public int QuestNormalPointsEarned
  { get => Accessor.GetInt32("quest_normal_points_earned"); set => Accessor.SetInt32("quest_normal_points_earned", value); }


  public int QuestBonusPointsEarned
  { get => Accessor.GetInt32("quest_bonus_points_earned"); set => Accessor.SetInt32("quest_bonus_points_earned", value); }


  public IProtobufRepeatedFieldValueType<int> QuestNormalPointsRequired
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "quest_normal_points_required"); }


  public IProtobufRepeatedFieldValueType<int> QuestRewardXp
  { get => new ProtobufRepeatedFieldValueType<int>(Accessor, "quest_reward_xp"); }


  public int QuestPeriod
  { get => Accessor.GetInt32("quest_period"); set => Accessor.SetInt32("quest_period", value); }


  public QuestType QuestType
  { get => (QuestType)Accessor.GetInt32("quest_type"); set => Accessor.SetInt32("quest_type", (int)value); }

}
