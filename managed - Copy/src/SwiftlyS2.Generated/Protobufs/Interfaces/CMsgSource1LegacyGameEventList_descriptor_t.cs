
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSource1LegacyGameEventList_descriptor_t : ITypedProtobuf<CMsgSource1LegacyGameEventList_descriptor_t>
{
  static CMsgSource1LegacyGameEventList_descriptor_t ITypedProtobuf<CMsgSource1LegacyGameEventList_descriptor_t>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSource1LegacyGameEventList_descriptor_tImpl(handle, isManuallyAllocated);


  public int Eventid { get; set; }


  public string Name { get; set; }


  public IProtobufRepeatedFieldSubMessageType<CMsgSource1LegacyGameEventList_key_t> Keys { get; }

}
