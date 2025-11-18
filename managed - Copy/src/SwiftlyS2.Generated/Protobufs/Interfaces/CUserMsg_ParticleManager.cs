
using SwiftlyS2.Core.ProtobufDefinitions;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.NetMessages;

namespace SwiftlyS2.Shared.ProtobufDefinitions;

public interface CUserMsg_ParticleManager : ITypedProtobuf<CUserMsg_ParticleManager>
{
  static CUserMsg_ParticleManager ITypedProtobuf<CUserMsg_ParticleManager>.Wrap(nint handle, bool isManuallyAllocated) => new CUserMsg_ParticleManagerImpl(handle, isManuallyAllocated);


  public PARTICLE_MESSAGE Type { get; set; }


  public uint Index { get; set; }


  public CUserMsg_ParticleManager_ReleaseParticleIndex ReleaseParticleIndex { get; }


  public CUserMsg_ParticleManager_CreateParticle CreateParticle { get; }


  public CUserMsg_ParticleManager_DestroyParticle DestroyParticle { get; }


  public CUserMsg_ParticleManager_DestroyParticleInvolving DestroyParticleInvolving { get; }


  public CUserMsg_ParticleManager_UpdateParticle_OBSOLETE UpdateParticle { get; }


  public CUserMsg_ParticleManager_UpdateParticleFwd_OBSOLETE UpdateParticleFwd { get; }


  public CUserMsg_ParticleManager_UpdateParticleOrient_OBSOLETE UpdateParticleOrient { get; }


  public CUserMsg_ParticleManager_UpdateParticleFallback UpdateParticleFallback { get; }


  public CUserMsg_ParticleManager_UpdateParticleOffset UpdateParticleOffset { get; }


  public CUserMsg_ParticleManager_UpdateParticleEnt UpdateParticleEnt { get; }


  public CUserMsg_ParticleManager_UpdateParticleShouldDraw UpdateParticleShouldDraw { get; }


  public CUserMsg_ParticleManager_UpdateParticleSetFrozen UpdateParticleSetFrozen { get; }


  public CUserMsg_ParticleManager_ChangeControlPointAttachment ChangeControlPointAttachment { get; }


  public CUserMsg_ParticleManager_UpdateEntityPosition UpdateEntityPosition { get; }


  public CUserMsg_ParticleManager_SetParticleFoWProperties SetParticleFowProperties { get; }


  public CUserMsg_ParticleManager_SetParticleText SetParticleText { get; }


  public CUserMsg_ParticleManager_SetParticleShouldCheckFoW SetParticleShouldCheckFow { get; }


  public CUserMsg_ParticleManager_SetControlPointModel SetControlPointModel { get; }


  public CUserMsg_ParticleManager_SetControlPointSnapshot SetControlPointSnapshot { get; }


  public CUserMsg_ParticleManager_SetTextureAttribute SetTextureAttribute { get; }


  public CUserMsg_ParticleManager_SetSceneObjectGenericFlag SetSceneObjectGenericFlag { get; }


  public CUserMsg_ParticleManager_SetSceneObjectTintAndDesat SetSceneObjectTintAndDesat { get; }


  public CUserMsg_ParticleManager_DestroyParticleNamed DestroyParticleNamed { get; }


  public CUserMsg_ParticleManager_ParticleSkipToTime ParticleSkipToTime { get; }


  public CUserMsg_ParticleManager_ParticleCanFreeze ParticleCanFreeze { get; }


  public CUserMsg_ParticleManager_SetParticleNamedValueContext SetNamedValueContext { get; }


  public CUserMsg_ParticleManager_UpdateParticleTransform UpdateParticleTransform { get; }


  public CUserMsg_ParticleManager_ParticleFreezeTransitionOverride ParticleFreezeTransitionOverride { get; }


  public CUserMsg_ParticleManager_FreezeParticleInvolving FreezeParticleInvolving { get; }


  public CUserMsg_ParticleManager_AddModellistOverrideElement AddModellistOverrideElement { get; }


  public CUserMsg_ParticleManager_ClearModellistOverride ClearModellistOverride { get; }


  public CUserMsg_ParticleManager_CreatePhysicsSim CreatePhysicsSim { get; }


  public CUserMsg_ParticleManager_DestroyPhysicsSim DestroyPhysicsSim { get; }


  public CUserMsg_ParticleManager_SetVData SetVdata { get; }


  public CUserMsg_ParticleManager_SetMaterialOverride SetMaterialOverride { get; }


  public CUserMsg_ParticleManager_AddFan AddFan { get; }


  public CUserMsg_ParticleManager_UpdateFan UpdateFan { get; }


  public CUserMsg_ParticleManager_SetParticleClusterGrowth SetParticleClusterGrowth { get; }


  public CUserMsg_ParticleManager_RemoveFan RemoveFan { get; }

}
