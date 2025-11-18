
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSOMultipleObjects : ITypedProtobuf<CMsgSOMultipleObjects>
{
  static CMsgSOMultipleObjects ITypedProtobuf<CMsgSOMultipleObjects>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSOMultipleObjectsImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CMsgSOMultipleObjects_SingleObject> ObjectsModified { get; }


  public ulong Version { get; set; }


  public CMsgSOIDOwner OwnerSoid { get; }

}
