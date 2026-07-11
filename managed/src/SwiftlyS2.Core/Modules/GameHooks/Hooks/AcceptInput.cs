using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Core.Extensions;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.Natives;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate void CEntityIdentityAcceptInput( nint pEntityIdentity, nint inputName, nint activator, nint caller, nint variant, nint unk1, nint unk2 );

    internal static unsafe Guid HookAcceptInput()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var address = _core.GameData.GetSignature("CEntityIdentity::AcceptInput");
        if (address == 0)
            throw new InvalidOperationException("Failed to find signature for CEntityIdentity::AcceptInput.");

        var fn = _core.Memory.GetUnmanagedFunctionByAddress<CEntityIdentityAcceptInput>(address);
        return fn.AddHook(next =>
        {
            return ( pEntityIdentity, pInputName, pActivator, pCaller, pVariant, unk1, unk2 ) =>
            {
                var dummy = _entityIdentityPool.Rent();
                dummy.DangerousSetHandle(pEntityIdentity);

                var entityIdentity = dummy;
                if (!entityIdentity.IsValid || !entityIdentity.EntityInstance.IsValid)
                {
                    _entityIdentityPool.Return(entityIdentity);
                    next()(pEntityIdentity, pInputName, pActivator, pCaller, pVariant, unk1, unk2);
                    return;
                }

                var inputName = pInputName != nint.Zero ? pInputName.AsRef<CUtlSymbolLarge>().Value : "";
                var activator = pActivator != nint.Zero ? EntityManager.GetEntityByAddress(pActivator) : null;
                var caller = pCaller != nint.Zero ? EntityManager.GetEntityByAddress(pCaller) : null;

                var preCtx = new AcceptInputEntityPreContext {
                    Params = new AcceptInputEntityParams {
                        Identity = entityIdentity,
                        EntityInstance = entityIdentity.EntityInstance,
                        DesignerName = entityIdentity.DesignerName,
                        InputName = inputName,
                        Activator = activator,
                        Caller = caller,
                        _variant = (CVariant<CVariantDefaultAllocator>*)pVariant,
                        OutputId = 0
                    }
                };

                if (EventPublisher.ListensToAcceptInput)
                {
                    var ev = new OnEntityIdentityAcceptInputHookEvent {
                        Identity = preCtx.Params.Identity,
                        EntityInstance = preCtx.Params.EntityInstance,
                        DesignerName = preCtx.Params.DesignerName,
                        InputName = preCtx.Params.InputName,
                        Activator = preCtx.Params.Activator,
                        Caller = preCtx.Params.Caller,
                        _variant = preCtx.Params._variant,
                        OutputId = preCtx.Params.OutputId,
                        Result = HookResult.Continue
                    };
                    EventPublisher.InvokeOnEntityIdentityAcceptInputHook(ev);
                    if (ev.Result == HookResult.Stop || ev.Result == HookResult.CancelOriginal)
                    {
                        _entityIdentityPool.Return(entityIdentity);
                        return;
                    }
                }

                InvokeAcceptInputPre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                {
                    _entityIdentityPool.Return(entityIdentity);
                    return;
                }

                next()(pEntityIdentity, pInputName, pActivator, pCaller, pVariant, unk1, unk2);

                var postCtx = new AcceptInputEntityPostContext { Params = preCtx.Params };
                InvokeAcceptInputPost(ref postCtx);

                _entityIdentityPool.Return(entityIdentity);
            };
        });
    }

    internal static Guid UnhookAcceptInput()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.AcceptInput, out var hookId))
        {
            var address = _core.GameData.GetSignature("CEntityIdentity::AcceptInput");
            if (address == 0)
                throw new InvalidOperationException("Failed to find signature for CEntityIdentity::AcceptInput.");

            var fn = _core.Memory.GetUnmanagedFunctionByAddress<CEntityIdentityAcceptInput>(address);
            fn.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeAcceptInputPre( ref AcceptInputEntityPreContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeAcceptInputPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeAcceptInputPost( ref AcceptInputEntityPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeAcceptInputPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
