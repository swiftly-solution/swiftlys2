
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CBidirMsg_RebroadcastSource : ITypedProtobuf<CBidirMsg_RebroadcastSource>
{
  static CBidirMsg_RebroadcastSource ITypedProtobuf<CBidirMsg_RebroadcastSource>.Wrap(nint handle, bool isManuallyAllocated) => new CBidirMsg_RebroadcastSourceImpl(handle, isManuallyAllocated);


  public int Eventsource { get; set; }

}
