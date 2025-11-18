
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PlayerQuestData_QuestItemData : ITypedProtobuf<PlayerQuestData_QuestItemData>
{
  static PlayerQuestData_QuestItemData ITypedProtobuf<PlayerQuestData_QuestItemData>.Wrap(nint handle, bool isManuallyAllocated) => new PlayerQuestData_QuestItemDataImpl(handle, isManuallyAllocated);


  public ulong QuestId { get; set; }


  public int QuestNormalPointsEarned { get; set; }


  public int QuestBonusPointsEarned { get; set; }


  public IProtobufRepeatedFieldValueType<int> QuestNormalPointsRequired { get; }


  public IProtobufRepeatedFieldValueType<int> QuestRewardXp { get; }


  public int QuestPeriod { get; set; }


  public QuestType QuestType { get; set; }

}
