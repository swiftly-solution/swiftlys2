
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CMsgSource1LegacyGameEventList : ITypedProtobuf<CMsgSource1LegacyGameEventList>, INetMessage<CMsgSource1LegacyGameEventList>, IDisposable
{
  static int INetMessage<CMsgSource1LegacyGameEventList>.MessageId => 207;
  
  static string INetMessage<CMsgSource1LegacyGameEventList>.MessageName => "CMsgSource1LegacyGameEventList";

  static CMsgSource1LegacyGameEventList ITypedProtobuf<CMsgSource1LegacyGameEventList>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSource1LegacyGameEventListImpl(handle, isManuallyAllocated);


  public IProtobufRepeatedFieldSubMessageType<CMsgSource1LegacyGameEventList_descriptor_t> Descriptors { get; }

}
