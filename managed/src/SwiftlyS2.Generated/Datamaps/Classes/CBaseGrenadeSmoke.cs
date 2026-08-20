using Spectre.Console;
using SwiftlyS2.Core.Natives;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.GameHooks;
using SwiftlyS2.Shared.Memory;
using SwiftlyS2.Shared.Misc;
using SwiftlyS2.Shared.SchemaDefinitions;

namespace SwiftlyS2.Core.GameHooks;

internal static partial class DatamapHooksPublisher
{
    private delegate void CBaseGrenadeSmokeDelegate(nint a1);

    private static IUnmanagedFunction<CBaseGrenadeSmokeDelegate>? CBaseGrenadeSmokeUnmanagedFunction;
    private static Guid CBaseGrenadeSmokeHookGuid;

    private static IUnmanagedFunction<CBaseGrenadeSmokeDelegate> CBaseGrenadeSmokeGetUnmanagedFunction()
    {
        if (CBaseGrenadeSmokeUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseGrenade", "CBaseGrenadeSmoke");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseGrenade::CBaseGrenadeSmoke.");
            }
            CBaseGrenadeSmokeUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseGrenadeSmokeDelegate>(address);
        }
        return CBaseGrenadeSmokeUnmanagedFunction;
    }

    internal static Guid HookCBaseGrenadeSmoke()
    {
        CBaseGrenadeSmokeHookGuid = CBaseGrenadeSmokeGetUnmanagedFunction().AddHook(next => (a1) => CBaseGrenadeSmokePipeline(a1, () => next()(a1)));
        return CBaseGrenadeSmokeHookGuid;
    }

    internal static Guid UnhookCBaseGrenadeSmoke()
    {
        CBaseGrenadeSmokeGetUnmanagedFunction().RemoveHook(CBaseGrenadeSmokeHookGuid);
        return Guid.Empty;
    }

    private static void CBaseGrenadeSmokePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseGrenade>(a1);

            var preCtx = new CBaseGrenadeSmokePreContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeSmokePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseGrenadeSmokePostContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeSmokePost(ref postCtx);
        }
        catch (Exception e)
        {
            if (!GlobalExceptionHandler.Handle(ref e))
            {
                return;
            }
            AnsiConsole.WriteException(e);
        }
    }

    internal static void InvokeCBaseGrenadeSmoke(nint a1)
    {
        CBaseGrenadeSmokeGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseGrenadeSmokePre(ref CBaseGrenadeSmokePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeSmokePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseGrenadeSmokePost(ref CBaseGrenadeSmokePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeSmokePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseGrenadeSmokeHook : ICBaseGrenadeSmokeHook
{
    private event OnCBaseGrenadeSmokePreDelegate? _Pre;
    private event OnCBaseGrenadeSmokePostDelegate? _Post;

    public event OnCBaseGrenadeSmokePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeSmoke);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeSmoke);
            }
        }
    }

    public event OnCBaseGrenadeSmokePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeSmoke);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeSmoke);
            }
        }
    }

    public void InvokePre(ref CBaseGrenadeSmokePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseGrenadeSmokePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeSmoke);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeSmoke);
        }
    }

    public void Invoke(CBaseGrenade schemaObject) => DatamapHooksPublisher.InvokeCBaseGrenadeSmoke(schemaObject.Address);
}