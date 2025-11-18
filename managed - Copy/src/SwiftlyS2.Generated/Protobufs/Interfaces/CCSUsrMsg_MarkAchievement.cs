
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CCSUsrMsg_MarkAchievement : ITypedProtobuf<CCSUsrMsg_MarkAchievement>, INetMessage<CCSUsrMsg_MarkAchievement>, IDisposable
{
  static int INetMessage<CCSUsrMsg_MarkAchievement>.MessageId => 357;
  
  static string INetMessage<CCSUsrMsg_MarkAchievement>.MessageName => "CCSUsrMsg_MarkAchievement";

  static CCSUsrMsg_MarkAchievement ITypedProtobuf<CCSUsrMsg_MarkAchievement>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_MarkAchievementImpl(handle, isManuallyAllocated);


  public string Achievement { get; set; }

}
