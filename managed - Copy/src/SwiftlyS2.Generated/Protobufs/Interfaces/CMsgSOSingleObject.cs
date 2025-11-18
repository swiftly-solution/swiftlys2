
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSOSingleObject : ITypedProtobuf<CMsgSOSingleObject>
{
  static CMsgSOSingleObject ITypedProtobuf<CMsgSOSingleObject>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSOSingleObjectImpl(handle, isManuallyAllocated);


  public int TypeId { get; set; }


  public byte[] ObjectData { get; set; }


  public ulong Version { get; set; }


  public CMsgSOIDOwner OwnerSoid { get; }

}
