
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSOIDOwner : ITypedProtobuf<CMsgSOIDOwner>
{
  static CMsgSOIDOwner ITypedProtobuf<CMsgSOIDOwner>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSOIDOwnerImpl(handle, isManuallyAllocated);


  public uint Type { get; set; }


  public ulong Id { get; set; }

}
