
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CEntityMessagePropagateForceImpl : TypedProtobuf<CEntityMessagePropagateForce>, CEntityMessagePropagateForce
{
  public CEntityMessagePropagateForceImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public Vector Impulse
  { get => Accessor.GetVector("impulse"); set => Accessor.SetVector("impulse", value); }


  public CEntityMsg EntityMsg
  { get => new CEntityMsgImpl(NativeNetMessages.GetNestedMessage(Address, "entity_msg"), false); }

}
