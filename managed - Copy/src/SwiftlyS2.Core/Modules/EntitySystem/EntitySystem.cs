using Microsoft.Extensions.Logging;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.NetMessages;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.EntitySystem;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Profiler;
using SwiftlyS2.Shared.SchemaDefinitions;
using SwiftlyS2.Shared.Schemas;
using System.Collections.Frozen;

namespace SwiftlyS2.Core.EntitySystem;

internal class EntitySystemService : IEntitySystemService, IDisposable
{
  private List<EntityOutputHookCallback> _callbacks = new();
  private Lock _lock = new();
  private ILoggerFactory _loggerFactory;
  private IContextedProfilerService _profiler;

  public EntitySystemService( ILoggerFactory loggerFactory, IContextedProfilerService profiler )
  {
    _loggerFactory = loggerFactory;
    _profiler = profiler;
  }

  public T CreateEntity<T>() where T : class, ISchemaClass<T>
  {
    var designerName = GetEntityDesignerName<T>();
    if (designerName == null)
    {
      throw new ArgumentException($"Can't create entity with class {typeof(T).Name}, which doesn't have a designer name.");
    }
    return CreateEntityByDesignerName<T>(designerName);
  }

  public T CreateEntityByDesignerName<T>( string designerName ) where T : ISchemaClass<T>
  {
    var handle = NativeEntitySystem.CreateEntityByName(designerName);
    if (handle == nint.Zero)
    {
      throw new ArgumentException($"Failed to create entity by designer name: {designerName}, probably invalid designer name.");
    }
    return T.From(handle);
  }

  public CHandle<T> GetRefEHandle<T>( T entity ) where T : class, ISchemaClass<T>
  {
    var handle = NativeEntitySystem.GetEntityHandleFromEntity(entity.Address);
    return new CHandle<T> {
      Raw = handle,
    };
  }

  public CCSGameRules? GetGameRules()
  {
    var handle = NativeEntitySystem.GetGameRules();
    return handle.IsValidPtr() ? new CCSGameRulesImpl(handle) : null;
  }

  public IEnumerable<CEntityInstance> GetAllEntities()
  {
    CEntityIdentity? pFirst = new CEntityIdentityImpl(NativeEntitySystem.GetFirstActiveEntity());

    for (; pFirst != null && pFirst.IsValid; pFirst = pFirst.Next)
    {
      yield return new CEntityInstanceImpl(pFirst.Address.Read<nint>());
    }
  }

  public IEnumerable<T> GetAllEntitiesByClass<T>() where T : class, ISchemaClass<T>
  {
    var designerName = GetEntityDesignerName<T>();
    if (designerName == null)
    {
      throw new ArgumentException($"Can't get entities with class {typeof(T).Name}, which doesn't have a designer name");
    }
    return GetAllEntities().Where(( entity ) => entity.Entity?.DesignerName == designerName).Select(( entity ) => T.From(entity.Address));
  }

  public IEnumerable<T> GetAllEntitiesByDesignerName<T>( string designerName ) where T : class, ISchemaClass<T>
  {
    return GetAllEntities()
      .Where(entity => entity.Entity?.DesignerName == designerName)
      .Select(entity => T.From(entity.Address));
  }

  public T? GetEntityByIndex<T>( uint index ) where T : class, ISchemaClass<T>
  {
    var handle = NativeEntitySystem.GetEntityByIndex(index);
    if (handle == nint.Zero)
    {
      return null;
    }
    return T.From(handle);
  }

  Guid IEntitySystemService.HookEntityOutput<T>( string outputName, IEntitySystemService.EntityOutputHandler callback )
  {
    var hook = new EntityOutputHookCallback(GetEntityDesignerName<T>() ?? "", outputName, callback, _loggerFactory, _profiler);
    lock (_lock)
    {
      _callbacks.Add(hook);
    }
    return hook.Guid;
  }

  public void UnhookEntityOutput( Guid guid )
  {
    lock (_lock)
    {
      _callbacks.RemoveAll(callback =>
      {
        if (callback.Guid == guid)
        {
          callback.Dispose();
          return true;
        }
        return false;
      });
    }
  }

  public static readonly FrozenDictionary<Type, string> TypeToDesignerName = new Dictionary<Type, string>() {
    { typeof(CCSPlayerController), "cs_player_controller" },
    { typeof(CCSPlayerPawn), "player" },
    { typeof(CCSObserverPawn), "observer" },
    { typeof(SpawnPoint), "spawnpoint" },
    { typeof(FilterHealth), "filter_health" },
    { typeof(FilterDamageType), "filter_damage_type" },
    { typeof(CWorld), "worldent" },
    { typeof(CWeaponXM1014), "weapon_xm1014" },
    { typeof(CWeaponUSPSilencer), "weapon_usp_silencer" },
    { typeof(CWeaponUMP45), "weapon_ump45" },
    { typeof(CWeaponTec9), "weapon_tec9" },
    { typeof(CWeaponTaser), "weapon_taser" },
    { typeof(CWeaponSawedoff), "weapon_sawedoff" },
    { typeof(CWeaponSSG08), "weapon_ssg08" },
    { typeof(CWeaponSG556), "weapon_sg556" },
    { typeof(CWeaponSCAR20), "weapon_scar20" },
    { typeof(CWeaponRevolver), "weapon_revolver" },
    { typeof(CWeaponP90), "weapon_p90" },
    { typeof(CWeaponP250), "weapon_p250" },
    { typeof(CWeaponNegev), "weapon_negev" },
    { typeof(CWeaponNOVA), "weapon_nova" },
    { typeof(CWeaponMag7), "weapon_mag7" },
    { typeof(CWeaponMP9), "weapon_mp9" },
    { typeof(CWeaponMP7), "weapon_mp7" },
    { typeof(CWeaponMP5SD), "weapon_mp5sd" },
    { typeof(CWeaponMAC10), "weapon_mac10" },
    { typeof(CWeaponM4A1Silencer), "weapon_m4a1_silencer" },
    { typeof(CWeaponM4A1), "weapon_m4a1" },
    { typeof(CWeaponM249), "weapon_m249" },
    { typeof(CWeaponHKP2000), "weapon_hkp2000" },
    { typeof(CWeaponGlock), "weapon_glock" },
    { typeof(CWeaponGalilAR), "weapon_galilar" },
    { typeof(CWeaponG3SG1), "weapon_g3sg1" },
    { typeof(CWeaponFiveSeven), "weapon_fiveseven" },
    { typeof(CWeaponFamas), "weapon_famas" },
    { typeof(CWeaponElite), "weapon_elite" },
    { typeof(CWeaponCZ75a), "weapon_cz75a" },
    { typeof(CWeaponBizon), "weapon_bizon" },
    { typeof(CWeaponBaseItem), "weapon_csbase" },
    { typeof(CWeaponAug), "weapon_aug" },
    { typeof(CWeaponAWP), "weapon_awp" },
    { typeof(CWaterBullet), "waterbullet" },
    { typeof(CVoteController), "vote_controller" },
    { typeof(CTriggerVolume), "trigger_transition" },
    { typeof(CTriggerToggleSave), "trigger_togglesave" },
    { typeof(CTriggerTeleport), "trigger_teleport" },
    { typeof(CTriggerSoundscape), "trigger_soundscape" },
    { typeof(CTriggerSndSosOpvar), "trigger_snd_sos_opvar" },
    { typeof(CTriggerSave), "trigger_autosave" },
    { typeof(CTriggerRemove), "trigger_remove" },
    { typeof(CTriggerPush), "trigger_push" },
    { typeof(CTriggerProximity), "trigger_proximity" },
    { typeof(CTriggerPhysics), "trigger_physics" },
    { typeof(CTriggerOnce), "trigger_once" },
    { typeof(CTriggerMultiple), "trigger_multiple" },
    { typeof(CTriggerLook), "trigger_look" },
    { typeof(CTriggerLerpObject), "trigger_lerp_object" },
    { typeof(CTriggerImpact), "trigger_impact" },
    { typeof(CTriggerHurt), "trigger_hurt" },
    { typeof(CTriggerHostageReset), "trigger_hostage_reset" },
    { typeof(CTriggerGravity), "trigger_gravity" },
    { typeof(CTriggerGameEvent), "trigger_game_event" },
    { typeof(CTriggerFan), "trigger_fan" },
    { typeof(CTriggerDetectExplosion), "trigger_detect_explosion" },
    { typeof(CTriggerDetectBulletFire), "trigger_detect_bullet_fire" },
    { typeof(CTriggerCallback), "trigger_callback" },
    { typeof(CTriggerBuoyancy), "trigger_buoyancy" },
    { typeof(CTriggerBrush), "trigger_brush" },
    { typeof(CTriggerBombReset), "trigger_bomb_reset" },
    { typeof(CTriggerActiveWeaponDetect), "trigger_active_weapon_detect" },
    { typeof(CTonemapTrigger), "trigger_tonemap" },
    { typeof(CTonemapController2), "env_tonemap_controller2" },
    { typeof(CTimerEntity), "logic_timer" },
    { typeof(CTextureBasedAnimatable), "hl_vr_texture_based_animatable" },
    { typeof(CTestPulseIO), "test_io_combinations" },
    { typeof(CTestEffect), "test_effect" },
    { typeof(CTeam), "team_manager" },
    { typeof(CTankTrainAI), "tanktrain_ai" },
    { typeof(CTankTargetChange), "tanktrain_aitarget" },
    { typeof(CSpriteOriented), "env_sprite_oriented" },
    { typeof(CSprite), "env_glow" },
    { typeof(CSpotlightEnd), "spotlight_end" },
    { typeof(CSplineConstraint), "phys_splineconstraint" },
    { typeof(CSoundStackSave), "snd_stack_save" },
    { typeof(CSoundOpvarSetPointEntity), "snd_opvar_set_point" },
    { typeof(CSoundOpvarSetPointBase), "snd_opvar_set_point_base" },
    { typeof(CSoundOpvarSetPathCornerEntity), "snd_opvar_set_path_corner" },
    { typeof(CSoundOpvarSetOBBWindEntity), "snd_opvar_set_wind_obb" },
    { typeof(CSoundOpvarSetOBBEntity), "snd_opvar_set_obb" },
    { typeof(CSoundOpvarSetEntity), "snd_opvar_set" },
    { typeof(CSoundOpvarSetAutoRoomEntity), "snd_opvar_set_auto_room" },
    { typeof(CSoundOpvarSetAABBEntity), "snd_opvar_set_aabb" },
    { typeof(CSoundEventSphereEntity), "snd_event_sphere" },
    { typeof(CSoundEventPathCornerEntity), "snd_event_path_corner" },
    { typeof(CSoundEventParameter), "snd_event_param" },
    { typeof(CSoundEventOBBEntity), "snd_event_orientedbox" },
    { typeof(CSoundEventEntity), "snd_event_point" },
    { typeof(CSoundEventAABBEntity), "snd_event_alignedbox" },
    { typeof(CSoundAreaEntitySphere), "snd_sound_area_sphere" },
    { typeof(CSoundAreaEntityOrientedBox), "snd_sound_area_obb" },
    { typeof(CSoundAreaEntityBase), "snd_sound_area_base" },
    { typeof(CSmokeGrenadeProjectile), "smokegrenade_projectile" },
    { typeof(CSmokeGrenade), "weapon_smokegrenade" },
    { typeof(CSkyboxReference), "skybox_reference" },
    { typeof(CSkyCamera), "sky_camera" },
    { typeof(CSimpleMarkupVolumeTagged), "markup_volume_tagged" },
    { typeof(CShower), "spark_shower" },
    { typeof(CShatterGlassShardPhysics), "shatterglass_shard" },
    { typeof(CServerRagdollTrigger), "trigger_serverragdoll" },
    { typeof(CScriptedSequence), "scripted_sequence" },
    { typeof(CScriptTriggerPush), "script_trigger_push" },
    { typeof(CScriptTriggerOnce), "script_trigger_once" },
    { typeof(CScriptTriggerMultiple), "script_trigger_multiple" },
    { typeof(CScriptTriggerHurt), "script_trigger_hurt" },
    { typeof(CScriptNavBlocker), "script_nav_blocker" },
    { typeof(CScriptItem), "scripted_item_drop" },
    { typeof(CSceneListManager), "logic_scene_list_manager" },
    { typeof(CSceneEntity), "scripted_scene" },
    { typeof(CRotatorTarget), "rotator_target" },
    { typeof(CRotDoor), "func_door_rotating" },
    { typeof(CRotButton), "func_rot_button" },
    { typeof(CRopeKeyframe), "keyframe_rope" },
    { typeof(CRevertSaved), "player_loadsaved" },
    { typeof(CRectLight), "light_rect" },
    { typeof(CRagdollPropAttached), "prop_ragdoll_attached" },
    { typeof(CRagdollProp), "prop_ragdoll" },
    { typeof(CRagdollManager), "game_ragdoll_manager" },
    { typeof(CRagdollMagnet), "phys_ragdollmagnet" },
    { typeof(CRagdollConstraint), "phys_ragdollconstraint" },
    { typeof(CPushable), "func_pushable" },
    { typeof(CPulseGameBlackboard), "pulse_game_blackboard" },
    { typeof(CPropDoorRotatingBreakable), "prop_door_rotating" },
    { typeof(CPrecipitationBlocker), "func_precipitation_blocker" },
    { typeof(CPrecipitation), "func_precipitation" },
    { typeof(CPostProcessingVolume), "post_processing_volume" },
    { typeof(CPointWorldText), "point_worldtext" },
    { typeof(CPointVelocitySensor), "point_velocitysensor" },
    { typeof(CPointValueRemapper), "point_value_remapper" },
    { typeof(CPointTemplate), "point_template" },
    { typeof(CPointTeleport), "point_teleport" },
    { typeof(CPointServerCommand), "point_servercommand" },
    { typeof(CPointPush), "point_push" },
    { typeof(CPointPulse), "point_pulse" },
    { typeof(CPointProximitySensor), "point_proximity_sensor" },
    { typeof(CPointPrefab), "point_prefab" },
    { typeof(CPointOrient), "point_orient" },
    { typeof(CPointHurt), "point_hurt" },
    { typeof(CPointGiveAmmo), "point_give_ammo" },
    { typeof(CPointGamestatsCounter), "point_gamestats_counter" },
    { typeof(CPointEntityFinder), "point_entity_finder" },
    { typeof(CPointEntity), "point_entity" },
    { typeof(CPointCommentaryNode), "point_commentary_node" },
    { typeof(CPointClientUIWorldTextPanel), "point_clientui_world_text_panel" },
    { typeof(CPointClientUIWorldPanel), "point_clientui_world_panel" },
    { typeof(CPointClientUIDialog), "point_clientui_dialog" },
    { typeof(CPointClientCommand), "point_clientcommand" },
    { typeof(CPointChildModifier), "point_childmodifier" },
    { typeof(CPointCameraVFOV), "point_camera_vertical_fov" },
    { typeof(CPointCamera), "point_camera" },
    { typeof(CPointBroadcastClientCommand), "point_broadcastclientcommand" },
    { typeof(CPointAngularVelocitySensor), "point_angularvelocitysensor" },
    { typeof(CPointAngleSensor), "point_anglesensor" },
    { typeof(CPlayerVisibility), "env_player_visibility" },
    { typeof(CPlayerSprayDecal), "player_spray_decal" },
    { typeof(CPlayerPing), "info_player_ping" },
    { typeof(CPlatTrigger), "plat_trigger" },
    { typeof(CPlantedC4), "planted_c4" },
    { typeof(CPhysicsWire), "env_physwire" },
    { typeof(CPhysicsSpring), "phys_spring" },
    { typeof(CPhysicsPropRespawnable), "prop_physics_respawnable" },
    { typeof(CPhysicsPropOverride), "prop_physics_override" },
    { typeof(CPhysicsPropMultiplayer), "prop_physics_multiplayer" },
    { typeof(CPhysicsProp), "prop_physics" },
    { typeof(CPhysicsEntitySolver), "physics_entity_solver" },
    { typeof(CPhysicalButton), "func_physical_button" },
    { typeof(CPhysWheelConstraint), "phys_wheelconstraint" },
    { typeof(CPhysTorque), "phys_torque" },
    { typeof(CPhysThruster), "phys_thruster" },
    { typeof(CPhysSlideConstraint), "phys_slideconstraint" },
    { typeof(CPhysPulley), "phys_pulleyconstraint" },
    { typeof(CPhysMotor), "phys_motor" },
    { typeof(CPhysMagnet), "phys_magnet" },
    { typeof(CPhysLength), "phys_lengthconstraint" },
    { typeof(CPhysImpact), "env_physimpact" },
    { typeof(CPhysHinge), "phys_hinge" },
    { typeof(CPhysFixed), "phys_constraint" },
    { typeof(CPhysExplosion), "env_physexplosion" },
    { typeof(CPhysBox), "func_physbox" },
    { typeof(CPhysBallSocket), "phys_ballsocket" },
    { typeof(CPathTrack), "path_track" },
    { typeof(CPathSimple), "path_simple" },
    { typeof(CPathParticleRope), "path_particle_rope" },
    { typeof(CPathMover), "path_mover" },
    { typeof(CPathKeyFrame), "keyframe_track" },
    { typeof(CPathCornerCrash), "path_corner_crash" },
    { typeof(CPathCorner), "path_corner" },
    { typeof(CParticleSystem), "info_particle_system" },
    { typeof(COrnamentProp), "prop_dynamic_ornament" },
    { typeof(COmniLight), "light_omni2" },
    { typeof(CNullEntity), "info_null" },
    { typeof(CNavWalkable), "point_nav_walkable" },
    { typeof(CNavSpaceInfo), "info_nav_space" },
    { typeof(CNavLinkAreaEntity), "ai_nav_link_area" },
    { typeof(CMultiSource), "multisource" },
    { typeof(CMultiLightProxy), "logic_multilight_proxy" },
    { typeof(CMoverPathNode), "path_node_mover" },
    { typeof(CMomentaryRotButton), "momentary_rot_button" },
    { typeof(CMolotovProjectile), "molotov_projectile" },
    { typeof(CMolotovGrenade), "weapon_molotov" },
    { typeof(CMessageEntity), "point_message" },
    { typeof(CMessage), "env_message" },
    { typeof(CMathRemap), "math_remap" },
    { typeof(CMathCounter), "math_counter" },
    { typeof(CMathColorBlend), "math_colorblend" },
    { typeof(CMarkupVolumeWithRef), "markup_volume_with_ref" },
    { typeof(CMarkupVolumeTagged_NavGame), "func_nav_markup_game" },
    { typeof(CMarkupVolumeTagged_Nav), "func_nav_markup" },
    { typeof(CMarkupVolume), "markup_volume" },
    { typeof(CMapVetoPickController), "mapvetopick_controller" },
    { typeof(CMapSharedEnvironment), "map_shared_environment" },
    { typeof(CMapInfo), "info_map_parameters" },
    { typeof(CLogicScript), "logic_script" },
    { typeof(CLogicRelay), "logic_relay" },
    { typeof(CLogicProximity), "logic_proximity" },
    { typeof(CLogicPlayerProxy), "logic_playerproxy" },
    { typeof(CLogicNavigation), "logic_navigation" },
    { typeof(CLogicNPCCounterOBB), "logic_npc_counter_obb" },
    { typeof(CLogicNPCCounterAABB), "logic_npc_counter_aabb" },
    { typeof(CLogicNPCCounter), "logic_npc_counter_radius" },
    { typeof(CLogicMeasureMovement), "logic_measure_movement" },
    { typeof(CLogicLineToEntity), "logic_lineto" },
    { typeof(CLogicGameEventListener), "logic_gameevent_listener" },
    { typeof(CLogicGameEvent), "logic_game_event" },
    { typeof(CLogicEventListener), "logic_eventlistener" },
    { typeof(CLogicDistanceCheck), "logic_distance_check" },
    { typeof(CLogicDistanceAutosave), "logic_distance_autosave" },
    { typeof(CLogicCompare), "logic_compare" },
    { typeof(CLogicCollisionPair), "logic_collision_pair" },
    { typeof(CLogicCase), "logic_case" },
    { typeof(CLogicBranchList), "logic_branch_listener" },
    { typeof(CLogicBranch), "logic_branch" },
    { typeof(CLogicAutosave), "logic_autosave" },
    { typeof(CLogicAuto), "logic_auto" },
    { typeof(CLogicActiveAutosave), "logic_active_autosave" },
    { typeof(CLogicAchievement), "logic_achievement" },
    { typeof(CLightSpotEntity), "light_spot" },
    { typeof(CLightOrthoEntity), "light_ortho" },
    { typeof(CLightEnvironmentEntity), "light_environment" },
    { typeof(CLightEntity), "light_omni" },
    { typeof(CLightDirectionalEntity), "light_directional" },
    { typeof(CKnife), "weapon_knife" },
    { typeof(CKeepUpright), "phys_keepupright" },
    { typeof(CItem_Healthshot), "weapon_healthshot" },
    { typeof(CItemSoda), "item_sodacan" },
    { typeof(CItemKevlar), "item_kevlar" },
    { typeof(CItemGenericTriggerHelper), "item_generic_trigger_helper" },
    { typeof(CItemGeneric), "item_generic" },
    { typeof(CItemDefuser), "item_defuser" },
    { typeof(CItemAssaultSuit), "item_assaultsuit" },
    { typeof(CInstructorEventEntity), "point_instructor_event" },
    { typeof(CInstancedSceneEntity), "instanced_scripted_scene" },
    { typeof(CInfoWorldLayer), "info_world_layer" },
    { typeof(CInfoVisibilityBox), "info_visibility_box" },
    { typeof(CInfoTeleportDestination), "info_teleport_destination" },
    { typeof(CInfoTargetServerOnly), "info_target_server_only" },
    { typeof(CInfoTarget), "info_target" },
    { typeof(CInfoSpawnGroupLoadUnload), "info_spawngroup_load_unload" },
    { typeof(CInfoSpawnGroupLandmark), "info_spawngroup_landmark" },
    { typeof(CInfoPlayerTerrorist), "info_player_terrorist" },
    { typeof(CInfoPlayerStart), "info_player_start" },
    { typeof(CInfoPlayerCounterterrorist), "info_player_counterterrorist" },
    { typeof(CInfoParticleTarget), "info_particle_target" },
    { typeof(CInfoOffscreenPanoramaTexture), "info_offscreen_panorama_texture" },
    { typeof(CInfoLandmark), "info_landmark" },
    { typeof(CInfoLadderDismount), "info_ladder_dismount" },
    { typeof(CInfoInstructorHintTarget), "info_target_instructor_hint" },
    { typeof(CInfoInstructorHintHostageRescueZone), "info_hostage_rescue_zone_hint" },
    { typeof(CInfoInstructorHintBombTargetB), "info_bomb_target_hint_B" },
    { typeof(CInfoInstructorHintBombTargetA), "info_bomb_target_hint_A" },
    { typeof(CInfoGameEventProxy), "info_game_event_proxy" },
    { typeof(CInfoFan), "info_trigger_fan" },
    { typeof(CInfoDynamicShadowHintBox), "info_dynamic_shadow_hint_box" },
    { typeof(CInfoDynamicShadowHint), "info_dynamic_shadow_hint" },
    { typeof(CInfoDeathmatchSpawn), "info_deathmatch_spawn" },
    { typeof(CInfoData), "info_data" },
    { typeof(CInferno), "inferno" },
    { typeof(CIncendiaryGrenade), "weapon_incgrenade" },
    { typeof(CHostageRescueZone), "func_hostage_rescue" },
    { typeof(CHostage), "hostage_entity" },
    { typeof(CHandleTest), "handle_test" },
    { typeof(CHandleDummy), "handle_dummy" },
    { typeof(CHEGrenadeProjectile), "hegrenade_projectile" },
    { typeof(CHEGrenade), "weapon_hegrenade" },
    { typeof(CGunTarget), "func_guntarget" },
    { typeof(CGradientFog), "env_gradient_fog" },
    { typeof(CGenericConstraint), "phys_genericconstraint" },
    { typeof(CGameText), "game_text" },
    { typeof(CGamePlayerZone), "game_zone_player" },
    { typeof(CGamePlayerEquip), "game_player_equip" },
    { typeof(CGameMoney), "game_money" },
    { typeof(CGameGibManager), "game_gib_manager" },
    { typeof(CGameEnd), "game_end" },
    { typeof(CFuncWater), "func_water" },
    { typeof(CFuncWallToggle), "func_wall_toggle" },
    { typeof(CFuncWall), "func_wall" },
    { typeof(CFuncVehicleClip), "func_vehicleclip" },
    { typeof(CFuncVPhysicsClip), "func_clip_vphysics" },
    { typeof(CFuncTrainControls), "func_traincontrols" },
    { typeof(CFuncTrain), "func_train" },
    { typeof(CFuncTrackTrain), "func_tracktrain" },
    { typeof(CFuncTrackChange), "func_trackchange" },
    { typeof(CFuncTrackAuto), "func_trackautochange" },
    { typeof(CFuncTimescale), "func_timescale" },
    { typeof(CFuncTankTrain), "func_tanktrain" },
    { typeof(CFuncShatterglass), "func_shatterglass" },
    { typeof(CFuncRetakeBarrier), "func_retakebarrier" },
    { typeof(CFuncRotator), "func_rotator" },
    { typeof(CFuncRotating), "func_rotating" },
    { typeof(CFuncPropRespawnZone), "func_proprrespawnzone" },
    { typeof(CFuncPlatRot), "func_platrot" },
    { typeof(CFuncPlat), "func_plat" },
    { typeof(CFuncNavObstruction), "func_nav_avoidance_obstacle" },
    { typeof(CFuncNavBlocker), "func_nav_blocker" },
    { typeof(CFuncMover), "func_mover" },
    { typeof(CFuncMoveLinear), "momentary_door" },
    { typeof(CFuncMonitor), "func_monitor" },
    { typeof(CFuncLadder), "func_useableladder" },
    { typeof(CFuncIllusionary), "func_illusionary" },
    { typeof(CFuncElectrifiedVolume), "func_electrified_volume" },
    { typeof(CFuncConveyor), "func_conveyor" },
    { typeof(CFuncBrush), "func_brush" },
    { typeof(CFootstepControl), "func_footstep_control" },
    { typeof(CFogVolume), "fog_volume" },
    { typeof(CFogTrigger), "trigger_fog" },
    { typeof(CFogController), "env_fog_controller" },
    { typeof(CFlashbangProjectile), "flashbang_projectile" },
    { typeof(CFlashbang), "weapon_flashbang" },
    { typeof(CFishPool), "func_fish_pool" },
    { typeof(CFish), "fish" },
    { typeof(CFilterTeam), "filter_activator_team" },
    { typeof(CFilterProximity), "filter_proximity" },
    { typeof(CFilterName), "filter_activator_name" },
    { typeof(CFilterMultiple), "filter_multi" },
    { typeof(CFilterModel), "filter_activator_model" },
    { typeof(CFilterMassGreater), "filter_activator_mass_greater" },
    { typeof(CFilterLOS), "filter_los" },
    { typeof(CFilterEnemy), "filter_enemy" },
    { typeof(CFilterContext), "filter_activator_context" },
    { typeof(CFilterClass), "filter_activator_class" },
    { typeof(CFilterAttributeInt), "filter_activator_attribute_int" },
    { typeof(CEnvWindVolume), "env_wind_volume" },
    { typeof(CEnvWindController), "env_wind_controller" },
    { typeof(CEnvWind), "env_wind" },
    { typeof(CEnvVolumetricFogVolume), "env_volumetric_fog_volume" },
    { typeof(CEnvVolumetricFogController), "env_volumetric_fog_controller" },
    { typeof(CEnvViewPunch), "env_viewpunch" },
    { typeof(CEnvTilt), "env_tilt" },
    { typeof(CEnvSplash), "env_splash" },
    { typeof(CEnvSpark), "env_spark" },
    { typeof(CEnvSoundscapeTriggerable), "env_soundscape_triggerable" },
    { typeof(CEnvSoundscapeProxy), "env_soundscape_proxy" },
    { typeof(CEnvSoundscape), "env_soundscape" },
    { typeof(CEnvSky), "env_sky" },
    { typeof(CEnvShake), "env_shake" },
    { typeof(CEnvParticleGlow), "env_particle_glow" },
    { typeof(CEnvMuzzleFlash), "env_muzzleflash" },
    { typeof(CEnvLightProbeVolume), "env_light_probe_volume" },
    { typeof(CEnvLaser), "env_laser" },
    { typeof(CEnvInstructorVRHint), "env_instructor_vr_hint" },
    { typeof(CEnvInstructorHint), "env_instructor_hint" },
    { typeof(CEnvHudHint), "env_hudhint" },
    { typeof(CEnvGlobal), "env_global" },
    { typeof(CEnvFade), "env_fade" },
    { typeof(CEnvExplosion), "env_explosion" },
    { typeof(CEnvEntityMaker), "env_entity_maker" },
    { typeof(CEnvEntityIgniter), "env_entity_igniter" },
    { typeof(CEnvDetailController), "env_detail_controller" },
    { typeof(CEnvDecal), "env_decal" },
    { typeof(CEnvCubemapFog), "env_cubemap_fog" },
    { typeof(CEnvCubemapBox), "env_cubemap_box" },
    { typeof(CEnvCubemap), "env_cubemap" },
    { typeof(CEnvCombinedLightProbeVolume), "env_combined_light_probe_volume" },
    { typeof(CEnvBeverage), "env_beverage" },
    { typeof(CEnvBeam), "env_beam" },
    { typeof(CEntityInstance), "root" },
    { typeof(CEntityFlame), "entityflame" },
    { typeof(CEntityDissolve), "env_entity_dissolver" },
    { typeof(CEntityBlocker), "entity_blocker" },
    { typeof(CEnableMotionFixup), "point_enable_motion_fixup" },
    { typeof(CEconWearable), "wearable_item" },
    { typeof(CDynamicProp), "dynamic_prop" },
    { typeof(CDynamicNavConnectionsVolume), "func_nav_dynamic_connections" },
    { typeof(CDynamicLight), "light_dynamic" },
    { typeof(CDecoyProjectile), "decoy_projectile" },
    { typeof(CDecoyGrenade), "weapon_decoy" },
    { typeof(CDebugHistory), "env_debughistory" },
    { typeof(CDEagle), "weapon_deagle" },
    { typeof(CCredits), "env_credits" },
    { typeof(CConstraintAnchor), "info_constraint_anchor" },
    { typeof(CCommentaryViewPosition), "point_commentary_viewpoint" },
    { typeof(CCommentaryAuto), "commentary_auto" },
    { typeof(CColorCorrectionVolume), "color_correction_volume" },
    { typeof(CColorCorrection), "color_correction" },
    { typeof(CCitadelSoundOpvarSetOBB), "citadel_snd_opvar_set_obb" },
    { typeof(CChicken), "chicken" },
    { typeof(CChangeLevel), "trigger_changelevel" },
    { typeof(CCSWeaponBase), "weapon_cs_base" },
    { typeof(CCSTeam), "cs_team_manager" },
    { typeof(CCSSprite), "env_sprite_clientside" },
    { typeof(CCSServerPointScriptEntity), "point_script" },
    { typeof(CCSPlayerResource), "cs_player_manager" },
    { typeof(CCSPlace), "env_cs_place" },
    { typeof(CCSPetPlacement), "cs_pet_placement" },
    { typeof(CCSMinimapBoundary), "cs_minimap_boundary" },
    { typeof(CCSGameRulesProxy), "cs_gamerules" },
    { typeof(CCSGO_WingmanIntroTerroristPosition), "wingman_intro_terrorist" },
    { typeof(CCSGO_WingmanIntroCounterTerroristPosition), "wingman_intro_counterterrorist" },
    { typeof(CCSGO_TeamSelectTerroristPosition), "team_select_terrorist" },
    { typeof(CCSGO_TeamSelectCounterTerroristPosition), "team_select_counterterrorist" },
    { typeof(CCSGO_TeamIntroTerroristPosition), "team_intro_terrorist" },
    { typeof(CCSGO_TeamIntroCounterTerroristPosition), "team_intro_counterterrorist" },
    { typeof(CC4), "weapon_c4" },
    { typeof(CBuyZone), "func_buyzone" },
    { typeof(CBreakable), "func_breakable" },
    { typeof(CBombTarget), "func_bomb_target" },
    { typeof(CBlood), "env_blood" },
    { typeof(CBeam), "beam" },
    { typeof(CBaseTrigger), "trigger" },
    { typeof(CBasePlayerPawn), "baseplayerpawn" },
    { typeof(CBasePlayerController), "player_controller" },
    { typeof(CBaseMoveBehavior), "move_keyframed" },
    { typeof(CBaseModelEntity), "basemodelentity" },
    { typeof(CBaseGrenade), "grenade" },
    { typeof(CBaseFlex), "baseflex" },
    { typeof(CBaseFilter), "filter_base" },
    { typeof(CBaseDoor), "func_door" },
    { typeof(CBaseDMStart), "info_player_deathmatch" },
    { typeof(CBaseCSGrenade), "weapon_basecsgrenade" },
    { typeof(CBaseButton), "func_button" },
    { typeof(CBaseAnimGraph), "baseanimgraph" },
    { typeof(CBarnLight), "light_barn" },
    { typeof(CAmbientGeneric), "ambient_generic" },
    { typeof(CAK47), "weapon_ak47" },
    { typeof(CAI_ChangeHintGroup), "ai_changehintgroup" }
    }.ToFrozenDictionary();

  private string? GetEntityDesignerName<T>() where T : class, ISchemaClass<T>
  {
    return TypeToDesignerName.TryGetValue(typeof(T), out var name) ? name : null;
  }

  public void Dispose()
  {
    lock (_lock)
    {
      foreach (var callback in _callbacks)
      {
        callback.Dispose();
      }
      _callbacks.Clear();
    }
  }
}
