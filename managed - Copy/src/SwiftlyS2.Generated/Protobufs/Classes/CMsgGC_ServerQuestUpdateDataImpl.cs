
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgGC_ServerQuestUpdateDataImpl : TypedProtobuf<CMsgGC_ServerQuestUpdateData>, CMsgGC_ServerQuestUpdateData
{
  public CMsgGC_ServerQuestUpdateDataImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<PlayerQuestData> PlayerQuestData
  { get => new ProtobufRepeatedFieldSubMessageType<PlayerQuestData>(Accessor, "player_quest_data"); }


  public byte[] BinaryData
  { get => Accessor.GetBytes("binary_data"); set => Accessor.SetBytes("binary_data", value); }


  public uint MmGameMode
  { get => Accessor.GetUInt32("mm_game_mode"); set => Accessor.SetUInt32("mm_game_mode", value); }


  public ScoreLeaderboardData Missionlbsdata
  { get => new ScoreLeaderboardDataImpl(NativeNetMessages.GetNestedMessage(Address, "missionlbsdata"), false); }


  public uint Flags
  { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }

}
