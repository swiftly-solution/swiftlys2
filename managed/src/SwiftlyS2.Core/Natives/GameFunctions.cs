using System.Text;
using System.Buffers;
using System.Runtime.InteropServices;
using Spectre.Console;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;

namespace SwiftlyS2.Core.Natives;

internal static class GameFunctions
{
    private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
    public static unsafe delegate* unmanaged< CTakeDamageInfo*, nint, nint, nint, Vector*, Vector*, float, int, int, void*, void > pCTakeDamageInfo_Constructor;
    public static unsafe delegate* unmanaged< nint, CTakeDamageInfo*, CTakeDamageResult*, void > pTakeDamage;
    public static unsafe delegate* unmanaged< nint, Ray_t*, Vector*, Vector*, CTraceFilter*, CGameTrace*, void > pTraceShape;
    public static unsafe delegate* unmanaged< Vector*, Vector*, BBox_t*, CTraceFilter*, CGameTrace*, void > pTracePlayerBBox;
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
    public static unsafe delegate* unmanaged< nint, int, void > pSwitchTeam;
    private static Lazy<int> CreateOffset( string name ) => new(() => NativeOffsets.Fetch(name));
    private static readonly Lazy<int> _teleportOffset = CreateOffset("CBaseEntity::Teleport");
    private static readonly Lazy<int> _commitSuicideOffset = CreateOffset("CBasePlayerPawn::CommitSuicide");
    private static readonly Lazy<int> _getSkeletonInstanceOffset = CreateOffset("CGameSceneNode::GetSkeletonInstance");
    private static readonly Lazy<int> _findPickerEntityOffset = CreateOffset("CGameRules::FindPickerEntity");
    private static readonly Lazy<int> _removeWeaponsOffset = CreateOffset("CCSPlayer_ItemServices::RemoveWeapons");
    private static readonly Lazy<int> _giveNamedItemOffset = CreateOffset("CCSPlayer_ItemServices::GiveNamedItem");
    private static readonly Lazy<int> _dropActiveItemOffset = CreateOffset("CCSPlayer_ItemServices::DropActiveItem");
    private static readonly Lazy<int> _dropWeaponOffset = CreateOffset("CCSPlayer_WeaponServices::DropWeapon");
    private static readonly Lazy<int> _selectWeaponOffset = CreateOffset("CCSPlayer_WeaponServices::SelectWeapon");
    private static readonly Lazy<int> _addResourceOffset = CreateOffset("CEntityResourceManifest::AddResource");
    private static readonly Lazy<int> _collisionRulesChangedOffset = CreateOffset("CBaseEntity::CollisionRulesChanged");
    private static readonly Lazy<int> _respawnOffset = CreateOffset("CCSPlayerController::Respawn");
    private static readonly Lazy<int> _getViewVectorsOffset = CreateOffset("CGameRules::GetViewVectors");
    private static readonly Lazy<int> _goToIntermissionOffset = CreateOffset("CGameRules::GoToIntermission");
    private static readonly Lazy<int> _changeTeamOffset = CreateOffset("CCSPlayerController::ChangeTeam");

    public static int TeleportOffset => _teleportOffset.Value;
    public static int CommitSuicideOffset => _commitSuicideOffset.Value;
    public static int GetSkeletonInstanceOffset => _getSkeletonInstanceOffset.Value;
    public static int FindPickerEntityOffset => _findPickerEntityOffset.Value;
    public static int RemoveWeaponsOffset => _removeWeaponsOffset.Value;
    public static int GiveNamedItemOffset => _giveNamedItemOffset.Value;
    public static int DropActiveItemOffset => _dropActiveItemOffset.Value;
    public static int DropWeaponOffset => _dropWeaponOffset.Value;
    public static int SelectWeaponOffset => _selectWeaponOffset.Value;
    public static int AddResourceOffset => _addResourceOffset.Value;
    public static int CollisionRulesChangedOffset => _collisionRulesChangedOffset.Value;
    public static int RespawnOffset => _respawnOffset.Value;
    public static int GetViewVectorsOffset => _getViewVectorsOffset.Value;
    public static int GoToIntermissionOffset => _goToIntermissionOffset.Value;
    public static int ChangeTeamOffset => _changeTeamOffset.Value;

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
            pCTakeDamageInfo_Constructor = (delegate* unmanaged< CTakeDamageInfo*, nint, nint, nint, Vector*, Vector*, float, int, int, void*, void >)NativeSignatures.Fetch("CTakeDamageInfo::Constructor");
            pTakeDamage = (delegate* unmanaged< nint, CTakeDamageInfo*, CTakeDamageResult*, void >)NativeSignatures.Fetch("CBaseEntity::TakeDamage");
            pTraceShape = (delegate* unmanaged< nint, Ray_t*, Vector*, Vector*, CTraceFilter*, CGameTrace*, void >)NativeSignatures.Fetch("TraceShape");
            pTracePlayerBBox = (delegate* unmanaged< Vector*, Vector*, BBox_t*, CTraceFilter*, CGameTrace*, void >)NativeSignatures.Fetch("TracePlayerBBox");
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
            if (IsWindows)
            {
                pTerminateRoundWindows = (delegate* unmanaged< nint, float, uint, nint, void >)NativeSignatures.Fetch("CGameRules::TerminateRound");
            }
            else
            {
                pTerminateRoundLinux = (delegate* unmanaged< nint, uint, nint, float, void >)NativeSignatures.Fetch("CGameRules::TerminateRound");
            }
        }
    }

    public static unsafe void* GetVirtualFunction( nint handle, int offset )
    {
        var ppVTable = (void***)handle;
        return *(*ppVTable + offset);
    }

    public static void DispatchParticleEffect( string particleName, uint attachmentType, nint entity, byte attachmentPoint, CUtlSymbolLarge attachmentName, bool resetAllParticlesOnEntity, int splitScreenSlot, CRecipientFilter filter )
    {
        try
        {
            NativeEngineHelpers.DispatchParticleEffect(particleName, attachmentType, entity, attachmentPoint, attachmentName._pString, resetAllParticlesOnEntity, splitScreenSlot, filter.ToMask());
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static void TerminateRound( nint gameRules, uint reason, float delay, uint teamId )
    {
        try
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
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static void GoToIntermission( nint gameRules, bool bAbortedMatch )
    {
        try
        {
            CheckPtr(gameRules, nameof(gameRules));
            unsafe
            {
                var pGoToIntermission = (delegate* unmanaged< nint, byte, void >)GetVirtualFunction(gameRules, GoToIntermissionOffset);
                pGoToIntermission(gameRules, (byte)(bAbortedMatch ? 1 : 0));
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static nint GetWeaponCSDataFromKey( int unknown, string key )
    {
        try
        {
            unsafe
            {
                return StringAlloc.CreateCString(key, pKey =>
                {
                    return pGetWeaponCSDataFromKey(unknown, pKey);
                });
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    public static nint FindPickerEntity( nint handle, nint controller )
    {
        try
        {
            unsafe
            {
                CheckPtr(handle, nameof(handle));
                CheckPtr(controller, nameof(controller));
                var vfunc = (delegate* unmanaged< nint, nint, nint, nint >)GetVirtualFunction(handle, FindPickerEntityOffset);
                return vfunc(handle, controller, IntPtr.Zero);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
        return 0;
    }

    public static nint GetSkeletonInstance( nint handle )
    {
        try
        {
            CheckPtr(handle, nameof(handle));
            unsafe
            {
                var pSkeletonInstance = (delegate* unmanaged< nint, nint >)GetVirtualFunction(handle, GetSkeletonInstanceOffset);
                return pSkeletonInstance(handle);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
        return 0;
    }

    public static void PawnCommitSuicide( nint pPawn, bool bExplode, bool bForce )
    {
        try
        {
            CheckPtr(pPawn, nameof(pPawn));
            unsafe
            {
                var pCommitSuicide = (delegate* unmanaged< nint, byte, byte, void >)GetVirtualFunction(pPawn, CommitSuicideOffset);
                pCommitSuicide(pPawn, (byte)(bExplode ? 1 : 0), (byte)(bForce ? 1 : 0));
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static void SetPlayerControllerPawn( nint pController, nint pPawn, bool b1, bool b2, bool b3, bool b4 )
    {
        try
        {
            CheckPtr(pController, nameof(pController));
            unsafe
            {
                pSetPlayerControllerPawn(pController, pPawn, (byte)(b1 ? 1 : 0), (byte)(b2 ? 1 : 0), (byte)(b3 ? 1 : 0), (byte)(b4 ? 1 : 0));
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static void SetModel( nint pEntity, string model )
    {
        try
        {
            CheckPtr(pEntity, nameof(pEntity));

            StringAlloc.CreateCString(model, pModel =>
            {
                unsafe
                {
                    _ = pSetModel(pEntity, pModel);
                }
            });
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static unsafe void Teleport(
        nint pEntity,
        Vector* vecPosition,
        QAngle* vecAngle,
        Vector* vecVelocity
    )
    {
        try
        {
            CheckPtr(pEntity, nameof(pEntity));
            unsafe
            {
                var pTeleport = (delegate* unmanaged< nint, Vector*, QAngle*, Vector*, void >)GetVirtualFunction(pEntity, TeleportOffset);
                pTeleport(pEntity, vecPosition, vecAngle, vecVelocity);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    private static unsafe bool Is16Aligned( CGameTrace* pTrace ) => ((nuint)pTrace & 15) == 0;

    public static unsafe void TracePlayerBBox(
        Vector vecStart,
        Vector vecEnd,
        BBox_t bounds,
        CTraceFilter* pFilter,
        CGameTrace* pTrace
    )
    {
        try
        {
            CheckPtr(pTrace, nameof(pTrace));
            unsafe
            {
                // FUCK ALL OF YOU SHIT
                if (IsWindows || Is16Aligned(pTrace))
                {
                    pTracePlayerBBox(&vecStart, &vecEnd, &bounds, pFilter, pTrace);
                }
                else
                {
                    var size = (nuint)sizeof(CGameTrace);
                    var rawBuffer = stackalloc byte[(int)size + 16];
                    var pAligned = (CGameTrace*)(((nuint)rawBuffer + 15) & ~(nuint)15);
                    NativeMemory.Copy(pTrace, pAligned, size);
                    pTracePlayerBBox(&vecStart, &vecEnd, &bounds, pFilter, pAligned);
                    NativeMemory.Copy(pAligned, pTrace, size);
                }
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static unsafe void TraceShape(
        nint pEngineTrace,
        Ray_t* ray,
        Vector vecStart,
        Vector vecEnd,
        CTraceFilter* pFilter,
        CGameTrace* pTrace
    )
    {
        try
        {
            unsafe
            {
                CheckPtr(pEngineTrace, nameof(pEngineTrace));
                CheckPtr(pTrace, nameof(pTrace));
                // FUCK YOU WINDOWS
                if (IsWindows || Is16Aligned(pTrace))
                {
                    pTraceShape(pEngineTrace, ray, &vecStart, &vecEnd, pFilter, pTrace);
                }
                // FUCK YOU LINUX SIMD ALIGNMENT
                else
                {
                    var size = (nuint)sizeof(CGameTrace);
                    var rawBuffer = stackalloc byte[(int)size + 16];
                    var pAligned = (CGameTrace*)(((nuint)rawBuffer + 15) & ~(nuint)15);
                    NativeMemory.Copy(pTrace, pAligned, size);
                    pTraceShape(pEngineTrace, ray, &vecStart, &vecEnd, pFilter, pAligned);
                    NativeMemory.Copy(pAligned, pTrace, size);
                }
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
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
        try
        {
            unsafe
            {
                CheckPtr(pThis, nameof(pThis));
                pCTakeDamageInfo_Constructor(pThis, pInflictor, pAttacker, pAbility, vecDamageForce, vecDamagePosition, flDamage, bitsDamageType, iCustomDamage, a10);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static void CCSPlayerControllerChangeTeam( nint controller, Team team )
    {
        try
        {
            unsafe
            {
                CheckPtr(controller, nameof(controller));
                var pChangeTeam = (delegate* unmanaged< nint, int, void >)GetVirtualFunction(controller, ChangeTeamOffset);
                pChangeTeam(controller, (int)team);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static void CCSPlayerControllerSwitchTeam( nint controller, Team team )
    {
        try
        {
            unsafe
            {
                CheckPtr(controller, nameof(controller));
                pSwitchTeam(controller, (int)team);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static unsafe void TakeDamage( nint pEntity, CTakeDamageInfo* info )
    {
        try
        {
            CheckPtr(pEntity, nameof(pEntity));
            unsafe
            {
                pTakeDamage(pEntity, info, (CTakeDamageResult*)0);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static void CCSPlayer_ItemServices_RemoveWeapons( nint pThis )
    {
        try
        {
            unsafe
            {
                CheckPtr(pThis, nameof(pThis));
                var pRemoveWeapons = (delegate* unmanaged< nint, void >)GetVirtualFunction(pThis, RemoveWeaponsOffset);
                pRemoveWeapons(pThis);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static nint CCSPlayer_ItemServices_GiveNamedItem( nint pThis, string name )
    {
        try
        {
            unsafe
            {
                CheckPtr(pThis, nameof(pThis));
                var ppVTable = (void***)pThis;
                var pGiveNamedItem = (delegate* unmanaged< nint, nint, nint >)ppVTable[0][GiveNamedItemOffset];
                return StringAlloc.CreateCString(name, pName =>
                {
                    return pGiveNamedItem(pThis, pName);
                });
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    public static void CCSPlayer_ItemServices_DropActiveItem( nint pThis, Vector momentum )
    {
        try
        {
            unsafe
            {
                CheckPtr(pThis, nameof(pThis));
                var pDropActiveItem = (delegate* unmanaged< nint, Vector*, void >)GetVirtualFunction(pThis, DropActiveItemOffset);
                pDropActiveItem(pThis, &momentum);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static unsafe void CCSPlayer_WeaponServices_DropWeapon( nint pThis, nint pWeapon, Vector* momentum )
    {
        try
        {
            unsafe
            {
                CheckPtr(pThis, nameof(pThis));
                CheckPtr(pWeapon, nameof(pWeapon));
                var pDropWeapon = (delegate* unmanaged< nint, nint, nint, Vector*, void >)GetVirtualFunction(pThis, DropWeaponOffset);
                pDropWeapon(pThis, pWeapon, 0, momentum);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static void CCSPlayer_WeaponServices_SelectWeapon( nint pThis, nint pWeapon )
    {
        try
        {
            unsafe
            {
                CheckPtr(pThis, nameof(pThis));
                CheckPtr(pWeapon, nameof(pWeapon));
                var pSelectWeapon = (delegate* unmanaged< nint, nint, void >)GetVirtualFunction(pThis, SelectWeaponOffset);
                pSelectWeapon(pThis, pWeapon);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static void CEntityResourceManifest_AddResource( nint pThis, string path )
    {
        try
        {
            unsafe
            {
                CheckPtr(pThis, nameof(pThis));
                var pAddResource = (delegate* unmanaged< nint, nint, void >)GetVirtualFunction(pThis, AddResourceOffset);
                StringAlloc.CreateCString(path, pPath =>
                {
                    pAddResource(pThis, pPath);
                });
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static void SetOrAddAttribute( nint handle, string name, float value )
    {
        try
        {
            unsafe
            {
                CheckPtr(handle, nameof(handle));
                StringAlloc.CreateCString(name, pName =>
                {
                    pSetOrAddAttribute(handle, (nint)pName, value);
                });
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static void CBaseEntity_CollisionRulesChanged( nint pThis )
    {
        try
        {
            unsafe
            {
                CheckPtr(pThis, nameof(pThis));
                var pCollisionRulesChanged = (delegate* unmanaged< nint, void >)GetVirtualFunction(pThis, CollisionRulesChangedOffset);
                pCollisionRulesChanged(pThis);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static void CCSPlayerController_Respawn( nint pThis )
    {
        try
        {
            CheckPtr(pThis, nameof(pThis));
            unsafe
            {
                var pRespawn = (delegate* unmanaged< nint, void >)GetVirtualFunction(pThis, RespawnOffset);
                pRespawn(pThis);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static nint CSmokeGrenadeProjectile_EmitGrenade( Vector pos, QAngle angle, Vector velocity, nint owner, Team team, uint itemdefindex )
    {
        try
        {
            unsafe
            {
                return pCSmokeGrenadeProjectileEmitGrenade(&pos, &angle, &velocity, &velocity, owner, itemdefindex, (int)team);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    public static nint CFlashbangProjectile_EmitGrenade( Vector pos, QAngle angle, Vector velocity, nint owner, uint itemdefindex )
    {
        try
        {
            unsafe
            {
                return pCFlashbangProjectileEmitGrenade(&pos, &angle, &velocity, &velocity, owner, itemdefindex);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    public static nint CHEGrenadeProjectile_EmitGrenade( Vector pos, QAngle angle, Vector velocity, nint owner, uint itemdefindex )
    {
        try
        {
            unsafe
            {
                return pCHEGrenadeProjectileEmitGrenade(&pos, &angle, &velocity, &velocity, owner, itemdefindex);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    public static nint CDecoyProjectile_EmitGrenade( Vector pos, QAngle angle, Vector velocity, nint owner, uint itemdefindex )
    {
        try
        {
            unsafe
            {
                return pCDecoyProjectileEmitGrenade(&pos, &angle, &velocity, &velocity, owner, itemdefindex);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    public static nint CMolotovProjectile_EmitGrenade( Vector pos, QAngle angle, Vector velocity, nint owner, uint itemdefindex )
    {
        try
        {
            unsafe
            {
                return pCMolotovProjectileEmitGrenade(&pos, &angle, &velocity, &velocity, owner, itemdefindex);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    public static unsafe CViewVectors* CGameRules_GetViewVectors( nint pThis )
    {
        try
        {
            unsafe
            {
                CheckPtr(pThis, nameof(pThis));
                var pGetViewVectors = (delegate* unmanaged< nint, CViewVectors* >)GetVirtualFunction(pThis, GetViewVectorsOffset);
                return pGetViewVectors(pThis);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return null;
        }
    }
}
