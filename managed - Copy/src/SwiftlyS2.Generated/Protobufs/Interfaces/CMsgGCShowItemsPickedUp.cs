
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CMsgGCShowItemsPickedUp : ITypedProtobuf<CMsgGCShowItemsPickedUp>
{
  static CMsgGCShowItemsPickedUp ITypedProtobuf<CMsgGCShowItemsPickedUp>.Wrap(nint handle, bool isManuallyAllocated) => new CMsgGCShowItemsPickedUpImpl(handle, isManuallyAllocated);


  public ulong PlayerSteamid { get; set; }

}
