
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CEntityMessageScreenOverlayImpl : TypedProtobuf<CEntityMessageScreenOverlay>, CEntityMessageScreenOverlay
{
  public CEntityMessageScreenOverlayImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool StartEffect
  { get => Accessor.GetBool("start_effect"); set => Accessor.SetBool("start_effect", value); }


  public CEntityMsg EntityMsg
  { get => new CEntityMsgImpl(NativeNetMessages.GetNestedMessage(Address, "entity_msg"), false); }

}
