
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_QuestProgress : ITypedProtobuf<CCSUsrMsg_QuestProgress>, INetMessage<CCSUsrMsg_QuestProgress>, IDisposable
{
  static int INetMessage<CCSUsrMsg_QuestProgress>.MessageId => 366;
  
  static string INetMessage<CCSUsrMsg_QuestProgress>.MessageName => "CCSUsrMsg_QuestProgress";

  static CCSUsrMsg_QuestProgress ITypedProtobuf<CCSUsrMsg_QuestProgress>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_QuestProgressImpl(handle, isManuallyAllocated);


  public uint QuestId { get; set; }


  public uint NormalPoints { get; set; }


  public uint BonusPoints { get; set; }


  public bool IsEventQuest { get; set; }

}
