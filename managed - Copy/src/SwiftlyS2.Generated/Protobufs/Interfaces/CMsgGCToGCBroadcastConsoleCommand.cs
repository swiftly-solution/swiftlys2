
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCToGCBroadcastConsoleCommand : ITypedProtobuf<CMsgGCToGCBroadcastConsoleCommand>
{
  static CMsgGCToGCBroadcastConsoleCommand ITypedProtobuf<CMsgGCToGCBroadcastConsoleCommand>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCToGCBroadcastConsoleCommandImpl(handle, isManuallyAllocated);


  public string ConCommand { get; set; }

}
