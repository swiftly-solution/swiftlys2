using System.Runtime.InteropServices;
using Spectre.Console;
using SwiftlyS2.Core.Memory;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Natives;

internal static class GameFunctions
{
    private static nint entitySystemPtr = 0;
    private static nint soundSystemPtr = 0;
    private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

    public static unsafe delegate* unmanaged< CTakeDamageInfo*, nint, nint, nint, Vector*, Vector*, float, int, int, void*, void > pCTakeDamageInfo_Constructor;
    public static unsafe delegate* unmanaged< nint, CTakeDamageInfo*, CTakeDamageResult*, void > pTakeDamage;
    public static unsafe delegate* unmanaged< nint, Ray_t*, Vector*, Vector*, CTraceFilter*, CGameTrace*, void > pTraceShape;
    public static unsafe delegate* unmanaged< nint, IntPtr, nint > pSetModel;
    public static unsafe delegate* unmanaged< nint, nint, byte, byte, byte, byte, void > pSetPlayerControllerPawn;
    public static unsafe delegate* unmanaged< nint, nint, float, void > pSetOrAddAttribute;
    public static unsafe delegate* unmanaged< int, nint, nint > pGetWeaponCSDataFromKey;
    public static unsafe delegate* unmanaged< nint, uint, nint, byte, CUtlSymbolLarge, byte, int, nint, nint, void > pDispatchParticleEffect;
    public static unsafe delegate* unmanaged< nint, float, uint, nint, void > pTerminateRoundWindows;
    public static unsafe delegate* unmanaged< nint, uint, nint, float, void > pTerminateRoundLinux;
    public static unsafe delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, int, nint > pCSmokeGrenadeProjectileEmitGrenade;
    public static unsafe delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint > pCFlashbangProjectileEmitGrenade;
    public static unsafe delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint > pCHEGrenadeProjectileEmitGrenade;
    public static unsafe delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint > pCDecoyProjectileEmitGrenade;
    public static unsafe delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint > pCMolotovProjectileEmitGrenade;
    public static unsafe delegate* unmanaged< nint, nint, nint, nint, nint, nint, float, nint, nint, void > pCEntitySystemAddEntityIOEvent;
    public static unsafe delegate* unmanaged< nint, int, nint > pCreateEntityByName;
    public static unsafe delegate* unmanaged< nint, nint, void > pCBaseEntityDispatchSpawn;
    public static unsafe delegate* unmanaged< nint, void > pUtilRemove;
    public static unsafe delegate* unmanaged< nint, nint, nint, nint, nint, void > pCEntityInstaceAcceptInput;
    public static unsafe delegate* unmanaged< nint, int, void > pSwitchTeam;
    public static unsafe delegate* unmanaged< nint, int, nint*, nint*, nint*, void > pCCSCustomHudLayoutSetDialogVariableStringForPlayer;
    public static unsafe delegate* unmanaged< nint, int, nint*, nint*, void > pCCSCustomHudLayoutRemoveDialogVariableStringForPlayer;
    public static unsafe delegate* unmanaged< nint, nint*, nint*, nint*, void > pCCSCustomHudLayoutSetDialogVariableString;
    public static unsafe delegate* unmanaged< nint, int, nint*, nint*, uint, void > pCCSCustomHudLayoutSetHasClassForPlayer;
    public static unsafe delegate* unmanaged< nint, nint*, nint*, uint, void > pCCSCustomHudLayoutSetHasClass;
    public static unsafe delegate* unmanaged< nint, int, byte, void > pCCSCustomHudLayoutSetInputCaptureEnabled;
    public static int TeleportOffset { get; private set; }
    public static int CommitSuicideOffset { get; private set; }
    public static int GetSkeletonInstanceOffset { get; private set; }
    public static int FindPickerEntityOffset { get; private set; }
    public static int RemoveWeaponsOffset { get; private set; }
    public static int GiveNamedItemOffset { get; private set; }
    public static int DropActiveItemOffset { get; private set; }
    public static int DropWeaponOffset { get; private set; }
    public static int SelectWeaponOffset { get; private set; }
    public static int AddResourceOffset { get; private set; }
    public static int CollisionRulesChangedOffset { get; private set; }
    public static int RespawnOffset { get; private set; }
    public static int GetViewVectorsOffset { get; private set; }
    public static int GoToIntermissionOffset { get; private set; }
    public static int ChangeTeamOffset { get; private set; }
    public static int TakeGuidOffset { get; private set; }
    public static int TraceShapeOffset { get; private set; }

    private static void CheckPtr( nint ptr, string name )
    {
        if (ptr == 0)
        {
            throw new ArgumentException($"Invalid pointer: {name}={ptr}");
        }
    }

    private static unsafe void CheckPtr( void* ptr, string name )
    {
        CheckPtr((nint)ptr, name);
    }

    public static void Initialize()
    {
        unsafe
        {
            TeleportOffset = NativeOffsets.Fetch("CBaseEntity::Teleport");
            CommitSuicideOffset = NativeOffsets.Fetch("CBasePlayerPawn::CommitSuicide");
            GetSkeletonInstanceOffset = NativeOffsets.Fetch("CGameSceneNode::GetSkeletonInstance");
            FindPickerEntityOffset = NativeOffsets.Fetch("CGameRules::FindPickerEntity");
            RemoveWeaponsOffset = NativeOffsets.Fetch("CCSPlayer_ItemServices::RemoveWeapons");
            GiveNamedItemOffset = NativeOffsets.Fetch("CCSPlayer_ItemServices::GiveNamedItem");
            DropActiveItemOffset = NativeOffsets.Fetch("CCSPlayer_ItemServices::DropActiveItem");
            DropWeaponOffset = NativeOffsets.Fetch("CCSPlayer_WeaponServices::DropWeapon");
            SelectWeaponOffset = NativeOffsets.Fetch("CCSPlayer_WeaponServices::SelectWeapon");
            AddResourceOffset = NativeOffsets.Fetch("CEntityResourceManifest::AddResource");
            CollisionRulesChangedOffset = NativeOffsets.Fetch("CBaseEntity::CollisionRulesChanged");
            RespawnOffset = NativeOffsets.Fetch("CCSPlayerController::Respawn");
            GetViewVectorsOffset = NativeOffsets.Fetch("CGameRules::GetViewVectors");
            GoToIntermissionOffset = NativeOffsets.Fetch("CGameRules::GoToIntermission");
            ChangeTeamOffset = NativeOffsets.Fetch("CCSPlayerController::ChangeTeam");
            TakeGuidOffset = NativeOffsets.Fetch("CSoundSystem::TakeGuid");
            TraceShapeOffset = NativeOffsets.Fetch("CNavPhysicsInterface::TraceShape");

            pCTakeDamageInfo_Constructor = (delegate* unmanaged< CTakeDamageInfo*, nint, nint, nint, Vector*, Vector*, float, int, int, void*, void >)NativeSignatures.Fetch("CTakeDamageInfo::Constructor");
            pTakeDamage = (delegate* unmanaged< nint, CTakeDamageInfo*, CTakeDamageResult*, void >)NativeSignatures.Fetch("CBaseEntity::TakeDamage");
            pTraceShape = (delegate* unmanaged< nint, Ray_t*, Vector*, Vector*, CTraceFilter*, CGameTrace*, void >)NativeSignatures.Fetch("TraceShape");
            pSetModel = (delegate* unmanaged< nint, IntPtr, nint >)NativeSignatures.Fetch("CBaseModelEntity::SetModel");
            pSetPlayerControllerPawn = (delegate* unmanaged< nint, nint, byte, byte, byte, byte, void >)NativeSignatures.Fetch("CBasePlayerController::SetPawn");
            pSetOrAddAttribute = (delegate* unmanaged< nint, IntPtr, float, void >)NativeSignatures.Fetch("CAttributeList::SetOrAddAttributeValueByName");
            pGetWeaponCSDataFromKey = (delegate* unmanaged< int, nint, nint >)NativeSignatures.Fetch("GetWeaponCSDataFromKey");
            pDispatchParticleEffect = (delegate* unmanaged< nint, uint, nint, byte, CUtlSymbolLarge, byte, int, nint, nint, void >)NativeSignatures.Fetch("DispatchParticleEffect");
            pCSmokeGrenadeProjectileEmitGrenade = (delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, int, nint >)NativeSignatures.Fetch("CSmokeGrenadeProjectile::EmitGrenade");
            pCFlashbangProjectileEmitGrenade = (delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint >)NativeSignatures.Fetch("CFlashbangProjectile::EmitGrenade");
            pCHEGrenadeProjectileEmitGrenade = (delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint >)NativeSignatures.Fetch("CHEGrenadeProjectile::EmitGrenade");
            pCDecoyProjectileEmitGrenade = (delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint >)NativeSignatures.Fetch("CDecoyProjectile::EmitGrenade");
            pCMolotovProjectileEmitGrenade = (delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint >)NativeSignatures.Fetch("CMolotovProjectile::EmitGrenade");
            pSwitchTeam = (delegate* unmanaged< nint, int, void >)NativeSignatures.Fetch("CCSPlayerController::SwitchTeam");

            pCEntitySystemAddEntityIOEvent = (delegate* unmanaged< nint, nint, nint, nint, nint, nint, float, nint, nint, void >)NativeSignatures.Fetch("CEntitySystem::AddEntityIOEvent");
            pCEntityInstaceAcceptInput = (delegate* unmanaged< nint, nint, nint, nint, nint, void >)NativeSignatures.Fetch("CEntityInstance::AcceptInput");
            pCreateEntityByName = (delegate* unmanaged< nint, int, nint >)NativeSignatures.Fetch("UTIL::CreateEntityByName");
            pCBaseEntityDispatchSpawn = (delegate* unmanaged< nint, nint, void >)NativeSignatures.Fetch("CBaseEntity::DispatchSpawn");
            pUtilRemove = (delegate* unmanaged< nint, void >)NativeSignatures.Fetch("UTIL::Remove");

            pCCSCustomHudLayoutSetDialogVariableStringForPlayer = (delegate* unmanaged< nint, int, nint*, nint*, nint*, void >)NativeSignatures.Fetch("CCSCustomHudLayout::SetDialogVariableStringForPlayer");
            pCCSCustomHudLayoutRemoveDialogVariableStringForPlayer = (delegate* unmanaged< nint, int, nint*, nint*, void >)NativeSignatures.Fetch("CCSCustomHudLayout::RemoveDialogVariableStringForPlayer");
            pCCSCustomHudLayoutSetDialogVariableString = (delegate* unmanaged< nint, nint*, nint*, nint*, void >)NativeSignatures.Fetch("CCSCustomHudLayout::SetDialogVariableString");
            pCCSCustomHudLayoutSetHasClassForPlayer = (delegate* unmanaged< nint, int, nint*, nint*, uint, void >)NativeSignatures.Fetch("CCSCustomHudLayout::SetHasClassForPlayer");
            pCCSCustomHudLayoutSetHasClass = (delegate* unmanaged< nint, nint*, nint*, uint, void >)NativeSignatures.Fetch("CCSCustomHudLayout::SetHasClass");
            pCCSCustomHudLayoutSetInputCaptureEnabled = (delegate* unmanaged< nint, int, byte, void >)NativeSignatures.Fetch("CCSCustomHudLayout::SetInputCaptureEnabled");

            if (IsWindows)
            {
                pTerminateRoundWindows = (delegate* unmanaged< nint, float, uint, nint, void >)NativeSignatures.Fetch("CGameRules::TerminateRound");
            }
            else
            {
                pTerminateRoundLinux = (delegate* unmanaged< nint, uint, nint, float, void >)NativeSignatures.Fetch("CGameRules::TerminateRound");
            }

            pTraceShape = (delegate* unmanaged< nint, Ray_t*, Vector*, Vector*, CTraceFilter*, CGameTrace*, void >)((void**)new S2BinLibService().FindVTable("server", "CNavPhysicsInterface")!.Value)[TraceShapeOffset];
        }
    }

    public static unsafe void* GetVirtualFunction( nint handle, int offset )
    {
        var ppVTable = (void***)handle;
        return *(*ppVTable + offset);
    }

    public static void DispatchParticleEffect( string particleName, uint attachmentType, nint entity, byte attachmentPoint, CUtlSymbolLarge attachmentName, bool resetAllParticlesOnEntity, int splitScreenSlot, CRecipientFilter filter )
    {
        NativeEngineHelpers.DispatchParticleEffect(particleName, attachmentType, entity, attachmentPoint, attachmentName._pString, resetAllParticlesOnEntity, splitScreenSlot, filter.ToMask());
    }

    public static nint CreateEntityByName( string name, int forcedIndex = -1 )
    {
        unsafe
        {
            using var nameStr = new ScopedCString(name);
            fixed (byte* pName = nameStr)
            {
                return pCreateEntityByName((nint)pName, forcedIndex);
            }
        }
    }

    public static void DispatchSpawn( nint pEntity, nint pKeyValues )
    {
        NativeBinding.ThrowIfNonMainThread();

        CheckPtr(pEntity, nameof(pEntity));

        unsafe
        {
            pCBaseEntityDispatchSpawn(pEntity, pKeyValues);
        }
    }

    public static void Despawn( nint pEntity )
    {
        NativeBinding.ThrowIfNonMainThread();

        CheckPtr(pEntity, nameof(pEntity));

        unsafe
        {
            pUtilRemove(pEntity);
        }
    }

    public static void TerminateRound( nint gameRules, uint reason, float delay, uint teamId )
    {
        CheckPtr(gameRules, nameof(gameRules));
        unsafe
        {
            if (IsWindows)
            {
                pTerminateRoundWindows(gameRules, delay, reason, teamId > 0 ? (nint)(&teamId) : 0);
            }
            else
            {
                pTerminateRoundLinux(gameRules, reason, teamId > 0 ? (nint)(&teamId) : 0, delay);
            }
        }
    }

    public static void GoToIntermission( nint gameRules, bool bAbortedMatch )
    {
        CheckPtr(gameRules, nameof(gameRules));
        unsafe
        {
            var pGoToIntermission = (delegate* unmanaged< nint, byte, void >)GetVirtualFunction(gameRules, GoToIntermissionOffset);
            pGoToIntermission(gameRules, (byte)(bAbortedMatch ? 1 : 0));
        }
    }

    public static nint GetWeaponCSDataFromKey( int unknown, string key )
    {
        unsafe
        {
            using var keyStr = new ScopedCString(key);
            fixed (byte* pKey = keyStr)
            {
                return pGetWeaponCSDataFromKey(unknown, (nint)pKey);
            }
        }
    }

    public static nint FindPickerEntity( nint handle, nint controller )
    {
        unsafe
        {
            CheckPtr(handle, nameof(handle));
            CheckPtr(controller, nameof(controller));
            var vfunc = (delegate* unmanaged< nint, nint, nint, nint >)GetVirtualFunction(handle, FindPickerEntityOffset);
            return vfunc(handle, controller, IntPtr.Zero);
        }
    }

    public static nint GetSkeletonInstance( nint handle )
    {
        CheckPtr(handle, nameof(handle));
        unsafe
        {
            var pSkeletonInstance = (delegate* unmanaged< nint, nint >)GetVirtualFunction(handle, GetSkeletonInstanceOffset);
            return pSkeletonInstance(handle);
        }
    }

    public static void PawnCommitSuicide( nint pPawn, bool bExplode, bool bForce )
    {
        CheckPtr(pPawn, nameof(pPawn));
        unsafe
        {
            var pCommitSuicide = (delegate* unmanaged< nint, byte, byte, void >)GetVirtualFunction(pPawn, CommitSuicideOffset);
            pCommitSuicide(pPawn, (byte)(bExplode ? 1 : 0), (byte)(bForce ? 1 : 0));
        }
    }

    public static void SetPlayerControllerPawn( nint pController, nint pPawn, bool b1, bool b2, bool b3, bool b4 )
    {
        CheckPtr(pController, nameof(pController));
        unsafe
        {
            pSetPlayerControllerPawn(pController, pPawn, (byte)(b1 ? 1 : 0), (byte)(b2 ? 1 : 0), (byte)(b3 ? 1 : 0), (byte)(b4 ? 1 : 0));
        }
    }

    public static void SetModel( nint pEntity, string model )
    {
        CheckPtr(pEntity, nameof(pEntity));

        unsafe
        {
            using var modelStr = new ScopedCString(model);
            fixed (byte* pModel = modelStr)
            {
                _ = pSetModel(pEntity, (nint)pModel);
            }
        }
    }

    public static unsafe void Teleport(
        nint pEntity,
        Vector* vecPosition,
        QAngle* vecAngle,
        Vector* vecVelocity
    )
    {
        CheckPtr(pEntity, nameof(pEntity));
        unsafe
        {
            var pTeleport = (delegate* unmanaged< nint, Vector*, QAngle*, Vector*, void >)GetVirtualFunction(pEntity, TeleportOffset);
            pTeleport(pEntity, vecPosition, vecAngle, vecVelocity);
        }
    }

    private static unsafe bool Is16Aligned( CGameTrace* pTrace ) => ((nuint)pTrace & 15) == 0;

    public static unsafe void TraceShape(
        Ray_t* ray,
        Vector vecStart,
        Vector vecEnd,
        CTraceFilter* pFilter,
        CGameTrace* pTrace
    )
    {
        unsafe
        {
            CheckPtr(pTrace, nameof(pTrace));
            // FUCK YOU WINDOWS
            if (IsWindows || Is16Aligned(pTrace))
            {
                pTraceShape(0, ray, &vecStart, &vecEnd, pFilter, pTrace);
            }
            // FUCK YOU LINUX SIMD ALIGNMENT
            else
            {
                var size = (nuint)sizeof(CGameTrace);
                var rawBuffer = stackalloc byte[(int)size + 16];
                var pAligned = (CGameTrace*)(((nuint)rawBuffer + 15) & ~(nuint)15);
                NativeMemory.Copy(pTrace, pAligned, size);
                pTraceShape(0, ray, &vecStart, &vecEnd, pFilter, pAligned);
                NativeMemory.Copy(pAligned, pTrace, size);
            }
        }
    }

    public static unsafe void CTakeDamageInfoConstructor(
        CTakeDamageInfo* pThis,
        nint pInflictor,
        nint pAttacker,
        nint pAbility,
        Vector* vecDamageForce,
        Vector* vecDamagePosition,
        float flDamage,
        int bitsDamageType,
        int iCustomDamage,
        void* a10
    )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            pCTakeDamageInfo_Constructor(pThis, pInflictor, pAttacker, pAbility, vecDamageForce, vecDamagePosition, flDamage, bitsDamageType, iCustomDamage, a10);
        }
    }

    public static void CCSPlayerControllerChangeTeam( nint controller, Team team )
    {
        unsafe
        {
            CheckPtr(controller, nameof(controller));
            var pChangeTeam = (delegate* unmanaged< nint, int, void >)GetVirtualFunction(controller, ChangeTeamOffset);
            pChangeTeam(controller, (int)team);
        }
    }

    public static void CCSPlayerControllerSwitchTeam( nint controller, Team team )
    {
        unsafe
        {
            CheckPtr(controller, nameof(controller));
            pSwitchTeam(controller, (int)team);
        }
    }

    public static unsafe void TakeDamage( nint pEntity, CTakeDamageInfo* info )
    {
        CheckPtr(pEntity, nameof(pEntity));
        unsafe
        {
            pTakeDamage(pEntity, info, (CTakeDamageResult*)0);
        }
    }

    public static void CCSPlayer_ItemServices_RemoveWeapons( nint pThis )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            var pRemoveWeapons = (delegate* unmanaged< nint, void >)GetVirtualFunction(pThis, RemoveWeaponsOffset);
            pRemoveWeapons(pThis);
        }
    }

    public static void CEntityInstance_AcceptInput( nint pEntity, string input, nint pActivator, nint pCaller, nint pVariant, int outputID )
    {
        unsafe
        {
            CheckPtr(pEntity, nameof(pEntity));

            using var inputStr = new ScopedCString(input);
            fixed (byte* pInput = inputStr)
            {
                pCEntityInstaceAcceptInput(pEntity, (nint)pInput, pActivator, pCaller, pVariant);
            }
        }
    }

    public static void AddEntityIOEvent( nint pEntity, string input, nint pActivator, nint pCaller, nint pVariant, float delay )
    {
        unsafe
        {
            CheckPtr(pEntity, nameof(pEntity));

            if (entitySystemPtr == 0)
                entitySystemPtr = NativeEntitySystem.GetEntitySystem();

            using var inputStr = new ScopedCString(input);
            fixed (byte* pInput = inputStr)
            {
                pCEntitySystemAddEntityIOEvent(entitySystemPtr, pEntity, (nint)pInput, pActivator, pCaller, pVariant, delay, 0, 0);
            }
        }
    }

    public static nint CCSPlayer_ItemServices_GiveNamedItem( nint pThis, string name )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            var ppVTable = (void***)pThis;
            var pGiveNamedItem = (delegate* unmanaged< nint, nint, nint >)ppVTable[0][GiveNamedItemOffset];
            using var nameStr = new ScopedCString(name);
            fixed (byte* pName = nameStr)
            {
                return pGiveNamedItem(pThis, (nint)pName);
            }
        }
    }

    public static void CCSPlayer_ItemServices_DropActiveItem( nint pThis, Vector momentum )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            var pDropActiveItem = (delegate* unmanaged< nint, Vector*, void >)GetVirtualFunction(pThis, DropActiveItemOffset);
            pDropActiveItem(pThis, &momentum);
        }
    }

    public static unsafe void CCSPlayer_WeaponServices_DropWeapon( nint pThis, nint pWeapon, Vector* momentum )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            CheckPtr(pWeapon, nameof(pWeapon));
            var pDropWeapon = (delegate* unmanaged< nint, nint, nint, Vector*, void >)GetVirtualFunction(pThis, DropWeaponOffset);
            pDropWeapon(pThis, pWeapon, 0, momentum);
        }
    }

    public static void CCSPlayer_WeaponServices_SelectWeapon( nint pThis, nint pWeapon )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            CheckPtr(pWeapon, nameof(pWeapon));
            var pSelectWeapon = (delegate* unmanaged< nint, nint, void >)GetVirtualFunction(pThis, SelectWeaponOffset);
            pSelectWeapon(pThis, pWeapon);
        }
    }

    public static void CEntityResourceManifest_AddResource( nint pThis, string path )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            var pAddResource = (delegate* unmanaged< nint, nint, void >)GetVirtualFunction(pThis, AddResourceOffset);
            using var pathStr = new ScopedCString(path);
            fixed (byte* pPath = pathStr)
            {
                pAddResource(pThis, (nint)pPath);
            }
        }
    }

    public static void SetOrAddAttribute( nint handle, string name, float value )
    {
        unsafe
        {
            CheckPtr(handle, nameof(handle));
            using var nameStr = new ScopedCString(name);
            fixed (byte* pName = nameStr)
            {
                pSetOrAddAttribute(handle, (nint)pName, value);
            }
        }
    }

    public static void CBaseEntity_CollisionRulesChanged( nint pThis )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            var pCollisionRulesChanged = (delegate* unmanaged< nint, void >)GetVirtualFunction(pThis, CollisionRulesChangedOffset);
            pCollisionRulesChanged(pThis);
        }
    }

    public static void CCSPlayerController_Respawn( nint pThis )
    {
        CheckPtr(pThis, nameof(pThis));
        unsafe
        {
            var pRespawn = (delegate* unmanaged< nint, void >)GetVirtualFunction(pThis, RespawnOffset);
            pRespawn(pThis);
        }
    }

    public static nint CSmokeGrenadeProjectile_EmitGrenade( Vector pos, QAngle angle, Vector velocity, nint owner, Team team, uint itemdefindex )
    {
        unsafe
        {
            return pCSmokeGrenadeProjectileEmitGrenade(&pos, &angle, &velocity, &velocity, owner, itemdefindex, (int)team);
        }
    }

    public static nint CFlashbangProjectile_EmitGrenade( Vector pos, QAngle angle, Vector velocity, nint owner, uint itemdefindex )
    {
        unsafe
        {
            return pCFlashbangProjectileEmitGrenade(&pos, &angle, &velocity, &velocity, owner, itemdefindex);
        }
    }

    public static nint CHEGrenadeProjectile_EmitGrenade( Vector pos, QAngle angle, Vector velocity, nint owner, uint itemdefindex )
    {
        unsafe
        {
            return pCHEGrenadeProjectileEmitGrenade(&pos, &angle, &velocity, &velocity, owner, itemdefindex);
        }
    }

    public static nint CDecoyProjectile_EmitGrenade( Vector pos, QAngle angle, Vector velocity, nint owner, uint itemdefindex )
    {
        unsafe
        {
            return pCDecoyProjectileEmitGrenade(&pos, &angle, &velocity, &velocity, owner, itemdefindex);
        }
    }

    public static nint CMolotovProjectile_EmitGrenade( Vector pos, QAngle angle, Vector velocity, nint owner, uint itemdefindex )
    {
        unsafe
        {
            return pCMolotovProjectileEmitGrenade(&pos, &angle, &velocity, &velocity, owner, itemdefindex);
        }
    }

    public static unsafe CViewVectors* CGameRules_GetViewVectors( nint pThis )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            var pGetViewVectors = (delegate* unmanaged< nint, CViewVectors* >)GetVirtualFunction(pThis, GetViewVectorsOffset);
            return pGetViewVectors(pThis);
        }
    }

    public static void CCSCustomHudLayout_SetDialogVariableStringForPlayer( nint pThis, int player, string panelId, string variableName, string value )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            using var panelIdStr = new ScopedCString(panelId);
            using var variableNameStr = new ScopedCString(variableName);
            using var valueStr = new ScopedCString(value);

            fixed (byte* pPanelId = panelIdStr)
            fixed (byte* pVariableName = variableNameStr)
            fixed (byte* pValue = valueStr)
            {
                var panelIdUtlString = (nint)pPanelId;
                var variableNameUtlString = (nint)pVariableName;
                var valueUtlString = (nint)pValue;

                pCCSCustomHudLayoutSetDialogVariableStringForPlayer(
                    pThis,
                    player,
                    &panelIdUtlString,
                    &variableNameUtlString,
                    &valueUtlString
                );
            }
        }
    }

    public static void CCSCustomHudLayout_RemoveDialogVariableStringForPlayer( nint pThis, int player, string panelId, string variableName )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            using var panelIdStr = new ScopedCString(panelId);
            using var variableNameStr = new ScopedCString(variableName);

            fixed (byte* pPanelId = panelIdStr)
            fixed (byte* pVariableName = variableNameStr)
            {
                var panelIdUtlString = (nint)pPanelId;
                var variableNameUtlString = (nint)pVariableName;

                pCCSCustomHudLayoutRemoveDialogVariableStringForPlayer(
                    pThis,
                    player,
                    &panelIdUtlString,
                    &variableNameUtlString
                );
            }
        }
    }

    public static void CCSCustomHudLayout_SetDialogVariableString( nint pThis, string panelId, string variableName, string value )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            using var panelIdStr = new ScopedCString(panelId);
            using var variableNameStr = new ScopedCString(variableName);
            using var valueStr = new ScopedCString(value);

            fixed (byte* pPanelId = panelIdStr)
            fixed (byte* pVariableName = variableNameStr)
            fixed (byte* pValue = valueStr)
            {
                var panelIdUtlString = (nint)pPanelId;
                var variableNameUtlString = (nint)pVariableName;
                var valueUtlString = (nint)pValue;

                pCCSCustomHudLayoutSetDialogVariableString(
                    pThis,
                    &panelIdUtlString,
                    &variableNameUtlString,
                    &valueUtlString
                );
            }
        }
    }

    public static void CCSCustomHudLayout_SetHasClassForPlayer( nint pThis, int player, string panelId, string className, EHudPanelClassStatus_t classStatus )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            using var panelIdStr = new ScopedCString(panelId);
            using var classNameStr = new ScopedCString(className);

            fixed (byte* pPanelId = panelIdStr)
            fixed (byte* pClassName = classNameStr)
            {
                var panelIdUtlString = (nint)pPanelId;
                var classNameUtlString = (nint)pClassName;

                pCCSCustomHudLayoutSetHasClassForPlayer(
                    pThis,
                    player,
                    &panelIdUtlString,
                    &classNameUtlString,
                    (uint)classStatus
                );
            }
        }
    }

    public static void CCSCustomHudLayout_SetHasClass( nint pThis, string panelId, string className, EHudPanelClassStatus_t classStatus )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            using var panelIdStr = new ScopedCString(panelId);
            using var classNameStr = new ScopedCString(className);

            fixed (byte* pPanelId = panelIdStr)
            fixed (byte* pClassName = classNameStr)
            {
                var panelIdUtlString = (nint)pPanelId;
                var classNameUtlString = (nint)pClassName;

                pCCSCustomHudLayoutSetHasClass(
                    pThis,
                    &panelIdUtlString,
                    &classNameUtlString,
                    (uint)classStatus
                );
            }
        }
    }

    public static unsafe uint CSoundSystem_TakeGuid()
    {
        if (soundSystemPtr == 0)
            soundSystemPtr = NativeMemoryHelpers.FetchInterfaceByName("SoundSystem001");

        CheckPtr(soundSystemPtr, nameof(soundSystemPtr));

        uint guid;
        if (IsWindows)
        {
            var pTakeGuid = (delegate* unmanaged< nint, uint*, void >)GetVirtualFunction(soundSystemPtr, TakeGuidOffset);
            pTakeGuid(soundSystemPtr, &guid);
        }
        else
        {
            var pTakeGuid = (delegate* unmanaged< nint, uint >)GetVirtualFunction(soundSystemPtr, TakeGuidOffset);
            guid = pTakeGuid(soundSystemPtr);
        }
        return guid;
    }

    public static void CCSCustomHudLayout_SetInputCaptureEnabled( nint pThis, int playerId, bool enabled )
    {
        unsafe
        {
            CheckPtr(pThis, nameof(pThis));
            pCCSCustomHudLayoutSetInputCaptureEnabled(pThis, playerId, enabled ? (byte)1 : (byte)0);
        }
    }
}
