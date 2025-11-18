
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEBaseBeamImpl : TypedProtobuf<CMsgTEBaseBeam>, CMsgTEBaseBeam
{
  public CMsgTEBaseBeamImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Modelindex
  { get => Accessor.GetUInt64("modelindex"); set => Accessor.SetUInt64("modelindex", value); }


  public ulong Haloindex
  { get => Accessor.GetUInt64("haloindex"); set => Accessor.SetUInt64("haloindex", value); }


  public uint Startframe
  { get => Accessor.GetUInt32("startframe"); set => Accessor.SetUInt32("startframe", value); }


  public uint Framerate
  { get => Accessor.GetUInt32("framerate"); set => Accessor.SetUInt32("framerate", value); }


  public float Life
  { get => Accessor.GetFloat("life"); set => Accessor.SetFloat("life", value); }


  public float Width
  { get => Accessor.GetFloat("width"); set => Accessor.SetFloat("width", value); }


  public float Endwidth
  { get => Accessor.GetFloat("endwidth"); set => Accessor.SetFloat("endwidth", value); }


  public uint Fadelength
  { get => Accessor.GetUInt32("fadelength"); set => Accessor.SetUInt32("fadelength", value); }


  public float Amplitude
  { get => Accessor.GetFloat("amplitude"); set => Accessor.SetFloat("amplitude", value); }


  public uint Color
  { get => Accessor.GetUInt32("color"); set => Accessor.SetUInt32("color", value); }


  public uint Speed
  { get => Accessor.GetUInt32("speed"); set => Accessor.SetUInt32("speed", value); }


  public uint Flags
  { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }

}
