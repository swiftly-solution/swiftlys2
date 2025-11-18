
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgReplicateConVarsImpl : TypedProtobuf<CMsgReplicateConVars>, CMsgReplicateConVars
{
  public CMsgReplicateConVarsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsgConVarValue> Convars
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgConVarValue>(Accessor, "convars"); }

}
