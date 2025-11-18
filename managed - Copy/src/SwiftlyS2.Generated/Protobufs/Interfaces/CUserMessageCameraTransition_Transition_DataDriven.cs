
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMessageCameraTransition_Transition_DataDriven : ITypedProtobuf<CUserMessageCameraTransition_Transition_DataDriven>
{
  static CUserMessageCameraTransition_Transition_DataDriven ITypedProtobuf<CUserMessageCameraTransition_Transition_DataDriven>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageCameraTransition_Transition_DataDrivenImpl(handle, isManuallyAllocated);


  public string Filename { get; set; }


  public int AttachEntIndex { get; set; }


  public float Duration { get; set; }

}
