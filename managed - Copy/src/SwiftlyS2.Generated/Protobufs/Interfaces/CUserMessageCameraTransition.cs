
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;
using SwiftlyS2.Shared.NetMessages;

public interface CUserMessageCameraTransition : ITypedProtobuf<CUserMessageCameraTransition>, INetMessage<CUserMessageCameraTransition>, IDisposable
{
  static int INetMessage<CUserMessageCameraTransition>.MessageId => 143;
  
  static string INetMessage<CUserMessageCameraTransition>.MessageName => "CUserMessageCameraTransition";

  static CUserMessageCameraTransition ITypedProtobuf<CUserMessageCameraTransition>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMessageCameraTransitionImpl(handle, isManuallyAllocated);


  public uint CameraType { get; set; }


  public float Duration { get; set; }


  public CUserMessageCameraTransition_Transition_DataDriven ParamsDataDriven { get; }

}
