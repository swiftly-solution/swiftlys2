using SwiftlyS2.Shared.Datamaps;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Datamaps;

internal partial class DatamapFunctionService : IDatamapFunctionService
{
    
    public IDatamapFunctionOperator<CBaseDoor, DHookCBaseDoorDoorTouch> CBaseDoorDoorTouch { get; } = manager.CBaseDoorDoorTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorDoorTouch> IDatamapFunctionService.CBaseDoorDoorTouch => CBaseDoorDoorTouch;


    public IDatamapFunctionOperator<CBaseDoor, DHookCBaseDoorDoorGoUp> CBaseDoorDoorGoUp { get; } = manager.CBaseDoorDoorGoUp.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorDoorGoUp> IDatamapFunctionService.CBaseDoorDoorGoUp => CBaseDoorDoorGoUp;


    public IDatamapFunctionOperator<CBaseDoor, DHookCBaseDoorDoorGoDown> CBaseDoorDoorGoDown { get; } = manager.CBaseDoorDoorGoDown.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorDoorGoDown> IDatamapFunctionService.CBaseDoorDoorGoDown => CBaseDoorDoorGoDown;


    public IDatamapFunctionOperator<CBaseDoor, DHookCBaseDoorDoorHitTop> CBaseDoorDoorHitTop { get; } = manager.CBaseDoorDoorHitTop.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorDoorHitTop> IDatamapFunctionService.CBaseDoorDoorHitTop => CBaseDoorDoorHitTop;


    public IDatamapFunctionOperator<CBaseDoor, DHookCBaseDoorDoorHitBottom> CBaseDoorDoorHitBottom { get; } = manager.CBaseDoorDoorHitBottom.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorDoorHitBottom> IDatamapFunctionService.CBaseDoorDoorHitBottom => CBaseDoorDoorHitBottom;


    public IDatamapFunctionOperator<CBaseDoor, DHookCBaseDoorMovingSoundThink> CBaseDoorMovingSoundThink { get; } = manager.CBaseDoorMovingSoundThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorMovingSoundThink> IDatamapFunctionService.CBaseDoorMovingSoundThink => CBaseDoorMovingSoundThink;


    public IDatamapFunctionOperator<CBaseDoor, DHookCBaseDoorCloseAreaPortalsThink> CBaseDoorCloseAreaPortalsThink { get; } = manager.CBaseDoorCloseAreaPortalsThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseDoor, IDHookCBaseDoorCloseAreaPortalsThink> IDatamapFunctionService.CBaseDoorCloseAreaPortalsThink => CBaseDoorCloseAreaPortalsThink;


    public IDatamapFunctionOperator<CGunTarget, DHookCGunTargetNext> CGunTargetNext { get; } = manager.CGunTargetNext.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CGunTarget, IDHookCGunTargetNext> IDatamapFunctionService.CGunTargetNext => CGunTargetNext;


    public IDatamapFunctionOperator<CGunTarget, DHookCGunTargetStart> CGunTargetStart { get; } = manager.CGunTargetStart.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CGunTarget, IDHookCGunTargetStart> IDatamapFunctionService.CGunTargetStart => CGunTargetStart;


    public IDatamapFunctionOperator<CGunTarget, DHookCGunTargetWait> CGunTargetWait { get; } = manager.CGunTargetWait.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CGunTarget, IDHookCGunTargetWait> IDatamapFunctionService.CGunTargetWait => CGunTargetWait;


    public IDatamapFunctionOperator<CDynamicLight, DHookCDynamicLightDynamicLightThink> CDynamicLightDynamicLightThink { get; } = manager.CDynamicLightDynamicLightThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CDynamicLight, IDHookCDynamicLightDynamicLightThink> IDatamapFunctionService.CDynamicLightDynamicLightThink => CDynamicLightDynamicLightThink;


    public IDatamapFunctionOperator<CBarnLight, DHookCBarnLightThink_SetNextQueuedLightStyle> CBarnLightThink_SetNextQueuedLightStyle { get; } = manager.CBarnLightThink_SetNextQueuedLightStyle.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBarnLight, IDHookCBarnLightThink_SetNextQueuedLightStyle> IDatamapFunctionService.CBarnLightThink_SetNextQueuedLightStyle => CBarnLightThink_SetNextQueuedLightStyle;


    public IDatamapFunctionOperator<CBarnLight, DHookCBarnLightThink_ApplyLightStylesToTargets> CBarnLightThink_ApplyLightStylesToTargets { get; } = manager.CBarnLightThink_ApplyLightStylesToTargets.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBarnLight, IDHookCBarnLightThink_ApplyLightStylesToTargets> IDatamapFunctionService.CBarnLightThink_ApplyLightStylesToTargets => CBarnLightThink_ApplyLightStylesToTargets;


    public IDatamapFunctionOperator<CBarnLight, DHookCBarnLightThink_LightStyleEvent> CBarnLightThink_LightStyleEvent { get; } = manager.CBarnLightThink_LightStyleEvent.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBarnLight, IDHookCBarnLightThink_LightStyleEvent> IDatamapFunctionService.CBarnLightThink_LightStyleEvent => CBarnLightThink_LightStyleEvent;


    public IDatamapFunctionOperator<CEnvLaser, DHookCEnvLaserStrikeThink> CEnvLaserStrikeThink { get; } = manager.CEnvLaserStrikeThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CEnvLaser, IDHookCEnvLaserStrikeThink> IDatamapFunctionService.CEnvLaserStrikeThink => CEnvLaserStrikeThink;


    public IDatamapFunctionOperator<CMultiSource, DHookCMultiSourceRegister> CMultiSourceRegister { get; } = manager.CMultiSourceRegister.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CMultiSource, IDHookCMultiSourceRegister> IDatamapFunctionService.CMultiSourceRegister => CMultiSourceRegister;


    public IDatamapFunctionOperator<CPhysHinge, DHookCPhysHingeSoundThink> CPhysHingeSoundThink { get; } = manager.CPhysHingeSoundThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPhysHinge, IDHookCPhysHingeSoundThink> IDatamapFunctionService.CPhysHingeSoundThink => CPhysHingeSoundThink;


    public IDatamapFunctionOperator<CPhysHinge, DHookCPhysHingeLimitThink> CPhysHingeLimitThink { get; } = manager.CPhysHingeLimitThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPhysHinge, IDHookCPhysHingeLimitThink> IDatamapFunctionService.CPhysHingeLimitThink => CPhysHingeLimitThink;


    public IDatamapFunctionOperator<CPhysHinge, DHookCPhysHingeMoveThink> CPhysHingeMoveThink { get; } = manager.CPhysHingeMoveThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPhysHinge, IDHookCPhysHingeMoveThink> IDatamapFunctionService.CPhysHingeMoveThink => CPhysHingeMoveThink;


    public IDatamapFunctionOperator<CSoundOpvarSetPointBase, DHookCSoundOpvarSetPointBaseSetOpvarThink> CSoundOpvarSetPointBaseSetOpvarThink { get; } = manager.CSoundOpvarSetPointBaseSetOpvarThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSoundOpvarSetPointBase, IDHookCSoundOpvarSetPointBaseSetOpvarThink> IDatamapFunctionService.CSoundOpvarSetPointBaseSetOpvarThink => CSoundOpvarSetPointBaseSetOpvarThink;


    public IDatamapFunctionOperator<CEnvEntityMaker, DHookCEnvEntityMakerCheckSpawnThink> CEnvEntityMakerCheckSpawnThink { get; } = manager.CEnvEntityMakerCheckSpawnThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CEnvEntityMaker, IDHookCEnvEntityMakerCheckSpawnThink> IDatamapFunctionService.CEnvEntityMakerCheckSpawnThink => CEnvEntityMakerCheckSpawnThink;


    public IDatamapFunctionOperator<CPointHurt, DHookCPointHurtHurtThink> CPointHurtHurtThink { get; } = manager.CPointHurtHurtThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPointHurt, IDHookCPointHurtHurtThink> IDatamapFunctionService.CPointHurtHurtThink => CPointHurtHurtThink;


    public IDatamapFunctionOperator<CColorCorrection, DHookCColorCorrectionFadeInThink> CColorCorrectionFadeInThink { get; } = manager.CColorCorrectionFadeInThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CColorCorrection, IDHookCColorCorrectionFadeInThink> IDatamapFunctionService.CColorCorrectionFadeInThink => CColorCorrectionFadeInThink;


    public IDatamapFunctionOperator<CColorCorrection, DHookCColorCorrectionFadeOutThink> CColorCorrectionFadeOutThink { get; } = manager.CColorCorrectionFadeOutThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CColorCorrection, IDHookCColorCorrectionFadeOutThink> IDatamapFunctionService.CColorCorrectionFadeOutThink => CColorCorrectionFadeOutThink;


    public IDatamapFunctionOperator<CPhysicsProp, DHookCPhysicsPropClearFlagsThink> CPhysicsPropClearFlagsThink { get; } = manager.CPhysicsPropClearFlagsThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPhysicsProp, IDHookCPhysicsPropClearFlagsThink> IDatamapFunctionService.CPhysicsPropClearFlagsThink => CPhysicsPropClearFlagsThink;


    public IDatamapFunctionOperator<CInferno, DHookCInfernoInfernoThink> CInfernoInfernoThink { get; } = manager.CInfernoInfernoThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CInferno, IDHookCInfernoInfernoThink> IDatamapFunctionService.CInfernoInfernoThink => CInfernoInfernoThink;


    public IDatamapFunctionOperator<CFuncTrain, DHookCFuncTrainWait> CFuncTrainWait { get; } = manager.CFuncTrainWait.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncTrain, IDHookCFuncTrainWait> IDatamapFunctionService.CFuncTrainWait => CFuncTrainWait;


    public IDatamapFunctionOperator<CFuncTrain, DHookCFuncTrainNext> CFuncTrainNext { get; } = manager.CFuncTrainNext.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncTrain, IDHookCFuncTrainNext> IDatamapFunctionService.CFuncTrainNext => CFuncTrainNext;


    public IDatamapFunctionOperator<CBasePropDoor, DHookCBasePropDoorDoorOpenMoveDone> CBasePropDoorDoorOpenMoveDone { get; } = manager.CBasePropDoorDoorOpenMoveDone.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBasePropDoor, IDHookCBasePropDoorDoorOpenMoveDone> IDatamapFunctionService.CBasePropDoorDoorOpenMoveDone => CBasePropDoorDoorOpenMoveDone;


    public IDatamapFunctionOperator<CBasePropDoor, DHookCBasePropDoorDoorCloseMoveDone> CBasePropDoorDoorCloseMoveDone { get; } = manager.CBasePropDoorDoorCloseMoveDone.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBasePropDoor, IDHookCBasePropDoorDoorCloseMoveDone> IDatamapFunctionService.CBasePropDoorDoorCloseMoveDone => CBasePropDoorDoorCloseMoveDone;


    public IDatamapFunctionOperator<CBasePropDoor, DHookCBasePropDoorDoorAutoCloseThink> CBasePropDoorDoorAutoCloseThink { get; } = manager.CBasePropDoorDoorAutoCloseThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBasePropDoor, IDHookCBasePropDoorDoorAutoCloseThink> IDatamapFunctionService.CBasePropDoorDoorAutoCloseThink => CBasePropDoorDoorAutoCloseThink;


    public IDatamapFunctionOperator<CBasePropDoor, DHookCBasePropDoorDisableAreaPortalThink> CBasePropDoorDisableAreaPortalThink { get; } = manager.CBasePropDoorDisableAreaPortalThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBasePropDoor, IDHookCBasePropDoorDisableAreaPortalThink> IDatamapFunctionService.CBasePropDoorDisableAreaPortalThink => CBasePropDoorDisableAreaPortalThink;


    public IDatamapFunctionOperator<CInfoSpawnGroupLoadUnload, DHookCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink> CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink { get; } = manager.CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CInfoSpawnGroupLoadUnload, IDHookCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink> IDatamapFunctionService.CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink => CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink;


    public IDatamapFunctionOperator<CInfoSpawnGroupLoadUnload, DHookCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink> CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink { get; } = manager.CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CInfoSpawnGroupLoadUnload, IDHookCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink> IDatamapFunctionService.CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink => CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink;


    public IDatamapFunctionOperator<CItemDefuser, DHookCItemDefuserActivateThink> CItemDefuserActivateThink { get; } = manager.CItemDefuserActivateThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CItemDefuser, IDHookCItemDefuserActivateThink> IDatamapFunctionService.CItemDefuserActivateThink => CItemDefuserActivateThink;


    public IDatamapFunctionOperator<CItemDefuser, DHookCItemDefuserDefuserTouch> CItemDefuserDefuserTouch { get; } = manager.CItemDefuserDefuserTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CItemDefuser, IDHookCItemDefuserDefuserTouch> IDatamapFunctionService.CItemDefuserDefuserTouch => CItemDefuserDefuserTouch;


    public IDatamapFunctionOperator<CEnvSpark, DHookCEnvSparkSparkThink> CEnvSparkSparkThink { get; } = manager.CEnvSparkSparkThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CEnvSpark, IDHookCEnvSparkSparkThink> IDatamapFunctionService.CEnvSparkSparkThink => CEnvSparkSparkThink;


    public IDatamapFunctionOperator<CBaseGrenade, DHookCBaseGrenadeSmoke> CBaseGrenadeSmoke { get; } = manager.CBaseGrenadeSmoke.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeSmoke> IDatamapFunctionService.CBaseGrenadeSmoke => CBaseGrenadeSmoke;


    public IDatamapFunctionOperator<CBaseGrenade, DHookCBaseGrenadeBounceTouch> CBaseGrenadeBounceTouch { get; } = manager.CBaseGrenadeBounceTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeBounceTouch> IDatamapFunctionService.CBaseGrenadeBounceTouch => CBaseGrenadeBounceTouch;


    public IDatamapFunctionOperator<CBaseGrenade, DHookCBaseGrenadeSlideTouch> CBaseGrenadeSlideTouch { get; } = manager.CBaseGrenadeSlideTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeSlideTouch> IDatamapFunctionService.CBaseGrenadeSlideTouch => CBaseGrenadeSlideTouch;


    public IDatamapFunctionOperator<CBaseGrenade, DHookCBaseGrenadeExplodeTouch> CBaseGrenadeExplodeTouch { get; } = manager.CBaseGrenadeExplodeTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeExplodeTouch> IDatamapFunctionService.CBaseGrenadeExplodeTouch => CBaseGrenadeExplodeTouch;


    public IDatamapFunctionOperator<CBaseGrenade, DHookCBaseGrenadeDetonateUse> CBaseGrenadeDetonateUse { get; } = manager.CBaseGrenadeDetonateUse.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeDetonateUse> IDatamapFunctionService.CBaseGrenadeDetonateUse => CBaseGrenadeDetonateUse;


    public IDatamapFunctionOperator<CBaseGrenade, DHookCBaseGrenadeDangerSoundThink> CBaseGrenadeDangerSoundThink { get; } = manager.CBaseGrenadeDangerSoundThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeDangerSoundThink> IDatamapFunctionService.CBaseGrenadeDangerSoundThink => CBaseGrenadeDangerSoundThink;


    public IDatamapFunctionOperator<CBaseGrenade, DHookCBaseGrenadePreDetonate> CBaseGrenadePreDetonate { get; } = manager.CBaseGrenadePreDetonate.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadePreDetonate> IDatamapFunctionService.CBaseGrenadePreDetonate => CBaseGrenadePreDetonate;


    public IDatamapFunctionOperator<CBaseGrenade, DHookCBaseGrenadeDetonate> CBaseGrenadeDetonate { get; } = manager.CBaseGrenadeDetonate.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeDetonate> IDatamapFunctionService.CBaseGrenadeDetonate => CBaseGrenadeDetonate;


    public IDatamapFunctionOperator<CBaseGrenade, DHookCBaseGrenadeTumbleThink> CBaseGrenadeTumbleThink { get; } = manager.CBaseGrenadeTumbleThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseGrenade, IDHookCBaseGrenadeTumbleThink> IDatamapFunctionService.CBaseGrenadeTumbleThink => CBaseGrenadeTumbleThink;


    public IDatamapFunctionOperator<CSprite, DHookCSpriteAnimateThink> CSpriteAnimateThink { get; } = manager.CSpriteAnimateThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSprite, IDHookCSpriteAnimateThink> IDatamapFunctionService.CSpriteAnimateThink => CSpriteAnimateThink;


    public IDatamapFunctionOperator<CSprite, DHookCSpriteExpandThink> CSpriteExpandThink { get; } = manager.CSpriteExpandThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSprite, IDHookCSpriteExpandThink> IDatamapFunctionService.CSpriteExpandThink => CSpriteExpandThink;


    public IDatamapFunctionOperator<CSprite, DHookCSpriteAnimateUntilDead> CSpriteAnimateUntilDead { get; } = manager.CSpriteAnimateUntilDead.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSprite, IDHookCSpriteAnimateUntilDead> IDatamapFunctionService.CSpriteAnimateUntilDead => CSpriteAnimateUntilDead;


    public IDatamapFunctionOperator<CSprite, DHookCSpriteBeginFadeOutThink> CSpriteBeginFadeOutThink { get; } = manager.CSpriteBeginFadeOutThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSprite, IDHookCSpriteBeginFadeOutThink> IDatamapFunctionService.CSpriteBeginFadeOutThink => CSpriteBeginFadeOutThink;


    public IDatamapFunctionOperator<CPhysSlideConstraint, DHookCPhysSlideConstraintSoundThink> CPhysSlideConstraintSoundThink { get; } = manager.CPhysSlideConstraintSoundThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPhysSlideConstraint, IDHookCPhysSlideConstraintSoundThink> IDatamapFunctionService.CPhysSlideConstraintSoundThink => CPhysSlideConstraintSoundThink;


    public IDatamapFunctionOperator<CEntityFlame, DHookCEntityFlameFlameThink> CEntityFlameFlameThink { get; } = manager.CEntityFlameFlameThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CEntityFlame, IDHookCEntityFlameFlameThink> IDatamapFunctionService.CEntityFlameFlameThink => CEntityFlameFlameThink;


    public IDatamapFunctionOperator<CSoundEventConeEntity, DHookCSoundEventConeEntitySoundEventConeThink> CSoundEventConeEntitySoundEventConeThink { get; } = manager.CSoundEventConeEntitySoundEventConeThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSoundEventConeEntity, IDHookCSoundEventConeEntitySoundEventConeThink> IDatamapFunctionService.CSoundEventConeEntitySoundEventConeThink => CSoundEventConeEntitySoundEventConeThink;


    public IDatamapFunctionOperator<CFuncMoveLinear, DHookCFuncMoveLinearNavObstacleThink> CFuncMoveLinearNavObstacleThink { get; } = manager.CFuncMoveLinearNavObstacleThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncMoveLinear, IDHookCFuncMoveLinearNavObstacleThink> IDatamapFunctionService.CFuncMoveLinearNavObstacleThink => CFuncMoveLinearNavObstacleThink;


    public IDatamapFunctionOperator<CFuncMoveLinear, DHookCFuncMoveLinearNavMovableThink> CFuncMoveLinearNavMovableThink { get; } = manager.CFuncMoveLinearNavMovableThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncMoveLinear, IDHookCFuncMoveLinearNavMovableThink> IDatamapFunctionService.CFuncMoveLinearNavMovableThink => CFuncMoveLinearNavMovableThink;


    public IDatamapFunctionOperator<CFuncMoveLinear, DHookCFuncMoveLinearStopMoveSound> CFuncMoveLinearStopMoveSound { get; } = manager.CFuncMoveLinearStopMoveSound.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncMoveLinear, IDHookCFuncMoveLinearStopMoveSound> IDatamapFunctionService.CFuncMoveLinearStopMoveSound => CFuncMoveLinearStopMoveSound;


    public IDatamapFunctionOperator<CHostage, DHookCHostageHostageUse> CHostageHostageUse { get; } = manager.CHostageHostageUse.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CHostage, IDHookCHostageHostageUse> IDatamapFunctionService.CHostageHostageUse => CHostageHostageUse;


    public IDatamapFunctionOperator<CHostage, DHookCHostageHostageThink> CHostageHostageThink { get; } = manager.CHostageHostageThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CHostage, IDHookCHostageHostageThink> IDatamapFunctionService.CHostageHostageThink => CHostageHostageThink;


    public IDatamapFunctionOperator<CLogicDistanceAutosave, DHookCLogicDistanceAutosaveSaveThink> CLogicDistanceAutosaveSaveThink { get; } = manager.CLogicDistanceAutosaveSaveThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CLogicDistanceAutosave, IDHookCLogicDistanceAutosaveSaveThink> IDatamapFunctionService.CLogicDistanceAutosaveSaveThink => CLogicDistanceAutosaveSaveThink;


    public IDatamapFunctionOperator<CSoundOpvarSetOBBWindEntity, DHookCSoundOpvarSetOBBWindEntitySetOpvarThink> CSoundOpvarSetOBBWindEntitySetOpvarThink { get; } = manager.CSoundOpvarSetOBBWindEntitySetOpvarThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSoundOpvarSetOBBWindEntity, IDHookCSoundOpvarSetOBBWindEntitySetOpvarThink> IDatamapFunctionService.CSoundOpvarSetOBBWindEntitySetOpvarThink => CSoundOpvarSetOBBWindEntitySetOpvarThink;


    public IDatamapFunctionOperator<CSmokeGrenadeProjectile, DHookCSmokeGrenadeProjectileThink_Detonate> CSmokeGrenadeProjectileThink_Detonate { get; } = manager.CSmokeGrenadeProjectileThink_Detonate.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSmokeGrenadeProjectile, IDHookCSmokeGrenadeProjectileThink_Detonate> IDatamapFunctionService.CSmokeGrenadeProjectileThink_Detonate => CSmokeGrenadeProjectileThink_Detonate;


    public IDatamapFunctionOperator<CSmokeGrenadeProjectile, DHookCSmokeGrenadeProjectileThink_Update> CSmokeGrenadeProjectileThink_Update { get; } = manager.CSmokeGrenadeProjectileThink_Update.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSmokeGrenadeProjectile, IDHookCSmokeGrenadeProjectileThink_Update> IDatamapFunctionService.CSmokeGrenadeProjectileThink_Update => CSmokeGrenadeProjectileThink_Update;


    public IDatamapFunctionOperator<CSmokeGrenadeProjectile, DHookCSmokeGrenadeProjectileThink_Remove> CSmokeGrenadeProjectileThink_Remove { get; } = manager.CSmokeGrenadeProjectileThink_Remove.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSmokeGrenadeProjectile, IDHookCSmokeGrenadeProjectileThink_Remove> IDatamapFunctionService.CSmokeGrenadeProjectileThink_Remove => CSmokeGrenadeProjectileThink_Remove;


    public IDatamapFunctionOperator<CSmokeGrenadeProjectile, DHookCSmokeGrenadeProjectileThink_BuildingSmokeVolume> CSmokeGrenadeProjectileThink_BuildingSmokeVolume { get; } = manager.CSmokeGrenadeProjectileThink_BuildingSmokeVolume.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSmokeGrenadeProjectile, IDHookCSmokeGrenadeProjectileThink_BuildingSmokeVolume> IDatamapFunctionService.CSmokeGrenadeProjectileThink_BuildingSmokeVolume => CSmokeGrenadeProjectileThink_BuildingSmokeVolume;


    public IDatamapFunctionOperator<CLogicNPCCounter, DHookCLogicNPCCounterSetNPCCounterThink> CLogicNPCCounterSetNPCCounterThink { get; } = manager.CLogicNPCCounterSetNPCCounterThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CLogicNPCCounter, IDHookCLogicNPCCounterSetNPCCounterThink> IDatamapFunctionService.CLogicNPCCounterSetNPCCounterThink => CLogicNPCCounterSetNPCCounterThink;


    public IDatamapFunctionOperator<CFuncShatterglass, DHookCFuncShatterglassGlassThink> CFuncShatterglassGlassThink { get; } = manager.CFuncShatterglassGlassThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncShatterglass, IDHookCFuncShatterglassGlassThink> IDatamapFunctionService.CFuncShatterglassGlassThink => CFuncShatterglassGlassThink;


    public IDatamapFunctionOperator<CSoundOpvarSetAutoRoomEntity, DHookCSoundOpvarSetAutoRoomEntitySetOpvarThink> CSoundOpvarSetAutoRoomEntitySetOpvarThink { get; } = manager.CSoundOpvarSetAutoRoomEntitySetOpvarThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSoundOpvarSetAutoRoomEntity, IDHookCSoundOpvarSetAutoRoomEntitySetOpvarThink> IDatamapFunctionService.CSoundOpvarSetAutoRoomEntitySetOpvarThink => CSoundOpvarSetAutoRoomEntitySetOpvarThink;


    public IDatamapFunctionOperator<CTriggerGravity, DHookCTriggerGravityGravityTouch> CTriggerGravityGravityTouch { get; } = manager.CTriggerGravityGravityTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerGravity, IDHookCTriggerGravityGravityTouch> IDatamapFunctionService.CTriggerGravityGravityTouch => CTriggerGravityGravityTouch;


    public IDatamapFunctionOperator<CSoundEventPathCornerEntity, DHookCSoundEventPathCornerEntitySoundEventPathCornerThink> CSoundEventPathCornerEntitySoundEventPathCornerThink { get; } = manager.CSoundEventPathCornerEntitySoundEventPathCornerThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSoundEventPathCornerEntity, IDHookCSoundEventPathCornerEntitySoundEventPathCornerThink> IDatamapFunctionService.CSoundEventPathCornerEntitySoundEventPathCornerThink => CSoundEventPathCornerEntitySoundEventPathCornerThink;


    public IDatamapFunctionOperator<CItem, DHookCItemItemTouch> CItemItemTouch { get; } = manager.CItemItemTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CItem, IDHookCItemItemTouch> IDatamapFunctionService.CItemItemTouch => CItemItemTouch;


    public IDatamapFunctionOperator<CItem, DHookCItemMaterialize> CItemMaterialize { get; } = manager.CItemMaterialize.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CItem, IDHookCItemMaterialize> IDatamapFunctionService.CItemMaterialize => CItemMaterialize;


    public IDatamapFunctionOperator<CItem, DHookCItemComeToRest> CItemComeToRest { get; } = manager.CItemComeToRest.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CItem, IDHookCItemComeToRest> IDatamapFunctionService.CItemComeToRest => CItemComeToRest;


    public IDatamapFunctionOperator<CBaseCSGrenadeProjectile, DHookCBaseCSGrenadeProjectileDangerSoundThink> CBaseCSGrenadeProjectileDangerSoundThink { get; } = manager.CBaseCSGrenadeProjectileDangerSoundThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseCSGrenadeProjectile, IDHookCBaseCSGrenadeProjectileDangerSoundThink> IDatamapFunctionService.CBaseCSGrenadeProjectileDangerSoundThink => CBaseCSGrenadeProjectileDangerSoundThink;


    public IDatamapFunctionOperator<CSoundOpvarSetPathCornerEntity, DHookCSoundOpvarSetPathCornerEntitySetOpvarThink> CSoundOpvarSetPathCornerEntitySetOpvarThink { get; } = manager.CSoundOpvarSetPathCornerEntitySetOpvarThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSoundOpvarSetPathCornerEntity, IDHookCSoundOpvarSetPathCornerEntitySetOpvarThink> IDatamapFunctionService.CSoundOpvarSetPathCornerEntitySetOpvarThink => CSoundOpvarSetPathCornerEntitySetOpvarThink;


    public IDatamapFunctionOperator<CBaseButton, DHookCBaseButtonButtonTouch> CBaseButtonButtonTouch { get; } = manager.CBaseButtonButtonTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonButtonTouch> IDatamapFunctionService.CBaseButtonButtonTouch => CBaseButtonButtonTouch;


    public IDatamapFunctionOperator<CBaseButton, DHookCBaseButtonButtonSpark> CBaseButtonButtonSpark { get; } = manager.CBaseButtonButtonSpark.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonButtonSpark> IDatamapFunctionService.CBaseButtonButtonSpark => CBaseButtonButtonSpark;


    public IDatamapFunctionOperator<CBaseButton, DHookCBaseButtonTriggerAndWait> CBaseButtonTriggerAndWait { get; } = manager.CBaseButtonTriggerAndWait.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonTriggerAndWait> IDatamapFunctionService.CBaseButtonTriggerAndWait => CBaseButtonTriggerAndWait;


    public IDatamapFunctionOperator<CBaseButton, DHookCBaseButtonButtonReturn> CBaseButtonButtonReturn { get; } = manager.CBaseButtonButtonReturn.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonButtonReturn> IDatamapFunctionService.CBaseButtonButtonReturn => CBaseButtonButtonReturn;


    public IDatamapFunctionOperator<CBaseButton, DHookCBaseButtonButtonBackHome> CBaseButtonButtonBackHome { get; } = manager.CBaseButtonButtonBackHome.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonButtonBackHome> IDatamapFunctionService.CBaseButtonButtonBackHome => CBaseButtonButtonBackHome;


    public IDatamapFunctionOperator<CBaseButton, DHookCBaseButtonButtonUse> CBaseButtonButtonUse { get; } = manager.CBaseButtonButtonUse.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonButtonUse> IDatamapFunctionService.CBaseButtonButtonUse => CBaseButtonButtonUse;


    public IDatamapFunctionOperator<CBaseButton, DHookCBaseButtonActivateTouch> CBaseButtonActivateTouch { get; } = manager.CBaseButtonActivateTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseButton, IDHookCBaseButtonActivateTouch> IDatamapFunctionService.CBaseButtonActivateTouch => CBaseButtonActivateTouch;


    public IDatamapFunctionOperator<CRevertSaved, DHookCRevertSavedLoadThink> CRevertSavedLoadThink { get; } = manager.CRevertSavedLoadThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CRevertSaved, IDHookCRevertSavedLoadThink> IDatamapFunctionService.CRevertSavedLoadThink => CRevertSavedLoadThink;


    public IDatamapFunctionOperator<CSoundEventOBBEntity, DHookCSoundEventOBBEntitySoundEventOBBThink> CSoundEventOBBEntitySoundEventOBBThink { get; } = manager.CSoundEventOBBEntitySoundEventOBBThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSoundEventOBBEntity, IDHookCSoundEventOBBEntitySoundEventOBBThink> IDatamapFunctionService.CSoundEventOBBEntitySoundEventOBBThink => CSoundEventOBBEntitySoundEventOBBThink;


    public IDatamapFunctionOperator<CSplineConstraint, DHookCSplineConstraintTransitionThink> CSplineConstraintTransitionThink { get; } = manager.CSplineConstraintTransitionThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSplineConstraint, IDHookCSplineConstraintTransitionThink> IDatamapFunctionService.CSplineConstraintTransitionThink => CSplineConstraintTransitionThink;


    public IDatamapFunctionOperator<CCSWeaponBase, DHookCCSWeaponBaseDefaultTouch> CCSWeaponBaseDefaultTouch { get; } = manager.CCSWeaponBaseDefaultTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CCSWeaponBase, IDHookCCSWeaponBaseDefaultTouch> IDatamapFunctionService.CCSWeaponBaseDefaultTouch => CCSWeaponBaseDefaultTouch;


    public IDatamapFunctionOperator<CCSWeaponBase, DHookCCSWeaponBaseRemoveUnownedWeaponThink> CCSWeaponBaseRemoveUnownedWeaponThink { get; } = manager.CCSWeaponBaseRemoveUnownedWeaponThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CCSWeaponBase, IDHookCCSWeaponBaseRemoveUnownedWeaponThink> IDatamapFunctionService.CCSWeaponBaseRemoveUnownedWeaponThink => CCSWeaponBaseRemoveUnownedWeaponThink;


    public IDatamapFunctionOperator<CChicken, DHookCChickenChickenTouch> CChickenChickenTouch { get; } = manager.CChickenChickenTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CChicken, IDHookCChickenChickenTouch> IDatamapFunctionService.CChickenChickenTouch => CChickenChickenTouch;


    public IDatamapFunctionOperator<CChicken, DHookCChickenChickenThink> CChickenChickenThink { get; } = manager.CChickenChickenThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CChicken, IDHookCChickenChickenThink> IDatamapFunctionService.CChickenChickenThink => CChickenChickenThink;


    public IDatamapFunctionOperator<CChicken, DHookCChickenChickenUse> CChickenChickenUse { get; } = manager.CChickenChickenUse.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CChicken, IDHookCChickenChickenUse> IDatamapFunctionService.CChickenChickenUse => CChickenChickenUse;


    public IDatamapFunctionOperator<CBaseAnimGraph, DHookCBaseAnimGraphChoreoServicesThink> CBaseAnimGraphChoreoServicesThink { get; } = manager.CBaseAnimGraphChoreoServicesThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseAnimGraph, IDHookCBaseAnimGraphChoreoServicesThink> IDatamapFunctionService.CBaseAnimGraphChoreoServicesThink => CBaseAnimGraphChoreoServicesThink;


    public IDatamapFunctionOperator<CParticleSystem, DHookCParticleSystemStartParticleSystemThink> CParticleSystemStartParticleSystemThink { get; } = manager.CParticleSystemStartParticleSystemThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CParticleSystem, IDHookCParticleSystemStartParticleSystemThink> IDatamapFunctionService.CParticleSystemStartParticleSystemThink => CParticleSystemStartParticleSystemThink;


    public IDatamapFunctionOperator<CBaseFlex, DHookCBaseFlexProcessSceneEventsThink> CBaseFlexProcessSceneEventsThink { get; } = manager.CBaseFlexProcessSceneEventsThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseFlex, IDHookCBaseFlexProcessSceneEventsThink> IDatamapFunctionService.CBaseFlexProcessSceneEventsThink => CBaseFlexProcessSceneEventsThink;


    public IDatamapFunctionOperator<CTriggerProximity, DHookCTriggerProximityMeasureThink> CTriggerProximityMeasureThink { get; } = manager.CTriggerProximityMeasureThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerProximity, IDHookCTriggerProximityMeasureThink> IDatamapFunctionService.CTriggerProximityMeasureThink => CTriggerProximityMeasureThink;


    public IDatamapFunctionOperator<CRagdollProp, DHookCRagdollPropSetDebrisThink> CRagdollPropSetDebrisThink { get; } = manager.CRagdollPropSetDebrisThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CRagdollProp, IDHookCRagdollPropSetDebrisThink> IDatamapFunctionService.CRagdollPropSetDebrisThink => CRagdollPropSetDebrisThink;


    public IDatamapFunctionOperator<CRagdollProp, DHookCRagdollPropClearFlagsThink> CRagdollPropClearFlagsThink { get; } = manager.CRagdollPropClearFlagsThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CRagdollProp, IDHookCRagdollPropClearFlagsThink> IDatamapFunctionService.CRagdollPropClearFlagsThink => CRagdollPropClearFlagsThink;


    public IDatamapFunctionOperator<CRagdollProp, DHookCRagdollPropFadeOutThink> CRagdollPropFadeOutThink { get; } = manager.CRagdollPropFadeOutThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CRagdollProp, IDHookCRagdollPropFadeOutThink> IDatamapFunctionService.CRagdollPropFadeOutThink => CRagdollPropFadeOutThink;


    public IDatamapFunctionOperator<CRagdollProp, DHookCRagdollPropSettleThink> CRagdollPropSettleThink { get; } = manager.CRagdollPropSettleThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CRagdollProp, IDHookCRagdollPropSettleThink> IDatamapFunctionService.CRagdollPropSettleThink => CRagdollPropSettleThink;


    public IDatamapFunctionOperator<CRagdollProp, DHookCRagdollPropAttachedItemsThink> CRagdollPropAttachedItemsThink { get; } = manager.CRagdollPropAttachedItemsThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CRagdollProp, IDHookCRagdollPropAttachedItemsThink> IDatamapFunctionService.CRagdollPropAttachedItemsThink => CRagdollPropAttachedItemsThink;


    public IDatamapFunctionOperator<CPointValueRemapper, DHookCPointValueRemapperUpdateThink> CPointValueRemapperUpdateThink { get; } = manager.CPointValueRemapperUpdateThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPointValueRemapper, IDHookCPointValueRemapperUpdateThink> IDatamapFunctionService.CPointValueRemapperUpdateThink => CPointValueRemapperUpdateThink;


    public IDatamapFunctionOperator<CBreakableProp, DHookCBreakablePropBreakThink> CBreakablePropBreakThink { get; } = manager.CBreakablePropBreakThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBreakableProp, IDHookCBreakablePropBreakThink> IDatamapFunctionService.CBreakablePropBreakThink => CBreakablePropBreakThink;


    public IDatamapFunctionOperator<CBreakableProp, DHookCBreakablePropRampToDefaultFadeScale> CBreakablePropRampToDefaultFadeScale { get; } = manager.CBreakablePropRampToDefaultFadeScale.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBreakableProp, IDHookCBreakablePropRampToDefaultFadeScale> IDatamapFunctionService.CBreakablePropRampToDefaultFadeScale => CBreakablePropRampToDefaultFadeScale;


    public IDatamapFunctionOperator<CGenericConstraint, DHookCGenericConstraintUpdateThink> CGenericConstraintUpdateThink { get; } = manager.CGenericConstraintUpdateThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CGenericConstraint, IDHookCGenericConstraintUpdateThink> IDatamapFunctionService.CGenericConstraintUpdateThink => CGenericConstraintUpdateThink;


    public IDatamapFunctionOperator<CItemSoda, DHookCItemSodaCanThink> CItemSodaCanThink { get; } = manager.CItemSodaCanThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CItemSoda, IDHookCItemSodaCanThink> IDatamapFunctionService.CItemSodaCanThink => CItemSodaCanThink;


    public IDatamapFunctionOperator<CFuncMover, DHookCFuncMoverLerpToNewPosition> CFuncMoverLerpToNewPosition { get; } = manager.CFuncMoverLerpToNewPosition.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncMover, IDHookCFuncMoverLerpToNewPosition> IDatamapFunctionService.CFuncMoverLerpToNewPosition => CFuncMoverLerpToNewPosition;


    public IDatamapFunctionOperator<CEnvWind, DHookCEnvWindWindThink> CEnvWindWindThink { get; } = manager.CEnvWindWindThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CEnvWind, IDHookCEnvWindWindThink> IDatamapFunctionService.CEnvWindWindThink => CEnvWindWindThink;


    public IDatamapFunctionOperator<CCSPlayerController, DHookCCSPlayerControllerPlayerForceTeamThink> CCSPlayerControllerPlayerForceTeamThink { get; } = manager.CCSPlayerControllerPlayerForceTeamThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CCSPlayerController, IDHookCCSPlayerControllerPlayerForceTeamThink> IDatamapFunctionService.CCSPlayerControllerPlayerForceTeamThink => CCSPlayerControllerPlayerForceTeamThink;


    public IDatamapFunctionOperator<CCSPlayerController, DHookCCSPlayerControllerResetForceTeamThink> CCSPlayerControllerResetForceTeamThink { get; } = manager.CCSPlayerControllerResetForceTeamThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CCSPlayerController, IDHookCCSPlayerControllerResetForceTeamThink> IDatamapFunctionService.CCSPlayerControllerResetForceTeamThink => CCSPlayerControllerResetForceTeamThink;


    public IDatamapFunctionOperator<CCSPlayerController, DHookCCSPlayerControllerResourceDataThink> CCSPlayerControllerResourceDataThink { get; } = manager.CCSPlayerControllerResourceDataThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CCSPlayerController, IDHookCCSPlayerControllerResourceDataThink> IDatamapFunctionService.CCSPlayerControllerResourceDataThink => CCSPlayerControllerResourceDataThink;


    public IDatamapFunctionOperator<CCSPlayerController, DHookCCSPlayerControllerInventoryUpdateThink> CCSPlayerControllerInventoryUpdateThink { get; } = manager.CCSPlayerControllerInventoryUpdateThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CCSPlayerController, IDHookCCSPlayerControllerInventoryUpdateThink> IDatamapFunctionService.CCSPlayerControllerInventoryUpdateThink => CCSPlayerControllerInventoryUpdateThink;


    public IDatamapFunctionOperator<CLogicActiveAutosave, DHookCLogicActiveAutosaveSaveThink> CLogicActiveAutosaveSaveThink { get; } = manager.CLogicActiveAutosaveSaveThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CLogicActiveAutosave, IDHookCLogicActiveAutosaveSaveThink> IDatamapFunctionService.CLogicActiveAutosaveSaveThink => CLogicActiveAutosaveSaveThink;


    public IDatamapFunctionOperator<CBaseEntity, DHookCBaseEntitySUB_Remove> CBaseEntitySUB_Remove { get; } = manager.CBaseEntitySUB_Remove.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_Remove> IDatamapFunctionService.CBaseEntitySUB_Remove => CBaseEntitySUB_Remove;


    public IDatamapFunctionOperator<CBaseEntity, DHookCBaseEntitySUB_RemoveIfUncarried> CBaseEntitySUB_RemoveIfUncarried { get; } = manager.CBaseEntitySUB_RemoveIfUncarried.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_RemoveIfUncarried> IDatamapFunctionService.CBaseEntitySUB_RemoveIfUncarried => CBaseEntitySUB_RemoveIfUncarried;


    public IDatamapFunctionOperator<CBaseEntity, DHookCBaseEntitySUB_DoNothing> CBaseEntitySUB_DoNothing { get; } = manager.CBaseEntitySUB_DoNothing.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_DoNothing> IDatamapFunctionService.CBaseEntitySUB_DoNothing => CBaseEntitySUB_DoNothing;


    public IDatamapFunctionOperator<CBaseEntity, DHookCBaseEntitySUB_Vanish> CBaseEntitySUB_Vanish { get; } = manager.CBaseEntitySUB_Vanish.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_Vanish> IDatamapFunctionService.CBaseEntitySUB_Vanish => CBaseEntitySUB_Vanish;


    public IDatamapFunctionOperator<CBaseEntity, DHookCBaseEntitySUB_CallUseToggle> CBaseEntitySUB_CallUseToggle { get; } = manager.CBaseEntitySUB_CallUseToggle.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_CallUseToggle> IDatamapFunctionService.CBaseEntitySUB_CallUseToggle => CBaseEntitySUB_CallUseToggle;


    public IDatamapFunctionOperator<CBaseEntity, DHookCBaseEntitySUB_KillSelf> CBaseEntitySUB_KillSelf { get; } = manager.CBaseEntitySUB_KillSelf.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_KillSelf> IDatamapFunctionService.CBaseEntitySUB_KillSelf => CBaseEntitySUB_KillSelf;


    public IDatamapFunctionOperator<CBaseEntity, DHookCBaseEntitySUB_KillSelfIfUncarried> CBaseEntitySUB_KillSelfIfUncarried { get; } = manager.CBaseEntitySUB_KillSelfIfUncarried.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntitySUB_KillSelfIfUncarried> IDatamapFunctionService.CBaseEntitySUB_KillSelfIfUncarried => CBaseEntitySUB_KillSelfIfUncarried;


    public IDatamapFunctionOperator<CBaseEntity, DHookCBaseEntityFakeScriptThinkFunc> CBaseEntityFakeScriptThinkFunc { get; } = manager.CBaseEntityFakeScriptThinkFunc.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntityFakeScriptThinkFunc> IDatamapFunctionService.CBaseEntityFakeScriptThinkFunc => CBaseEntityFakeScriptThinkFunc;


    public IDatamapFunctionOperator<CBaseEntity, DHookCBaseEntityClearNavIgnoreContentsThink> CBaseEntityClearNavIgnoreContentsThink { get; } = manager.CBaseEntityClearNavIgnoreContentsThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseEntity, IDHookCBaseEntityClearNavIgnoreContentsThink> IDatamapFunctionService.CBaseEntityClearNavIgnoreContentsThink => CBaseEntityClearNavIgnoreContentsThink;


    public IDatamapFunctionOperator<CFuncRotator, DHookCFuncRotatorRotateThink> CFuncRotatorRotateThink { get; } = manager.CFuncRotatorRotateThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncRotator, IDHookCFuncRotatorRotateThink> IDatamapFunctionService.CFuncRotatorRotateThink => CFuncRotatorRotateThink;


    public IDatamapFunctionOperator<CPointPush, DHookCPointPushPushThink> CPointPushPushThink { get; } = manager.CPointPushPushThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPointPush, IDHookCPointPushPushThink> IDatamapFunctionService.CPointPushPushThink => CPointPushPushThink;


    public IDatamapFunctionOperator<CTriggerFan, DHookCTriggerFanPushThink> CTriggerFanPushThink { get; } = manager.CTriggerFanPushThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerFan, IDHookCTriggerFanPushThink> IDatamapFunctionService.CTriggerFanPushThink => CTriggerFanPushThink;


    public IDatamapFunctionOperator<CDynamicProp, DHookCDynamicPropAnimThink> CDynamicPropAnimThink { get; } = manager.CDynamicPropAnimThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CDynamicProp, IDHookCDynamicPropAnimThink> IDatamapFunctionService.CDynamicPropAnimThink => CDynamicPropAnimThink;


    public IDatamapFunctionOperator<CHostageRescueZone, DHookCHostageRescueZoneCHostageRescueZoneShim_Touch> CHostageRescueZoneCHostageRescueZoneShim_Touch { get; } = manager.CHostageRescueZoneCHostageRescueZoneShim_Touch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CHostageRescueZone, IDHookCHostageRescueZoneCHostageRescueZoneShim_Touch> IDatamapFunctionService.CHostageRescueZoneCHostageRescueZoneShim_Touch => CHostageRescueZoneCHostageRescueZoneShim_Touch;


    public IDatamapFunctionOperator<CBombTarget, DHookCBombTargetCBombTargetShim_Touch> CBombTargetCBombTargetShim_Touch { get; } = manager.CBombTargetCBombTargetShim_Touch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBombTarget, IDHookCBombTargetCBombTargetShim_Touch> IDatamapFunctionService.CBombTargetCBombTargetShim_Touch => CBombTargetCBombTargetShim_Touch;


    public IDatamapFunctionOperator<CBombTarget, DHookCBombTargetCBombTargetShim_BombTargetUse> CBombTargetCBombTargetShim_BombTargetUse { get; } = manager.CBombTargetCBombTargetShim_BombTargetUse.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBombTarget, IDHookCBombTargetCBombTargetShim_BombTargetUse> IDatamapFunctionService.CBombTargetCBombTargetShim_BombTargetUse => CBombTargetCBombTargetShim_BombTargetUse;


    public IDatamapFunctionOperator<CSoundEventEntity, DHookCSoundEventEntitySoundFinishedThink> CSoundEventEntitySoundFinishedThink { get; } = manager.CSoundEventEntitySoundFinishedThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSoundEventEntity, IDHookCSoundEventEntitySoundFinishedThink> IDatamapFunctionService.CSoundEventEntitySoundFinishedThink => CSoundEventEntitySoundFinishedThink;


    public IDatamapFunctionOperator<CFuncTrackTrain, DHookCFuncTrackTrainNext> CFuncTrackTrainNext { get; } = manager.CFuncTrackTrainNext.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncTrackTrain, IDHookCFuncTrackTrainNext> IDatamapFunctionService.CFuncTrackTrainNext => CFuncTrackTrainNext;


    public IDatamapFunctionOperator<CFuncTrackTrain, DHookCFuncTrackTrainFind> CFuncTrackTrainFind { get; } = manager.CFuncTrackTrainFind.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncTrackTrain, IDHookCFuncTrackTrainFind> IDatamapFunctionService.CFuncTrackTrainFind => CFuncTrackTrainFind;


    public IDatamapFunctionOperator<CFuncTrackTrain, DHookCFuncTrackTrainNearestPath> CFuncTrackTrainNearestPath { get; } = manager.CFuncTrackTrainNearestPath.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncTrackTrain, IDHookCFuncTrackTrainNearestPath> IDatamapFunctionService.CFuncTrackTrainNearestPath => CFuncTrackTrainNearestPath;


    public IDatamapFunctionOperator<CFuncTrackTrain, DHookCFuncTrackTrainDeadEnd> CFuncTrackTrainDeadEnd { get; } = manager.CFuncTrackTrainDeadEnd.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncTrackTrain, IDHookCFuncTrackTrainDeadEnd> IDatamapFunctionService.CFuncTrackTrainDeadEnd => CFuncTrackTrainDeadEnd;


    public IDatamapFunctionOperator<CMomentaryRotButton, DHookCMomentaryRotButtonUseMoveDone> CMomentaryRotButtonUseMoveDone { get; } = manager.CMomentaryRotButtonUseMoveDone.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CMomentaryRotButton, IDHookCMomentaryRotButtonUseMoveDone> IDatamapFunctionService.CMomentaryRotButtonUseMoveDone => CMomentaryRotButtonUseMoveDone;


    public IDatamapFunctionOperator<CMomentaryRotButton, DHookCMomentaryRotButtonReturnMoveDone> CMomentaryRotButtonReturnMoveDone { get; } = manager.CMomentaryRotButtonReturnMoveDone.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CMomentaryRotButton, IDHookCMomentaryRotButtonReturnMoveDone> IDatamapFunctionService.CMomentaryRotButtonReturnMoveDone => CMomentaryRotButtonReturnMoveDone;


    public IDatamapFunctionOperator<CMomentaryRotButton, DHookCMomentaryRotButtonSetPositionMoveDone> CMomentaryRotButtonSetPositionMoveDone { get; } = manager.CMomentaryRotButtonSetPositionMoveDone.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CMomentaryRotButton, IDHookCMomentaryRotButtonSetPositionMoveDone> IDatamapFunctionService.CMomentaryRotButtonSetPositionMoveDone => CMomentaryRotButtonSetPositionMoveDone;


    public IDatamapFunctionOperator<CMomentaryRotButton, DHookCMomentaryRotButtonUpdateThink> CMomentaryRotButtonUpdateThink { get; } = manager.CMomentaryRotButtonUpdateThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CMomentaryRotButton, IDHookCMomentaryRotButtonUpdateThink> IDatamapFunctionService.CMomentaryRotButtonUpdateThink => CMomentaryRotButtonUpdateThink;


    public IDatamapFunctionOperator<CTriggerSndSosOpvar, DHookCTriggerSndSosOpvarSndSosTriggerOpvarWaitOver> CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver { get; } = manager.CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerSndSosOpvar, IDHookCTriggerSndSosOpvarSndSosTriggerOpvarWaitOver> IDatamapFunctionService.CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver => CTriggerSndSosOpvarSndSosTriggerOpvarWaitOver;


    public IDatamapFunctionOperator<CVoteController, DHookCVoteControllerVoteControllerThink> CVoteControllerVoteControllerThink { get; } = manager.CVoteControllerVoteControllerThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CVoteController, IDHookCVoteControllerVoteControllerThink> IDatamapFunctionService.CVoteControllerVoteControllerThink => CVoteControllerVoteControllerThink;


    public IDatamapFunctionOperator<CPhysForce, DHookCPhysForceForceOff> CPhysForceForceOff { get; } = manager.CPhysForceForceOff.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPhysForce, IDHookCPhysForceForceOff> IDatamapFunctionService.CPhysForceForceOff => CPhysForceForceOff;


    public IDatamapFunctionOperator<CPhysForce, DHookCPhysForceInitialThink> CPhysForceInitialThink { get; } = manager.CPhysForceInitialThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPhysForce, IDHookCPhysForceInitialThink> IDatamapFunctionService.CPhysForceInitialThink => CPhysForceInitialThink;


    public IDatamapFunctionOperator<CSoundOpvarSetPointEntity, DHookCSoundOpvarSetPointEntitySetOpvarThink> CSoundOpvarSetPointEntitySetOpvarThink { get; } = manager.CSoundOpvarSetPointEntitySetOpvarThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSoundOpvarSetPointEntity, IDHookCSoundOpvarSetPointEntitySetOpvarThink> IDatamapFunctionService.CSoundOpvarSetPointEntitySetOpvarThink => CSoundOpvarSetPointEntitySetOpvarThink;


    public IDatamapFunctionOperator<CDecoyProjectile, DHookCDecoyProjectileThink_Detonate> CDecoyProjectileThink_Detonate { get; } = manager.CDecoyProjectileThink_Detonate.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CDecoyProjectile, IDHookCDecoyProjectileThink_Detonate> IDatamapFunctionService.CDecoyProjectileThink_Detonate => CDecoyProjectileThink_Detonate;


    public IDatamapFunctionOperator<CDecoyProjectile, DHookCDecoyProjectileGunfireThink> CDecoyProjectileGunfireThink { get; } = manager.CDecoyProjectileGunfireThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CDecoyProjectile, IDHookCDecoyProjectileGunfireThink> IDatamapFunctionService.CDecoyProjectileGunfireThink => CDecoyProjectileGunfireThink;


    public IDatamapFunctionOperator<CPlantedC4, DHookCPlantedC4C4Think> CPlantedC4C4Think { get; } = manager.CPlantedC4C4Think.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPlantedC4, IDHookCPlantedC4C4Think> IDatamapFunctionService.CPlantedC4C4Think => CPlantedC4C4Think;


    public IDatamapFunctionOperator<CItemGenericTriggerHelper, DHookCItemGenericTriggerHelperItemGenericTriggerHelperTouch> CItemGenericTriggerHelperItemGenericTriggerHelperTouch { get; } = manager.CItemGenericTriggerHelperItemGenericTriggerHelperTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CItemGenericTriggerHelper, IDHookCItemGenericTriggerHelperItemGenericTriggerHelperTouch> IDatamapFunctionService.CItemGenericTriggerHelperItemGenericTriggerHelperTouch => CItemGenericTriggerHelperItemGenericTriggerHelperTouch;


    public IDatamapFunctionOperator<CPointOrient, DHookCPointOrientReorientThink> CPointOrientReorientThink { get; } = manager.CPointOrientReorientThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPointOrient, IDHookCPointOrientReorientThink> IDatamapFunctionService.CPointOrientReorientThink => CPointOrientReorientThink;


    public IDatamapFunctionOperator<CMultiLightProxy, DHookCMultiLightProxyRestoreFlashlightThink> CMultiLightProxyRestoreFlashlightThink { get; } = manager.CMultiLightProxyRestoreFlashlightThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CMultiLightProxy, IDHookCMultiLightProxyRestoreFlashlightThink> IDatamapFunctionService.CMultiLightProxyRestoreFlashlightThink => CMultiLightProxyRestoreFlashlightThink;


    public IDatamapFunctionOperator<CMultiLightProxy, DHookCMultiLightProxyApproachBrightnessThink> CMultiLightProxyApproachBrightnessThink { get; } = manager.CMultiLightProxyApproachBrightnessThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CMultiLightProxy, IDHookCMultiLightProxyApproachBrightnessThink> IDatamapFunctionService.CMultiLightProxyApproachBrightnessThink => CMultiLightProxyApproachBrightnessThink;


    public IDatamapFunctionOperator<CAmbientGeneric, DHookCAmbientGenericRampThink> CAmbientGenericRampThink { get; } = manager.CAmbientGenericRampThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CAmbientGeneric, IDHookCAmbientGenericRampThink> IDatamapFunctionService.CAmbientGenericRampThink => CAmbientGenericRampThink;


    public IDatamapFunctionOperator<CSoundEventSphereEntity, DHookCSoundEventSphereEntitySoundEventSphereThink> CSoundEventSphereEntitySoundEventSphereThink { get; } = manager.CSoundEventSphereEntitySoundEventSphereThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSoundEventSphereEntity, IDHookCSoundEventSphereEntitySoundEventSphereThink> IDatamapFunctionService.CSoundEventSphereEntitySoundEventSphereThink => CSoundEventSphereEntitySoundEventSphereThink;


    public IDatamapFunctionOperator<CBreakable, DHookCBreakableDie> CBreakableDie { get; } = manager.CBreakableDie.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBreakable, IDHookCBreakableDie> IDatamapFunctionService.CBreakableDie => CBreakableDie;


    public IDatamapFunctionOperator<CTriggerHurt, DHookCTriggerHurtRadiationThink> CTriggerHurtRadiationThink { get; } = manager.CTriggerHurtRadiationThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerHurt, IDHookCTriggerHurtRadiationThink> IDatamapFunctionService.CTriggerHurtRadiationThink => CTriggerHurtRadiationThink;


    public IDatamapFunctionOperator<CTriggerHurt, DHookCTriggerHurtHurtThink> CTriggerHurtHurtThink { get; } = manager.CTriggerHurtHurtThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerHurt, IDHookCTriggerHurtHurtThink> IDatamapFunctionService.CTriggerHurtHurtThink => CTriggerHurtHurtThink;


    public IDatamapFunctionOperator<CTriggerHurt, DHookCTriggerHurtNavThink> CTriggerHurtNavThink { get; } = manager.CTriggerHurtNavThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerHurt, IDHookCTriggerHurtNavThink> IDatamapFunctionService.CTriggerHurtNavThink => CTriggerHurtNavThink;


    public IDatamapFunctionOperator<CScriptedSequence, DHookCScriptedSequenceScriptThink> CScriptedSequenceScriptThink { get; } = manager.CScriptedSequenceScriptThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CScriptedSequence, IDHookCScriptedSequenceScriptThink> IDatamapFunctionService.CScriptedSequenceScriptThink => CScriptedSequenceScriptThink;


    public IDatamapFunctionOperator<CEnvWindController, DHookCEnvWindControllerWindThink> CEnvWindControllerWindThink { get; } = manager.CEnvWindControllerWindThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CEnvWindController, IDHookCEnvWindControllerWindThink> IDatamapFunctionService.CEnvWindControllerWindThink => CEnvWindControllerWindThink;


    public IDatamapFunctionOperator<CTriggerMultiple, DHookCTriggerMultipleMultiTouch> CTriggerMultipleMultiTouch { get; } = manager.CTriggerMultipleMultiTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerMultiple, IDHookCTriggerMultipleMultiTouch> IDatamapFunctionService.CTriggerMultipleMultiTouch => CTriggerMultipleMultiTouch;


    public IDatamapFunctionOperator<CTriggerMultiple, DHookCTriggerMultipleMultiWaitOver> CTriggerMultipleMultiWaitOver { get; } = manager.CTriggerMultipleMultiWaitOver.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerMultiple, IDHookCTriggerMultipleMultiWaitOver> IDatamapFunctionService.CTriggerMultipleMultiWaitOver => CTriggerMultipleMultiWaitOver;


    public IDatamapFunctionOperator<CFuncRotating, DHookCFuncRotatingSpinUpMove> CFuncRotatingSpinUpMove { get; } = manager.CFuncRotatingSpinUpMove.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncRotating, IDHookCFuncRotatingSpinUpMove> IDatamapFunctionService.CFuncRotatingSpinUpMove => CFuncRotatingSpinUpMove;


    public IDatamapFunctionOperator<CFuncRotating, DHookCFuncRotatingSpinDownMove> CFuncRotatingSpinDownMove { get; } = manager.CFuncRotatingSpinDownMove.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncRotating, IDHookCFuncRotatingSpinDownMove> IDatamapFunctionService.CFuncRotatingSpinDownMove => CFuncRotatingSpinDownMove;


    public IDatamapFunctionOperator<CFuncRotating, DHookCFuncRotatingHurtTouch> CFuncRotatingHurtTouch { get; } = manager.CFuncRotatingHurtTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncRotating, IDHookCFuncRotatingHurtTouch> IDatamapFunctionService.CFuncRotatingHurtTouch => CFuncRotatingHurtTouch;


    public IDatamapFunctionOperator<CFuncRotating, DHookCFuncRotatingRotatingUse> CFuncRotatingRotatingUse { get; } = manager.CFuncRotatingRotatingUse.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncRotating, IDHookCFuncRotatingRotatingUse> IDatamapFunctionService.CFuncRotatingRotatingUse => CFuncRotatingRotatingUse;


    public IDatamapFunctionOperator<CFuncRotating, DHookCFuncRotatingRotateMove> CFuncRotatingRotateMove { get; } = manager.CFuncRotatingRotateMove.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncRotating, IDHookCFuncRotatingRotateMove> IDatamapFunctionService.CFuncRotatingRotateMove => CFuncRotatingRotateMove;


    public IDatamapFunctionOperator<CFuncRotating, DHookCFuncRotatingReverseMove> CFuncRotatingReverseMove { get; } = manager.CFuncRotatingReverseMove.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncRotating, IDHookCFuncRotatingReverseMove> IDatamapFunctionService.CFuncRotatingReverseMove => CFuncRotatingReverseMove;


    public IDatamapFunctionOperator<CCSPlayerPawn, DHookCCSPlayerPawnCheckStuffThink> CCSPlayerPawnCheckStuffThink { get; } = manager.CCSPlayerPawnCheckStuffThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CCSPlayerPawn, IDHookCCSPlayerPawnCheckStuffThink> IDatamapFunctionService.CCSPlayerPawnCheckStuffThink => CCSPlayerPawnCheckStuffThink;


    public IDatamapFunctionOperator<CCSPlayerPawn, DHookCCSPlayerPawnPushawayThink> CCSPlayerPawnPushawayThink { get; } = manager.CCSPlayerPawnPushawayThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CCSPlayerPawn, IDHookCCSPlayerPawnPushawayThink> IDatamapFunctionService.CCSPlayerPawnPushawayThink => CCSPlayerPawnPushawayThink;


    public IDatamapFunctionOperator<CMapVetoPickController, DHookCMapVetoPickControllerVoteControllerThink> CMapVetoPickControllerVoteControllerThink { get; } = manager.CMapVetoPickControllerVoteControllerThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CMapVetoPickController, IDHookCMapVetoPickControllerVoteControllerThink> IDatamapFunctionService.CMapVetoPickControllerVoteControllerThink => CMapVetoPickControllerVoteControllerThink;


    public IDatamapFunctionOperator<CEntityDissolve, DHookCEntityDissolveDissolveThink> CEntityDissolveDissolveThink { get; } = manager.CEntityDissolveDissolveThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CEntityDissolve, IDHookCEntityDissolveDissolveThink> IDatamapFunctionService.CEntityDissolveDissolveThink => CEntityDissolveDissolveThink;


    public IDatamapFunctionOperator<CEntityDissolve, DHookCEntityDissolveElectrocuteThink> CEntityDissolveElectrocuteThink { get; } = manager.CEntityDissolveElectrocuteThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CEntityDissolve, IDHookCEntityDissolveElectrocuteThink> IDatamapFunctionService.CEntityDissolveElectrocuteThink => CEntityDissolveElectrocuteThink;


    public IDatamapFunctionOperator<CLogicMeasureMovement, DHookCLogicMeasureMovementMeasureThink> CLogicMeasureMovementMeasureThink { get; } = manager.CLogicMeasureMovementMeasureThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CLogicMeasureMovement, IDHookCLogicMeasureMovementMeasureThink> IDatamapFunctionService.CLogicMeasureMovementMeasureThink => CLogicMeasureMovementMeasureThink;


    public IDatamapFunctionOperator<CTriggerImpact, DHookCTriggerImpactDisable> CTriggerImpactDisable { get; } = manager.CTriggerImpactDisable.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerImpact, IDHookCTriggerImpactDisable> IDatamapFunctionService.CTriggerImpactDisable => CTriggerImpactDisable;


    public IDatamapFunctionOperator<CEnvBeam, DHookCEnvBeamStrikeThink> CEnvBeamStrikeThink { get; } = manager.CEnvBeamStrikeThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CEnvBeam, IDHookCEnvBeamStrikeThink> IDatamapFunctionService.CEnvBeamStrikeThink => CEnvBeamStrikeThink;


    public IDatamapFunctionOperator<CEnvBeam, DHookCEnvBeamUpdateThink> CEnvBeamUpdateThink { get; } = manager.CEnvBeamUpdateThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CEnvBeam, IDHookCEnvBeamUpdateThink> IDatamapFunctionService.CEnvBeamUpdateThink => CEnvBeamUpdateThink;


    public IDatamapFunctionOperator<CCSPlayerResource, DHookCCSPlayerResourceResourceThink> CCSPlayerResourceResourceThink { get; } = manager.CCSPlayerResourceResourceThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CCSPlayerResource, IDHookCCSPlayerResourceResourceThink> IDatamapFunctionService.CCSPlayerResourceResourceThink => CCSPlayerResourceResourceThink;


    public IDatamapFunctionOperator<CBaseModelEntity, DHookCBaseModelEntitySUB_DissolveIfUncarried> CBaseModelEntitySUB_DissolveIfUncarried { get; } = manager.CBaseModelEntitySUB_DissolveIfUncarried.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_DissolveIfUncarried> IDatamapFunctionService.CBaseModelEntitySUB_DissolveIfUncarried => CBaseModelEntitySUB_DissolveIfUncarried;


    public IDatamapFunctionOperator<CBaseModelEntity, DHookCBaseModelEntitySUB_StartFadeOut> CBaseModelEntitySUB_StartFadeOut { get; } = manager.CBaseModelEntitySUB_StartFadeOut.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_StartFadeOut> IDatamapFunctionService.CBaseModelEntitySUB_StartFadeOut => CBaseModelEntitySUB_StartFadeOut;


    public IDatamapFunctionOperator<CBaseModelEntity, DHookCBaseModelEntitySUB_StartFadeOutInstant> CBaseModelEntitySUB_StartFadeOutInstant { get; } = manager.CBaseModelEntitySUB_StartFadeOutInstant.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_StartFadeOutInstant> IDatamapFunctionService.CBaseModelEntitySUB_StartFadeOutInstant => CBaseModelEntitySUB_StartFadeOutInstant;


    public IDatamapFunctionOperator<CBaseModelEntity, DHookCBaseModelEntitySUB_FadeOut> CBaseModelEntitySUB_FadeOut { get; } = manager.CBaseModelEntitySUB_FadeOut.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_FadeOut> IDatamapFunctionService.CBaseModelEntitySUB_FadeOut => CBaseModelEntitySUB_FadeOut;


    public IDatamapFunctionOperator<CBaseModelEntity, DHookCBaseModelEntitySUB_StartShadowFadeOut> CBaseModelEntitySUB_StartShadowFadeOut { get; } = manager.CBaseModelEntitySUB_StartShadowFadeOut.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_StartShadowFadeOut> IDatamapFunctionService.CBaseModelEntitySUB_StartShadowFadeOut => CBaseModelEntitySUB_StartShadowFadeOut;


    public IDatamapFunctionOperator<CBaseModelEntity, DHookCBaseModelEntitySUB_PerformShadowFadeOut> CBaseModelEntitySUB_PerformShadowFadeOut { get; } = manager.CBaseModelEntitySUB_PerformShadowFadeOut.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_PerformShadowFadeOut> IDatamapFunctionService.CBaseModelEntitySUB_PerformShadowFadeOut => CBaseModelEntitySUB_PerformShadowFadeOut;


    public IDatamapFunctionOperator<CBaseModelEntity, DHookCBaseModelEntitySUB_StartShadowFadeIn> CBaseModelEntitySUB_StartShadowFadeIn { get; } = manager.CBaseModelEntitySUB_StartShadowFadeIn.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_StartShadowFadeIn> IDatamapFunctionService.CBaseModelEntitySUB_StartShadowFadeIn => CBaseModelEntitySUB_StartShadowFadeIn;


    public IDatamapFunctionOperator<CBaseModelEntity, DHookCBaseModelEntitySUB_PerformShadowFadeIn> CBaseModelEntitySUB_PerformShadowFadeIn { get; } = manager.CBaseModelEntitySUB_PerformShadowFadeIn.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_PerformShadowFadeIn> IDatamapFunctionService.CBaseModelEntitySUB_PerformShadowFadeIn => CBaseModelEntitySUB_PerformShadowFadeIn;


    public IDatamapFunctionOperator<CBaseModelEntity, DHookCBaseModelEntitySUB_StopShadowFade> CBaseModelEntitySUB_StopShadowFade { get; } = manager.CBaseModelEntitySUB_StopShadowFade.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CBaseModelEntity, IDHookCBaseModelEntitySUB_StopShadowFade> IDatamapFunctionService.CBaseModelEntitySUB_StopShadowFade => CBaseModelEntitySUB_StopShadowFade;


    public IDatamapFunctionOperator<CSoundOpvarSetAABBEntity, DHookCSoundOpvarSetAABBEntitySetOpvarThink> CSoundOpvarSetAABBEntitySetOpvarThink { get; } = manager.CSoundOpvarSetAABBEntitySetOpvarThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSoundOpvarSetAABBEntity, IDHookCSoundOpvarSetAABBEntitySetOpvarThink> IDatamapFunctionService.CSoundOpvarSetAABBEntitySetOpvarThink => CSoundOpvarSetAABBEntitySetOpvarThink;


    public IDatamapFunctionOperator<CFuncTrainControls, DHookCFuncTrainControlsFind> CFuncTrainControlsFind { get; } = manager.CFuncTrainControlsFind.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncTrainControls, IDHookCFuncTrainControlsFind> IDatamapFunctionService.CFuncTrainControlsFind => CFuncTrainControlsFind;


    public IDatamapFunctionOperator<CPhysicsPropRespawnable, DHookCPhysicsPropRespawnableMaterialize> CPhysicsPropRespawnableMaterialize { get; } = manager.CPhysicsPropRespawnableMaterialize.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPhysicsPropRespawnable, IDHookCPhysicsPropRespawnableMaterialize> IDatamapFunctionService.CPhysicsPropRespawnableMaterialize => CPhysicsPropRespawnableMaterialize;


    public IDatamapFunctionOperator<CFishPool, DHookCFishPoolUpdate> CFishPoolUpdate { get; } = manager.CFishPoolUpdate.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFishPool, IDHookCFishPoolUpdate> IDatamapFunctionService.CFishPoolUpdate => CFishPoolUpdate;


    public IDatamapFunctionOperator<CTriggerSoundscape, DHookCTriggerSoundscapePlayerUpdateThink> CTriggerSoundscapePlayerUpdateThink { get; } = manager.CTriggerSoundscapePlayerUpdateThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerSoundscape, IDHookCTriggerSoundscapePlayerUpdateThink> IDatamapFunctionService.CTriggerSoundscapePlayerUpdateThink => CTriggerSoundscapePlayerUpdateThink;


    public IDatamapFunctionOperator<CItemGeneric, DHookCItemGenericItemGenericTouch> CItemGenericItemGenericTouch { get; } = manager.CItemGenericItemGenericTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CItemGeneric, IDHookCItemGenericItemGenericTouch> IDatamapFunctionService.CItemGenericItemGenericTouch => CItemGenericItemGenericTouch;


    public IDatamapFunctionOperator<CTriggerActiveWeaponDetect, DHookCTriggerActiveWeaponDetectActiveWeaponThink> CTriggerActiveWeaponDetectActiveWeaponThink { get; } = manager.CTriggerActiveWeaponDetectActiveWeaponThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerActiveWeaponDetect, IDHookCTriggerActiveWeaponDetectActiveWeaponThink> IDatamapFunctionService.CTriggerActiveWeaponDetectActiveWeaponThink => CTriggerActiveWeaponDetectActiveWeaponThink;


    public IDatamapFunctionOperator<CFogController, DHookCFogControllerSetLerpValues> CFogControllerSetLerpValues { get; } = manager.CFogControllerSetLerpValues.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFogController, IDHookCFogControllerSetLerpValues> IDatamapFunctionService.CFogControllerSetLerpValues => CFogControllerSetLerpValues;


    public IDatamapFunctionOperator<CFuncTrackChange, DHookCFuncTrackChangeFind> CFuncTrackChangeFind { get; } = manager.CFuncTrackChangeFind.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncTrackChange, IDHookCFuncTrackChangeFind> IDatamapFunctionService.CFuncTrackChangeFind => CFuncTrackChangeFind;


    public IDatamapFunctionOperator<CTriggerLook, DHookCTriggerLookTimeoutThink> CTriggerLookTimeoutThink { get; } = manager.CTriggerLookTimeoutThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerLook, IDHookCTriggerLookTimeoutThink> IDatamapFunctionService.CTriggerLookTimeoutThink => CTriggerLookTimeoutThink;


    public IDatamapFunctionOperator<CSoundOpvarSetOBBEntity, DHookCSoundOpvarSetOBBEntitySetOpvarThink> CSoundOpvarSetOBBEntitySetOpvarThink { get; } = manager.CSoundOpvarSetOBBEntitySetOpvarThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CSoundOpvarSetOBBEntity, IDHookCSoundOpvarSetOBBEntitySetOpvarThink> IDatamapFunctionService.CSoundOpvarSetOBBEntitySetOpvarThink => CSoundOpvarSetOBBEntitySetOpvarThink;


    public IDatamapFunctionOperator<CColorCorrectionVolume, DHookCColorCorrectionVolumeThinkFunc> CColorCorrectionVolumeThinkFunc { get; } = manager.CColorCorrectionVolumeThinkFunc.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CColorCorrectionVolume, IDHookCColorCorrectionVolumeThinkFunc> IDatamapFunctionService.CColorCorrectionVolumeThinkFunc => CColorCorrectionVolumeThinkFunc;


    public IDatamapFunctionOperator<CWaterBullet, DHookCWaterBulletBulletThink> CWaterBulletBulletThink { get; } = manager.CWaterBulletBulletThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CWaterBullet, IDHookCWaterBulletBulletThink> IDatamapFunctionService.CWaterBulletBulletThink => CWaterBulletBulletThink;


    public IDatamapFunctionOperator<CFuncPlat, DHookCFuncPlatPlatUse> CFuncPlatPlatUse { get; } = manager.CFuncPlatPlatUse.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncPlat, IDHookCFuncPlatPlatUse> IDatamapFunctionService.CFuncPlatPlatUse => CFuncPlatPlatUse;


    public IDatamapFunctionOperator<CFuncPlat, DHookCFuncPlatCallGoDown> CFuncPlatCallGoDown { get; } = manager.CFuncPlatCallGoDown.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncPlat, IDHookCFuncPlatCallGoDown> IDatamapFunctionService.CFuncPlatCallGoDown => CFuncPlatCallGoDown;


    public IDatamapFunctionOperator<CFuncPlat, DHookCFuncPlatCallHitTop> CFuncPlatCallHitTop { get; } = manager.CFuncPlatCallHitTop.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncPlat, IDHookCFuncPlatCallHitTop> IDatamapFunctionService.CFuncPlatCallHitTop => CFuncPlatCallHitTop;


    public IDatamapFunctionOperator<CFuncPlat, DHookCFuncPlatCallHitBottom> CFuncPlatCallHitBottom { get; } = manager.CFuncPlatCallHitBottom.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CFuncPlat, IDHookCFuncPlatCallHitBottom> IDatamapFunctionService.CFuncPlatCallHitBottom => CFuncPlatCallHitBottom;


    public IDatamapFunctionOperator<CPhysImpact, DHookCPhysImpactPointAtEntity> CPhysImpactPointAtEntity { get; } = manager.CPhysImpactPointAtEntity.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPhysImpact, IDHookCPhysImpactPointAtEntity> IDatamapFunctionService.CPhysImpactPointAtEntity => CPhysImpactPointAtEntity;


    public IDatamapFunctionOperator<CTriggerLerpObject, DHookCTriggerLerpObjectLerpThink> CTriggerLerpObjectLerpThink { get; } = manager.CTriggerLerpObjectLerpThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerLerpObject, IDHookCTriggerLerpObjectLerpThink> IDatamapFunctionService.CTriggerLerpObjectLerpThink => CTriggerLerpObjectLerpThink;


    public IDatamapFunctionOperator<CTriggerLerpObject, DHookCTriggerLerpObjectUnsetWaitForEntity> CTriggerLerpObjectUnsetWaitForEntity { get; } = manager.CTriggerLerpObjectUnsetWaitForEntity.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerLerpObject, IDHookCTriggerLerpObjectUnsetWaitForEntity> IDatamapFunctionService.CTriggerLerpObjectUnsetWaitForEntity => CTriggerLerpObjectUnsetWaitForEntity;


    public IDatamapFunctionOperator<CTriggerLerpObject, DHookCTriggerLerpObjectAttachedEntityThink> CTriggerLerpObjectAttachedEntityThink { get; } = manager.CTriggerLerpObjectAttachedEntityThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CTriggerLerpObject, IDHookCTriggerLerpObjectAttachedEntityThink> IDatamapFunctionService.CTriggerLerpObjectAttachedEntityThink => CTriggerLerpObjectAttachedEntityThink;


    public IDatamapFunctionOperator<CPathMoverEntitySpawner, DHookCPathMoverEntitySpawnerSpawnThink> CPathMoverEntitySpawnerSpawnThink { get; } = manager.CPathMoverEntitySpawnerSpawnThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPathMoverEntitySpawner, IDHookCPathMoverEntitySpawnerSpawnThink> IDatamapFunctionService.CPathMoverEntitySpawnerSpawnThink => CPathMoverEntitySpawnerSpawnThink;


    public IDatamapFunctionOperator<CPointCommentaryNode, DHookCPointCommentaryNodeSpinThink> CPointCommentaryNodeSpinThink { get; } = manager.CPointCommentaryNodeSpinThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPointCommentaryNode, IDHookCPointCommentaryNodeSpinThink> IDatamapFunctionService.CPointCommentaryNodeSpinThink => CPointCommentaryNodeSpinThink;


    public IDatamapFunctionOperator<CPointCommentaryNode, DHookCPointCommentaryNodeUpdateViewThink> CPointCommentaryNodeUpdateViewThink { get; } = manager.CPointCommentaryNodeUpdateViewThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPointCommentaryNode, IDHookCPointCommentaryNodeUpdateViewThink> IDatamapFunctionService.CPointCommentaryNodeUpdateViewThink => CPointCommentaryNodeUpdateViewThink;


    public IDatamapFunctionOperator<CPointCommentaryNode, DHookCPointCommentaryNodeUpdateViewPostThink> CPointCommentaryNodeUpdateViewPostThink { get; } = manager.CPointCommentaryNodeUpdateViewPostThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPointCommentaryNode, IDHookCPointCommentaryNodeUpdateViewPostThink> IDatamapFunctionService.CPointCommentaryNodeUpdateViewPostThink => CPointCommentaryNodeUpdateViewPostThink;


    public IDatamapFunctionOperator<CPointCommentaryNode, DHookCPointCommentaryNodeAcculumatePlayTimeThink> CPointCommentaryNodeAcculumatePlayTimeThink { get; } = manager.CPointCommentaryNodeAcculumatePlayTimeThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPointCommentaryNode, IDHookCPointCommentaryNodeAcculumatePlayTimeThink> IDatamapFunctionService.CPointCommentaryNodeAcculumatePlayTimeThink => CPointCommentaryNodeAcculumatePlayTimeThink;


    public IDatamapFunctionOperator<CPathNode, DHookCPathNodeParentedMoveThink> CPathNodeParentedMoveThink { get; } = manager.CPathNodeParentedMoveThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPathNode, IDHookCPathNodeParentedMoveThink> IDatamapFunctionService.CPathNodeParentedMoveThink => CPathNodeParentedMoveThink;


    public IDatamapFunctionOperator<CPhysicalButton, DHookCPhysicalButtonPhysicsThink> CPhysicalButtonPhysicsThink { get; } = manager.CPhysicalButtonPhysicsThink.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPhysicalButton, IDHookCPhysicalButtonPhysicsThink> IDatamapFunctionService.CPhysicalButtonPhysicsThink => CPhysicalButtonPhysicsThink;


    public IDatamapFunctionOperator<CPhysicalButton, DHookCPhysicalButtonButtonTouch> CPhysicalButtonButtonTouch { get; } = manager.CPhysicalButtonButtonTouch.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPhysicalButton, IDHookCPhysicalButtonButtonTouch> IDatamapFunctionService.CPhysicalButtonButtonTouch => CPhysicalButtonButtonTouch;


    public IDatamapFunctionOperator<CPhysicalButton, DHookCPhysicalButtonTriggerAndWait> CPhysicalButtonTriggerAndWait { get; } = manager.CPhysicalButtonTriggerAndWait.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPhysicalButton, IDHookCPhysicalButtonTriggerAndWait> IDatamapFunctionService.CPhysicalButtonTriggerAndWait => CPhysicalButtonTriggerAndWait;


    public IDatamapFunctionOperator<CPhysicalButton, DHookCPhysicalButtonButtonBackHome> CPhysicalButtonButtonBackHome { get; } = manager.CPhysicalButtonButtonBackHome.Get(ctx.Name, profiler);

    IDatamapFunctionOperator<CPhysicalButton, IDHookCPhysicalButtonButtonBackHome> IDatamapFunctionService.CPhysicalButtonButtonBackHome => CPhysicalButtonButtonBackHome;

}
