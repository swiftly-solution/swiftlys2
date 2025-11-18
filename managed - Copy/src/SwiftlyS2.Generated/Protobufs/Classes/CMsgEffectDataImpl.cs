
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgEffectDataImpl : TypedProtobuf<CMsgEffectData>, CMsgEffectData
{
  public CMsgEffectDataImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public Vector Start
  { get => Accessor.GetVector("start"); set => Accessor.SetVector("start", value); }


  public Vector Normal
  { get => Accessor.GetVector("normal"); set => Accessor.SetVector("normal", value); }


  public QAngle Angles
  { get => Accessor.GetQAngle("angles"); set => Accessor.SetQAngle("angles", value); }


  public uint Entity
  { get => Accessor.GetUInt32("entity"); set => Accessor.SetUInt32("entity", value); }


  public uint Otherentity
  { get => Accessor.GetUInt32("otherentity"); set => Accessor.SetUInt32("otherentity", value); }


  public float Scale
  { get => Accessor.GetFloat("scale"); set => Accessor.SetFloat("scale", value); }


  public float Magnitude
  { get => Accessor.GetFloat("magnitude"); set => Accessor.SetFloat("magnitude", value); }


  public float Radius
  { get => Accessor.GetFloat("radius"); set => Accessor.SetFloat("radius", value); }


  public uint Surfaceprop
  { get => Accessor.GetUInt32("surfaceprop"); set => Accessor.SetUInt32("surfaceprop", value); }


  public ulong Effectindex
  { get => Accessor.GetUInt64("effectindex"); set => Accessor.SetUInt64("effectindex", value); }


  public uint Damagetype
  { get => Accessor.GetUInt32("damagetype"); set => Accessor.SetUInt32("damagetype", value); }


  public uint Material
  { get => Accessor.GetUInt32("material"); set => Accessor.SetUInt32("material", value); }


  public uint Hitbox
  { get => Accessor.GetUInt32("hitbox"); set => Accessor.SetUInt32("hitbox", value); }


  public uint Color
  { get => Accessor.GetUInt32("color"); set => Accessor.SetUInt32("color", value); }


  public uint Flags
  { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }


  public int Attachmentindex
  { get => Accessor.GetInt32("attachmentindex"); set => Accessor.SetInt32("attachmentindex", value); }


  public uint Effectname
  { get => Accessor.GetUInt32("effectname"); set => Accessor.SetUInt32("effectname", value); }


  public uint Attachmentname
  { get => Accessor.GetUInt32("attachmentname"); set => Accessor.SetUInt32("attachmentname", value); }

}
