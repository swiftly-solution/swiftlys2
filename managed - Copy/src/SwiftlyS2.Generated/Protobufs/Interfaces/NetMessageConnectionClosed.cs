
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface NetMessageConnectionClosed : ITypedProtobuf<NetMessageConnectionClosed>
{
  static NetMessageConnectionClosed ITypedProtobuf<NetMessageConnectionClosed>.Wrap(nint handle, bool isManuallyAllocated) => new NetMessageConnectionClosedImpl(handle, isManuallyAllocated);


  public uint Reason { get; set; }


  public string Message { get; set; }

}
