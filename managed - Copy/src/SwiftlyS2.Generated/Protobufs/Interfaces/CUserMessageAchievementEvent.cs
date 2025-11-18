
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageAchievementEvent : ITypedProtobuf<CUserMessageAchievementEvent>, INetMessage<CUserMessageAchievementEvent>, IDisposable
{
  static int INetMessage<CUserMessageAchievementEvent>.MessageId => 101;
  
  static string INetMessage<CUserMessageAchievementEvent>.MessageName => "CUserMessageAchievementEvent";

  static CUserMessageAchievementEvent ITypedProtobuf<CUserMessageAchievementEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageAchievementEventImpl(handle, isManuallyAllocated);


  public uint Achievement { get; set; }

}
