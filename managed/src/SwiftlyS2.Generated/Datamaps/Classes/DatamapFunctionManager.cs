using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Core.Hooks;

namespace SwiftlyS2.Core.Datamaps;

internal partial class DatamapFunctionManager
{
    public HookManager HookManager { get; }

    public BaseDatamapFunction<CBaseDoor, DHookCBaseDoorDoorTouch> CBaseDoorDoorTouch { get; init; }

    public BaseDatamapFunction<CBaseDoor, DHookCBaseDoorDoorGoUp> CBaseDoorDoorGoUp { get; init; }

    public BaseDatamapFunction<CBaseDoor, DHookCBaseDoorDoorGoDown> CBaseDoorDoorGoDown { get; init; }

    public BaseDatamapFunction<CBaseDoor, DHookCBaseDoorDoorHitTop> CBaseDoorDoorHitTop { get; init; }

    public BaseDatamapFunction<CBaseDoor, DHookCBaseDoorDoorHitBottom> CBaseDoorDoorHitBottom { get; init; }

    public BaseDatamapFunction<CBaseDoor, DHookCBaseDoorMovingSoundThink> CBaseDoorMovingSoundThink { get; init; }

    public BaseDatamapFunction<CBaseDoor, DHookCBaseDoorCloseAreaPortalsThink> CBaseDoorCloseAreaPortalsThink { get; init; }

    public BaseDatamapFunction<CGunTarget, DHookCGunTargetNext> CGunTargetNext { get; init; }

    public BaseDatamapFunction<CGunTarget, DHookCGunTargetStart> CGunTargetStart { get; init; }

    public BaseDatamapFunction<CGunTarget, DHookCGunTargetWait> CGunTargetWait { get; init; }

    public BaseDatamapFunction<CDynamicLight, DHookCDynamicLightDynamicLightThink> CDynamicLightDynamicLightThink { get; init; }

    public BaseDatamapFunction<CBarnLight, DHookCBarnLightThink_SetNextQueuedLightStyle> CBarnLightThink_SetNextQueuedLightStyle { get; init; }

    public BaseDatamapFunction<CBarnLight, DHookCBarnLightThink_ApplyLightStylesToTargets> CBarnLightThink_ApplyLightStylesToTargets { get; init; }

    public BaseDatamapFunction<CBarnLight, DHookCBarnLightThink_LightStyleEvent> CBarnLightThink_LightStyleEvent { get; init; }

    public BaseDatamapFunction<CEnvLaser, DHookCEnvLaserStrikeThink> CEnvLaserStrikeThink { get; init; }

    public BaseDatamapFunction<CMultiSource, DHookCMultiSourceRegister> CMultiSourceRegister { get; init; }

    public BaseDatamapFunction<CPhysHinge, DHookCPhysHingeSoundThink> CPhysHingeSoundThink { get; init; }

    public BaseDatamapFunction<CPhysHinge, DHookCPhysHingeLimitThink> CPhysHingeLimitThink { get; init; }

    public BaseDatamapFunction<CPhysHinge, DHookCPhysHingeMoveThink> CPhysHingeMoveThink { get; init; }

    public BaseDatamapFunction<CSoundOpvarSetPointBase, DHookCSoundOpvarSetPointBaseSetOpvarThink> CSoundOpvarSetPointBaseSetOpvarThink { get; init; }

    public BaseDatamapFunction<CEnvEntityMaker, DHookCEnvEntityMakerCheckSpawnThink> CEnvEntityMakerCheckSpawnThink { get; init; }

    public BaseDatamapFunction<CPointHurt, DHookCPointHurtHurtThink> CPointHurtHurtThink { get; init; }

    public BaseDatamapFunction<CColorCorrection, DHookCColorCorrectionFadeInThink> CColorCorrectionFadeInThink { get; init; }

    public BaseDatamapFunction<CColorCorrection, DHookCColorCorrectionFadeOutThink> CColorCorrectionFadeOutThink { get; init; }

    public BaseDatamapFunction<CPhysicsProp, DHookCPhysicsPropClearFlagsThink> CPhysicsPropClearFlagsThink { get; init; }

    public BaseDatamapFunction<CInferno, DHookCInfernoInfernoThink> CInfernoInfernoThink { get; init; }

    public BaseDatamapFunction<CFuncTrain, DHookCFuncTrainWait> CFuncTrainWait { get; init; }

    public BaseDatamapFunction<CFuncTrain, DHookCFuncTrainNext> CFuncTrainNext { get; init; }

    public BaseDatamapFunction<CBasePropDoor, DHookCBasePropDoorDoorOpenMoveDone> CBasePropDoorDoorOpenMoveDone { get; init; }

    public BaseDatamapFunction<CBasePropDoor, DHookCBasePropDoorDoorCloseMoveDone> CBasePropDoorDoorCloseMoveDone { get; init; }

    public BaseDatamapFunction<CBasePropDoor, DHookCBasePropDoorDoorAutoCloseThink> CBasePropDoorDoorAutoCloseThink { get; init; }

    public BaseDatamapFunction<CBasePropDoor, DHookCBasePropDoorDisableAreaPortalThink> CBasePropDoorDisableAreaPortalThink { get; init; }

    public BaseDatamapFunction<CInfoSpawnGroupLoadUnload, DHookCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink> CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink { get; init; }

    public BaseDatamapFunction<CInfoSpawnGroupLoadUnload, DHookCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink> CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink { get; init; }

    public BaseDatamapFunction<CItemDefuser, DHookCItemDefuserActivateThink> CItemDefuserActivateThink { get; init; }

    public BaseDatamapFunction<CItemDefuser, DHookCItemDefuserDefuserTouch> CItemDefuserDefuserTouch { get; init; }

    public BaseDatamapFunction<CEnvSpark, DHookCEnvSparkSparkThink> CEnvSparkSparkThink { get; init; }

    public BaseDatamapFunction<CBaseGrenade, DHookCBaseGrenadeSmoke> CBaseGrenadeSmoke { get; init; }

    public BaseDatamapFunction<CBaseGrenade, DHookCBaseGrenadeBounceTouch> CBaseGrenadeBounceTouch { get; init; }

    public BaseDatamapFunction<CBaseGrenade, DHookCBaseGrenadeSlideTouch> CBaseGrenadeSlideTouch { get; init; }

    public BaseDatamapFunction<CBaseGrenade, DHookCBaseGrenadeExplodeTouch> CBaseGrenadeExplodeTouch { get; init; }

    public BaseDatamapFunction<CBaseGrenade, DHookCBaseGrenadeDetonateUse> CBaseGrenadeDetonateUse { get; init; }

    public BaseDatamapFunction<CBaseGrenade, DHookCBaseGrenadeDangerSoundThink> CBaseGrenadeDangerSoundThink { get; init; }

    public BaseDatamapFunction<CBaseGrenade, DHookCBaseGrenadePreDetonate> CBaseGrenadePreDetonate { get; init; }

    public BaseDatamapFunction<CBaseGrenade, DHookCBaseGrenadeDetonate> CBaseGrenadeDetonate { get; init; }

    public BaseDatamapFunction<CBaseGrenade, DHookCBaseGrenadeTumbleThink> CBaseGrenadeTumbleThink { get; init; }

    public BaseDatamapFunction<CSprite, DHookCSpriteAnimateThink> CSpriteAnimateThink { get; init; }

    public BaseDatamapFunction<CSprite, DHookCSpriteExpandThink> CSpriteExpandThink { get; init; }

    public BaseDatamapFunction<CSprite, DHookCSpriteAnimateUntilDead> CSpriteAnimateUntilDead { get; init; }

    public BaseDatamapFunction<CSprite, DHookCSpriteBeginFadeOutThink> CSpriteBeginFadeOutThink { get; init; }

    public BaseDatamapFunction<CPhysSlideConstraint, DHookCPhysSlideConstraintSoundThink> CPhysSlideConstraintSoundThink { get; init; }

    public BaseDatamapFunction<CEntityFlame, DHookCEntityFlameFlameThink> CEntityFlameFlameThink { get; init; }

    public BaseDatamapFunction<CSoundEventConeEntity, DHookCSoundEventConeEntitySoundEventConeThink> CSoundEventConeEntitySoundEventConeThink { get; init; }

    public BaseDatamapFunction<CFuncMoveLinear, DHookCFuncMoveLinearNavObstacleThink> CFuncMoveLinearNavObstacleThink { get; init; }

    public BaseDatamapFunction<CFuncMoveLinear, DHookCFuncMoveLinearNavMovableThink> CFuncMoveLinearNavMovableThink { get; init; }

    public BaseDatamapFunction<CFuncMoveLinear, DHookCFuncMoveLinearStopMoveSound> CFuncMoveLinearStopMoveSound { get; init; }

    public BaseDatamapFunction<CHostage, DHookCHostageHostageUse> CHostageHostageUse { get; init; }

    public BaseDatamapFunction<CHostage, DHookCHostageHostageThink> CHostageHostageThink { get; init; }

    public BaseDatamapFunction<CLogicDistanceAutosave, DHookCLogicDistanceAutosaveSaveThink> CLogicDistanceAutosaveSaveThink { get; init; }

    public BaseDatamapFunction<CSoundOpvarSetOBBWindEntity, DHookCSoundOpvarSetOBBWindEntitySetOpvarThink> CSoundOpvarSetOBBWindEntitySetOpvarThink { get; init; }

    public BaseDatamapFunction<CSmokeGrenadeProjectile, DHookCSmokeGrenadeProjectileThink_Detonate> CSmokeGrenadeProjectileThink_Detonate { get; init; }

    public BaseDatamapFunction<CSmokeGrenadeProjectile, DHookCSmokeGrenadeProjectileThink_Update> CSmokeGrenadeProjectileThink_Update { get; init; }

    public BaseDatamapFunction<CSmokeGrenadeProjectile, DHookCSmokeGrenadeProjectileThink_Remove> CSmokeGrenadeProjectileThink_Remove { get; init; }

    public BaseDatamapFunction<CSmokeGrenadeProjectile, DHookCSmokeGrenadeProjectileThink_BuildingSmokeVolume> CSmokeGrenadeProjectileThink_BuildingSmokeVolume { get; init; }

    public BaseDatamapFunction<CLogicNPCCounter, DHookCLogicNPCCounterSetNPCCounterThink> CLogicNPCCounterSetNPCCounterThink { get; init; }

    public BaseDatamapFunction<CFuncShatterglass, DHookCFuncShatterglassGlassThink> CFuncShatterglassGlassThink { get; init; }

    public BaseDatamapFunction<CSoundOpvarSetAutoRoomEntity, DHookCSoundOpvarSetAutoRoomEntitySetOpvarThink> CSoundOpvarSetAutoRoomEntitySetOpvarThink { get; init; }

    public BaseDatamapFunction<CTriggerGravity, DHookCTriggerGravityGravityTouch> CTriggerGravityGravityTouch { get; init; }

    public BaseDatamapFunction<CSoundEventPathCornerEntity, DHookCSoundEventPathCornerEntitySoundEventPathCornerThink> CSoundEventPathCornerEntitySoundEventPathCornerThink { get; init; }

    public BaseDatamapFunction<CItem, DHookCItemItemTouch> CItemItemTouch { get; init; }

    public BaseDatamapFunction<CItem, DHookCItemMaterialize> CItemMaterialize { get; init; }

    public BaseDatamapFunction<CItem, DHookCItemComeToRest> CItemComeToRest { get; init; }

    public BaseDatamapFunction<CBaseCSGrenadeProjectile, DHookCBaseCSGrenadeProjectileDangerSoundThink> CBaseCSGrenadeProjectileDangerSoundThink { get; init; }

    public BaseDatamapFunction<CSoundOpvarSetPathCornerEntity, DHookCSoundOpvarSetPathCornerEntitySetOpvarThink> CSoundOpvarSetPathCornerEntitySetOpvarThink { get; init; }

    public BaseDatamapFunction<CBaseButton, DHookCBaseButtonButtonTouch> CBaseButtonButtonTouch { get; init; }

    public BaseDatamapFunction<CBaseButton, DHookCBaseButtonButtonSpark> CBaseButtonButtonSpark { get; init; }

    public BaseDatamapFunction<CBaseButton, DHookCBaseButtonTriggerAndWait> CBaseButtonTriggerAndWait { get; init; }

    public BaseDatamapFunction<CBaseButton, DHookCBaseButtonButtonReturn> CBaseButtonButtonReturn { get; init; }

    public BaseDatamapFunction<CBaseButton, DHookCBaseButtonButtonBackHome> CBaseButtonButtonBackHome { get; init; }

    public BaseDatamapFunction<CBaseButton, DHookCBaseButtonButtonUse> CBaseButtonButtonUse { get; init; }

    public BaseDatamapFunction<CBaseButton, DHookCBaseButtonActivateTouch> CBaseButtonActivateTouch { get; init; }

    public BaseDatamapFunction<CRevertSaved, DHookCRevertSavedLoadThink> CRevertSavedLoadThink { get; init; }

    public BaseDatamapFunction<CSoundEventOBBEntity, DHookCSoundEventOBBEntitySoundEventOBBThink> CSoundEventOBBEntitySoundEventOBBThink { get; init; }

    public BaseDatamapFunction<CSplineConstraint, DHookCSplineConstraintTransitionThink> CSplineConstraintTransitionThink { get; init; }

    public BaseDatamapFunction<CCSWeaponBase, DHookCCSWeaponBaseDefaultTouch> CCSWeaponBaseDefaultTouch { get; init; }

    public BaseDatamapFunction<CCSWeaponBase, DHookCCSWeaponBaseRemoveUnownedWeaponThink> CCSWeaponBaseRemoveUnownedWeaponThink { get; init; }

    public BaseDatamapFunction<CChicken, DHookCChickenChickenTouch> CChickenChickenTouch { get; init; }

    public BaseDatamapFunction<CChicken, DHookCChickenChickenThink> CChickenChickenThink { get; init; }

    public BaseDatamapFunction<CChicken, DHookCChickenChickenUse> CChickenChickenUse { get; init; }

    public BaseDatamapFunction<CBaseAnimGraph, DHookCBaseAnimGraphChoreoServicesThink> CBaseAnimGraphChoreoServicesThink { get; init; }

    public BaseDatamapFunction<CParticleSystem, DHookCParticleSystemStartParticleSystemThink> CParticleSystemStartParticleSystemThink { get; init; }

    public BaseDatamapFunction<CBaseFlex, DHookCBaseFlexProcessSceneEventsThink> CBaseFlexProcessSceneEventsThink { get; init; }

    public BaseDatamapFunction<CTriggerProximity, DHookCTriggerProximityMeasureThink> CTriggerProximityMeasureThink { get; init; }

    public BaseDatamapFunction<CRagdollProp, DHookCRagdollPropSetDebrisThink> CRagdollPropSetDebrisThink { get; init; }

    public BaseDatamapFunction<CRagdollProp, DHookCRagdollPropClearFlagsThink> CRagdollPropClearFlagsThink { get; init; }

    public BaseDatamapFunction<CRagdollProp, DHookCRagdollPropFadeOutThink> CRagdollPropFadeOutThink { get; init; }

    public BaseDatamapFunction<CRagdollProp, DHookCRagdollPropSettleThink> CRagdollPropSettleThink { get; init; }

    public BaseDatamapFunction<CRagdollProp, DHookCRagdollPropAttachedItemsThink> CRagdollPropAttachedItemsThink { get; init; }

    public BaseDatamapFunction<CPointValueRemapper, DHookCPointValueRemapperUpdateThink> CPointValueRemapperUpdateThink { get; init; }

    public BaseDatamapFunction<CBreakableProp, DHookCBreakablePropBreakThink> CBreakablePropBreakThink { get; init; }

    public BaseDatamapFunction<CBreakableProp, DHookCBreakablePropRampToDefaultFadeScale> CBreakablePropRampToDefaultFadeScale { get; init; }

    public BaseDatamapFunction<CGenericConstraint, DHookCGenericConstraintUpdateThink> CGenericConstraintUpdateThink { get; init; }

    public BaseDatamapFunction<CItemSoda, DHookCItemSodaCanThink> CItemSodaCanThink { get; init; }

    public BaseDatamapFunction<CFuncMover, DHookCFuncMoverLerpToNewPosition> CFuncMoverLerpToNewPosition { get; init; }

    public BaseDatamapFunction<CEnvWind, DHookCEnvWindWindThink> CEnvWindWindThink { get; init; }

    public BaseDatamapFunction<CCSPlayerController, DHookCCSPlayerControllerPlayerForceTeamThink> CCSPlayerControllerPlayerForceTeamThink { get; init; }

    public BaseDatamapFunction<CCSPlayerController, DHookCCSPlayerControllerResetForceTeamThink> CCSPlayerControllerResetForceTeamThink { get; init; }

    public BaseDatamapFunction<CCSPlayerController, DHookCCSPlayerControllerResourceDataThink> CCSPlayerControllerResourceDataThink { get; init; }

    public BaseDatamapFunction<CCSPlayerController, DHookCCSPlayerControllerInventoryUpdateThink> CCSPlayerControllerInventoryUpdateThink { get; init; }

    public BaseDatamapFunction<CLogicActiveAutosave, DHookCLogicActiveAutosaveSaveThink> CLogicActiveAutosaveSaveThink { get; init; }

    public BaseDatamapFunction<CBaseEntity, DHookCBaseEntitySUB_Remove> CBaseEntitySUB_Remove { get; init; }

    public BaseDatamapFunction<CBaseEntity, DHookCBaseEntitySUB_RemoveIfUncarried> CBaseEntitySUB_RemoveIfUncarried { get; init; }

    public BaseDatamapFunction<CBaseEntity, DHookCBaseEntitySUB_DoNothing> CBaseEntitySUB_DoNothing { get; init; }

    public BaseDatamapFunction<CBaseEntity, DHookCBaseEntitySUB_Vanish> CBaseEntitySUB_Vanish { get; init; }

    public BaseDatamapFunction<CBaseEntity, DHookCBaseEntitySUB_CallUseToggle> CBaseEntitySUB_CallUseToggle { get; init; }

    public BaseDatamapFunction<CBaseEntity, DHookCBaseEntitySUB_KillSelf> CBaseEntitySUB_KillSelf { get; init; }

    public BaseDatamapFunction<CBaseEntity, DHookCBaseEntitySUB_KillSelfIfUncarried> CBaseEntitySUB_KillSelfIfUncarried { get; init; }

    public BaseDatamapFunction<CBaseEntity, DHookCBaseEntityFakeScriptThinkFunc> CBaseEntityFakeScriptThinkFunc { get; init; }

    public BaseDatamapFunction<CBaseEntity, DHookCBaseEntityClearNavIgnoreContentsThink> CBaseEntityClearNavIgnoreContentsThink { get; init; }

    public BaseDatamapFunction<CFuncRotator, DHookCFuncRotatorRotateThink> CFuncRotatorRotateThink { get; init; }

    public BaseDatamapFunction<CPointPush, DHookCPointPushPushThink> CPointPushPushThink { get; init; }

    public BaseDatamapFunction<CTriggerFan, DHookCTriggerFanPushThink> CTriggerFanPushThink { get; init; }

    public BaseDatamapFunction<CDynamicProp, DHookCDynamicPropAnimThink> CDynamicPropAnimThink { get; init; }

    public BaseDatamapFunction<CHostageRescueZone, DHookCHostageRescueZoneCHostageRescueZoneShim_Touch> CHostageRescueZoneCHostageRescueZoneShim_Touch { get; init; }

    public BaseDatamapFunction<CBombTarget, DHookCBombTargetCBombTargetShim_Touch> CBombTargetCBombTargetShim_Touch { get; init; }

    public BaseDatamapFunction<CBombTarget, DHookCBombTargetCBombTargetShim_BombTargetUse> CBombTargetCBombTargetShim_BombTargetUse { get; init; }

    public BaseDatamapFunction<CSoundEventEntity, DHookCSoundEventEntitySoundFinishedThink> CSoundEventEntitySoundFinishedThink { get; init; }

    public BaseDatamapFunction<CFuncTrackTrain, DHookCFuncTrackTrainNext> CFuncTrackTrainNext { get; init; }

    public BaseDatamapFunction<CFuncTrackTrain, DHookCFuncTrackTrainFind> CFuncTrackTrainFind { get; init; }

    public BaseDatamapFunction<CFuncTrackTrain, DHookCFuncTrackTrainNearestPath> CFuncTrackTrainNearestPath { get; init; }

    public BaseDatamapFunction<CFuncTrackTrain, DHookCFuncTrackTrainDeadEnd> CFuncTrackTrainDeadEnd { get; init; }

    public BaseDatamapFunction<CMomentaryRotButton, DHookCMomentaryRotButtonUseMoveDone> CMomentaryRotButtonUseMoveDone { get; init; }

    public BaseDatamapFunction<CMomentaryRotButton, DHookCMomentaryRotButtonReturnMoveDone> CMomentaryRotButtonReturnMoveDone { get; init; }

    public BaseDatamapFunction<CMomentaryRotButton, DHookCMomentaryRotButtonSetPositionMoveDone> CMomentaryRotButtonSetPositionMoveDone { get; init; }

    public BaseDatamapFunction<CMomentaryRotButton, DHookCMomentaryRotButtonUpdateThink> CMomentaryRotButtonUpdateThink { get; init; }

    public BaseDatamapFunction<CTriggerSndSosOpvar, DHookCTriggerSndSosOpvarSndSosTriggerOpvarWaitOver> CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver { get; init; }

    public BaseDatamapFunction<CVoteController, DHookCVoteControllerVoteControllerThink> CVoteControllerVoteControllerThink { get; init; }

    public BaseDatamapFunction<CPhysForce, DHookCPhysForceForceOff> CPhysForceForceOff { get; init; }

    public BaseDatamapFunction<CPhysForce, DHookCPhysForceInitialThink> CPhysForceInitialThink { get; init; }

    public BaseDatamapFunction<CSoundOpvarSetPointEntity, DHookCSoundOpvarSetPointEntitySetOpvarThink> CSoundOpvarSetPointEntitySetOpvarThink { get; init; }

    public BaseDatamapFunction<CDecoyProjectile, DHookCDecoyProjectileThink_Detonate> CDecoyProjectileThink_Detonate { get; init; }

    public BaseDatamapFunction<CDecoyProjectile, DHookCDecoyProjectileGunfireThink> CDecoyProjectileGunfireThink { get; init; }

    public BaseDatamapFunction<CPlantedC4, DHookCPlantedC4C4Think> CPlantedC4C4Think { get; init; }

    public BaseDatamapFunction<CItemGenericTriggerHelper, DHookCItemGenericTriggerHelperItemGenericTriggerHelperTouch> CItemGenericTriggerHelperItemGenericTriggerHelperTouch { get; init; }

    public BaseDatamapFunction<CPointOrient, DHookCPointOrientReorientThink> CPointOrientReorientThink { get; init; }

    public BaseDatamapFunction<CMultiLightProxy, DHookCMultiLightProxyRestoreFlashlightThink> CMultiLightProxyRestoreFlashlightThink { get; init; }

    public BaseDatamapFunction<CMultiLightProxy, DHookCMultiLightProxyApproachBrightnessThink> CMultiLightProxyApproachBrightnessThink { get; init; }

    public BaseDatamapFunction<CAmbientGeneric, DHookCAmbientGenericRampThink> CAmbientGenericRampThink { get; init; }

    public BaseDatamapFunction<CSoundEventSphereEntity, DHookCSoundEventSphereEntitySoundEventSphereThink> CSoundEventSphereEntitySoundEventSphereThink { get; init; }

    public BaseDatamapFunction<CBreakable, DHookCBreakableDie> CBreakableDie { get; init; }

    public BaseDatamapFunction<CTriggerHurt, DHookCTriggerHurtRadiationThink> CTriggerHurtRadiationThink { get; init; }

    public BaseDatamapFunction<CTriggerHurt, DHookCTriggerHurtHurtThink> CTriggerHurtHurtThink { get; init; }

    public BaseDatamapFunction<CTriggerHurt, DHookCTriggerHurtNavThink> CTriggerHurtNavThink { get; init; }

    public BaseDatamapFunction<CScriptedSequence, DHookCScriptedSequenceScriptThink> CScriptedSequenceScriptThink { get; init; }

    public BaseDatamapFunction<CEnvWindController, DHookCEnvWindControllerWindThink> CEnvWindControllerWindThink { get; init; }

    public BaseDatamapFunction<CTriggerMultiple, DHookCTriggerMultipleMultiTouch> CTriggerMultipleMultiTouch { get; init; }

    public BaseDatamapFunction<CTriggerMultiple, DHookCTriggerMultipleMultiWaitOver> CTriggerMultipleMultiWaitOver { get; init; }

    public BaseDatamapFunction<CFuncRotating, DHookCFuncRotatingSpinUpMove> CFuncRotatingSpinUpMove { get; init; }

    public BaseDatamapFunction<CFuncRotating, DHookCFuncRotatingSpinDownMove> CFuncRotatingSpinDownMove { get; init; }

    public BaseDatamapFunction<CFuncRotating, DHookCFuncRotatingHurtTouch> CFuncRotatingHurtTouch { get; init; }

    public BaseDatamapFunction<CFuncRotating, DHookCFuncRotatingRotatingUse> CFuncRotatingRotatingUse { get; init; }

    public BaseDatamapFunction<CFuncRotating, DHookCFuncRotatingRotateMove> CFuncRotatingRotateMove { get; init; }

    public BaseDatamapFunction<CFuncRotating, DHookCFuncRotatingReverseMove> CFuncRotatingReverseMove { get; init; }

    public BaseDatamapFunction<CCSPlayerPawn, DHookCCSPlayerPawnCheckStuffThink> CCSPlayerPawnCheckStuffThink { get; init; }

    public BaseDatamapFunction<CCSPlayerPawn, DHookCCSPlayerPawnPushawayThink> CCSPlayerPawnPushawayThink { get; init; }

    public BaseDatamapFunction<CMapVetoPickController, DHookCMapVetoPickControllerVoteControllerThink> CMapVetoPickControllerVoteControllerThink { get; init; }

    public BaseDatamapFunction<CEntityDissolve, DHookCEntityDissolveDissolveThink> CEntityDissolveDissolveThink { get; init; }

    public BaseDatamapFunction<CEntityDissolve, DHookCEntityDissolveElectrocuteThink> CEntityDissolveElectrocuteThink { get; init; }

    public BaseDatamapFunction<CLogicMeasureMovement, DHookCLogicMeasureMovementMeasureThink> CLogicMeasureMovementMeasureThink { get; init; }

    public BaseDatamapFunction<CTriggerImpact, DHookCTriggerImpactDisable> CTriggerImpactDisable { get; init; }

    public BaseDatamapFunction<CEnvBeam, DHookCEnvBeamStrikeThink> CEnvBeamStrikeThink { get; init; }

    public BaseDatamapFunction<CEnvBeam, DHookCEnvBeamUpdateThink> CEnvBeamUpdateThink { get; init; }

    public BaseDatamapFunction<CCSPlayerResource, DHookCCSPlayerResourceResourceThink> CCSPlayerResourceResourceThink { get; init; }

    public BaseDatamapFunction<CBaseModelEntity, DHookCBaseModelEntitySUB_DissolveIfUncarried> CBaseModelEntitySUB_DissolveIfUncarried { get; init; }

    public BaseDatamapFunction<CBaseModelEntity, DHookCBaseModelEntitySUB_StartFadeOut> CBaseModelEntitySUB_StartFadeOut { get; init; }

    public BaseDatamapFunction<CBaseModelEntity, DHookCBaseModelEntitySUB_StartFadeOutInstant> CBaseModelEntitySUB_StartFadeOutInstant { get; init; }

    public BaseDatamapFunction<CBaseModelEntity, DHookCBaseModelEntitySUB_FadeOut> CBaseModelEntitySUB_FadeOut { get; init; }

    public BaseDatamapFunction<CBaseModelEntity, DHookCBaseModelEntitySUB_StartShadowFadeOut> CBaseModelEntitySUB_StartShadowFadeOut { get; init; }

    public BaseDatamapFunction<CBaseModelEntity, DHookCBaseModelEntitySUB_PerformShadowFadeOut> CBaseModelEntitySUB_PerformShadowFadeOut { get; init; }

    public BaseDatamapFunction<CBaseModelEntity, DHookCBaseModelEntitySUB_StartShadowFadeIn> CBaseModelEntitySUB_StartShadowFadeIn { get; init; }

    public BaseDatamapFunction<CBaseModelEntity, DHookCBaseModelEntitySUB_PerformShadowFadeIn> CBaseModelEntitySUB_PerformShadowFadeIn { get; init; }

    public BaseDatamapFunction<CBaseModelEntity, DHookCBaseModelEntitySUB_StopShadowFade> CBaseModelEntitySUB_StopShadowFade { get; init; }

    public BaseDatamapFunction<CSoundOpvarSetAABBEntity, DHookCSoundOpvarSetAABBEntitySetOpvarThink> CSoundOpvarSetAABBEntitySetOpvarThink { get; init; }

    public BaseDatamapFunction<CFuncTrainControls, DHookCFuncTrainControlsFind> CFuncTrainControlsFind { get; init; }

    public BaseDatamapFunction<CPhysicsPropRespawnable, DHookCPhysicsPropRespawnableMaterialize> CPhysicsPropRespawnableMaterialize { get; init; }

    public BaseDatamapFunction<CFishPool, DHookCFishPoolUpdate> CFishPoolUpdate { get; init; }

    public BaseDatamapFunction<CTriggerSoundscape, DHookCTriggerSoundscapePlayerUpdateThink> CTriggerSoundscapePlayerUpdateThink { get; init; }

    public BaseDatamapFunction<CItemGeneric, DHookCItemGenericItemGenericTouch> CItemGenericItemGenericTouch { get; init; }

    public BaseDatamapFunction<CTriggerActiveWeaponDetect, DHookCTriggerActiveWeaponDetectActiveWeaponThink> CTriggerActiveWeaponDetectActiveWeaponThink { get; init; }

    public BaseDatamapFunction<CFogController, DHookCFogControllerSetLerpValues> CFogControllerSetLerpValues { get; init; }

    public BaseDatamapFunction<CFuncTrackChange, DHookCFuncTrackChangeFind> CFuncTrackChangeFind { get; init; }

    public BaseDatamapFunction<CTriggerLook, DHookCTriggerLookTimeoutThink> CTriggerLookTimeoutThink { get; init; }

    public BaseDatamapFunction<CSoundOpvarSetOBBEntity, DHookCSoundOpvarSetOBBEntitySetOpvarThink> CSoundOpvarSetOBBEntitySetOpvarThink { get; init; }

    public BaseDatamapFunction<CColorCorrectionVolume, DHookCColorCorrectionVolumeThinkFunc> CColorCorrectionVolumeThinkFunc { get; init; }

    public BaseDatamapFunction<CWaterBullet, DHookCWaterBulletBulletThink> CWaterBulletBulletThink { get; init; }

    public BaseDatamapFunction<CFuncPlat, DHookCFuncPlatPlatUse> CFuncPlatPlatUse { get; init; }

    public BaseDatamapFunction<CFuncPlat, DHookCFuncPlatCallGoDown> CFuncPlatCallGoDown { get; init; }

    public BaseDatamapFunction<CFuncPlat, DHookCFuncPlatCallHitTop> CFuncPlatCallHitTop { get; init; }

    public BaseDatamapFunction<CFuncPlat, DHookCFuncPlatCallHitBottom> CFuncPlatCallHitBottom { get; init; }

    public BaseDatamapFunction<CPhysImpact, DHookCPhysImpactPointAtEntity> CPhysImpactPointAtEntity { get; init; }

    public BaseDatamapFunction<CTriggerLerpObject, DHookCTriggerLerpObjectLerpThink> CTriggerLerpObjectLerpThink { get; init; }

    public BaseDatamapFunction<CTriggerLerpObject, DHookCTriggerLerpObjectUnsetWaitForEntity> CTriggerLerpObjectUnsetWaitForEntity { get; init; }

    public BaseDatamapFunction<CTriggerLerpObject, DHookCTriggerLerpObjectAttachedEntityThink> CTriggerLerpObjectAttachedEntityThink { get; init; }

    public BaseDatamapFunction<CPathMoverEntitySpawner, DHookCPathMoverEntitySpawnerSpawnThink> CPathMoverEntitySpawnerSpawnThink { get; init; }

    public BaseDatamapFunction<CPointCommentaryNode, DHookCPointCommentaryNodeSpinThink> CPointCommentaryNodeSpinThink { get; init; }

    public BaseDatamapFunction<CPointCommentaryNode, DHookCPointCommentaryNodeUpdateViewThink> CPointCommentaryNodeUpdateViewThink { get; init; }

    public BaseDatamapFunction<CPointCommentaryNode, DHookCPointCommentaryNodeUpdateViewPostThink> CPointCommentaryNodeUpdateViewPostThink { get; init; }

    public BaseDatamapFunction<CPointCommentaryNode, DHookCPointCommentaryNodeAcculumatePlayTimeThink> CPointCommentaryNodeAcculumatePlayTimeThink { get; init; }

    public BaseDatamapFunction<CPathNode, DHookCPathNodeParentedMoveThink> CPathNodeParentedMoveThink { get; init; }

    public BaseDatamapFunction<CPhysicalButton, DHookCPhysicalButtonPhysicsThink> CPhysicalButtonPhysicsThink { get; init; }

    public BaseDatamapFunction<CPhysicalButton, DHookCPhysicalButtonButtonTouch> CPhysicalButtonButtonTouch { get; init; }

    public BaseDatamapFunction<CPhysicalButton, DHookCPhysicalButtonTriggerAndWait> CPhysicalButtonTriggerAndWait { get; init; }

    public BaseDatamapFunction<CPhysicalButton, DHookCPhysicalButtonButtonBackHome> CPhysicalButtonButtonBackHome { get; init; }


    public DatamapFunctionManager(HookManager hookManager)
    {
        HookManager = hookManager;
        CBaseDoorDoorTouch = new(this, 172685942);
        CBaseDoorDoorGoUp = new(this, 2298197376);
        CBaseDoorDoorGoDown = new(this, 289487733);
        CBaseDoorDoorHitTop = new(this, 1665805083);
        CBaseDoorDoorHitBottom = new(this, 2303542595);
        CBaseDoorMovingSoundThink = new(this, 3792098904);
        CBaseDoorCloseAreaPortalsThink = new(this, 3919462989);
        CGunTargetNext = new(this, 1693417876);
        CGunTargetStart = new(this, 990623819);
        CGunTargetWait = new(this, 1485786948);
        CDynamicLightDynamicLightThink = new(this, 1147710618);
        CBarnLightThink_SetNextQueuedLightStyle = new(this, 4218503553);
        CBarnLightThink_ApplyLightStylesToTargets = new(this, 442160547);
        CBarnLightThink_LightStyleEvent = new(this, 4274811955);
        CEnvLaserStrikeThink = new(this, 432302058);
        CMultiSourceRegister = new(this, 526664079);
        CPhysHingeSoundThink = new(this, 3550960992);
        CPhysHingeLimitThink = new(this, 1741603608);
        CPhysHingeMoveThink = new(this, 1970551832);
        CSoundOpvarSetPointBaseSetOpvarThink = new(this, 37103116);
        CEnvEntityMakerCheckSpawnThink = new(this, 3394589803);
        CPointHurtHurtThink = new(this, 3625807744);
        CColorCorrectionFadeInThink = new(this, 358959564);
        CColorCorrectionFadeOutThink = new(this, 333339303);
        CPhysicsPropClearFlagsThink = new(this, 2855085258);
        CInfernoInfernoThink = new(this, 2710234000);
        CFuncTrainWait = new(this, 1391763459);
        CFuncTrainNext = new(this, 1038607779);
        CBasePropDoorDoorOpenMoveDone = new(this, 1166911171);
        CBasePropDoorDoorCloseMoveDone = new(this, 2066379847);
        CBasePropDoorDoorAutoCloseThink = new(this, 702825821);
        CBasePropDoorDisableAreaPortalThink = new(this, 786657295);
        CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink = new(this, 1866401433);
        CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink = new(this, 3970637834);
        CItemDefuserActivateThink = new(this, 2686159618);
        CItemDefuserDefuserTouch = new(this, 3566382424);
        CEnvSparkSparkThink = new(this, 433087153);
        CBaseGrenadeSmoke = new(this, 2847480880);
        CBaseGrenadeBounceTouch = new(this, 2139252718);
        CBaseGrenadeSlideTouch = new(this, 1912494007);
        CBaseGrenadeExplodeTouch = new(this, 4187078727);
        CBaseGrenadeDetonateUse = new(this, 446557562);
        CBaseGrenadeDangerSoundThink = new(this, 461348933);
        CBaseGrenadePreDetonate = new(this, 745064160);
        CBaseGrenadeDetonate = new(this, 2824803227);
        CBaseGrenadeTumbleThink = new(this, 2678031248);
        CSpriteAnimateThink = new(this, 273960002);
        CSpriteExpandThink = new(this, 1052354365);
        CSpriteAnimateUntilDead = new(this, 3987928220);
        CSpriteBeginFadeOutThink = new(this, 3382773562);
        CPhysSlideConstraintSoundThink = new(this, 961481177);
        CEntityFlameFlameThink = new(this, 85144681);
        CSoundEventConeEntitySoundEventConeThink = new(this, 3250820479);
        CFuncMoveLinearNavObstacleThink = new(this, 4084335944);
        CFuncMoveLinearNavMovableThink = new(this, 705562651);
        CFuncMoveLinearStopMoveSound = new(this, 927952112);
        CHostageHostageUse = new(this, 2638229067);
        CHostageHostageThink = new(this, 1551136336);
        CLogicDistanceAutosaveSaveThink = new(this, 3131539880);
        CSoundOpvarSetOBBWindEntitySetOpvarThink = new(this, 1255326113);
        CSmokeGrenadeProjectileThink_Detonate = new(this, 2095245481);
        CSmokeGrenadeProjectileThink_Update = new(this, 2312060300);
        CSmokeGrenadeProjectileThink_Remove = new(this, 2902628597);
        CSmokeGrenadeProjectileThink_BuildingSmokeVolume = new(this, 4080814448);
        CLogicNPCCounterSetNPCCounterThink = new(this, 429592434);
        CFuncShatterglassGlassThink = new(this, 440465615);
        CSoundOpvarSetAutoRoomEntitySetOpvarThink = new(this, 3522155238);
        CTriggerGravityGravityTouch = new(this, 1620585753);
        CSoundEventPathCornerEntitySoundEventPathCornerThink = new(this, 1473375619);
        CItemItemTouch = new(this, 4289756597);
        CItemMaterialize = new(this, 3550505174);
        CItemComeToRest = new(this, 2585904682);
        CBaseCSGrenadeProjectileDangerSoundThink = new(this, 2012643680);
        CSoundOpvarSetPathCornerEntitySetOpvarThink = new(this, 2732685688);
        CBaseButtonButtonTouch = new(this, 3309694118);
        CBaseButtonButtonSpark = new(this, 3954073884);
        CBaseButtonTriggerAndWait = new(this, 3111889679);
        CBaseButtonButtonReturn = new(this, 580875661);
        CBaseButtonButtonBackHome = new(this, 3013982703);
        CBaseButtonButtonUse = new(this, 2580252838);
        CBaseButtonActivateTouch = new(this, 160908445);
        CRevertSavedLoadThink = new(this, 2543623985);
        CSoundEventOBBEntitySoundEventOBBThink = new(this, 1596337789);
        CSplineConstraintTransitionThink = new(this, 2609104901);
        CCSWeaponBaseDefaultTouch = new(this, 511461301);
        CCSWeaponBaseRemoveUnownedWeaponThink = new(this, 4037965113);
        CChickenChickenTouch = new(this, 3948946571);
        CChickenChickenThink = new(this, 4036752404);
        CChickenChickenUse = new(this, 1622952319);
        CBaseAnimGraphChoreoServicesThink = new(this, 2787098916);
        CParticleSystemStartParticleSystemThink = new(this, 84602600);
        CBaseFlexProcessSceneEventsThink = new(this, 1611527130);
        CTriggerProximityMeasureThink = new(this, 1319143027);
        CRagdollPropSetDebrisThink = new(this, 1961649649);
        CRagdollPropClearFlagsThink = new(this, 3168198396);
        CRagdollPropFadeOutThink = new(this, 2875446254);
        CRagdollPropSettleThink = new(this, 1965885873);
        CRagdollPropAttachedItemsThink = new(this, 1890222416);
        CPointValueRemapperUpdateThink = new(this, 4273486172);
        CBreakablePropBreakThink = new(this, 284837109);
        CBreakablePropRampToDefaultFadeScale = new(this, 158178530);
        CGenericConstraintUpdateThink = new(this, 3300921543);
        CItemSodaCanThink = new(this, 1201283826);
        CFuncMoverLerpToNewPosition = new(this, 3716047670);
        CEnvWindWindThink = new(this, 793249519);
        CCSPlayerControllerPlayerForceTeamThink = new(this, 4291504818);
        CCSPlayerControllerResetForceTeamThink = new(this, 4082383154);
        CCSPlayerControllerResourceDataThink = new(this, 4104975071);
        CCSPlayerControllerInventoryUpdateThink = new(this, 3498351998);
        CLogicActiveAutosaveSaveThink = new(this, 1525155835);
        CBaseEntitySUB_Remove = new(this, 3088348627);
        CBaseEntitySUB_RemoveIfUncarried = new(this, 2445452551);
        CBaseEntitySUB_DoNothing = new(this, 2656069795);
        CBaseEntitySUB_Vanish = new(this, 3508517696);
        CBaseEntitySUB_CallUseToggle = new(this, 3694644190);
        CBaseEntitySUB_KillSelf = new(this, 41316081);
        CBaseEntitySUB_KillSelfIfUncarried = new(this, 837897133);
        CBaseEntityFakeScriptThinkFunc = new(this, 1013231140);
        CBaseEntityClearNavIgnoreContentsThink = new(this, 1730283338);
        CFuncRotatorRotateThink = new(this, 3896099490);
        CPointPushPushThink = new(this, 3428574372);
        CTriggerFanPushThink = new(this, 3089865481);
        CDynamicPropAnimThink = new(this, 402960493);
        CHostageRescueZoneCHostageRescueZoneShim_Touch = new(this, 1569481127);
        CBombTargetCBombTargetShim_Touch = new(this, 2490735979);
        CBombTargetCBombTargetShim_BombTargetUse = new(this, 1419976908);
        CSoundEventEntitySoundFinishedThink = new(this, 3120072467);
        CFuncTrackTrainNext = new(this, 363492402);
        CFuncTrackTrainFind = new(this, 2869975352);
        CFuncTrackTrainNearestPath = new(this, 467502584);
        CFuncTrackTrainDeadEnd = new(this, 1435258642);
        CMomentaryRotButtonUseMoveDone = new(this, 917218629);
        CMomentaryRotButtonReturnMoveDone = new(this, 2460686390);
        CMomentaryRotButtonSetPositionMoveDone = new(this, 3509146897);
        CMomentaryRotButtonUpdateThink = new(this, 3271468684);
        CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver = new(this, 1540079925);
        CVoteControllerVoteControllerThink = new(this, 1824657406);
        CPhysForceForceOff = new(this, 766626009);
        CPhysForceInitialThink = new(this, 3432042773);
        CSoundOpvarSetPointEntitySetOpvarThink = new(this, 2321269202);
        CDecoyProjectileThink_Detonate = new(this, 1790733560);
        CDecoyProjectileGunfireThink = new(this, 3029763819);
        CPlantedC4C4Think = new(this, 4028234458);
        CItemGenericTriggerHelperItemGenericTriggerHelperTouch = new(this, 184049029);
        CPointOrientReorientThink = new(this, 2181598047);
        CMultiLightProxyRestoreFlashlightThink = new(this, 1243671739);
        CMultiLightProxyApproachBrightnessThink = new(this, 305482120);
        CAmbientGenericRampThink = new(this, 3257029041);
        CSoundEventSphereEntitySoundEventSphereThink = new(this, 1390234487);
        CBreakableDie = new(this, 3544444333);
        CTriggerHurtRadiationThink = new(this, 3253529214);
        CTriggerHurtHurtThink = new(this, 1096455314);
        CTriggerHurtNavThink = new(this, 268962832);
        CScriptedSequenceScriptThink = new(this, 2651723774);
        CEnvWindControllerWindThink = new(this, 279180227);
        CTriggerMultipleMultiTouch = new(this, 1748335918);
        CTriggerMultipleMultiWaitOver = new(this, 968336836);
        CFuncRotatingSpinUpMove = new(this, 4188548792);
        CFuncRotatingSpinDownMove = new(this, 3548937677);
        CFuncRotatingHurtTouch = new(this, 1595369496);
        CFuncRotatingRotatingUse = new(this, 3843995871);
        CFuncRotatingRotateMove = new(this, 348668314);
        CFuncRotatingReverseMove = new(this, 51372959);
        CCSPlayerPawnCheckStuffThink = new(this, 3004532091);
        CCSPlayerPawnPushawayThink = new(this, 2730763711);
        CMapVetoPickControllerVoteControllerThink = new(this, 1625784599);
        CEntityDissolveDissolveThink = new(this, 1248647427);
        CEntityDissolveElectrocuteThink = new(this, 793263003);
        CLogicMeasureMovementMeasureThink = new(this, 647876505);
        CTriggerImpactDisable = new(this, 69542484);
        CEnvBeamStrikeThink = new(this, 4237119704);
        CEnvBeamUpdateThink = new(this, 3604916645);
        CCSPlayerResourceResourceThink = new(this, 3168552603);
        CBaseModelEntitySUB_DissolveIfUncarried = new(this, 2676717591);
        CBaseModelEntitySUB_StartFadeOut = new(this, 1455511482);
        CBaseModelEntitySUB_StartFadeOutInstant = new(this, 370733907);
        CBaseModelEntitySUB_FadeOut = new(this, 2527838686);
        CBaseModelEntitySUB_StartShadowFadeOut = new(this, 4219529750);
        CBaseModelEntitySUB_PerformShadowFadeOut = new(this, 3596630549);
        CBaseModelEntitySUB_StartShadowFadeIn = new(this, 3483318829);
        CBaseModelEntitySUB_PerformShadowFadeIn = new(this, 846751984);
        CBaseModelEntitySUB_StopShadowFade = new(this, 3509617046);
        CSoundOpvarSetAABBEntitySetOpvarThink = new(this, 4119631456);
        CFuncTrainControlsFind = new(this, 3278352523);
        CPhysicsPropRespawnableMaterialize = new(this, 2419595351);
        CFishPoolUpdate = new(this, 3086078691);
        CTriggerSoundscapePlayerUpdateThink = new(this, 3821240667);
        CItemGenericItemGenericTouch = new(this, 591656589);
        CTriggerActiveWeaponDetectActiveWeaponThink = new(this, 664960715);
        CFogControllerSetLerpValues = new(this, 2145300013);
        CFuncTrackChangeFind = new(this, 1061347604);
        CTriggerLookTimeoutThink = new(this, 2837438172);
        CSoundOpvarSetOBBEntitySetOpvarThink = new(this, 3777443659);
        CColorCorrectionVolumeThinkFunc = new(this, 2400113471);
        CWaterBulletBulletThink = new(this, 3710010675);
        CFuncPlatPlatUse = new(this, 115095359);
        CFuncPlatCallGoDown = new(this, 1888421867);
        CFuncPlatCallHitTop = new(this, 4100835961);
        CFuncPlatCallHitBottom = new(this, 4277463349);
        CPhysImpactPointAtEntity = new(this, 3822250024);
        CTriggerLerpObjectLerpThink = new(this, 2032094099);
        CTriggerLerpObjectUnsetWaitForEntity = new(this, 2557540000);
        CTriggerLerpObjectAttachedEntityThink = new(this, 2115356301);
        CPathMoverEntitySpawnerSpawnThink = new(this, 1954646118);
        CPointCommentaryNodeSpinThink = new(this, 892464231);
        CPointCommentaryNodeUpdateViewThink = new(this, 2856636899);
        CPointCommentaryNodeUpdateViewPostThink = new(this, 3862575177);
        CPointCommentaryNodeAcculumatePlayTimeThink = new(this, 4107731308);
        CPathNodeParentedMoveThink = new(this, 2864393235);
        CPhysicalButtonPhysicsThink = new(this, 1818335420);
        CPhysicalButtonButtonTouch = new(this, 1827702710);
        CPhysicalButtonTriggerAndWait = new(this, 1029534207);
        CPhysicalButtonButtonBackHome = new(this, 1032498399);
    }

}
