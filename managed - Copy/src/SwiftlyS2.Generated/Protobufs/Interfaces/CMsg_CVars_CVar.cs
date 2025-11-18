
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsg_CVars_CVar : ITypedProtobuf<CMsg_CVars_CVar>
{
  static CMsg_CVars_CVar ITypedProtobuf<CMsg_CVars_CVar>.Wrap(nint handle, bool isManuallyAllocated) => new CMsg_CVars_CVarImpl(handle, isManuallyAllocated);


  public string Name { get; set; }


  public string Value { get; set; }

}
