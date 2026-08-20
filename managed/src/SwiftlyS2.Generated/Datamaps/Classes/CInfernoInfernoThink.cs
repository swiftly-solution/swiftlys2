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
    private delegate void CInfernoInfernoThinkDelegate(nint a1);

    private static IUnmanagedFunction<CInfernoInfernoThinkDelegate>? CInfernoInfernoThinkUnmanagedFunction;
    private static Guid CInfernoInfernoThinkHookGuid;

    private static IUnmanagedFunction<CInfernoInfernoThinkDelegate> CInfernoInfernoThinkGetUnmanagedFunction()
    {
        if (CInfernoInfernoThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CInferno", "CInfernoInfernoThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CInferno::CInfernoInfernoThink.");
            }
            CInfernoInfernoThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CInfernoInfernoThinkDelegate>(address);
        }
        return CInfernoInfernoThinkUnmanagedFunction;
    }

    internal static Guid HookCInfernoInfernoThink()
    {
        CInfernoInfernoThinkHookGuid = CInfernoInfernoThinkGetUnmanagedFunction().AddHook(next => (a1) => CInfernoInfernoThinkPipeline(a1, () => next()(a1)));
        return CInfernoInfernoThinkHookGuid;
    }

    internal static Guid UnhookCInfernoInfernoThink()
    {
        CInfernoInfernoThinkGetUnmanagedFunction().RemoveHook(CInfernoInfernoThinkHookGuid);
        return Guid.Empty;
    }

    private static void CInfernoInfernoThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CInferno>(a1);

            var preCtx = new CInfernoInfernoThinkPreContext { SchemaObject = schemaObject };
            InvokeCInfernoInfernoThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CInfernoInfernoThinkPostContext { SchemaObject = schemaObject };
            InvokeCInfernoInfernoThinkPost(ref postCtx);
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

    internal static void InvokeCInfernoInfernoThink(nint a1)
    {
        CInfernoInfernoThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCInfernoInfernoThinkPre(ref CInfernoInfernoThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCInfernoInfernoThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCInfernoInfernoThinkPost(ref CInfernoInfernoThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCInfernoInfernoThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CInfernoInfernoThinkHook : ICInfernoInfernoThinkHook
{
    private event OnCInfernoInfernoThinkPreDelegate? _Pre;
    private event OnCInfernoInfernoThinkPostDelegate? _Post;

    public event OnCInfernoInfernoThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CInfernoInfernoThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CInfernoInfernoThink);
            }
        }
    }

    public event OnCInfernoInfernoThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CInfernoInfernoThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CInfernoInfernoThink);
            }
        }
    }

    public void InvokePre(ref CInfernoInfernoThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CInfernoInfernoThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CInfernoInfernoThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CInfernoInfernoThink);
        }
    }

    public void Invoke(CInferno schemaObject) => DatamapHooksPublisher.InvokeCInfernoInfernoThink(schemaObject.Address);
}