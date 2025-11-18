
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CDemoCustomData : ITypedProtobuf<CDemoCustomData>
{
  static CDemoCustomData ITypedProtobuf<CDemoCustomData>.Wrap(nint handle, bool isManuallyAllocated) => new CDemoCustomDataImpl(handle, isManuallyAllocated);


  public int CallbackIndex { get; set; }


  public byte[] Data { get; set; }

}
