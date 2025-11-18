
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CEntityMessageFixAngleImpl : TypedProtobuf<CEntityMessageFixAngle>, CEntityMessageFixAngle
{
  public CEntityMessageFixAngleImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public bool Relative
  { get => Accessor.GetBool("relative"); set => Accessor.SetBool("relative", value); }


  public QAngle Angle
  { get => Accessor.GetQAngle("angle"); set => Accessor.SetQAngle("angle", value); }


  public CEntityMsg EntityMsg
  { get => new CEntityMsgImpl(NativeNetMessages.GetNestedMessage(Address, "entity_msg"), false); }

}
