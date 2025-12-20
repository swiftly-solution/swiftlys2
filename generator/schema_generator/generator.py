from class_name_convertor import get_interface_name, get_impl_name
import json
import os
from tqdm import tqdm
from pathlib import Path

from field_parser import parse_field

OUT_DIR = Path("../../managed/src/SwiftlyS2.Generated/Schemas/")

os.makedirs(OUT_DIR, exist_ok=True)
os.makedirs(OUT_DIR / "Classes", exist_ok=True)
os.makedirs(OUT_DIR / "Interfaces", exist_ok=True)
os.makedirs(OUT_DIR / "Enums", exist_ok=True)

# no one need them
blacklisted_classes = [
  "FeSimdTri_t",
  "CTakeDamageInfo",
  "CTakeDamageResult",
  "CNetworkVarChainer",
  "CVariantDefaultAllocator",
  "CEntityIOOutput",
  "ChangeAccessorFieldPathIndex_t"
]

managed_types = [
  "SchemaClass",
  "SchemaField",
  "SchemaFixedArray",
  "SchemaFixedString"
]

dangerous_fields = [
  "m_bIsValveDS",
  "m_bIsQuestEligible",
  "m_iEntityLevel",
  "m_iItemIDHigh",
  "m_iItemIDLow",
  "m_iAccountID",
  "m_iEntityQuality",
  "m_bInitialized",
  "m_szCustomName",
  "m_iAttributeDefinitionIndex",
  "m_iRawValue32",
  "m_iRawInitialValue32",
  "m_flValue",
  "m_flInitialValue",
  "m_bSetBonus",
  "m_nRefundableCurrency",
  "m_OriginalOwnerXuidLow",
  "m_OriginalOwnerXuidHigh",
  "m_nFallbackPaintKit",
  "m_nFallbackSeed",
  "m_flFallbackWear",
  "m_nFallbackStatTrak",
  "m_iCompetitiveWins",
  "m_iCompetitiveRanking",
  "m_iCompetitiveRankType",
  "m_iCompetitiveRankingPredicted_Win",
  "m_iCompetitiveRankingPredicted_Loss",
  "m_iCompetitiveRankingPredicted_Tie",
  "m_nActiveCoinRank",
  "m_nMusicID"
]

classname_dict = {
  "CCSPlayerController": "cs_player_controller",
  "CCSPlayerPawn": "player",
  "CCSObserverPawn": "observer",
  "SpawnPoint": "spawnpoint",
  "FilterHealth": "filter_health",
  "FilterDamageType": "filter_damage_type",
  "CWorld": "worldent",
  "CWeaponXM1014": "weapon_xm1014",
  "CWeaponUSPSilencer": "weapon_usp_silencer",
  "CWeaponUMP45": "weapon_ump45",
  "CWeaponTec9": "weapon_tec9",
  "CWeaponTaser": "weapon_taser",
  "CWeaponSawedoff": "weapon_sawedoff",
  "CWeaponSSG08": "weapon_ssg08",
  "CWeaponSG556": "weapon_sg556",
  "CWeaponSCAR20": "weapon_scar20",
  "CWeaponRevolver": "weapon_revolver",
  "CWeaponP90": "weapon_p90",
  "CWeaponP250": "weapon_p250",
  "CWeaponNegev": "weapon_negev",
  "CWeaponNOVA": "weapon_nova",
  "CWeaponMag7": "weapon_mag7",
  "CWeaponMP9": "weapon_mp9",
  "CWeaponMP7": "weapon_mp7",
  "CWeaponMP5SD": "weapon_mp5sd",
  "CWeaponMAC10": "weapon_mac10",
  "CWeaponM4A1Silencer": "weapon_m4a1_silencer",
  "CWeaponM4A1": "weapon_m4a1",
  "CWeaponM249": "weapon_m249",
  "CWeaponHKP2000": "weapon_hkp2000",
  "CWeaponGlock": "weapon_glock",
  "CWeaponGalilAR": "weapon_galilar",
  "CWeaponG3SG1": "weapon_g3sg1",
  "CWeaponFiveSeven": "weapon_fiveseven",
  "CWeaponFamas": "weapon_famas",
  "CWeaponElite": "weapon_elite",
  "CWeaponCZ75a": "weapon_cz75a",
  "CWeaponBizon": "weapon_bizon",
  "CWeaponBaseItem": "weapon_csbase",
  "CWeaponAug": "weapon_aug",
  "CWeaponAWP": "weapon_awp",
  "CWaterBullet": "waterbullet",
  "CVoteController": "vote_controller",
  "CTriggerVolume": "trigger_transition",
  "CTriggerToggleSave": "trigger_togglesave",
  "CTriggerTeleport": "trigger_teleport",
  "CTriggerSoundscape": "trigger_soundscape",
  "CTriggerSndSosOpvar": "trigger_snd_sos_opvar",
  "CTriggerSave": "trigger_autosave",
  "CTriggerRemove": "trigger_remove",
  "CTriggerPush": "trigger_push",
  "CTriggerProximity": "trigger_proximity",
  "CTriggerPhysics": "trigger_physics",
  "CTriggerOnce": "trigger_once",
  "CTriggerMultiple": "trigger_multiple",
  "CTriggerLook": "trigger_look",
  "CTriggerLerpObject": "trigger_lerp_object",
  "CTriggerImpact": "trigger_impact",
  "CTriggerHurt": "trigger_hurt",
  "CTriggerHostageReset": "trigger_hostage_reset",
  "CTriggerGravity": "trigger_gravity",
  "CTriggerGameEvent": "trigger_game_event",
  "CTriggerFan": "trigger_fan",
  "CTriggerDetectExplosion": "trigger_detect_explosion",
  "CTriggerDetectBulletFire": "trigger_detect_bullet_fire",
  "CTriggerCallback": "trigger_callback",
  "CTriggerBuoyancy": "trigger_buoyancy",
  "CTriggerBrush": "trigger_brush",
  "CTriggerBombReset": "trigger_bomb_reset",
  "CTriggerActiveWeaponDetect": "trigger_active_weapon_detect",
  "CTonemapTrigger": "trigger_tonemap",
  "CTonemapController2": "env_tonemap_controller2",
  "CTimerEntity": "logic_timer",
  "CTextureBasedAnimatable": "hl_vr_texture_based_animatable",
  "CTestPulseIO": "test_io_combinations",
  "CTestEffect": "test_effect",
  "CTeam": "team_manager",
  "CTankTrainAI": "tanktrain_ai",
  "CTankTargetChange": "tanktrain_aitarget",
  "CSpriteOriented": "env_sprite_oriented",
  "CSprite": "env_glow",
  "CSpotlightEnd": "spotlight_end",
  "CSplineConstraint": "phys_splineconstraint",
  "CSoundStackSave": "snd_stack_save",
  "CSoundOpvarSetPointEntity": "snd_opvar_set_point",
  "CSoundOpvarSetPointBase": "snd_opvar_set_point_base",
  "CSoundOpvarSetPathCornerEntity": "snd_opvar_set_path_corner",
  "CSoundOpvarSetOBBWindEntity": "snd_opvar_set_wind_obb",
  "CSoundOpvarSetOBBEntity": "snd_opvar_set_obb",
  "CSoundOpvarSetEntity": "snd_opvar_set",
  "CSoundOpvarSetAutoRoomEntity": "snd_opvar_set_auto_room",
  "CSoundOpvarSetAABBEntity": "snd_opvar_set_aabb",
  "CSoundEventSphereEntity": "snd_event_sphere",
  "CSoundEventPathCornerEntity": "snd_event_path_corner",
  "CSoundEventParameter": "snd_event_param",
  "CSoundEventOBBEntity": "snd_event_orientedbox",
  "CSoundEventEntity": "snd_event_point",
  "CSoundEventAABBEntity": "snd_event_alignedbox",
  "CSoundAreaEntitySphere": "snd_sound_area_sphere",
  "CSoundAreaEntityOrientedBox": "snd_sound_area_obb",
  "CSoundAreaEntityBase": "snd_sound_area_base",
  "CSmokeGrenadeProjectile": "smokegrenade_projectile",
  "CSmokeGrenade": "weapon_smokegrenade",
  "CSkyboxReference": "skybox_reference",
  "CSkyCamera": "sky_camera",
  "CSimpleMarkupVolumeTagged": "markup_volume_tagged",
  "CShower": "spark_shower",
  "CShatterGlassShardPhysics": "shatterglass_shard",
  "CServerRagdollTrigger": "trigger_serverragdoll",
  "CScriptedSequence": "scripted_sequence",
  "CScriptTriggerPush": "script_trigger_push",
  "CScriptTriggerOnce": "script_trigger_once",
  "CScriptTriggerMultiple": "script_trigger_multiple",
  "CScriptTriggerHurt": "script_trigger_hurt",
  "CScriptNavBlocker": "script_nav_blocker",
  "CScriptItem": "scripted_item_drop",
  "CSceneListManager": "logic_scene_list_manager",
  "CSceneEntity": "scripted_scene",
  "CRotatorTarget": "rotator_target",
  "CRotDoor": "func_door_rotating",
  "CRotButton": "func_rot_button",
  "CRopeKeyframe": "keyframe_rope",
  "CRevertSaved": "player_loadsaved",
  "CRectLight": "light_rect",
  "CRagdollPropAttached": "prop_ragdoll_attached",
  "CRagdollProp": "prop_ragdoll",
  "CRagdollManager": "game_ragdoll_manager",
  "CRagdollMagnet": "phys_ragdollmagnet",
  "CRagdollConstraint": "phys_ragdollconstraint",
  "CPushable": "func_pushable",
  "CPulseGameBlackboard": "pulse_game_blackboard",
  "CPropDoorRotatingBreakable": "prop_door_rotating",
  "CPrecipitationBlocker": "func_precipitation_blocker",
  "CPrecipitation": "func_precipitation",
  "CPostProcessingVolume": "post_processing_volume",
  "CPointWorldText": "point_worldtext",
  "CPointVelocitySensor": "point_velocitysensor",
  "CPointValueRemapper": "point_value_remapper",
  "CPointTemplate": "point_template",
  "CPointTeleport": "point_teleport",
  "CPointServerCommand": "point_servercommand",
  "CPointPush": "point_push",
  "CPointPulse": "point_pulse",
  "CPointProximitySensor": "point_proximity_sensor",
  "CPointPrefab": "point_prefab",
  "CPointOrient": "point_orient",
  "CPointHurt": "point_hurt",
  "CPointGiveAmmo": "point_give_ammo",
  "CPointGamestatsCounter": "point_gamestats_counter",
  "CPointEntityFinder": "point_entity_finder",
  "CPointEntity": "point_entity",
  "CPointCommentaryNode": "point_commentary_node",
  "CPointClientUIWorldTextPanel": "point_clientui_world_text_panel",
  "CPointClientUIWorldPanel": "point_clientui_world_panel",
  "CPointClientUIDialog": "point_clientui_dialog",
  "CPointClientCommand": "point_clientcommand",
  "CPointChildModifier": "point_childmodifier",
  "CPointCameraVFOV": "point_camera_vertical_fov",
  "CPointCamera": "point_camera",
  "CPointBroadcastClientCommand": "point_broadcastclientcommand",
  "CPointAngularVelocitySensor": "point_angularvelocitysensor",
  "CPointAngleSensor": "point_anglesensor",
  "CPlayerVisibility": "env_player_visibility",
  "CPlayerSprayDecal": "player_spray_decal",
  "CPlayerPing": "info_player_ping",
  "CPlatTrigger": "plat_trigger",
  "CPlantedC4": "planted_c4",
  "CPhysicsWire": "env_physwire",
  "CPhysicsSpring": "phys_spring",
  "CPhysicsPropRespawnable": "prop_physics_respawnable",
  "CPhysicsPropOverride": "prop_physics_override",
  "CPhysicsPropMultiplayer": "prop_physics_multiplayer",
  "CPhysicsProp": "prop_physics",
  "CPhysicsEntitySolver": "physics_entity_solver",
  "CPhysicalButton": "func_physical_button",
  "CPhysWheelConstraint": "phys_wheelconstraint",
  "CPhysTorque": "phys_torque",
  "CPhysThruster": "phys_thruster",
  "CPhysSlideConstraint": "phys_slideconstraint",
  "CPhysPulley": "phys_pulleyconstraint",
  "CPhysMotor": "phys_motor",
  "CPhysMagnet": "phys_magnet",
  "CPhysLength": "phys_lengthconstraint",
  "CPhysImpact": "env_physimpact",
  "CPhysHinge": "phys_hinge",
  "CPhysFixed": "phys_constraint",
  "CPhysExplosion": "env_physexplosion",
  "CPhysBox": "func_physbox",
  "CPhysBallSocket": "phys_ballsocket",
  "CPathTrack": "path_track",
  "CPathSimple": "path_simple",
  "CPathParticleRope": "path_particle_rope",
  "CPathMover": "path_mover",
  "CPathKeyFrame": "keyframe_track",
  "CPathCornerCrash": "path_corner_crash",
  "CPathCorner": "path_corner",
  "CParticleSystem": "info_particle_system",
  "COrnamentProp": "prop_dynamic_ornament",
  "COmniLight": "light_omni2",
  "CNullEntity": "info_null",
  "CNavWalkable": "point_nav_walkable",
  "CNavSpaceInfo": "info_nav_space",
  "CNavLinkAreaEntity": "ai_nav_link_area",
  "CMultiSource": "multisource",
  "CMultiLightProxy": "logic_multilight_proxy",
  "CMoverPathNode": "path_node_mover",
  "CMomentaryRotButton": "momentary_rot_button",
  "CMolotovProjectile": "molotov_projectile",
  "CMolotovGrenade": "weapon_molotov",
  "CMessageEntity": "point_message",
  "CMessage": "env_message",
  "CMathRemap": "math_remap",
  "CMathCounter": "math_counter",
  "CMathColorBlend": "math_colorblend",
  "CMarkupVolumeWithRef": "markup_volume_with_ref",
  "CMarkupVolumeTagged_NavGame": "func_nav_markup_game",
  "CMarkupVolumeTagged_Nav": "func_nav_markup",
  "CMarkupVolume": "markup_volume",
  "CMapVetoPickController": "mapvetopick_controller",
  "CMapSharedEnvironment": "map_shared_environment",
  "CMapInfo": "info_map_parameters",
  "CLogicScript": "logic_script",
  "CLogicRelay": "logic_relay",
  "CLogicProximity": "logic_proximity",
  "CLogicPlayerProxy": "logic_playerproxy",
  "CLogicNavigation": "logic_navigation",
  "CLogicNPCCounterOBB": "logic_npc_counter_obb",
  "CLogicNPCCounterAABB": "logic_npc_counter_aabb",
  "CLogicNPCCounter": "logic_npc_counter_radius",
  "CLogicMeasureMovement": "logic_measure_movement",
  "CLogicLineToEntity": "logic_lineto",
  "CLogicGameEventListener": "logic_gameevent_listener",
  "CLogicGameEvent": "logic_game_event",
  "CLogicEventListener": "logic_eventlistener",
  "CLogicDistanceCheck": "logic_distance_check",
  "CLogicDistanceAutosave": "logic_distance_autosave",
  "CLogicCompare": "logic_compare",
  "CLogicCollisionPair": "logic_collision_pair",
  "CLogicCase": "logic_case",
  "CLogicBranchList": "logic_branch_listener",
  "CLogicBranch": "logic_branch",
  "CLogicAutosave": "logic_autosave",
  "CLogicAuto": "logic_auto",
  "CLogicActiveAutosave": "logic_active_autosave",
  "CLogicAchievement": "logic_achievement",
  "CLightSpotEntity": "light_spot",
  "CLightOrthoEntity": "light_ortho",
  "CLightEnvironmentEntity": "light_environment",
  "CLightEntity": "light_omni",
  "CLightDirectionalEntity": "light_directional",
  "CKnife": "weapon_knife",
  "CKeepUpright": "phys_keepupright",
  "CItem_Healthshot": "weapon_healthshot",
  "CItemSoda": "item_sodacan",
  "CItemKevlar": "item_kevlar",
  "CItemGenericTriggerHelper": "item_generic_trigger_helper",
  "CItemGeneric": "item_generic",
  "CItemDefuser": "item_defuser",
  "CItemAssaultSuit": "item_assaultsuit",
  "CInstructorEventEntity": "point_instructor_event",
  "CInstancedSceneEntity": "instanced_scripted_scene",
  "CInfoWorldLayer": "info_world_layer",
  "CInfoVisibilityBox": "info_visibility_box",
  "CInfoTeleportDestination": "info_teleport_destination",
  "CInfoTargetServerOnly": "info_target_server_only",
  "CInfoTarget": "info_target",
  "CInfoSpawnGroupLoadUnload": "info_spawngroup_load_unload",
  "CInfoSpawnGroupLandmark": "info_spawngroup_landmark",
  "CInfoPlayerTerrorist": "info_player_terrorist",
  "CInfoPlayerStart": "info_player_start",
  "CInfoPlayerCounterterrorist": "info_player_counterterrorist",
  "CInfoParticleTarget": "info_particle_target",
  "CInfoOffscreenPanoramaTexture": "info_offscreen_panorama_texture",
  "CInfoLandmark": "info_landmark",
  "CInfoLadderDismount": "info_ladder_dismount",
  "CInfoInstructorHintTarget": "info_target_instructor_hint",
  "CInfoInstructorHintHostageRescueZone": "info_hostage_rescue_zone_hint",
  "CInfoInstructorHintBombTargetB": "info_bomb_target_hint_B",
  "CInfoInstructorHintBombTargetA": "info_bomb_target_hint_A",
  "CInfoGameEventProxy": "info_game_event_proxy",
  "CInfoFan": "info_trigger_fan",
  "CInfoDynamicShadowHintBox": "info_dynamic_shadow_hint_box",
  "CInfoDynamicShadowHint": "info_dynamic_shadow_hint",
  "CInfoDeathmatchSpawn": "info_deathmatch_spawn",
  "CInfoData": "info_data",
  "CInferno": "inferno",
  "CIncendiaryGrenade": "weapon_incgrenade",
  "CHostageRescueZone": "func_hostage_rescue",
  "CHostage": "hostage_entity",
  "CHandleTest": "handle_test",
  "CHandleDummy": "handle_dummy",
  "CHEGrenadeProjectile": "hegrenade_projectile",
  "CHEGrenade": "weapon_hegrenade",
  "CGunTarget": "func_guntarget",
  "CGradientFog": "env_gradient_fog",
  "CGenericConstraint": "phys_genericconstraint",
  "CGameText": "game_text",
  "CGamePlayerZone": "game_zone_player",
  "CGamePlayerEquip": "game_player_equip",
  "CGameMoney": "game_money",
  "CGameGibManager": "game_gib_manager",
  "CGameEnd": "game_end",
  "CFuncWater": "func_water",
  "CFuncWallToggle": "func_wall_toggle",
  "CFuncWall": "func_wall",
  "CFuncVehicleClip": "func_vehicleclip",
  "CFuncVPhysicsClip": "func_clip_vphysics",
  "CFuncTrainControls": "func_traincontrols",
  "CFuncTrain": "func_train",
  "CFuncTrackTrain": "func_tracktrain",
  "CFuncTrackChange": "func_trackchange",
  "CFuncTrackAuto": "func_trackautochange",
  "CFuncTimescale": "func_timescale",
  "CFuncTankTrain": "func_tanktrain",
  "CFuncShatterglass": "func_shatterglass",
  "CFuncRetakeBarrier": "func_retakebarrier",
  "CFuncRotator": "func_rotator",
  "CFuncRotating": "func_rotating",
  "CFuncPropRespawnZone": "func_proprrespawnzone",
  "CFuncPlatRot": "func_platrot",
  "CFuncPlat": "func_plat",
  "CFuncNavObstruction": "func_nav_avoidance_obstacle",
  "CFuncNavBlocker": "func_nav_blocker",
  "CFuncMover": "func_mover",
  "CFuncMoveLinear": "momentary_door",
  "CFuncMonitor": "func_monitor",
  "CFuncLadder": "func_useableladder",
  "CFuncIllusionary": "func_illusionary",
  "CFuncElectrifiedVolume": "func_electrified_volume",
  "CFuncConveyor": "func_conveyor",
  "CFuncBrush": "func_brush",
  "CFootstepControl": "func_footstep_control",
  "CFogVolume": "fog_volume",
  "CFogTrigger": "trigger_fog",
  "CFogController": "env_fog_controller",
  "CFlashbangProjectile": "flashbang_projectile",
  "CFlashbang": "weapon_flashbang",
  "CFishPool": "func_fish_pool",
  "CFish": "fish",
  "CFilterTeam": "filter_activator_team",
  "CFilterProximity": "filter_proximity",
  "CFilterName": "filter_activator_name",
  "CFilterMultiple": "filter_multi",
  "CFilterModel": "filter_activator_model",
  "CFilterMassGreater": "filter_activator_mass_greater",
  "CFilterLOS": "filter_los",
  "CFilterEnemy": "filter_enemy",
  "CFilterContext": "filter_activator_context",
  "CFilterClass": "filter_activator_class",
  "CFilterAttributeInt": "filter_activator_attribute_int",
  "CEnvWindVolume": "env_wind_volume",
  "CEnvWindController": "env_wind_controller",
  "CEnvWind": "env_wind",
  "CEnvVolumetricFogVolume": "env_volumetric_fog_volume",
  "CEnvVolumetricFogController": "env_volumetric_fog_controller",
  "CEnvViewPunch": "env_viewpunch",
  "CEnvTilt": "env_tilt",
  "CEnvSplash": "env_splash",
  "CEnvSpark": "env_spark",
  "CEnvSoundscapeTriggerable": "env_soundscape_triggerable",
  "CEnvSoundscapeProxy": "env_soundscape_proxy",
  "CEnvSoundscape": "env_soundscape",
  "CEnvSky": "env_sky",
  "CEnvShake": "env_shake",
  "CEnvParticleGlow": "env_particle_glow",
  "CEnvMuzzleFlash": "env_muzzleflash",
  "CEnvLightProbeVolume": "env_light_probe_volume",
  "CEnvLaser": "env_laser",
  "CEnvInstructorVRHint": "env_instructor_vr_hint",
  "CEnvInstructorHint": "env_instructor_hint",
  "CEnvHudHint": "env_hudhint",
  "CEnvGlobal": "env_global",
  "CEnvFade": "env_fade",
  "CEnvExplosion": "env_explosion",
  "CEnvEntityMaker": "env_entity_maker",
  "CEnvEntityIgniter": "env_entity_igniter",
  "CEnvDetailController": "env_detail_controller",
  "CEnvDecal": "env_decal",
  "CEnvCubemapFog": "env_cubemap_fog",
  "CEnvCubemapBox": "env_cubemap_box",
  "CEnvCubemap": "env_cubemap",
  "CEnvCombinedLightProbeVolume": "env_combined_light_probe_volume",
  "CEnvBeverage": "env_beverage",
  "CEnvBeam": "env_beam",
  "CEntityInstance": "root",
  "CEntityFlame": "entityflame",
  "CEntityDissolve": "env_entity_dissolver",
  "CEntityBlocker": "entity_blocker",
  "CEnableMotionFixup": "point_enable_motion_fixup",
  "CEconWearable": "wearable_item",
  "CDynamicProp": "dynamic_prop",
  "CDynamicNavConnectionsVolume": "func_nav_dynamic_connections",
  "CDynamicLight": "light_dynamic",
  "CDecoyProjectile": "decoy_projectile",
  "CDecoyGrenade": "weapon_decoy",
  "CDebugHistory": "env_debughistory",
  "CDEagle": "weapon_deagle",
  "CCredits": "env_credits",
  "CConstraintAnchor": "info_constraint_anchor",
  "CCommentaryViewPosition": "point_commentary_viewpoint",
  "CCommentaryAuto": "commentary_auto",
  "CColorCorrectionVolume": "color_correction_volume",
  "CColorCorrection": "color_correction",
  "CCitadelSoundOpvarSetOBB": "citadel_snd_opvar_set_obb",
  "CChicken": "chicken",
  "CChangeLevel": "trigger_changelevel",
  "CCSWeaponBase": "weapon_cs_base",
  "CCSTeam": "cs_team_manager",
  "CCSSprite": "env_sprite_clientside",
  "CCSServerPointScriptEntity": "point_script",
  "CCSPlayerResource": "cs_player_manager",
  "CCSPlace": "env_cs_place",
  "CCSPetPlacement": "cs_pet_placement",
  "CCSMinimapBoundary": "cs_minimap_boundary",
  "CCSGameRulesProxy": "cs_gamerules",
  "CCSGO_WingmanIntroTerroristPosition": "wingman_intro_terrorist",
  "CCSGO_WingmanIntroCounterTerroristPosition": "wingman_intro_counterterrorist",
  "CCSGO_TeamSelectTerroristPosition": "team_select_terrorist",
  "CCSGO_TeamSelectCounterTerroristPosition": "team_select_counterterrorist",
  "CCSGO_TeamIntroTerroristPosition": "team_intro_terrorist",
  "CCSGO_TeamIntroCounterTerroristPosition": "team_intro_counterterrorist",
  "CC4": "weapon_c4",
  "CBuyZone": "func_buyzone",
  "CBreakable": "func_breakable",
  "CBombTarget": "func_bomb_target",
  "CBlood": "env_blood",
  "CBeam": "beam",
  "CBaseTrigger": "trigger",
  "CBasePlayerPawn": "baseplayerpawn",
  "CBasePlayerController": "player_controller",
  "CBaseMoveBehavior": "move_keyframed",
  "CBaseModelEntity": "basemodelentity",
  "CBaseGrenade": "grenade",
  "CBaseFlex": "baseflex",
  "CBaseFilter": "filter_base",
  "CBaseDoor": "func_door",
  "CBaseDMStart": "info_player_deathmatch",
  "CBaseCSGrenade": "weapon_basecsgrenade",
  "CBaseButton": "func_button",
  "CBaseAnimGraph": "baseanimgraph",
  "CBarnLight": "light_barn",
  "CAmbientGeneric": "ambient_generic",
  "CAK47": "weapon_ak47",
  "CAI_ChangeHintGroup": "ai_changehintgroup",
  "CPropDoorRotating": "prop_door_rotating",
  "CPropDoorRotatingBreakable": "prop_door_rotating",
  "CEnvTracer": "env_tracer",
}

found_dangerous_fields = []

def render_template(template, params):
  for param, value in params.items():
    template = template.replace(f"${param}$", str(value))
  return template

class Writer():

  def __init__(self, class_def, all_class_names, all_enum_names):
    self.class_def = class_def
    self.all_class_names = all_class_names
    self.all_enum_names = all_enum_names

    self.class_name = class_def["name"]
    self.size = class_def["size"]
    self.class_name = self.class_name.replace(":", "_")
    
    self.interface_name = get_impl_name(self.class_name)


    self.class_ref_field_template = open("templates/class_ref_field_template.cs", "r").read()
    self.class_value_field_template = open("templates/class_value_field_template.cs", "r").read()
    self.class_fixed_array_field_template = open("templates/class_fixed_array_field_template.cs", "r").read()
    self.class_ptr_field_template = open("templates/class_ptr_field_template.cs", "r").read()
    self.class_string_field_template = open("templates/class_string_field_template.cs", "r").read()
    self.class_fixed_string_field_template = open("templates/class_fixed_string_field_template.cs", "r").read()

    self.interface_field_template = open("templates/interface_field_template.cs", "r").read()
    self.class_template = open("templates/class_template.cs", "r").read()
    self.interface_template = open("templates/interface_template.cs", "r").read()
    self.class_updator_template = open("templates/class_updator_template.cs", "r").read()
    self.interface_updator_template = open("templates/interface_updator_template.cs", "r").read()

    self.enum_template = open("templates/enum_template.cs", "r").read()

    self.base_class = self.class_def["base_classes"][0] if "base_classes" in self.class_def else "SchemaClass"
    self.base_class = self.base_class.replace(":", "_")

  def write_class(self):
    self.class_file_handle = open(OUT_DIR / "Classes" / f"{get_impl_name(self.class_name)}.cs", "w")

    fields = []
    updators = []

    duplicated_counter = 0
    names = []

    if "fields" in self.class_def:
      for field in self.class_def["fields"]:

        field_info = parse_field(field, self.all_class_names, self.all_enum_names)

        if field_info["NAME"] in names:
          duplicated_counter += 1
          field_info["NAME"] = f"{field_info['NAME']}{duplicated_counter}"
        else:
          names.append(field_info["NAME"])

        field_info["REF_METHOD"] = "Deref" if field_info["KIND"] == "ptr" else "AsRef"
        if field["name"] in dangerous_fields:
          found_dangerous_fields.append({
            "class": self.class_name,
            "field": field["name"],
            "hash": field_info["HASH"]
          })

        if field_info["IS_NETWORKED"] == "true":
          updators.append(render_template(self.class_updator_template, field_info))

        # Special string cases
        if field_info["IS_FIXED_CHAR_STRING"]:
          fields.append(render_template(self.class_fixed_string_field_template, field_info))
          continue
        if field_info["IS_CHAR_PTR_STRING"] or field_info["IS_STRING_HANDLE"]:
          fields.append(render_template(self.class_string_field_template, field_info))
          continue

        if field_info["KIND"] == "fixed_array" and field_info["IMPL_TYPE"] != "SchemaUntypedField":
          fields.append(render_template(self.class_fixed_array_field_template, field_info))
        elif field_info["IS_VALUE_TYPE"]:
          fields.append(render_template(self.class_value_field_template, field_info))
        else:
          if field_info["KIND"] == "ptr" and field_info["IMPL_TYPE"] not in managed_types:
            fields.append(render_template(self.class_ptr_field_template, field_info))
          else:
            fields.append(render_template(self.class_ref_field_template, field_info))

    params = {
      "CLASS_NAME": get_impl_name(self.class_name),
      "INTERFACE_NAME": get_interface_name(self.class_name),
      "BASE_CLASS": get_impl_name(self.base_class),
      "BASE_INTERFACE": get_interface_name(self.base_class),
      "FIELDS": "\n".join(fields),
      "UPDATORS": "\n".join(updators)
    }

    self.class_file_handle.write(render_template(self.class_template, params))

  def write_interface(self):
    self.interface_file_handle = open(OUT_DIR / "Interfaces" / f"{get_interface_name(self.class_name)}.cs", "w")

    fields = []
    updators = []

    # the types whose generic has been erased, we add a comment to tell user whats the real type
    erased_generics = [
      "CUtlVector",
      "CUtlVectorFixedGrowable",
      "CUtlVectorEmbeddedNetworkVar",
      "CNetworkUtlVectorBase",
    ]

    duplicated_counter = 0
    names = []

    if "fields" in self.class_def:
      for field in self.class_def["fields"]:
        field_info = parse_field(field, self.all_class_names, self.all_enum_names)
      
        
        if field_info["NAME"] in names:
          duplicated_counter += 1
          field_info["NAME"] = f"{field_info['NAME']}{duplicated_counter}"
        else:
          names.append(field_info["NAME"])
        field_info["SETTER"] = ""

        # Interface type overrides for special string cases
        if field_info["IS_FIXED_CHAR_STRING"] or field_info["IS_CHAR_PTR_STRING"] or field_info["IS_STRING_HANDLE"]:
          field_info["REF"] = ""
          field_info["INTERFACE_TYPE"] = "string"
          field_info["NULLABLE"] = ""
          field_info["SETTER"] = " set;"
        else:
          field_info["REF"] = "ref " if field_info["IS_VALUE_TYPE"] else ""
          field_info["NULLABLE"] = "?" if field_info["KIND"] == "ptr" and not field_info["IS_VALUE_TYPE"] and field_info["IMPL_TYPE"] != "SchemaUntypedField" else ""

        field_info["COMMENT"] = ""

        if field_info["IMPL_TYPE"] in erased_generics or field_info["IMPL_TYPE"] == "SchemaUntypedField":
          if "templated" in field:
            field_info["COMMENT"] = f"\n    // {field['templated']}"
          else:
            field_info["COMMENT"] = f"\n    // {field['type']}"

        if field_info["IS_NETWORKED"] == "true":
          updators.append(render_template(self.interface_updator_template, field_info))

        fields.append(render_template(self.interface_field_template, field_info))

    designername = classname_dict.get(self.class_name, "null")
    if designername != "null":
      designername = f"\"{designername}\""

    params = {
      "INTERFACE_NAME": get_interface_name(self.class_name),
      "BASE_INTERFACE": f"" if self.base_class == "SchemaClass" else get_interface_name(self.base_class) + ", ",
      "IMPL_TYPE": get_impl_name(self.class_name),
      "FIELDS": "\n".join(fields),
      "UPDATORS": "\n".join(updators),
      "SIZE": self.size,
      "CLASSNAME": designername,
    }
    self.interface_file_handle.write(render_template(self.interface_template, params))

  def write_enum(self):
    enum_file_handle = open(OUT_DIR / "Enums" / f"{self.class_name}.cs", "w")
    types = {
      1: "byte",
      2: "ushort",
      4: "uint",
      8: "ulong"
    }

    type = types[self.size]

    fields = []
    for field in self.class_def["fields"]:
      value = field["value"] if field["value"] != -1 else f"{type}.MaxValue"
      value = value if value != -2 else f"{type}.MaxValue - 1"
      fields.append(f" {field['name']} = {value},")

    params = {
      "ENUM_NAME": self.class_name,
      "BASE_TYPE": type,
      "ENUM_VALUES": "\n\n".join(fields)
    }
    enum_file_handle.write(render_template(self.enum_template, params))

with open("sdk.json", "r") as f:
  data = json.load(f)

  all_class_names = [class_def["name"] for class_def in data["classes"]]
  all_enum_names = [enum_def["name"] for enum_def in data["enums"]]

  for class_def in tqdm(data["classes"], desc="Classes"):


    if class_def["name"] in blacklisted_classes:
      continue

    writer = Writer(class_def, all_class_names, all_enum_names)
    writer.write_class()
    writer.write_interface()

  for enum_def in tqdm(data["enums"], desc="Enums"):
    if enum_def["name"] in blacklisted_classes:
      continue

    writer = Writer(enum_def, all_class_names, all_enum_names)
    writer.write_enum()

  if found_dangerous_fields:
    print("\n")
    print("  private static readonly HashSet<ulong> dangerousFields = new() {")
    for item in found_dangerous_fields:
      print(f"    {item['hash']}, // {item['class']}.{item['field']}")
    print("  };")