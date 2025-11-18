
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSOQuestProgress : ITypedProtobuf<CSOQuestProgress>
{
  static CSOQuestProgress ITypedProtobuf<CSOQuestProgress>.Wrap(nint handle, bool isManuallyAllocated) => new CSOQuestProgressImpl(handle, isManuallyAllocated);


  public uint Questid { get; set; }


  public uint PointsRemaining { get; set; }


  public uint BonusPoints { get; set; }

}
