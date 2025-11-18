
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTransformImpl : TypedProtobuf<CMsgTransform>, CMsgTransform
{
  public CMsgTransformImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public Vector Position
  { get => Accessor.GetVector("position"); set => Accessor.SetVector("position", value); }


  public float Scale
  { get => Accessor.GetFloat("scale"); set => Accessor.SetFloat("scale", value); }


  public CMsgQuaternion Orientation
  { get => new CMsgQuaternionImpl(NativeNetMessages.GetNestedMessage(Address, "orientation"), false); }

}
