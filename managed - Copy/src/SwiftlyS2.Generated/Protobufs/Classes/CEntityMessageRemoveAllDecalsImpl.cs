
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CEntityMessageRemoveAllDecalsImpl : TypedProtobuf<CEntityMessageRemoveAllDecals>, CEntityMessageRemoveAllDecals
{
  public CEntityMessageRemoveAllDecalsImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool RemoveDecals
  { get => Accessor.GetBool("remove_decals"); set => Accessor.SetBool("remove_decals", value); }


  public CEntityMsg EntityMsg
  { get => new CEntityMsgImpl(NativeNetMessages.GetNestedMessage(Address, "entity_msg"), false); }

}
