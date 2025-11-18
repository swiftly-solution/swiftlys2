
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgClearWorldDecalsEvent : ITypedProtobuf<CMsgClearWorldDecalsEvent>, INetMessage<CMsgClearWorldDecalsEvent>, IDisposable
{
  static int INetMessage<CMsgClearWorldDecalsEvent>.MessageId => 202;
  
  static string INetMessage<CMsgClearWorldDecalsEvent>.MessageName => "CMsgClearWorldDecalsEvent";

  static CMsgClearWorldDecalsEvent ITypedProtobuf<CMsgClearWorldDecalsEvent>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgClearWorldDecalsEventImpl(handle, isManuallyAllocated);


  public uint Flagstoclear { get; set; }

}
