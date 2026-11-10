using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate nint CBaseEntityTouchTemplate( nint pBaseEntity, nint pOtherEntity );

    internal static Guid HookStartTouch()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var offset = _core.GameData.GetOffset("CBaseEntity::StartTouch");
        if (offset < 0)
            throw new InvalidOperationException("Failed to find offset for CBaseEntity::StartTouch.");

        var fn = _core.Memory.GetUnmanagedFunctionByVTable<CBaseEntityTouchTemplate>(_core.Memory.GetVTableAddress(Library.Server, "CBaseEntity")!.Value, offset);
        return fn.AddHook(next =>
        {
            return ( pBaseEntity, pOtherEntity ) =>
            {
                var entity = EntityManager.GetEntityByAddress(pBaseEntity) as CBaseEntity ?? _core.Memory.ToSchemaClass<CBaseEntity>(pBaseEntity);
                var otherEntity = EntityManager.GetEntityByAddress(pOtherEntity) as CBaseEntity ?? _core.Memory.ToSchemaClass<CBaseEntity>(pOtherEntity);

                var preCtx = new StartTouchEntityPreContext {
                    Params = new EntityTouchParams {
                        Entity = entity,
                        OtherEntity = otherEntity
                    }
                };

                if (EventPublisher.ListensToEntityStartTouch)
                {
                    using var ev = new OnEntityStartTouchEvent {
                        Entity = preCtx.Params.Entity,
                        OtherEntity = preCtx.Params.OtherEntity
                    };
                    EventPublisher.InvokeOnEntityStartTouch(ev);
                }

                InvokeStartTouchPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return nint.Zero;

                var result = next()(pBaseEntity, pOtherEntity);

                var postCtx = new StartTouchEntityPostContext { Params = preCtx.Params };
                InvokeStartTouchPost(ref postCtx);
                return result;
            };
        });
    }

    internal static Guid UnhookStartTouch()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.StartTouch, out var hookId))
        {
            var offset = _core.GameData.GetOffset("CBaseEntity::StartTouch");
            if (offset < 0)
                throw new InvalidOperationException("Failed to find offset for CBaseEntity::StartTouch.");

            var fn = _core.Memory.GetUnmanagedFunctionByVTable<CBaseEntityTouchTemplate>(_core.Memory.GetVTableAddress(Library.Server, "CBaseEntity")!.Value, offset);
            fn.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static Guid HookTouch()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var offset = _core.GameData.GetOffset("CBaseEntity::Touch");
        if (offset < 0)
            throw new InvalidOperationException("Failed to find offset for CBaseEntity::Touch.");

        var fn = _core.Memory.GetUnmanagedFunctionByVTable<CBaseEntityTouchTemplate>(_core.Memory.GetVTableAddress(Library.Server, "CBaseEntity")!.Value, offset);
        return fn.AddHook(next =>
        {
            return ( pBaseEntity, pOtherEntity ) =>
            {
                var entity = EntityManager.GetEntityByAddress(pBaseEntity) as CBaseEntity ?? _core.Memory.ToSchemaClass<CBaseEntity>(pBaseEntity);
                var otherEntity = EntityManager.GetEntityByAddress(pOtherEntity) as CBaseEntity ?? _core.Memory.ToSchemaClass<CBaseEntity>(pOtherEntity);

                var preCtx = new TouchEntityPreContext {
                    Params = new EntityTouchParams {
                        Entity = entity,
                        OtherEntity = otherEntity
                    }
                };

                if (EventPublisher.ListensToEntityTouch)
                {
                    using var ev = new OnEntityTouchEvent {
                        Entity = preCtx.Params.Entity,
                        OtherEntity = preCtx.Params.OtherEntity
                    };
                    EventPublisher.InvokeOnEntityTouch(ev);
                }

                InvokeTouchPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return nint.Zero;

                var result = next()(pBaseEntity, pOtherEntity);

                var postCtx = new TouchEntityPostContext { Params = preCtx.Params };
                InvokeTouchPost(ref postCtx);
                return result;
            };
        });
    }

    internal static Guid UnhookTouch()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.Touch, out var hookId))
        {
            var offset = _core.GameData.GetOffset("CBaseEntity::Touch");
            if (offset < 0)
                throw new InvalidOperationException("Failed to find offset for CBaseEntity::Touch.");

            var fn = _core.Memory.GetUnmanagedFunctionByVTable<CBaseEntityTouchTemplate>(_core.Memory.GetVTableAddress(Library.Server, "CBaseEntity")!.Value, offset);
            fn.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static Guid HookEndTouch()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var offset = _core.GameData.GetOffset("CBaseEntity::EndTouch");
        if (offset < 0)
            throw new InvalidOperationException("Failed to find offset for CBaseEntity::EndTouch.");

        var fn = _core.Memory.GetUnmanagedFunctionByVTable<CBaseEntityTouchTemplate>(_core.Memory.GetVTableAddress(Library.Server, "CBaseEntity")!.Value, offset);
        return fn.AddHook(next =>
        {
            return ( pBaseEntity, pOtherEntity ) =>
            {
                var entity = EntityManager.GetEntityByAddress(pBaseEntity) as CBaseEntity ?? _core.Memory.ToSchemaClass<CBaseEntity>(pBaseEntity);
                var otherEntity = EntityManager.GetEntityByAddress(pOtherEntity) as CBaseEntity ?? _core.Memory.ToSchemaClass<CBaseEntity>(pOtherEntity);

                var preCtx = new EndTouchEntityPreContext {
                    Params = new EntityTouchParams {
                        Entity = entity,
                        OtherEntity = otherEntity
                    }
                };

                if (EventPublisher.ListensToEntityEndTouch)
                {
                    using var ev = new OnEntityEndTouchEvent {
                        Entity = preCtx.Params.Entity,
                        OtherEntity = preCtx.Params.OtherEntity
                    };
                    EventPublisher.InvokeOnEntityEndTouch(ev);
                }

                InvokeEndTouchPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return nint.Zero;

                var result = next()(pBaseEntity, pOtherEntity);

                var postCtx = new EndTouchEntityPostContext { Params = preCtx.Params };
                InvokeEndTouchPost(ref postCtx);
                return result;
            };
        });
    }

    internal static Guid UnhookEndTouch()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.EndTouch, out var hookId))
        {
            var offset = _core.GameData.GetOffset("CBaseEntity::EndTouch");
            if (offset < 0)
                throw new InvalidOperationException("Failed to find offset for CBaseEntity::EndTouch.");

            var fn = _core.Memory.GetUnmanagedFunctionByVTable<CBaseEntityTouchTemplate>(_core.Memory.GetVTableAddress(Library.Server, "CBaseEntity")!.Value, offset);
            fn.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeStartTouchPre( ref StartTouchEntityPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnStartTouchEntityPreDelegate>.Snapshot))
        {
            subscriber.InvokeStartTouchPre(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }

    internal static void InvokeStartTouchPost( ref StartTouchEntityPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnStartTouchEntityPostDelegate>.Snapshot))
        {
            subscriber.InvokeStartTouchPost(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }

    internal static void InvokeTouchPre( ref TouchEntityPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnTouchEntityPreDelegate>.Snapshot))
        {
            subscriber.InvokeTouchPre(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }

    internal static void InvokeTouchPost( ref TouchEntityPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnTouchEntityPostDelegate>.Snapshot))
        {
            subscriber.InvokeTouchPost(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }

    internal static void InvokeEndTouchPre( ref EndTouchEntityPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnEndTouchEntityPreDelegate>.Snapshot))
        {
            subscriber.InvokeEndTouchPre(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }

    internal static void InvokeEndTouchPost( ref EndTouchEntityPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnEndTouchEntityPostDelegate>.Snapshot))
        {
            subscriber.InvokeEndTouchPost(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }
}
