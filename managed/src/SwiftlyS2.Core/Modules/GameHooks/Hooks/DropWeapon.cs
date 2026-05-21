using SwiftlyS2.Core.EntitySystem;
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
                    _dummyPawnComponent.DangerousSetHandle(weaponServices);
                    var player = _dummyPawnComponent.ToPlayer();
                    if (player == null) return next()(weaponServices, playerWeapon, swapping);

                    var basePlayerWeapon = playerWeapon != nint.Zero ? EntityManager.GetEntityByAddress(playerWeapon) as CBasePlayerWeapon ?? _core.Memory.ToSchemaClass<CBasePlayerWeapon>(playerWeapon) : null;

                    var preCtx = new WeaponDropPreContext {
                        Params = new WeaponDropParams {
                            Player = player,
                            Weapon = basePlayerWeapon,
                            SwappingWeapon = swapping == 1
                        }
                    };

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
                    _dummyPawnComponent.DangerousSetHandle(weaponServices);
                    var player = _dummyPawnComponent.ToPlayer();
                    if (player == null) return next()(weaponServices, playerWeapon, swapping);

                    var basePlayerWeapon = playerWeapon != nint.Zero ? EntityManager.GetEntityByAddress(playerWeapon) as CBasePlayerWeapon ?? _core.Memory.ToSchemaClass<CBasePlayerWeapon>(playerWeapon) : null;

                    var preCtx = new WeaponDropPreContext {
                        Params = new WeaponDropParams {
                            Player = player,
                            Weapon = basePlayerWeapon,
                            SwappingWeapon = swapping == 1
                        }
                    };

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
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeWeaponDropPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }

    internal static void InvokeDropWeaponPost( ref WeaponDropPostContext ctx )
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeWeaponDropPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled) return;
            }
        }
    }
}
