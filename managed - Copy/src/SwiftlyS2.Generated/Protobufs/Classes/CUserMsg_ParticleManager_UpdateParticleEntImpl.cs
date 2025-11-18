
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManager_UpdateParticleEntImpl : TypedProtobuf<CUserMsg_ParticleManager_UpdateParticleEnt>, CUserMsg_ParticleManager_UpdateParticleEnt
{
  public CUserMsg_ParticleManager_UpdateParticleEntImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public int ControlPoint
  { get => Accessor.GetInt32("control_point"); set => Accessor.SetInt32("control_point", value); }


  public uint EntityHandle
  { get => Accessor.GetUInt32("entity_handle"); set => Accessor.SetUInt32("entity_handle", value); }


  public int AttachType
  { get => Accessor.GetInt32("attach_type"); set => Accessor.SetInt32("attach_type", value); }


  public int Attachment
  { get => Accessor.GetInt32("attachment"); set => Accessor.SetInt32("attachment", value); }


  public Vector FallbackPosition
  { get => Accessor.GetVector("fallback_position"); set => Accessor.SetVector("fallback_position", value); }


  public bool IncludeWearables
  { get => Accessor.GetBool("include_wearables"); set => Accessor.SetBool("include_wearables", value); }


  public Vector OffsetPosition
  { get => Accessor.GetVector("offset_position"); set => Accessor.SetVector("offset_position", value); }


  public QAngle OffsetAngles
  { get => Accessor.GetQAngle("offset_angles"); set => Accessor.SetQAngle("offset_angles", value); }

}
