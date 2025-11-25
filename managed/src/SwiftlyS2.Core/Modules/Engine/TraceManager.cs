using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared.Natives;
using SwiftlyS2.Shared.Services;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.Services;

internal class TraceManager : ITraceManager
{
    public void TracePlayerBBox( Vector start, Vector end, BBox_t bounds, CTraceFilter filter, ref CGameTrace trace )
    {
        unsafe
        {
            fixed (CGameTrace* tracePtr = &trace)
            {
                GameFunctions.TracePlayerBBox(start, end, bounds, &filter, tracePtr);
            }
        }
    }

    public void TraceShape( Vector start, Vector end, Ray_t ray, CTraceFilter filter, ref CGameTrace trace )
    {
        unsafe
        {
            fixed (CGameTrace* tracePtr = &trace)
            {
                GameFunctions.TraceShape(NativeEngineHelpers.GetTraceManager(), &ray, start, end, &filter, tracePtr);
            }
        }
    }

    public static void SimpleTrace( Vector start, Vector end, RayType_t rayKind, RnQueryObjectSet objectQuery, MaskTrace interactWith, MaskTrace interactExclude, MaskTrace interactAs, CollisionGroup collision, ref CGameTrace trace, nint filterEntity, nint filterSecondEntity )
    {
        var filter = new CTraceFilter(true) {
            IterateEntities = true,
            QueryShapeAttributes = new RnQueryShapeAttr_t {
                ObjectSetMask = objectQuery,
                InteractsWith = interactWith,
                InteractsExclude = interactExclude,
                InteractsAs = interactAs,
                CollisionGroup = collision,
                HitSolid = true
            }
        };

        unsafe
        {
            filter.QueryShapeAttributes.EntityIdsToIgnore[0] = uint.MaxValue;
            filter.QueryShapeAttributes.EntityIdsToIgnore[1] = uint.MaxValue;
        }

        var ray = new Ray_t {
            Type = rayKind
        };

        unsafe
        {
            if (filterEntity != nint.Zero)
            {
                var entity = new CBaseEntityImpl(filterEntity);
                filter.QueryShapeAttributes.EntityIdsToIgnore[0] = entity.Index;
            }
            if (filterSecondEntity != nint.Zero)
            {
                var entity = new CBaseEntityImpl(filterSecondEntity);
                filter.QueryShapeAttributes.EntityIdsToIgnore[1] = entity.Index;
            }

            fixed (CGameTrace* tracePtr = &trace)
            {
                GameFunctions.TraceShape(NativeEngineHelpers.GetTraceManager(), &ray, start, end, &filter, tracePtr);
            }
        }
    }

    public void SimpleTrace( Vector start, Vector end, RayType_t rayKind, RnQueryObjectSet objectQuery, MaskTrace interactWith, MaskTrace interactExclude, MaskTrace interactAs, CollisionGroup collision, ref CGameTrace trace, CBaseEntity? filterEntity = null, CBaseEntity? filterSecondEntity = null )
    {
        var entityPtr = filterEntity?.Address ?? nint.Zero;
        var entitySecondPtr = filterSecondEntity?.Address ?? nint.Zero;
        SimpleTrace(start, end, rayKind, objectQuery, interactWith, interactExclude, interactAs, collision, ref trace, entityPtr, entitySecondPtr);
    }

    public void SimpleTrace( Vector start, QAngle angle, RayType_t rayKind, RnQueryObjectSet objectQuery, MaskTrace interactWith, MaskTrace interactExclude, MaskTrace interactAs, CollisionGroup collision, ref CGameTrace trace, CBaseEntity? filterEntity = null, CBaseEntity? filterSecondEntity = null )
    {
        angle.ToDirectionVectors(out var fwd, out var _, out var _);
        var end = start + new Vector(
            fwd.X * 8192f,
            fwd.Y * 8192f,
            fwd.Z * 8192f
        );
        SimpleTrace(start, end, rayKind, objectQuery, interactWith, interactExclude, interactAs, collision, ref trace, filterEntity, filterSecondEntity);
    }
}