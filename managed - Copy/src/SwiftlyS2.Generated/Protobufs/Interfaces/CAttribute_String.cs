
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CAttribute_String : ITypedProtobuf<CAttribute_String>
{
  static CAttribute_String ITypedProtobuf<CAttribute_String>.Wrap(nint handle, bool isManuallyAllocated) => new CAttribute_StringImpl(handle, isManuallyAllocated);


  public string Value { get; set; }

}
