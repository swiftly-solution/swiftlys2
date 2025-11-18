
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEShatterSurfaceImpl : NetMessage<CMsgTEShatterSurface>, CMsgTEShatterSurface
{
  public CMsgTEShatterSurfaceImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public QAngle Angles
  { get => Accessor.GetQAngle("angles"); set => Accessor.SetQAngle("angles", value); }


  public Vector Force
  { get => Accessor.GetVector("force"); set => Accessor.SetVector("force", value); }


  public Vector Forcepos
  { get => Accessor.GetVector("forcepos"); set => Accessor.SetVector("forcepos", value); }


  public float Width
  { get => Accessor.GetFloat("width"); set => Accessor.SetFloat("width", value); }


  public float Height
  { get => Accessor.GetFloat("height"); set => Accessor.SetFloat("height", value); }


  public float Shardsize
  { get => Accessor.GetFloat("shardsize"); set => Accessor.SetFloat("shardsize", value); }


  public uint Surfacetype
  { get => Accessor.GetUInt32("surfacetype"); set => Accessor.SetUInt32("surfacetype", value); }


  public uint Frontcolor
  { get => Accessor.GetUInt32("frontcolor"); set => Accessor.SetUInt32("frontcolor", value); }


  public uint Backcolor
  { get => Accessor.GetUInt32("backcolor"); set => Accessor.SetUInt32("backcolor", value); }

}
