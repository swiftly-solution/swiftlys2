
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGC_ServerQuestUpdateData : ITypedProtobuf<CMsgGC_ServerQuestUpdateData>
{
  static CMsgGC_ServerQuestUpdateData ITypedProtobuf<CMsgGC_ServerQuestUpdateData>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGC_ServerQuestUpdateDataImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<PlayerQuestData> PlayerQuestData { get; }


  public byte[] BinaryData { get; set; }


  public uint MmGameMode { get; set; }


  public ScoreLeaderboardData Missionlbsdata { get; }


  public uint Flags { get; set; }

}
