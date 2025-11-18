
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMessageCameraTransition_Transition_DataDrivenImpl : TypedProtobuf<CUserMessageCameraTransition_Transition_DataDriven>, CUserMessageCameraTransition_Transition_DataDriven
{
  public CUserMessageCameraTransition_Transition_DataDrivenImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public string Filename
  { get => Accessor.GetString("filename"); set => Accessor.SetString("filename", value); }


  public int AttachEntIndex
  { get => Accessor.GetInt32("attach_ent_index"); set => Accessor.SetInt32("attach_ent_index", value); }


  public float Duration
  { get => Accessor.GetFloat("duration"); set => Accessor.SetFloat("duration", value); }

}
