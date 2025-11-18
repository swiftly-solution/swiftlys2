
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEMuzzleFlashImpl : NetMessage<CMsgTEMuzzleFlash>, CMsgTEMuzzleFlash
{
  public CMsgTEMuzzleFlashImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public QAngle Angles
  { get => Accessor.GetQAngle("angles"); set => Accessor.SetQAngle("angles", value); }


  public float Scale
  { get => Accessor.GetFloat("scale"); set => Accessor.SetFloat("scale", value); }


  public uint Type
  { get => Accessor.GetUInt32("type"); set => Accessor.SetUInt32("type", value); }

}
