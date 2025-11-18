
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSystemBroadcast : ITypedProtobuf<CMsgSystemBroadcast>
{
  static CMsgSystemBroadcast ITypedProtobuf<CMsgSystemBroadcast>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSystemBroadcastImpl(handle, isManuallyAllocated);


  public string Message { get; set; }

}
