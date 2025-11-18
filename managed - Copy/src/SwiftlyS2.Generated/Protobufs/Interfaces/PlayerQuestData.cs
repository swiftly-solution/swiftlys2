
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface PlayerQuestData : ITypedProtobuf<PlayerQuestData>
{
  static PlayerQuestData ITypedProtobuf<PlayerQuestData>.Wrap(nint handle, bool isManuallyAllocated) => new PlayerQuestDataImpl(handle, isManuallyAllocated);


  public uint QuesterAccountId { get; set; }


  public IProtobufRepeatedFieldSubMessageType<PlayerQuestData_QuestItemData> QuestItemData { get; }


  public IProtobufRepeatedFieldSubMessageType<XpProgressData> XpProgressData { get; }


  public uint TimePlayed { get; set; }


  public uint MmGameMode { get; set; }


  public IProtobufRepeatedFieldSubMessageType<MatchEndItemUpdates> ItemUpdates { get; }


  public bool OperationPointsEligible { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgCsgoSteamUserStatChange> Userstatchanges { get; }

}
