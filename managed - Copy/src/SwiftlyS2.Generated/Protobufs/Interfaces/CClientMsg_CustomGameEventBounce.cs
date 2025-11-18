
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CClientMsg_CustomGameEventBounce : ITypedProtobuf<CClientMsg_CustomGameEventBounce>
{
  static CClientMsg_CustomGameEventBounce ITypedProtobuf<CClientMsg_CustomGameEventBounce>.Wrap(nint handle, bool isManuallyAllocated) => new CClientMsg_CustomGameEventBounceImpl(handle, isManuallyAllocated);


  public string EventName { get; set; }


  public byte[] Data { get; set; }


  public int PlayerSlot { get; set; }

}
