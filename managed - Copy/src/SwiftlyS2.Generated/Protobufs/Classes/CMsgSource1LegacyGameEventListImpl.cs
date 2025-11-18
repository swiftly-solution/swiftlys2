
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgSource1LegacyGameEventListImpl : NetMessage<CMsgSource1LegacyGameEventList>, CMsgSource1LegacyGameEventList
{
  public CMsgSource1LegacyGameEventListImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CMsgSource1LegacyGameEventList_descriptor_t> Descriptors
  { get => new ProtobufRepeatedFieldSubMessageType<CMsgSource1LegacyGameEventList_descriptor_t>(Accessor, "descriptors"); }

}
