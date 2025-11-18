
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_SendPlayerItemFoundImpl : NetMessage<CCSUsrMsg_SendPlayerItemFound>, CCSUsrMsg_SendPlayerItemFound
{
  public CCSUsrMsg_SendPlayerItemFoundImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public CEconItemPreviewDataBlock Iteminfo
  { get => new CEconItemPreviewDataBlockImpl(NativeNetMessages.GetNestedMessage(Address, "iteminfo"), false); }


  public int Playerslot
  { get => Accessor.GetInt32("playerslot"); set => Accessor.SetInt32("playerslot", value); }

}
