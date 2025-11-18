
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_AchievementEvent : ITypedProtobuf<CCSUsrMsg_AchievementEvent>, INetMessage<CCSUsrMsg_AchievementEvent>, IDisposable
{
  static int INetMessage<CCSUsrMsg_AchievementEvent>.MessageId => 333;
  
  static string INetMessage<CCSUsrMsg_AchievementEvent>.MessageName => "CCSUsrMsg_AchievementEvent";

  static CCSUsrMsg_AchievementEvent ITypedProtobuf<CCSUsrMsg_AchievementEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_AchievementEventImpl(handle, isManuallyAllocated);


  public int Achievement { get; set; }


  public int Count { get; set; }


  public int UserId { get; set; }

}
