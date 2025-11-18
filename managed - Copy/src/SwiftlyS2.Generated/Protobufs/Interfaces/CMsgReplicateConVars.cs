
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgReplicateConVars : ITypedProtobuf<CMsgReplicateConVars>
{
  static CMsgReplicateConVars ITypedProtobuf<CMsgReplicateConVars>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgReplicateConVarsImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CMsgConVarValue> Convars { get; }

}
