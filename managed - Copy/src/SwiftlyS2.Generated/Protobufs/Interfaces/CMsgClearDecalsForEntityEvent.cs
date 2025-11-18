
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgClearDecalsForEntityEvent : ITypedProtobuf<CMsgClearDecalsForEntityEvent>, INetMessage<CMsgClearDecalsForEntityEvent>, IDisposable
{
  static int INetMessage<CMsgClearDecalsForEntityEvent>.MessageId => 204;
  
  static string INetMessage<CMsgClearDecalsForEntityEvent>.MessageName => "CMsgClearDecalsForEntityEvent";

  static CMsgClearDecalsForEntityEvent ITypedProtobuf<CMsgClearDecalsForEntityEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgClearDecalsForEntityEventImpl(handle, isManuallyAllocated);


  public uint Flagstoclear { get; set; }


  public uint Entityhandle { get; set; }

}
