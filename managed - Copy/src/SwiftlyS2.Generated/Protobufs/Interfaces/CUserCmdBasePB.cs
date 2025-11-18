
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserCmdBasePB : ITypedProtobuf<CUserCmdBasePB>
{
  static CUserCmdBasePB ITypedProtobuf<CUserCmdBasePB>.Wrap(nint handle, bool isManuallyAllocated) => new CUserCmdBasePBImpl(handle, isManuallyAllocated);


  public CBaseUserCmdPB Base { get; }

}
