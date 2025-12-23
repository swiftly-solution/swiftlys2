using System.Text;
using System.Buffers;
using System.Runtime.InteropServices;
using Spectre.Console;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Players;
namespace SwiftlyS2.Core.Natives;

internal static class GameFunctions
{
    private static readonly bool IsWindows = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows);
    public static unsafe delegate* unmanaged< CTakeDamageInfo*, nint, nint, nint, Vector*, Vector*, float, int, int, void*, void > pCTakeDamageInfo_Constructor;
    public static unsafe delegate* unmanaged< nint, CTakeDamageInfo*, CTakeDamageResult*, void > pTakeDamage;
    public static unsafe delegate* unmanaged< nint, Ray_t*, Vector*, Vector*, CTraceFilter*, CGameTrace*, void > pTraceShape;
    public static unsafe delegate* unmanaged< Vector*, Vector*, BBox_t*, CTraceFilter*, CGameTrace*, void > pTracePlayerBBox;
    public static unsafe delegate* unmanaged< nint, IntPtr, void > pSetModel;
    public static unsafe delegate* unmanaged< nint, nint, byte, byte, byte, byte, void > pSetPlayerControllerPawn;
    public static unsafe delegate* unmanaged< nint, nint, float, void > pSetOrAddAttribute;
    public static unsafe delegate* unmanaged< int, nint, nint > pGetWeaponCSDataFromKey;
    public static unsafe delegate* unmanaged< nint, uint, nint, byte, CUtlSymbolLarge, byte, int, nint, nint, void > pDispatchParticleEffect;
    public static unsafe delegate* unmanaged< nint, uint, nint, uint, float, void > pTerminateRoundLinux;
    public static unsafe delegate* unmanaged< nint, float, uint, nint, uint, void > pTerminateRoundWindows;
    public static unsafe delegate* unmanaged< nint, Vector*, QAngle*, Vector*, void > pTeleport;
    public static unsafe delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, int, nint > pCSmokeGrenadeProjectileEmitGrenade;
    public static unsafe delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint > pCFlashbangProjectileEmitGrenade;
    public static unsafe delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint > pCHEGrenadeProjectileEmitGrenade;
    public static unsafe delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint > pCDecoyProjectileEmitGrenade;
    public static unsafe delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint > pCMolotovProjectileEmitGrenade;
    public static int TeleportOffset => NativeOffsets.Fetch("CBaseEntity::Teleport");
    public static int CommitSuicideOffset => NativeOffsets.Fetch("CBasePlayerPawn::CommitSuicide");
    public static int GetSkeletonInstanceOffset => NativeOffsets.Fetch("CGameSceneNode::GetSkeletonInstance");
    public static int FindPickerEntityOffset => NativeOffsets.Fetch("CGameRules::FindPickerEntity");
    public static int RemoveWeaponsOffset => NativeOffsets.Fetch("CCSPlayer_ItemServices::RemoveWeapons");
    public static int GiveNamedItemOffset => NativeOffsets.Fetch("CCSPlayer_ItemServices::GiveNamedItem");
    public static int DropActiveItemOffset => NativeOffsets.Fetch("CCSPlayer_ItemServices::DropActiveItem");
    public static int DropWeaponOffset => NativeOffsets.Fetch("CCSPlayer_WeaponServices::DropWeapon");
    public static int SelectWeaponOffset => NativeOffsets.Fetch("CCSPlayer_WeaponServices::SelectWeapon");
    public static int AddResourceOffset => NativeOffsets.Fetch("CEntityResourceManifest::AddResource");
    public static int CollisionRulesChangedOffset => NativeOffsets.Fetch("CBaseEntity::CollisionRulesChanged");
    public static int RespawnOffset => NativeOffsets.Fetch("CCSPlayerController::Respawn");

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
            pSetModel = (delegate* unmanaged< nint, IntPtr, void >)NativeSignatures.Fetch("CBaseModelEntity::SetModel");
            pSetPlayerControllerPawn = (delegate* unmanaged< nint, nint, byte, byte, byte, byte, void >)NativeSignatures.Fetch("CBasePlayerController::SetPawn");
            pSetOrAddAttribute = (delegate* unmanaged< nint, IntPtr, float, void >)NativeSignatures.Fetch("CAttributeList::SetOrAddAttributeValueByName");
            pGetWeaponCSDataFromKey = (delegate* unmanaged< int, nint, nint >)NativeSignatures.Fetch("GetWeaponCSDataFromKey");
            pDispatchParticleEffect = (delegate* unmanaged< nint, uint, nint, byte, CUtlSymbolLarge, byte, int, nint, nint, void >)NativeSignatures.Fetch("DispatchParticleEffect");
            if (IsWindows)
            {
                pTerminateRoundWindows = (delegate* unmanaged< nint, float, uint, nint, uint, void >)NativeSignatures.Fetch("CGameRules::TerminateRound");
            }
            else
            {
                pTerminateRoundLinux = (delegate* unmanaged< nint, uint, nint, uint, float, void >)NativeSignatures.Fetch("CGameRules::TerminateRound");
            }
            pTeleport = (delegate* unmanaged< nint, Vector*, QAngle*, Vector*, void >)((void**)NativeMemoryHelpers.GetVirtualTableAddress("server", "CBaseEntity"))[TeleportOffset];
            pCSmokeGrenadeProjectileEmitGrenade = (delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, int, nint >)NativeSignatures.Fetch("CSmokeGrenadeProjectile::EmitGrenade");
            pCFlashbangProjectileEmitGrenade = (delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint >)NativeSignatures.Fetch("CFlashbangProjectile::EmitGrenade");
            pCHEGrenadeProjectileEmitGrenade = (delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint >)NativeSignatures.Fetch("CHEGrenadeProjectile::EmitGrenade");
            pCDecoyProjectileEmitGrenade = (delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint >)NativeSignatures.Fetch("CDecoyProjectile::EmitGrenade");
            pCMolotovProjectileEmitGrenade = (delegate* unmanaged< Vector*, QAngle*, Vector*, Vector*, nint, uint, nint >)NativeSignatures.Fetch("CMolotovProjectile::EmitGrenade");
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
            unsafe
            {
                var pool = ArrayPool<byte>.Shared;
                var nameLength = Encoding.UTF8.GetByteCount(particleName);
                var nameBuffer = pool.Rent(nameLength + 1);
                _ = Encoding.UTF8.GetBytes(particleName, nameBuffer);
                nameBuffer[nameLength] = 0;
                fixed (byte* pParticleName = nameBuffer)
                {
                    pDispatchParticleEffect((nint)pParticleName, attachmentType, entity, attachmentPoint, attachmentName, (byte)(resetAllParticlesOnEntity ? 1 : 0), splitScreenSlot, (nint)(&filter), IntPtr.Zero);
                    pool.Return(nameBuffer);
                }
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static void TerminateRound( nint gameRules, uint reason, float delay, uint teamId, uint unk01 )
    {
        try
        {
            CheckPtr(gameRules, nameof(gameRules));
            unsafe
            {
                if (IsWindows)
                {
                    pTerminateRoundWindows(gameRules, delay, reason, teamId > 0 ? (nint)(&teamId) : 0, unk01);
                }
                else
                {
                    pTerminateRoundLinux(gameRules, reason, teamId > 0 ? (nint)(&teamId) : 0, unk01, delay);
                }
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
                var pool = ArrayPool<byte>.Shared;
                var keyLength = Encoding.UTF8.GetByteCount(key);
                var keyBuffer = pool.Rent(keyLength + 1);
                _ = Encoding.UTF8.GetBytes(key, keyBuffer);
                keyBuffer[keyLength] = 0;
                fixed (byte* pKey = keyBuffer)
                {
                    return pGetWeaponCSDataFromKey(unknown, (nint)pKey);
                }
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

    public static unsafe void PawnCommitSuicide( nint pPawn, bool bExplode, bool bForce )
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

    public static unsafe void SetPlayerControllerPawn( nint pController, nint pPawn, bool b1, bool b2, bool b3, bool b4 )
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

    public static unsafe void SetModel( nint pEntity, string model )
    {
        try
        {
            CheckPtr(pEntity, nameof(pEntity));
            unsafe
            {
                var pool = ArrayPool<byte>.Shared;
                var modelLength = Encoding.UTF8.GetByteCount(model);
                var modelBuffer = pool.Rent(modelLength + 1);
                _ = Encoding.UTF8.GetBytes(model, modelBuffer);
                modelBuffer[modelLength] = 0;
                fixed (byte* pModel = modelBuffer)
                {
                    pSetModel(pEntity, (IntPtr)pModel);
                    pool.Return(modelBuffer);
                }
            }
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
                pTeleport(pEntity, vecPosition, vecAngle, vecVelocity);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    private static unsafe bool Is16Aligned(CGameTrace* pTrace) => ((nuint)pTrace & 15) == 0;

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

    public static unsafe void CCSPlayer_ItemServices_RemoveWeapons( nint pThis )
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

    public static unsafe nint CCSPlayer_ItemServices_GiveNamedItem( nint pThis, string name )
    {
        try
        {
            unsafe
            {
                CheckPtr(pThis, nameof(pThis));
                var ppVTable = (void***)pThis;
                var pGiveNamedItem = (delegate* unmanaged< nint, nint, nint >)ppVTable[0][GiveNamedItemOffset];
                var pool = ArrayPool<byte>.Shared;
                var nameLength = Encoding.UTF8.GetByteCount(name);
                var nameBuffer = pool.Rent(nameLength + 1);
                _ = Encoding.UTF8.GetBytes(name, nameBuffer);
                nameBuffer[nameLength] = 0;
                fixed (byte* pName = nameBuffer)
                {
                    return pGiveNamedItem(pThis, (IntPtr)pName);
                }
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
            return 0;
        }
    }

    public static unsafe void CCSPlayer_ItemServices_DropActiveItem( nint pThis, Vector momentum )
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

    public static unsafe void CCSPlayer_WeaponServices_DropWeapon( nint pThis, nint pWeapon )
    {
        try
        {
            unsafe
            {
                CheckPtr(pThis, nameof(pThis));
                CheckPtr(pWeapon, nameof(pWeapon));
                var pDropWeapon = (delegate* unmanaged< nint, nint, nint, nint, void >)GetVirtualFunction(pThis, DropWeaponOffset);
                pDropWeapon(pThis, pWeapon, 0, 0);
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static unsafe void CCSPlayer_WeaponServices_SelectWeapon( nint pThis, nint pWeapon )
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

    public static unsafe void CEntityResourceManifest_AddResource( nint pThis, string path )
    {
        try
        {
            unsafe
            {
                CheckPtr(pThis, nameof(pThis));
                var pool = ArrayPool<byte>.Shared;
                var pathLength = Encoding.UTF8.GetByteCount(path);
                var pathBuffer = pool.Rent(pathLength + 1);
                _ = Encoding.UTF8.GetBytes(path, pathBuffer);
                pathBuffer[pathLength] = 0;
                var pAddResource = (delegate* unmanaged< nint, nint, void >)GetVirtualFunction(pThis, AddResourceOffset);
                fixed (byte* pPath = pathBuffer)
                {
                    pAddResource(pThis, (IntPtr)pPath);
                    pool.Return(pathBuffer);
                }
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static unsafe void SetOrAddAttribute( nint handle, string name, float value )
    {
        try
        {
            unsafe
            {
                CheckPtr(handle, nameof(handle));
                var pool = ArrayPool<byte>.Shared;
                var nameLength = Encoding.UTF8.GetByteCount(name);
                var nameBuffer = pool.Rent(nameLength + 1);
                _ = Encoding.UTF8.GetBytes(name, nameBuffer);
                nameBuffer[nameLength] = 0;
                fixed (byte* pName = nameBuffer)
                {
                    pSetOrAddAttribute(handle, (nint)pName, value);
                    pool.Return(nameBuffer);
                }
            }
        }
        catch (Exception e)
        {
            AnsiConsole.WriteException(e);
        }
    }

    public static unsafe void CBaseEntity_CollisionRulesChanged( nint pThis )
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

    public static unsafe void CCSPlayerController_Respawn( nint pThis )
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
}