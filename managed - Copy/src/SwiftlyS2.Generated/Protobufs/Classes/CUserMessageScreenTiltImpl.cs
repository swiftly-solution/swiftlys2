
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageScreenTiltImpl : NetMessage<CUserMessageScreenTilt>, CUserMessageScreenTilt
{
  public CUserMessageScreenTiltImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint Command
  { get => Accessor.GetUInt32("command"); set => Accessor.SetUInt32("command", value); }


  public bool EaseInOut
  { get => Accessor.GetBool("ease_in_out"); set => Accessor.SetBool("ease_in_out", value); }


  public Vector Angle
  { get => Accessor.GetVector("angle"); set => Accessor.SetVector("angle", value); }


  public float Duration
  { get => Accessor.GetFloat("duration"); set => Accessor.SetFloat("duration", value); }


  public float Time
  { get => Accessor.GetFloat("time"); set => Accessor.SetFloat("time", value); }

}
