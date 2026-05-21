using SwiftlyS2.Core.Natives;
using SwiftlyS2.Core.SchemaDefinitions;
using SwiftlyS2.Core.Schemas;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate nint CCSPlayerItemsServicesCanAcquire( nint pItemServices, nint pEconItemView, int acquireMethod, nint unk1 );
    private static CPlayerPawnComponentImpl _dummyPawnComponent = new(0);

    internal static Guid HookCanAcquire()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var canAcquirePtr = _core.GameData.GetSignature("CCSPlayer_ItemServices::CanAcquire");
        if (canAcquirePtr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_ItemServices::CanAcquire.");

        var canAcquireUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerItemsServicesCanAcquire>(canAcquirePtr);
        return canAcquireUmanagedFunction.AddHook(next =>
        {
            return ( pItemServices, pEconItemView, acquireMethod, unk1 ) =>
            {
                _dummyPawnComponent.DangerousSetHandle(pItemServices);
                var player = _dummyPawnComponent.ToPlayer();
                if (player == null) return next()(pItemServices, pEconItemView, acquireMethod, unk1);

                Schema.isFollowingServerGuidelines = false;

                var econItemView = _core.Memory.ToSchemaClass<CEconItemView>(pEconItemView);

                var preCtx = new CanAcquireItemPreContext {
                    Params = new CanAcquireItemParams {
                        Player = player,
                        EconItemView = econItemView,
                        WeaponVData = _core.Helpers.GetWeaponCSDataFromKey(econItemView.ItemDefinitionIndex),
                        AcquireMethod = (AcquireMethod)acquireMethod
                    }
                };

                Schema.isFollowingServerGuidelines = NativeServerHelpers.IsFollowingServerGuidelines();

                InvokeCanAcquirePre(ref preCtx);
                if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
                    return (int)(preCtx.IsReturnSet ? preCtx.Return : AcquireResult.Allowed);

                var result = next()(pItemServices, pEconItemView, acquireMethod, unk1);

                var postCtx = new CanAcquireItemPostContext { Params = preCtx.Params, Return = (AcquireResult)result };

                InvokeCanAcquirePost(ref postCtx);

                return (int)postCtx.Return;
            };
        });
    }

    internal static Guid UnhookCanAcquire()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.CanAcquire, out var hookId))
        {
            var canAcquirePtr = _core.GameData.GetSignature("CCSPlayer_ItemServices::CanAcquire");
            if (canAcquirePtr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_ItemServices::CanAcquire.");

            var canAcquireUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerItemsServicesCanAcquire>(canAcquirePtr);
            canAcquireUmanagedFunction.RemoveHook(hookId);
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeCanAcquirePre( ref CanAcquireItemPreContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCanAcquirePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeCanAcquirePost( ref CanAcquireItemPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCanAcquirePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
