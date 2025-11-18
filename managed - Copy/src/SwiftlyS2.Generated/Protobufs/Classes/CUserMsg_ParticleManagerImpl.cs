
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;
using SwiftlyS2.Shared.ProtobufDefinitions;

namespace SwiftlyS2.Core.ProtobufDefinitions;

internal class CUserMsg_ParticleManagerImpl : TypedProtobuf<CUserMsg_ParticleManager>, CUserMsg_ParticleManager
{
  public CUserMsg_ParticleManagerImpl(nint handle, bool isManuallyAllocated): base(handle)
  {
  }


  public PARTICLE_MESSAGE Type
  { get => (PARTICLE_MESSAGE)Accessor.GetInt32("type"); set => Accessor.SetInt32("type", (int)value); }


  public uint Index
  { get => Accessor.GetUInt32("index"); set => Accessor.SetUInt32("index", value); }


  public CUserMsg_ParticleManager_ReleaseParticleIndex ReleaseParticleIndex
  { get => new CUserMsg_ParticleManager_ReleaseParticleIndexImpl(NativeNetMessages.GetNestedMessage(Address, "release_particle_index"), false); }


  public CUserMsg_ParticleManager_CreateParticle CreateParticle
  { get => new CUserMsg_ParticleManager_CreateParticleImpl(NativeNetMessages.GetNestedMessage(Address, "create_particle"), false); }


  public CUserMsg_ParticleManager_DestroyParticle DestroyParticle
  { get => new CUserMsg_ParticleManager_DestroyParticleImpl(NativeNetMessages.GetNestedMessage(Address, "destroy_particle"), false); }


  public CUserMsg_ParticleManager_DestroyParticleInvolving DestroyParticleInvolving
  { get => new CUserMsg_ParticleManager_DestroyParticleInvolvingImpl(NativeNetMessages.GetNestedMessage(Address, "destroy_particle_involving"), false); }


  public CUserMsg_ParticleManager_UpdateParticle_OBSOLETE UpdateParticle
  { get => new CUserMsg_ParticleManager_UpdateParticle_OBSOLETEImpl(NativeNetMessages.GetNestedMessage(Address, "update_particle"), false); }


  public CUserMsg_ParticleManager_UpdateParticleFwd_OBSOLETE UpdateParticleFwd
  { get => new CUserMsg_ParticleManager_UpdateParticleFwd_OBSOLETEImpl(NativeNetMessages.GetNestedMessage(Address, "update_particle_fwd"), false); }


  public CUserMsg_ParticleManager_UpdateParticleOrient_OBSOLETE UpdateParticleOrient
  { get => new CUserMsg_ParticleManager_UpdateParticleOrient_OBSOLETEImpl(NativeNetMessages.GetNestedMessage(Address, "update_particle_orient"), false); }


  public CUserMsg_ParticleManager_UpdateParticleFallback UpdateParticleFallback
  { get => new CUserMsg_ParticleManager_UpdateParticleFallbackImpl(NativeNetMessages.GetNestedMessage(Address, "update_particle_fallback"), false); }


  public CUserMsg_ParticleManager_UpdateParticleOffset UpdateParticleOffset
  { get => new CUserMsg_ParticleManager_UpdateParticleOffsetImpl(NativeNetMessages.GetNestedMessage(Address, "update_particle_offset"), false); }


  public CUserMsg_ParticleManager_UpdateParticleEnt UpdateParticleEnt
  { get => new CUserMsg_ParticleManager_UpdateParticleEntImpl(NativeNetMessages.GetNestedMessage(Address, "update_particle_ent"), false); }


  public CUserMsg_ParticleManager_UpdateParticleShouldDraw UpdateParticleShouldDraw
  { get => new CUserMsg_ParticleManager_UpdateParticleShouldDrawImpl(NativeNetMessages.GetNestedMessage(Address, "update_particle_should_draw"), false); }


  public CUserMsg_ParticleManager_UpdateParticleSetFrozen UpdateParticleSetFrozen
  { get => new CUserMsg_ParticleManager_UpdateParticleSetFrozenImpl(NativeNetMessages.GetNestedMessage(Address, "update_particle_set_frozen"), false); }


  public CUserMsg_ParticleManager_ChangeControlPointAttachment ChangeControlPointAttachment
  { get => new CUserMsg_ParticleManager_ChangeControlPointAttachmentImpl(NativeNetMessages.GetNestedMessage(Address, "change_control_point_attachment"), false); }


  public CUserMsg_ParticleManager_UpdateEntityPosition UpdateEntityPosition
  { get => new CUserMsg_ParticleManager_UpdateEntityPositionImpl(NativeNetMessages.GetNestedMessage(Address, "update_entity_position"), false); }


  public CUserMsg_ParticleManager_SetParticleFoWProperties SetParticleFowProperties
  { get => new CUserMsg_ParticleManager_SetParticleFoWPropertiesImpl(NativeNetMessages.GetNestedMessage(Address, "set_particle_fow_properties"), false); }


  public CUserMsg_ParticleManager_SetParticleText SetParticleText
  { get => new CUserMsg_ParticleManager_SetParticleTextImpl(NativeNetMessages.GetNestedMessage(Address, "set_particle_text"), false); }


  public CUserMsg_ParticleManager_SetParticleShouldCheckFoW SetParticleShouldCheckFow
  { get => new CUserMsg_ParticleManager_SetParticleShouldCheckFoWImpl(NativeNetMessages.GetNestedMessage(Address, "set_particle_should_check_fow"), false); }


  public CUserMsg_ParticleManager_SetControlPointModel SetControlPointModel
  { get => new CUserMsg_ParticleManager_SetControlPointModelImpl(NativeNetMessages.GetNestedMessage(Address, "set_control_point_model"), false); }


  public CUserMsg_ParticleManager_SetControlPointSnapshot SetControlPointSnapshot
  { get => new CUserMsg_ParticleManager_SetControlPointSnapshotImpl(NativeNetMessages.GetNestedMessage(Address, "set_control_point_snapshot"), false); }


  public CUserMsg_ParticleManager_SetTextureAttribute SetTextureAttribute
  { get => new CUserMsg_ParticleManager_SetTextureAttributeImpl(NativeNetMessages.GetNestedMessage(Address, "set_texture_attribute"), false); }


  public CUserMsg_ParticleManager_SetSceneObjectGenericFlag SetSceneObjectGenericFlag
  { get => new CUserMsg_ParticleManager_SetSceneObjectGenericFlagImpl(NativeNetMessages.GetNestedMessage(Address, "set_scene_object_generic_flag"), false); }


  public CUserMsg_ParticleManager_SetSceneObjectTintAndDesat SetSceneObjectTintAndDesat
  { get => new CUserMsg_ParticleManager_SetSceneObjectTintAndDesatImpl(NativeNetMessages.GetNestedMessage(Address, "set_scene_object_tint_and_desat"), false); }


  public CUserMsg_ParticleManager_DestroyParticleNamed DestroyParticleNamed
  { get => new CUserMsg_ParticleManager_DestroyParticleNamedImpl(NativeNetMessages.GetNestedMessage(Address, "destroy_particle_named"), false); }


  public CUserMsg_ParticleManager_ParticleSkipToTime ParticleSkipToTime
  { get => new CUserMsg_ParticleManager_ParticleSkipToTimeImpl(NativeNetMessages.GetNestedMessage(Address, "particle_skip_to_time"), false); }


  public CUserMsg_ParticleManager_ParticleCanFreeze ParticleCanFreeze
  { get => new CUserMsg_ParticleManager_ParticleCanFreezeImpl(NativeNetMessages.GetNestedMessage(Address, "particle_can_freeze"), false); }


  public CUserMsg_ParticleManager_SetParticleNamedValueContext SetNamedValueContext
  { get => new CUserMsg_ParticleManager_SetParticleNamedValueContextImpl(NativeNetMessages.GetNestedMessage(Address, "set_named_value_context"), false); }


  public CUserMsg_ParticleManager_UpdateParticleTransform UpdateParticleTransform
  { get => new CUserMsg_ParticleManager_UpdateParticleTransformImpl(NativeNetMessages.GetNestedMessage(Address, "update_particle_transform"), false); }


  public CUserMsg_ParticleManager_ParticleFreezeTransitionOverride ParticleFreezeTransitionOverride
  { get => new CUserMsg_ParticleManager_ParticleFreezeTransitionOverrideImpl(NativeNetMessages.GetNestedMessage(Address, "particle_freeze_transition_override"), false); }


  public CUserMsg_ParticleManager_FreezeParticleInvolving FreezeParticleInvolving
  { get => new CUserMsg_ParticleManager_FreezeParticleInvolvingImpl(NativeNetMessages.GetNestedMessage(Address, "freeze_particle_involving"), false); }


  public CUserMsg_ParticleManager_AddModellistOverrideElement AddModellistOverrideElement
  { get => new CUserMsg_ParticleManager_AddModellistOverrideElementImpl(NativeNetMessages.GetNestedMessage(Address, "add_modellist_override_element"), false); }


  public CUserMsg_ParticleManager_ClearModellistOverride ClearModellistOverride
  { get => new CUserMsg_ParticleManager_ClearModellistOverrideImpl(NativeNetMessages.GetNestedMessage(Address, "clear_modellist_override"), false); }


  public CUserMsg_ParticleManager_CreatePhysicsSim CreatePhysicsSim
  { get => new CUserMsg_ParticleManager_CreatePhysicsSimImpl(NativeNetMessages.GetNestedMessage(Address, "create_physics_sim"), false); }


  public CUserMsg_ParticleManager_DestroyPhysicsSim DestroyPhysicsSim
  { get => new CUserMsg_ParticleManager_DestroyPhysicsSimImpl(NativeNetMessages.GetNestedMessage(Address, "destroy_physics_sim"), false); }


  public CUserMsg_ParticleManager_SetVData SetVdata
  { get => new CUserMsg_ParticleManager_SetVDataImpl(NativeNetMessages.GetNestedMessage(Address, "set_vdata"), false); }


  public CUserMsg_ParticleManager_SetMaterialOverride SetMaterialOverride
  { get => new CUserMsg_ParticleManager_SetMaterialOverrideImpl(NativeNetMessages.GetNestedMessage(Address, "set_material_override"), false); }


  public CUserMsg_ParticleManager_AddFan AddFan
  { get => new CUserMsg_ParticleManager_AddFanImpl(NativeNetMessages.GetNestedMessage(Address, "add_fan"), false); }


  public CUserMsg_ParticleManager_UpdateFan UpdateFan
  { get => new CUserMsg_ParticleManager_UpdateFanImpl(NativeNetMessages.GetNestedMessage(Address, "update_fan"), false); }


  public CUserMsg_ParticleManager_SetParticleClusterGrowth SetParticleClusterGrowth
  { get => new CUserMsg_ParticleManager_SetParticleClusterGrowthImpl(NativeNetMessages.GetNestedMessage(Address, "set_particle_cluster_growth"), false); }


  public CUserMsg_ParticleManager_RemoveFan RemoveFan
  { get => new CUserMsg_ParticleManager_RemoveFanImpl(NativeNetMessages.GetNestedMessage(Address, "remove_fan"), false); }

}
