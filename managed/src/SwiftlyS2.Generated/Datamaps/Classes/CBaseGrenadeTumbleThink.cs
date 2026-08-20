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
    private delegate void CBaseGrenadeTumbleThinkDelegate(nint a1);

    private static IUnmanagedFunction<CBaseGrenadeTumbleThinkDelegate>? CBaseGrenadeTumbleThinkUnmanagedFunction;
    private static Guid CBaseGrenadeTumbleThinkHookGuid;

    private static IUnmanagedFunction<CBaseGrenadeTumbleThinkDelegate> CBaseGrenadeTumbleThinkGetUnmanagedFunction()
    {
        if (CBaseGrenadeTumbleThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseGrenade", "CBaseGrenadeTumbleThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseGrenade::CBaseGrenadeTumbleThink.");
            }
            CBaseGrenadeTumbleThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseGrenadeTumbleThinkDelegate>(address);
        }
        return CBaseGrenadeTumbleThinkUnmanagedFunction;
    }

    internal static Guid HookCBaseGrenadeTumbleThink()
    {
        CBaseGrenadeTumbleThinkHookGuid = CBaseGrenadeTumbleThinkGetUnmanagedFunction().AddHook(next => (a1) => CBaseGrenadeTumbleThinkPipeline(a1, () => next()(a1)));
        return CBaseGrenadeTumbleThinkHookGuid;
    }

    internal static Guid UnhookCBaseGrenadeTumbleThink()
    {
        CBaseGrenadeTumbleThinkGetUnmanagedFunction().RemoveHook(CBaseGrenadeTumbleThinkHookGuid);
        return Guid.Empty;
    }

    private static void CBaseGrenadeTumbleThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseGrenade>(a1);

            var preCtx = new CBaseGrenadeTumbleThinkPreContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeTumbleThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseGrenadeTumbleThinkPostContext { SchemaObject = schemaObject };
            InvokeCBaseGrenadeTumbleThinkPost(ref postCtx);
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

    internal static void InvokeCBaseGrenadeTumbleThink(nint a1)
    {
        CBaseGrenadeTumbleThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseGrenadeTumbleThinkPre(ref CBaseGrenadeTumbleThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeTumbleThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseGrenadeTumbleThinkPost(ref CBaseGrenadeTumbleThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseGrenadeTumbleThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseGrenadeTumbleThinkHook : ICBaseGrenadeTumbleThinkHook
{
    private event OnCBaseGrenadeTumbleThinkPreDelegate? _Pre;
    private event OnCBaseGrenadeTumbleThinkPostDelegate? _Post;

    public event OnCBaseGrenadeTumbleThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeTumbleThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeTumbleThink);
            }
        }
    }

    public event OnCBaseGrenadeTumbleThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseGrenadeTumbleThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeTumbleThink);
            }
        }
    }

    public void InvokePre(ref CBaseGrenadeTumbleThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseGrenadeTumbleThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeTumbleThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseGrenadeTumbleThink);
        }
    }

    public void Invoke(CBaseGrenade schemaObject) => DatamapHooksPublisher.InvokeCBaseGrenadeTumbleThink(schemaObject.Address);
}