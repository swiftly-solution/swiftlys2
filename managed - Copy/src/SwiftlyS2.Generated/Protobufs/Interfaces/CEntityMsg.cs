
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CEntityMsg : ITypedProtobuf<CEntityMsg>
{
  static CEntityMsg ITypedProtobuf<CEntityMsg>.Wrap(nint handle, bool isManuallyAllocated) => new CEntityMsgImpl(handle, isManuallyAllocated);


  public uint TargetEntity { get; set; }

}
