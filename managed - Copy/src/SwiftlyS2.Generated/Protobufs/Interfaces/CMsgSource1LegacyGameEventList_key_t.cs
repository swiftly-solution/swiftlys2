
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgSource1LegacyGameEventList_key_t : ITypedProtobuf<CMsgSource1LegacyGameEventList_key_t>
{
  static CMsgSource1LegacyGameEventList_key_t ITypedProtobuf<CMsgSource1LegacyGameEventList_key_t>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgSource1LegacyGameEventList_key_tImpl(handle, isManuallyAllocated);


  public int Type { get; set; }


  public string Name { get; set; }

}
