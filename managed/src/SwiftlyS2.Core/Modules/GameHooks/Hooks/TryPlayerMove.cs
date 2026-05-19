using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Trace;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private unsafe delegate void CCSPlayerMovementServicesTryPlayerMove( nint movementServices, nint moveData, Vector* firstDest, CGameTrace* firstTrace, byte* isSurfing );

    internal static unsafe Guid HookTryPlayerMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::TryPlayerMove");
        if (ptr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::TryPlayerMove.");

        var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesTryPlayerMove>(ptr);
        return unmanagedFunction.AddHook(next =>
        {
            return ( movementServices, moveData, firstDest, firstTrace, isSurfing ) =>
            {
                _dummyPawnComponent.DangerousSetHandle(movementServices);
                var player = _dummyPawnComponent.ToPlayer();
                if (player == null) { next()(movementServices, moveData, firstDest, firstTrace, isSurfing); return; }

                ITryPlayerMoveMovement @event = new TryPlayerMoveMovementData {
                    Player = player,
                    MoveData = new CMoveDataImpl { Address = moveData },
                    FirstDestPtr = (nint)firstDest,
                    FirstTrace = ConvertGameTrace(firstTrace),
                    IsSurfingPtr = (nint)isSurfing,
                    Result = HookResult.Continue
                };

                InvokeTryPlayerMovePre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal) return;

                next()(movementServices, moveData, firstDest, firstTrace, isSurfing);

                @event.Result = HookResult.Continue;

                InvokeTryPlayerMovePost(ref @event);
            };
        });
    }

    internal static unsafe Guid UnhookTryPlayerMove()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.TryPlayerMove, out var hookId))
        {
            var ptr = _core.GameData.GetSignature("CCSPlayer_MovementServices::TryPlayerMove");
            if (ptr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_MovementServices::TryPlayerMove.");

            var unmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerMovementServicesTryPlayerMove>(ptr);
            unmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    private static unsafe TraceResult ConvertGameTrace( CGameTrace* t )
    {
        if (t == null) return new TraceResult();

        var result = new TraceResult {
            Contents = t->Contents,
            BodyTransform = t->BodyTransform,
            ShapeAttributes = new TraceCollisionAttributes {
                InteractsAs = t->ShapeAttributes.InteractsAs,
                InteractsWith = t->ShapeAttributes.InteractsWith,
                InteractsExclude = t->ShapeAttributes.InteractsExclude,
                EntityId = t->ShapeAttributes.EntityId,
                OwnerId = t->ShapeAttributes.OwnerId,
                HierarchyId = t->ShapeAttributes.HierarchyId,
                DetailLayerMask = t->ShapeAttributes.DetailLayerMask,
                DetailLayerMaskType = t->ShapeAttributes.DetailLayerMaskType,
                TargetDetailLayer = t->ShapeAttributes.TargetDetailLayer,
                CollisionGroup = t->ShapeAttributes.CollisionGroup,
                CollisionFunctionMask = t->ShapeAttributes.CollisionFunctionMask
            },
            StartPos = t->StartPos,
            EndPos = t->EndPos,
            HitNormal = t->HitNormal,
            HitPoint = t->HitPoint,
            HitOffset = t->HitOffset,
            Fraction = t->Fraction,
            Triangle = t->Triangle,
            HitboxBoneIndex = t->HitboxBoneIndex,
            RayType = t->RayType,
            StartInSolid = t->StartInSolid,
            ExactHitPoint = t->ExactHitPoint,
            Entity = t->pEntity != null ? t->Entity : null
        };

        if (t->SurfaceProperties != null)
        {
            var sp = t->SurfaceProperties;
            result.SurfaceProperties = new PhysSurfacePropertiesTrace {
                Name = sp->Name,
                NameHash = sp->NameHash,
                BaseNameHash = sp->BaseNameHash,
                ListIndex = sp->ListIndex,
                BaseListIndex = sp->BaseListIndex,
                Hidden = sp->Hidden,
                Description = sp->Description,
                Physics = new PhysSurfacePropertiesPhysicsTrace {
                    Friction = sp->Physics.Friction,
                    Elasticity = sp->Physics.Elasticity,
                    Density = sp->Physics.Density,
                    Thickness = sp->Physics.Thickness,
                    SoftContactFrequency = sp->Physics.SoftContactFrequency,
                    SoftContactDampingRatio = sp->Physics.SoftContactDampingRatio,
                    WheelDrag = sp->Physics.WheelDrag,
                    HeatConductivity = sp->Physics.HeatConductivity,
                    Flashpoint = sp->Physics.Flashpoint
                },
                AudioSounds = new PhysSurfacePropertiesSoundNamesTrace {
                    ImpactSoft = sp->AudioSounds.ImpactSoft,
                    ImpactHard = sp->AudioSounds.ImpactHard,
                    ScrapeSmooth = sp->AudioSounds.ScrapeSmooth,
                    ScrapeRough = sp->AudioSounds.ScrapeRough,
                    BulletImpact = sp->AudioSounds.BulletImpact,
                    Rolling = sp->AudioSounds.Rolling,
                    Break = sp->AudioSounds.Break,
                    Strain = sp->AudioSounds.Strain,
                    MeleeImpact = sp->AudioSounds.MeleeImpact,
                    PushOff = sp->AudioSounds.PushOff,
                    SkidStop = sp->AudioSounds.SkidStop
                },
                AudioParams = new PhysSurfacePropertiesAudioTrace {
                    Reflectivity = sp->AudioParams.Reflectivity,
                    HardnessFactor = sp->AudioParams.HardnessFactor,
                    RoughnessFactor = sp->AudioParams.RoughnessFactor,
                    RoughThreshold = sp->AudioParams.RoughThreshold,
                    HardThreshold = sp->AudioParams.HardThreshold,
                    HardVelocityThreshold = sp->AudioParams.HardVelocityThreshold,
                    StaticImpactVolume = sp->AudioParams.StaticImpactVolume,
                    OcclusionFactor = sp->AudioParams.OcclusionFactor
                }
            };
        }

        if (t->HitBox != null)
        {
            var hb = t->HitBox;
            result.HitBox = new HitBoxTrace {
                Name = hb->m_name,
                SurfaceProperty = hb->m_sSurfaceProperty,
                BoneName = hb->m_sBoneName,
                MinBounds = hb->m_vMinBounds,
                MaxBounds = hb->m_vMaxBounds,
                ShapeRadius = hb->m_flShapeRadius,
                BoneNameHash = hb->m_nBoneNameHash,
                GroupId = hb->m_nGroupId,
                ShapeType = hb->m_nShapeType,
                TranslationOnly = hb->m_bTranslationOnly,
                CRC = hb->m_CRC,
                RenderColor = hb->m_cRenderColor,
                HitBoxIndex = hb->m_nHitBoxIndex,
                ForcedTransform = hb->m_bForcedTransform,
                ForcedTransformObject = hb->m_forcedTransform
            };
        }

        return result;
    }

    internal static void InvokeTryPlayerMovePre( ref ITryPlayerMoveMovement @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeTryPlayerMovePre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeTryPlayerMovePost( ref ITryPlayerMoveMovement @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeTryPlayerMovePost(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }
}
