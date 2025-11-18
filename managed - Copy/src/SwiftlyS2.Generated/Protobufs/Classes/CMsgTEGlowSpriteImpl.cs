
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEGlowSpriteImpl : NetMessage<CMsgTEGlowSprite>, CMsgTEGlowSprite
{
  public CMsgTEGlowSpriteImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public float Scale
  { get => Accessor.GetFloat("scale"); set => Accessor.SetFloat("scale", value); }


  public float Life
  { get => Accessor.GetFloat("life"); set => Accessor.SetFloat("life", value); }


  public uint Brightness
  { get => Accessor.GetUInt32("brightness"); set => Accessor.SetUInt32("brightness", value); }

}
