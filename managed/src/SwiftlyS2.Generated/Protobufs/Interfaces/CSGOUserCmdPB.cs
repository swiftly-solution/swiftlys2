
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CSGOUserCmdPB : ITypedProtobuf<CSGOUserCmdPB>
{
  static CSGOUserCmdPB ITypedProtobuf<CSGOUserCmdPB>.Wrap(nint handle, bool isManuallyAllocated) => new CSGOUserCmdPBImpl(handle, isManuallyAllocated);


  public CBaseUserCmdPB Base { get; }


  public IProtobufRepeatedFieldSubMessageType<CSGOInputHistoryEntryPB> InputHistory { get; }


  public int Attack1StartHistoryIndex { get; set; }


  public int Attack2StartHistoryIndex { get; set; }


  public bool LeftHandDesired { get; set; }


  public bool IsPredictingBodyShotFx { get; set; }


  public bool IsPredictingHeadShotFx { get; set; }


  public bool IsPredictingKillRagdolls { get; set; }

}
