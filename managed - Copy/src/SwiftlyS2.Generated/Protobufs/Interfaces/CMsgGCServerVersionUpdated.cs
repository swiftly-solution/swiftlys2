
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCServerVersionUpdated : ITypedProtobuf<CMsgGCServerVersionUpdated>
{
  static CMsgGCServerVersionUpdated ITypedProtobuf<CMsgGCServerVersionUpdated>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCServerVersionUpdatedImpl(handle, isManuallyAllocated);


  public uint ServerVersion { get; set; }

}
