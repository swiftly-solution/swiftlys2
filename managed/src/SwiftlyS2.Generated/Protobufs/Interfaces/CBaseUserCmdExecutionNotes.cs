
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CBaseUserCmdExecutionNotes : ITypedProtobuf<CBaseUserCmdExecutionNotes>
{
  static CBaseUserCmdExecutionNotes ITypedProtobuf<CBaseUserCmdExecutionNotes>.Wrap(nint handle, bool isManuallyAllocated) => new CBaseUserCmdExecutionNotesImpl(handle, isManuallyAllocated);


  public string IgnoredReason { get; set; }

}
