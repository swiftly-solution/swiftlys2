
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CClientMsg_CustomGameEvent : ITypedProtobuf<CClientMsg_CustomGameEvent>
{
  static CClientMsg_CustomGameEvent ITypedProtobuf<CClientMsg_CustomGameEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CClientMsg_CustomGameEventImpl(handle, isManuallyAllocated);


  public string EventName { get; set; }


  public byte[] Data { get; set; }

}
