using SwiftlyS2.Shared.Datamaps;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Shared.Datamaps;

public partial interface IDatamapFunctionService
{

    public IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorDoorTouch> CBaseDoorDoorTouch { get; }


    public IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorDoorGoUp> CBaseDoorDoorGoUp { get; }


    public IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorDoorGoDown> CBaseDoorDoorGoDown { get; }


    public IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorDoorHitTop> CBaseDoorDoorHitTop { get; }


    public IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorDoorHitBottom> CBaseDoorDoorHitBottom { get; }


    public IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorMovingSoundThink> CBaseDoorMovingSoundThink { get; }


    public IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorCloseAreaPortalsThink> CBaseDoorCloseAreaPortalsThink { get; }


    public IDatamapFunctionOperator<CGunTarget, IDHookCGunTargetNext> CGunTargetNext { get; }


    public IDatamapFunctionOperator<CGunTarget, IDHookCGunTargetStart> CGunTargetStart { get; }


    public IDatamapFunctionOperator<CGunTarget, IDHookCGunTargetWait> CGunTargetWait { get; }


    public IDatamapFunctionOperator<CDynamicLight, IDHookCDynamicLightDynamicLightThink> CDynamicLightDynamicLightThink { get; }


    public IDatamapFunctionOperator<CBarnLight, IDHookCBarnLightThink_SetNextQueuedLightStyle> CBarnLightThink_SetNextQueuedLightStyle { get; }


    public IDatamapFunctionOperator<CBarnLight, IDHookCBarnLightThink_ApplyLightStylesToTargets> CBarnLightThink_ApplyLightStylesToTargets { get; }


    public IDatamapFunctionOperator<CBarnLight, IDHookCBarnLightThink_LightStyleEvent> CBarnLightThink_LightStyleEvent { get; }


    public IDatamapFunctionOperator<CEnvLaser, IDHookCEnvLaserStrikeThink> CEnvLaserStrikeThink { get; }


    public IDatamapFunctionOperator<CMultiSource, IDHookCMultiSourceRegister> CMultiSourceRegister { get; }


    public IDatamapFunctionOperator<CPhysHinge, IDHookCPhysHingeSoundThink> CPhysHingeSoundThink { get; }


    public IDatamapFunctionOperator<CPhysHinge, IDHookCPhysHingeLimitThink> CPhysHingeLimitThink { get; }


    public IDatamapFunctionOperator<CPhysHinge, IDHookCPhysHingeMoveThink> CPhysHingeMoveThink { get; }


    public IDatamapFunctionOperator<CSoundOpvarSetPointBase, IDHookCSoundOpvarSetPointBaseSetOpvarThink> CSoundOpvarSetPointBaseSetOpvarThink { get; }


    public IDatamapFunctionOperator<CEnvEntityMaker, IDHookCEnvEntityMakerCheckSpawnThink> CEnvEntityMakerCheckSpawnThink { get; }


    public IDatamapFunctionOperator<CPointHurt, IDHookCPointHurtHurtThink> CPointHurtHurtThink { get; }


    public IDatamapFunctionOperator<CColorCorrection, IDHookCColorCorrectionFadeInThink> CColorCorrectionFadeInThink { get; }


    public IDatamapFunctionOperator<CColorCorrection, IDHookCColorCorrectionFadeOutThink> CColorCorrectionFadeOutThink { get; }


    public IDatamapFunctionOperator<CPhysicsProp, IDHookCPhysicsPropClearFlagsThink> CPhysicsPropClearFlagsThink { get; }


    public IDatamapFunctionOperator<CInferno, IDHookCInfernoInfernoThink> CInfernoInfernoThink { get; }


    public IDatamapFunctionOperator<CFuncTrain, IDHookCFuncTrainWait> CFuncTrainWait { get; }


    public IDatamapFunctionOperator<CFuncTrain, IDHookCFuncTrainNext> CFuncTrainNext { get; }


    public IDatamapFunctionOperator<CBasePropDoor, IDHookCBasePropDoorDoorOpenMoveDone> CBasePropDoorDoorOpenMoveDone { get; }


    public IDatamapFunctionOperator<CBasePropDoor, IDHookCBasePropDoorDoorCloseMoveDone> CBasePropDoorDoorCloseMoveDone { get; }


    public IDatamapFunctionOperator<CBasePropDoor, IDHookCBasePropDoorDoorAutoCloseThink> CBasePropDoorDoorAutoCloseThink { get; }


    public IDatamapFunctionOperator<CBasePropDoor, IDHookCBasePropDoorDisableAreaPortalThink> CBasePropDoorDisableAreaPortalThink { get; }


    public IDatamapFunctionOperator<CInfoSpawnGroupLoadUnload, IDHookCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink> CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink { get; }


    public IDatamapFunctionOperator<CInfoSpawnGroupLoadUnload, IDHookCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink> CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink { get; }


    public IDatamapFunctionOperator<CItemDefuser, IDHookCItemDefuserActivateThink> CItemDefuserActivateThink { get; }


    public IDatamapFunctionOperator<CItemDefuser, IDHookCItemDefuserDefuserTouch> CItemDefuserDefuserTouch { get; }


    public IDatamapFunctionOperator<CEnvSpark, IDHookCEnvSparkSparkThink> CEnvSparkSparkThink { get; }


    public IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeSmoke> CBaseGrenadeSmoke { get; }


    public IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeBounceTouch> CBaseGrenadeBounceTouch { get; }


    public IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeSlideTouch> CBaseGrenadeSlideTouch { get; }


    public IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeExplodeTouch> CBaseGrenadeExplodeTouch { get; }


    public IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeDetonateUse> CBaseGrenadeDetonateUse { get; }


    public IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeDangerSoundThink> CBaseGrenadeDangerSoundThink { get; }


    public IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadePreDetonate> CBaseGrenadePreDetonate { get; }


    public IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeDetonate> CBaseGrenadeDetonate { get; }


    public IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeTumbleThink> CBaseGrenadeTumbleThink { get; }


    public IDatamapFunctionOperator<CSprite, IDHookCSpriteAnimateThink> CSpriteAnimateThink { get; }


    public IDatamapFunctionOperator<CSprite, IDHookCSpriteExpandThink> CSpriteExpandThink { get; }


    public IDatamapFunctionOperator<CSprite, IDHookCSpriteAnimateUntilDead> CSpriteAnimateUntilDead { get; }


    public IDatamapFunctionOperator<CSprite, IDHookCSpriteBeginFadeOutThink> CSpriteBeginFadeOutThink { get; }


    public IDatamapFunctionOperator<CPhysSlideConstraint, IDHookCPhysSlideConstraintSoundThink> CPhysSlideConstraintSoundThink { get; }


    public IDatamapFunctionOperator<CEntityFlame, IDHookCEntityFlameFlameThink> CEntityFlameFlameThink { get; }


    public IDatamapFunctionOperator<CSoundEventConeEntity, IDHookCSoundEventConeEntitySoundEventConeThink> CSoundEventConeEntitySoundEventConeThink { get; }


    public IDatamapFunctionOperator<CFuncMoveLinear, IDHookCFuncMoveLinearNavObstacleThink> CFuncMoveLinearNavObstacleThink { get; }


    public IDatamapFunctionOperator<CFuncMoveLinear, IDHookCFuncMoveLinearNavMovableThink> CFuncMoveLinearNavMovableThink { get; }


    public IDatamapFunctionOperator<CFuncMoveLinear, IDHookCFuncMoveLinearStopMoveSound> CFuncMoveLinearStopMoveSound { get; }


    public IDatamapFunctionOperator<CHostage, IDHookCHostageHostageUse> CHostageHostageUse { get; }


    public IDatamapFunctionOperator<CHostage, IDHookCHostageHostageThink> CHostageHostageThink { get; }


    public IDatamapFunctionOperator<CLogicDistanceAutosave, IDHookCLogicDistanceAutosaveSaveThink> CLogicDistanceAutosaveSaveThink { get; }


    public IDatamapFunctionOperator<CSoundOpvarSetOBBWindEntity, IDHookCSoundOpvarSetOBBWindEntitySetOpvarThink> CSoundOpvarSetOBBWindEntitySetOpvarThink { get; }


    public IDatamapFunctionOperator<CSmokeGrenadeProjectile, IDHookCSmokeGrenadeProjectileThink_Detonate> CSmokeGrenadeProjectileThink_Detonate { get; }


    public IDatamapFunctionOperator<CSmokeGrenadeProjectile, IDHookCSmokeGrenadeProjectileThink_Update> CSmokeGrenadeProjectileThink_Update { get; }


    public IDatamapFunctionOperator<CSmokeGrenadeProjectile, IDHookCSmokeGrenadeProjectileThink_Remove> CSmokeGrenadeProjectileThink_Remove { get; }


    public IDatamapFunctionOperator<CSmokeGrenadeProjectile, IDHookCSmokeGrenadeProjectileThink_BuildingSmokeVolume> CSmokeGrenadeProjectileThink_BuildingSmokeVolume { get; }


    public IDatamapFunctionOperator<CLogicNPCCounter, IDHookCLogicNPCCounterSetNPCCounterThink> CLogicNPCCounterSetNPCCounterThink { get; }


    public IDatamapFunctionOperator<CFuncShatterglass, IDHookCFuncShatterglassGlassThink> CFuncShatterglassGlassThink { get; }


    public IDatamapFunctionOperator<CSoundOpvarSetAutoRoomEntity, IDHookCSoundOpvarSetAutoRoomEntitySetOpvarThink> CSoundOpvarSetAutoRoomEntitySetOpvarThink { get; }


    public IDatamapFunctionOperator<CTriggerGravity, IDHookCTriggerGravityGravityTouch> CTriggerGravityGravityTouch { get; }


    public IDatamapFunctionOperator<CSoundEventPathCornerEntity, IDHookCSoundEventPathCornerEntitySoundEventPathCornerThink> CSoundEventPathCornerEntitySoundEventPathCornerThink { get; }


    public IDatamapFunctionOperator<CItem, IDHookCItemItemTouch> CItemItemTouch { get; }


    public IDatamapFunctionOperator<CItem, IDHookCItemMaterialize> CItemMaterialize { get; }


    public IDatamapFunctionOperator<CItem, IDHookCItemComeToRest> CItemComeToRest { get; }


    public IDatamapFunctionOperator<CBaseCSGrenadeProjectile, IDHookCBaseCSGrenadeProjectileDangerSoundThink> CBaseCSGrenadeProjectileDangerSoundThink { get; }


    public IDatamapFunctionOperator<CSoundOpvarSetPathCornerEntity, IDHookCSoundOpvarSetPathCornerEntitySetOpvarThink> CSoundOpvarSetPathCornerEntitySetOpvarThink { get; }


    public IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonButtonTouch> CBaseButtonButtonTouch { get; }


    public IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonButtonSpark> CBaseButtonButtonSpark { get; }


    public IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonTriggerAndWait> CBaseButtonTriggerAndWait { get; }


    public IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonButtonReturn> CBaseButtonButtonReturn { get; }


    public IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonButtonBackHome> CBaseButtonButtonBackHome { get; }


    public IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonButtonUse> CBaseButtonButtonUse { get; }


    public IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonActivateTouch> CBaseButtonActivateTouch { get; }


    public IDatamapFunctionOperator<CRevertSaved, IDHookCRevertSavedLoadThink> CRevertSavedLoadThink { get; }


    public IDatamapFunctionOperator<CSoundEventOBBEntity, IDHookCSoundEventOBBEntitySoundEventOBBThink> CSoundEventOBBEntitySoundEventOBBThink { get; }


    public IDatamapFunctionOperator<CSplineConstraint, IDHookCSplineConstraintTransitionThink> CSplineConstraintTransitionThink { get; }


    public IDatamapFunctionOperator<CCSWeaponBase, IDHookCCSWeaponBaseDefaultTouch> CCSWeaponBaseDefaultTouch { get; }


    public IDatamapFunctionOperator<CCSWeaponBase, IDHookCCSWeaponBaseRemoveUnownedWeaponThink> CCSWeaponBaseRemoveUnownedWeaponThink { get; }


    public IDatamapFunctionOperator<CChicken, IDHookCChickenChickenTouch> CChickenChickenTouch { get; }


    public IDatamapFunctionOperator<CChicken, IDHookCChickenChickenThink> CChickenChickenThink { get; }


    public IDatamapFunctionOperator<CChicken, IDHookCChickenChickenUse> CChickenChickenUse { get; }


    public IDatamapFunctionOperator<CBaseAnimGraph, IDHookCBaseAnimGraphChoreoServicesThink> CBaseAnimGraphChoreoServicesThink { get; }


    public IDatamapFunctionOperator<CParticleSystem, IDHookCParticleSystemStartParticleSystemThink> CParticleSystemStartParticleSystemThink { get; }


    public IDatamapFunctionOperator<CBaseFlex, IDHookCBaseFlexProcessSceneEventsThink> CBaseFlexProcessSceneEventsThink { get; }


    public IDatamapFunctionOperator<CTriggerProximity, IDHookCTriggerProximityMeasureThink> CTriggerProximityMeasureThink { get; }


    public IDatamapFunctionOperator<CRagdollProp, IDHookCRagdollPropSetDebrisThink> CRagdollPropSetDebrisThink { get; }


    public IDatamapFunctionOperator<CRagdollProp, IDHookCRagdollPropClearFlagsThink> CRagdollPropClearFlagsThink { get; }


    public IDatamapFunctionOperator<CRagdollProp, IDHookCRagdollPropFadeOutThink> CRagdollPropFadeOutThink { get; }


    public IDatamapFunctionOperator<CRagdollProp, IDHookCRagdollPropSettleThink> CRagdollPropSettleThink { get; }


    public IDatamapFunctionOperator<CRagdollProp, IDHookCRagdollPropAttachedItemsThink> CRagdollPropAttachedItemsThink { get; }


    public IDatamapFunctionOperator<CPointValueRemapper, IDHookCPointValueRemapperUpdateThink> CPointValueRemapperUpdateThink { get; }


    public IDatamapFunctionOperator<CBreakableProp, IDHookCBreakablePropBreakThink> CBreakablePropBreakThink { get; }


    public IDatamapFunctionOperator<CBreakableProp, IDHookCBreakablePropRampToDefaultFadeScale> CBreakablePropRampToDefaultFadeScale { get; }


    public IDatamapFunctionOperator<CGenericConstraint, IDHookCGenericConstraintUpdateThink> CGenericConstraintUpdateThink { get; }


    public IDatamapFunctionOperator<CItemSoda, IDHookCItemSodaCanThink> CItemSodaCanThink { get; }


    public IDatamapFunctionOperator<CFuncMover, IDHookCFuncMoverLerpToNewPosition> CFuncMoverLerpToNewPosition { get; }


    public IDatamapFunctionOperator<CEnvWind, IDHookCEnvWindWindThink> CEnvWindWindThink { get; }


    public IDatamapFunctionOperator<CCSPlayerController, IDHookCCSPlayerControllerPlayerForceTeamThink> CCSPlayerControllerPlayerForceTeamThink { get; }


    public IDatamapFunctionOperator<CCSPlayerController, IDHookCCSPlayerControllerResetForceTeamThink> CCSPlayerControllerResetForceTeamThink { get; }


    public IDatamapFunctionOperator<CCSPlayerController, IDHookCCSPlayerControllerResourceDataThink> CCSPlayerControllerResourceDataThink { get; }


    public IDatamapFunctionOperator<CCSPlayerController, IDHookCCSPlayerControllerInventoryUpdateThink> CCSPlayerControllerInventoryUpdateThink { get; }


    public IDatamapFunctionOperator<CLogicActiveAutosave, IDHookCLogicActiveAutosaveSaveThink> CLogicActiveAutosaveSaveThink { get; }


    public IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_Remove> CBaseEntitySUB_Remove { get; }


    public IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_RemoveIfUncarried> CBaseEntitySUB_RemoveIfUncarried { get; }


    public IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_DoNothing> CBaseEntitySUB_DoNothing { get; }


    public IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_Vanish> CBaseEntitySUB_Vanish { get; }


    public IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_CallUseToggle> CBaseEntitySUB_CallUseToggle { get; }


    public IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_KillSelf> CBaseEntitySUB_KillSelf { get; }


    public IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_KillSelfIfUncarried> CBaseEntitySUB_KillSelfIfUncarried { get; }


    public IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntityFakeScriptThinkFunc> CBaseEntityFakeScriptThinkFunc { get; }


    public IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntityClearNavIgnoreContentsThink> CBaseEntityClearNavIgnoreContentsThink { get; }


    public IDatamapFunctionOperator<CFuncRotator, IDHookCFuncRotatorRotateThink> CFuncRotatorRotateThink { get; }


    public IDatamapFunctionOperator<CPointPush, IDHookCPointPushPushThink> CPointPushPushThink { get; }


    public IDatamapFunctionOperator<CTriggerFan, IDHookCTriggerFanPushThink> CTriggerFanPushThink { get; }


    public IDatamapFunctionOperator<CDynamicProp, IDHookCDynamicPropAnimThink> CDynamicPropAnimThink { get; }


    public IDatamapFunctionOperator<CHostageRescueZone, IDHookCHostageRescueZoneCHostageRescueZoneShim_Touch> CHostageRescueZoneCHostageRescueZoneShim_Touch { get; }


    public IDatamapFunctionOperator<CBombTarget, IDHookCBombTargetCBombTargetShim_Touch> CBombTargetCBombTargetShim_Touch { get; }


    public IDatamapFunctionOperator<CBombTarget, IDHookCBombTargetCBombTargetShim_BombTargetUse> CBombTargetCBombTargetShim_BombTargetUse { get; }


    public IDatamapFunctionOperator<CSoundEventEntity, IDHookCSoundEventEntitySoundFinishedThink> CSoundEventEntitySoundFinishedThink { get; }


    public IDatamapFunctionOperator<CFuncTrackTrain, IDHookCFuncTrackTrainNext> CFuncTrackTrainNext { get; }


    public IDatamapFunctionOperator<CFuncTrackTrain, IDHookCFuncTrackTrainFind> CFuncTrackTrainFind { get; }


    public IDatamapFunctionOperator<CFuncTrackTrain, IDHookCFuncTrackTrainNearestPath> CFuncTrackTrainNearestPath { get; }


    public IDatamapFunctionOperator<CFuncTrackTrain, IDHookCFuncTrackTrainDeadEnd> CFuncTrackTrainDeadEnd { get; }


    public IDatamapFunctionOperator<CMomentaryRotButton, IDHookCMomentaryRotButtonUseMoveDone> CMomentaryRotButtonUseMoveDone { get; }


    public IDatamapFunctionOperator<CMomentaryRotButton, IDHookCMomentaryRotButtonReturnMoveDone> CMomentaryRotButtonReturnMoveDone { get; }


    public IDatamapFunctionOperator<CMomentaryRotButton, IDHookCMomentaryRotButtonSetPositionMoveDone> CMomentaryRotButtonSetPositionMoveDone { get; }


    public IDatamapFunctionOperator<CMomentaryRotButton, IDHookCMomentaryRotButtonUpdateThink> CMomentaryRotButtonUpdateThink { get; }


    public IDatamapFunctionOperator<CTriggerSndSosOpvar, IDHookCTriggerSndSosOpvarSndSosTriggerOpvarWaitOver> CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver { get; }


    public IDatamapFunctionOperator<CVoteController, IDHookCVoteControllerVoteControllerThink> CVoteControllerVoteControllerThink { get; }


    public IDatamapFunctionOperator<CPhysForce, IDHookCPhysForceForceOff> CPhysForceForceOff { get; }


    public IDatamapFunctionOperator<CPhysForce, IDHookCPhysForceInitialThink> CPhysForceInitialThink { get; }


    public IDatamapFunctionOperator<CSoundOpvarSetPointEntity, IDHookCSoundOpvarSetPointEntitySetOpvarThink> CSoundOpvarSetPointEntitySetOpvarThink { get; }


    public IDatamapFunctionOperator<CDecoyProjectile, IDHookCDecoyProjectileThink_Detonate> CDecoyProjectileThink_Detonate { get; }


    public IDatamapFunctionOperator<CDecoyProjectile, IDHookCDecoyProjectileGunfireThink> CDecoyProjectileGunfireThink { get; }


    public IDatamapFunctionOperator<CPlantedC4, IDHookCPlantedC4C4Think> CPlantedC4C4Think { get; }


    public IDatamapFunctionOperator<CItemGenericTriggerHelper, IDHookCItemGenericTriggerHelperItemGenericTriggerHelperTouch> CItemGenericTriggerHelperItemGenericTriggerHelperTouch { get; }


    public IDatamapFunctionOperator<CPointOrient, IDHookCPointOrientReorientThink> CPointOrientReorientThink { get; }


    public IDatamapFunctionOperator<CMultiLightProxy, IDHookCMultiLightProxyRestoreFlashlightThink> CMultiLightProxyRestoreFlashlightThink { get; }


    public IDatamapFunctionOperator<CMultiLightProxy, IDHookCMultiLightProxyApproachBrightnessThink> CMultiLightProxyApproachBrightnessThink { get; }


    public IDatamapFunctionOperator<CAmbientGeneric, IDHookCAmbientGenericRampThink> CAmbientGenericRampThink { get; }


    public IDatamapFunctionOperator<CSoundEventSphereEntity, IDHookCSoundEventSphereEntitySoundEventSphereThink> CSoundEventSphereEntitySoundEventSphereThink { get; }


    public IDatamapFunctionOperator<CBreakable, IDHookCBreakableDie> CBreakableDie { get; }


    public IDatamapFunctionOperator<CTriggerHurt, IDHookCTriggerHurtRadiationThink> CTriggerHurtRadiationThink { get; }


    public IDatamapFunctionOperator<CTriggerHurt, IDHookCTriggerHurtHurtThink> CTriggerHurtHurtThink { get; }


    public IDatamapFunctionOperator<CTriggerHurt, IDHookCTriggerHurtNavThink> CTriggerHurtNavThink { get; }


    public IDatamapFunctionOperator<CScriptedSequence, IDHookCScriptedSequenceScriptThink> CScriptedSequenceScriptThink { get; }


    public IDatamapFunctionOperator<CEnvWindController, IDHookCEnvWindControllerWindThink> CEnvWindControllerWindThink { get; }


    public IDatamapFunctionOperator<CTriggerMultiple, IDHookCTriggerMultipleMultiTouch> CTriggerMultipleMultiTouch { get; }


    public IDatamapFunctionOperator<CTriggerMultiple, IDHookCTriggerMultipleMultiWaitOver> CTriggerMultipleMultiWaitOver { get; }


    public IDatamapFunctionOperator<CFuncRotating, IDHookCFuncRotatingSpinUpMove> CFuncRotatingSpinUpMove { get; }


    public IDatamapFunctionOperator<CFuncRotating, IDHookCFuncRotatingSpinDownMove> CFuncRotatingSpinDownMove { get; }


    public IDatamapFunctionOperator<CFuncRotating, IDHookCFuncRotatingHurtTouch> CFuncRotatingHurtTouch { get; }


    public IDatamapFunctionOperator<CFuncRotating, IDHookCFuncRotatingRotatingUse> CFuncRotatingRotatingUse { get; }


    public IDatamapFunctionOperator<CFuncRotating, IDHookCFuncRotatingRotateMove> CFuncRotatingRotateMove { get; }


    public IDatamapFunctionOperator<CFuncRotating, IDHookCFuncRotatingReverseMove> CFuncRotatingReverseMove { get; }


    public IDatamapFunctionOperator<CCSPlayerPawn, IDHookCCSPlayerPawnCheckStuffThink> CCSPlayerPawnCheckStuffThink { get; }


    public IDatamapFunctionOperator<CCSPlayerPawn, IDHookCCSPlayerPawnPushawayThink> CCSPlayerPawnPushawayThink { get; }


    public IDatamapFunctionOperator<CMapVetoPickController, IDHookCMapVetoPickControllerVoteControllerThink> CMapVetoPickControllerVoteControllerThink { get; }


    public IDatamapFunctionOperator<CEntityDissolve, IDHookCEntityDissolveDissolveThink> CEntityDissolveDissolveThink { get; }


    public IDatamapFunctionOperator<CEntityDissolve, IDHookCEntityDissolveElectrocuteThink> CEntityDissolveElectrocuteThink { get; }


    public IDatamapFunctionOperator<CLogicMeasureMovement, IDHookCLogicMeasureMovementMeasureThink> CLogicMeasureMovementMeasureThink { get; }


    public IDatamapFunctionOperator<CTriggerImpact, IDHookCTriggerImpactDisable> CTriggerImpactDisable { get; }


    public IDatamapFunctionOperator<CEnvBeam, IDHookCEnvBeamStrikeThink> CEnvBeamStrikeThink { get; }


    public IDatamapFunctionOperator<CEnvBeam, IDHookCEnvBeamUpdateThink> CEnvBeamUpdateThink { get; }


    public IDatamapFunctionOperator<CCSPlayerResource, IDHookCCSPlayerResourceResourceThink> CCSPlayerResourceResourceThink { get; }


    public IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_DissolveIfUncarried> CBaseModelEntitySUB_DissolveIfUncarried { get; }


    public IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_StartFadeOut> CBaseModelEntitySUB_StartFadeOut { get; }


    public IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_StartFadeOutInstant> CBaseModelEntitySUB_StartFadeOutInstant { get; }


    public IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_FadeOut> CBaseModelEntitySUB_FadeOut { get; }


    public IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_StartShadowFadeOut> CBaseModelEntitySUB_StartShadowFadeOut { get; }


    public IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_PerformShadowFadeOut> CBaseModelEntitySUB_PerformShadowFadeOut { get; }


    public IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_StartShadowFadeIn> CBaseModelEntitySUB_StartShadowFadeIn { get; }


    public IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_PerformShadowFadeIn> CBaseModelEntitySUB_PerformShadowFadeIn { get; }


    public IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_StopShadowFade> CBaseModelEntitySUB_StopShadowFade { get; }


    public IDatamapFunctionOperator<CSoundOpvarSetAABBEntity, IDHookCSoundOpvarSetAABBEntitySetOpvarThink> CSoundOpvarSetAABBEntitySetOpvarThink { get; }


    public IDatamapFunctionOperator<CFuncTrainControls, IDHookCFuncTrainControlsFind> CFuncTrainControlsFind { get; }


    public IDatamapFunctionOperator<CPhysicsPropRespawnable, IDHookCPhysicsPropRespawnableMaterialize> CPhysicsPropRespawnableMaterialize { get; }


    public IDatamapFunctionOperator<CFishPool, IDHookCFishPoolUpdate> CFishPoolUpdate { get; }


    public IDatamapFunctionOperator<CTriggerSoundscape, IDHookCTriggerSoundscapePlayerUpdateThink> CTriggerSoundscapePlayerUpdateThink { get; }


    public IDatamapFunctionOperator<CItemGeneric, IDHookCItemGenericItemGenericTouch> CItemGenericItemGenericTouch { get; }


    public IDatamapFunctionOperator<CTriggerActiveWeaponDetect, IDHookCTriggerActiveWeaponDetectActiveWeaponThink> CTriggerActiveWeaponDetectActiveWeaponThink { get; }


    public IDatamapFunctionOperator<CFogController, IDHookCFogControllerSetLerpValues> CFogControllerSetLerpValues { get; }


    public IDatamapFunctionOperator<CFuncTrackChange, IDHookCFuncTrackChangeFind> CFuncTrackChangeFind { get; }


    public IDatamapFunctionOperator<CTriggerLook, IDHookCTriggerLookTimeoutThink> CTriggerLookTimeoutThink { get; }


    public IDatamapFunctionOperator<CSoundOpvarSetOBBEntity, IDHookCSoundOpvarSetOBBEntitySetOpvarThink> CSoundOpvarSetOBBEntitySetOpvarThink { get; }


    public IDatamapFunctionOperator<CColorCorrectionVolume, IDHookCColorCorrectionVolumeThinkFunc> CColorCorrectionVolumeThinkFunc { get; }


    public IDatamapFunctionOperator<CWaterBullet, IDHookCWaterBulletBulletThink> CWaterBulletBulletThink { get; }


    public IDatamapFunctionOperator<CFuncPlat, IDHookCFuncPlatPlatUse> CFuncPlatPlatUse { get; }


    public IDatamapFunctionOperator<CFuncPlat, IDHookCFuncPlatCallGoDown> CFuncPlatCallGoDown { get; }


    public IDatamapFunctionOperator<CFuncPlat, IDHookCFuncPlatCallHitTop> CFuncPlatCallHitTop { get; }


    public IDatamapFunctionOperator<CFuncPlat, IDHookCFuncPlatCallHitBottom> CFuncPlatCallHitBottom { get; }


    public IDatamapFunctionOperator<CPhysImpact, IDHookCPhysImpactPointAtEntity> CPhysImpactPointAtEntity { get; }


    public IDatamapFunctionOperator<CTriggerLerpObject, IDHookCTriggerLerpObjectLerpThink> CTriggerLerpObjectLerpThink { get; }


    public IDatamapFunctionOperator<CTriggerLerpObject, IDHookCTriggerLerpObjectUnsetWaitForEntity> CTriggerLerpObjectUnsetWaitForEntity { get; }


    public IDatamapFunctionOperator<CTriggerLerpObject, IDHookCTriggerLerpObjectAttachedEntityThink> CTriggerLerpObjectAttachedEntityThink { get; }


    public IDatamapFunctionOperator<CPathMoverEntitySpawner, IDHookCPathMoverEntitySpawnerSpawnThink> CPathMoverEntitySpawnerSpawnThink { get; }


    public IDatamapFunctionOperator<CPointCommentaryNode, IDHookCPointCommentaryNodeSpinThink> CPointCommentaryNodeSpinThink { get; }


    public IDatamapFunctionOperator<CPointCommentaryNode, IDHookCPointCommentaryNodeUpdateViewThink> CPointCommentaryNodeUpdateViewThink { get; }


    public IDatamapFunctionOperator<CPointCommentaryNode, IDHookCPointCommentaryNodeUpdateViewPostThink> CPointCommentaryNodeUpdateViewPostThink { get; }


    public IDatamapFunctionOperator<CPointCommentaryNode, IDHookCPointCommentaryNodeAcculumatePlayTimeThink> CPointCommentaryNodeAcculumatePlayTimeThink { get; }


    public IDatamapFunctionOperator<CPathNode, IDHookCPathNodeParentedMoveThink> CPathNodeParentedMoveThink { get; }


    public IDatamapFunctionOperator<CPhysicalButton, IDHookCPhysicalButtonPhysicsThink> CPhysicalButtonPhysicsThink { get; }


    public IDatamapFunctionOperator<CPhysicalButton, IDHookCPhysicalButtonButtonTouch> CPhysicalButtonButtonTouch { get; }


    public IDatamapFunctionOperator<CPhysicalButton, IDHookCPhysicalButtonTriggerAndWait> CPhysicalButtonTriggerAndWait { get; }


    public IDatamapFunctionOperator<CPhysicalButton, IDHookCPhysicalButtonButtonBackHome> CPhysicalButtonButtonBackHome { get; }

}
