
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CMsgTEPhysicsPropImpl : NetMessage<CMsgTEPhysicsProp>, CMsgTEPhysicsProp
{
  public CMsgTEPhysicsPropImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public Vector Origin
  { get => Accessor.GetVector("origin"); set => Accessor.SetVector("origin", value); }


  public Vector Velocity
  { get => Accessor.GetVector("velocity"); set => Accessor.SetVector("velocity", value); }


  public QAngle Angles
  { get => Accessor.GetQAngle("angles"); set => Accessor.SetQAngle("angles", value); }


  public uint Skin
  { get => Accessor.GetUInt32("skin"); set => Accessor.SetUInt32("skin", value); }


  public uint Flags
  { get => Accessor.GetUInt32("flags"); set => Accessor.SetUInt32("flags", value); }


  public uint Effects
  { get => Accessor.GetUInt32("effects"); set => Accessor.SetUInt32("effects", value); }


  public uint Color
  { get => Accessor.GetUInt32("color"); set => Accessor.SetUInt32("color", value); }


  public ulong Modelindex
  { get => Accessor.GetUInt64("modelindex"); set => Accessor.SetUInt64("modelindex", value); }


  public uint UnusedBreakmodelsnottomake
  { get => Accessor.GetUInt32("unused_breakmodelsnottomake"); set => Accessor.SetUInt32("unused_breakmodelsnottomake", value); }


  public float Scale
  { get => Accessor.GetFloat("scale"); set => Accessor.SetFloat("scale", value); }


  public Vector Dmgpos
  { get => Accessor.GetVector("dmgpos"); set => Accessor.SetVector("dmgpos", value); }


  public Vector Dmgdir
  { get => Accessor.GetVector("dmgdir"); set => Accessor.SetVector("dmgdir", value); }


  public int Dmgtype
  { get => Accessor.GetInt32("dmgtype"); set => Accessor.SetInt32("dmgtype", value); }

}
