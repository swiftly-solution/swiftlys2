
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_CustomGameEvent : ITypedProtobuf<CUserMsg_CustomGameEvent>
{
  static CUserMsg_CustomGameEvent ITypedProtobuf<CUserMsg_CustomGameEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_CustomGameEventImpl(handle, isManuallyAllocated);


  public string EventName { get; set; }


  public byte[] Data { get; set; }

}
