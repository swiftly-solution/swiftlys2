
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgConVarValue : ITypedProtobuf<CMsgConVarValue>
{
  static CMsgConVarValue ITypedProtobuf<CMsgConVarValue>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgConVarValueImpl(handle, isManuallyAllocated);


  public string Name { get; set; }


  public string Value { get; set; }

}
