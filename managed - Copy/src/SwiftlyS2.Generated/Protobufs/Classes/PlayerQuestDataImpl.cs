
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class PlayerQuestDataImpl : TypedProtobuf<PlayerQuestData>, PlayerQuestData
{
  public PlayerQuestDataImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public uint QuesterAccountId
  { get => Accessor.GetUInt32("quester_account_id"); set => Accessor.SetUInt32("quester_account_id", value); }


  public IProtobufRepeatedFieldSubMessageType<PlayerQuestData_QuestItemData> QuestItemData
  { get => new ProtobufRepeatedFieldSubMessageType<PlayerQuestData_QuestItemData>(Accessor, "quest_item_data"); }


  public IProtobufRepeatedFieldSubMessageType<XpProgressData> XpProgressData
  { get => new ProtobufRepeatedFieldSubMessageType<XpProgressData>(Accessor, "xp_progress_data"); }


  public uint TimePlayed
  { get => Accessor.GetUInt32("time_played"); set => Accessor.SetUInt32("time_played", value); }


  public uint MmGameMode
  { get => Accessor.GetUInt32("mm_game_mode"); set => Accessor.SetUInt32("mm_game_mode", value); }


  public IProtobufRepeatedFieldSubMessageType<MatchEndItemUpdates> ItemUpdates
  { get => new ProtobufRepeatedFieldSubMessageType<MatchEndItemUpdates>(Accessor, "item_updates"); }


  public bool OperationPointsEligible
  { get => Accessor.GetBool("operation_points_eligible"); set => Accessor.SetBool("operation_points_eligible", value); }


  public IProtobufRepeatedFieldSubMessageType<CMsgCsgoSteamUserStatChange> Userstatchanges
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgCsgoSteamUserStatChange>(Accessor, "userstatchanges"); }

}
