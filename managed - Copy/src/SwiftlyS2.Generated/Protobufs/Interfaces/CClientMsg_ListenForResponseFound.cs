
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CClientMsg_ListenForResponseFound : ITypedProtobuf<CClientMsg_ListenForResponseFound>
{
  static CClientMsg_ListenForResponseFound ITypedProtobuf<CClientMsg_ListenForResponseFound>.Wrap(nint handle, bool isManuallyAllocated) => new CClientMsg_ListenForResponseFoundImpl(handle, isManuallyAllocated);


  public int PlayerSlot { get; set; }

}
