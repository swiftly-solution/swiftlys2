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

                ICanAcquireItem @event = new CanAcquireItemData {
                    Player = player,
                    EconItemView = econItemView,
                    WeaponVData = _core.Helpers.GetWeaponCSDataFromKey(econItemView.ItemDefinitionIndex),
                    AcquireMethod = (AcquireMethod)acquireMethod,
                    OriginalResult = AcquireResult.Allowed
                };

                Schema.isFollowingServerGuidelines = NativeServerHelpers.IsFollowingServerGuidelines();

                InvokeCanAcquirePre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.CancelOriginal)
                {
                    return (int)@event.OriginalResult;
                }

                var result = next()(pItemServices, pEconItemView, acquireMethod, unk1);

                @event.SetAcquireResult((AcquireResult)result);
                @event.Intercepted = false;

                InvokeCanAcquirePost(ref @event);

                return @event.Intercepted ? (int)@event.OriginalResult : result;
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

    internal static void InvokeCanAcquirePre( ref ICanAcquireItem @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCanAcquirePre(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeCanAcquirePost( ref ICanAcquireItem @event )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCanAcquirePost(ref @event);
                if (@event.Result == HookResult.Stop || @event.Result == HookResult.Handled) return;
            }
        }
    }
}
