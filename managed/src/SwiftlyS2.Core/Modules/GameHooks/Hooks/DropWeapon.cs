using SwiftlyS2.Core.EntitySystem;
using SwiftlyS2.Core.Events;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class GameHooksPublisher
{
    private delegate byte DropWeaponWindows( nint weaponServices, nint playerWeapon, byte swapping );
    private delegate nint DropWeaponLinux( nint weaponServices, nint playerWeapon, byte swapping );

    internal static Guid HookDropWeapon()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        var dropWeaponPtr = _core.GameData.GetSignature("CCSPlayer_WeaponServices::DropWeapon");
        if (dropWeaponPtr == 0)
            throw new InvalidOperationException("Failed to find signature for CCSPlayer_WeaponServices::DropWeapon.");

        if (IsWindows)
        {
            var dropWeaponUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<DropWeaponWindows>(dropWeaponPtr);
            return dropWeaponUmanagedFunction.AddHook(next =>
            {
                return ( weaponServices, playerWeapon, swapping ) =>
                {
                    var dummy = _pawnComponentPool.Rent();
                    dummy.DangerousSetHandle(weaponServices);
                    var player = dummy.ToPlayer();
                    _pawnComponentPool.Return(dummy);
                    if (player == null) return next()(weaponServices, playerWeapon, swapping);

                    var basePlayerWeapon = playerWeapon != nint.Zero ? EntityManager.GetEntityByAddress(playerWeapon) as CBasePlayerWeapon ?? _core.Memory.ToSchemaClass<CBasePlayerWeapon>(playerWeapon) : null;

                    var preCtx = new WeaponDropPreContext {
                        Params = new WeaponDropParams {
                            Player = player,
                            Weapon = basePlayerWeapon,
                            SwappingWeapon = swapping == 1
                        }
                    };

                    if (EventPublisher.ListensToWeaponDrop)
                    {
                        var ev = new OnWeaponServicesDropWeaponHook {
                            WeaponServices = preCtx.Params.Player.PlayerPawn!.WeaponServices!,
                            Weapon = preCtx.Params.Weapon,
                            SwappingWeapon = preCtx.Params.SwappingWeapon,
                            Result = preCtx.HookResult
                        };
                        EventPublisher.InvokeOnWeaponServicesDropWeaponHook(ev);
                        if (ev.Result == HookResult.Stop || ev.Result == HookResult.CancelOriginal) return 0;
                    }

                    InvokeDropWeaponPre(ref preCtx);
                    if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return 0;

                    var result = next()(weaponServices, playerWeapon, swapping);

                    var postCtx = new WeaponDropPostContext { Params = preCtx.Params };

                    InvokeDropWeaponPost(ref postCtx);

                    return result;
                };
            });
        }
        else
        {
            var dropWeaponUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<DropWeaponLinux>(dropWeaponPtr);
            return dropWeaponUmanagedFunction.AddHook(next =>
            {
                return ( weaponServices, playerWeapon, swapping ) =>
                {
                    var dummy = _pawnComponentPool.Rent();
                    dummy.DangerousSetHandle(weaponServices);
                    var player = dummy.ToPlayer();
                    _pawnComponentPool.Return(dummy);
                    if (player == null) return next()(weaponServices, playerWeapon, swapping);

                    var basePlayerWeapon = playerWeapon != nint.Zero ? EntityManager.GetEntityByAddress(playerWeapon) as CBasePlayerWeapon ?? _core.Memory.ToSchemaClass<CBasePlayerWeapon>(playerWeapon) : null;

                    var preCtx = new WeaponDropPreContext {
                        Params = new WeaponDropParams {
                            Player = player,
                            Weapon = basePlayerWeapon,
                            SwappingWeapon = swapping == 1
                        }
                    };

                    if (EventPublisher.ListensToWeaponDrop)
                    {
                        var ev = new OnWeaponServicesDropWeaponHook {
                            WeaponServices = preCtx.Params.Player.PlayerPawn!.WeaponServices!,
                            Weapon = preCtx.Params.Weapon,
                            SwappingWeapon = preCtx.Params.SwappingWeapon,
                            Result = preCtx.HookResult
                        };
                        EventPublisher.InvokeOnWeaponServicesDropWeaponHook(ev);
                        if (ev.Result == HookResult.Stop || ev.Result == HookResult.CancelOriginal) return 0;
                    }

                    InvokeDropWeaponPre(ref preCtx);
                    if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal) return 0;

                    var result = next()(weaponServices, playerWeapon, swapping);

                    var postCtx = new WeaponDropPostContext { Params = preCtx.Params };

                    InvokeDropWeaponPost(ref postCtx);

                    return result;
                };
            });
        }
    }

    internal static Guid UnhookDropWeapon()
    {
        if (_core == null) throw new InvalidOperationException("GameHooksCore is not initialized.");

        if (hookIds.TryGetValue(HookListener.WeaponDrop, out var hookId))
        {
            var dropWeaponPtr = _core.GameData.GetSignature("CCSPlayer_WeaponServices::DropWeapon");
            if (dropWeaponPtr == 0)
                throw new InvalidOperationException("Failed to find signature for CCSPlayer_WeaponServices::DropWeapon.");

            if (IsWindows)
            {
                var dropWeaponUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<DropWeaponWindows>(dropWeaponPtr);
                dropWeaponUmanagedFunction.RemoveHook(hookId);
            }
            else
            {
                var dropWeaponUmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<DropWeaponLinux>(dropWeaponPtr);
                dropWeaponUmanagedFunction.RemoveHook(hookId);
            }
            return hookId;
        }
        else return Guid.Empty;
    }

    internal static void InvokeDropWeaponPre( ref WeaponDropPreContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnWeaponDropPreDelegate>.Snapshot))
        {
            subscriber.InvokeWeaponDropPre(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }

    internal static void InvokeDropWeaponPost( ref WeaponDropPostContext ctx )
    {
        foreach (var subscriber in Volatile.Read(ref Listeners<OnWeaponDropPostDelegate>.Snapshot))
        {
            subscriber.InvokeWeaponDropPost(ref ctx);
            if (ctx.HookResult is HookResult.Stop or HookResult.Handled) return;
        }
    }
}
