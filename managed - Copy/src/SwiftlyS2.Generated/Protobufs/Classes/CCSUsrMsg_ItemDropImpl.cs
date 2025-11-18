
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CCSUsrMsg_ItemDropImpl : NetMessage<CCSUsrMsg_ItemDrop>, CCSUsrMsg_ItemDrop
{
  public CCSUsrMsg_ItemDropImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public long Itemid
  { get => Accessor.GetInt64("itemid"); set => Accessor.SetInt64("itemid", value); }


  public bool Death
  { get => Accessor.GetBool("death"); set => Accessor.SetBool("death", value); }

}
