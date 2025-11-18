
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSOMultipleObjects_SingleObject : ITypedProtobuf<CMsgSOMultipleObjects_SingleObject>
{
  static CMsgSOMultipleObjects_SingleObject ITypedProtobuf<CMsgSOMultipleObjects_SingleObject>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSOMultipleObjects_SingleObjectImpl(handle, isManuallyAllocated);


  public int TypeId { get; set; }


  public byte[] ObjectData { get; set; }

}
