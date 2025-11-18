
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CCSUsrMsg_SendPlayerLoadout_LoadoutItem : ITypedProtobuf<CCSUsrMsg_SendPlayerLoadout_LoadoutItem>
{
  static CCSUsrMsg_SendPlayerLoadout_LoadoutItem ITypedProtobuf<CCSUsrMsg_SendPlayerLoadout_LoadoutItem>.Wrap(nint handle, bool isManuallyAllocated) => new CCSUsrMsg_SendPlayerLoadout_LoadoutItemImpl(handle, isManuallyAllocated);


  public CEconItemPreviewDataBlock EconItem { get; }


  public int Team { get; set; }


  public int Slot { get; set; }

}
