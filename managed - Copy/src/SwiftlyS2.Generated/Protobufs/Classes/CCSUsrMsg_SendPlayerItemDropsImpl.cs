
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_SendPlayerItemDropsImpl : NetMessage<CCSUsrMsg_SendPlayerItemDrops>, CCSUsrMsg_SendPlayerItemDrops
{
  public CCSUsrMsg_SendPlayerItemDropsImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public IProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock> EntityUpdates
  { get => new ProtobufRepeatedFieldSubMessageType<CEconItemPreviewDataBlock>(Accessor, "entity_updates"); }

}
