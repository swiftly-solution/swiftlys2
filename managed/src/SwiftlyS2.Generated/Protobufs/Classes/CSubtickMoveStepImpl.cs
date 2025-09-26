
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CSubtickMoveStepImpl : TypedProtobuf<CSubtickMoveStep>, CSubtickMoveStep
{
  public CSubtickMoveStepImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public ulong Button
  { get => Accessor.GetUInt64("button"); set => Accessor.SetUInt64("button", value); }


  public bool Pressed
  { get => Accessor.GetBool("pressed"); set => Accessor.SetBool("pressed", value); }


  public float When
  { get => Accessor.GetFloat("when"); set => Accessor.SetFloat("when", value); }


  public float AnalogForwardDelta
  { get => Accessor.GetFloat("analog_forward_delta"); set => Accessor.SetFloat("analog_forward_delta", value); }


  public float AnalogLeftDelta
  { get => Accessor.GetFloat("analog_left_delta"); set => Accessor.SetFloat("analog_left_delta", value); }


  public float PitchDelta
  { get => Accessor.GetFloat("pitch_delta"); set => Accessor.SetFloat("pitch_delta", value); }


  public float YawDelta
  { get => Accessor.GetFloat("yaw_delta"); set => Accessor.SetFloat("yaw_delta", value); }

}
