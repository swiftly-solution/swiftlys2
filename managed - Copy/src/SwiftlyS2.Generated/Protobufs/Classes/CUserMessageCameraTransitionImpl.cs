
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageCameraTransitionImpl : NetMessage<CUserMessageCameraTransition>, CUserMessageCameraTransition
{
  public CUserMessageCameraTransitionImpl(nint handle, bool isManuallyAllocated): base(handle, isManuallyAllocated)
  {
  }


  public uint CameraType
  { get => Accessor.GetUInt32("camera_type"); set => Accessor.SetUInt32("camera_type", value); }


  public float Duration
  { get => Accessor.GetFloat("duration"); set => Accessor.SetFloat("duration", value); }


  public CUserMessageCameraTransition_Transition_DataDriven ParamsDataDriven
  { get => new CUserMessageCameraTransition_Transition_DataDrivenImpl(NativeNetMessages.GetNestedMessage(Address, "params_data_driven"), false); }

}
