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
    private delegate void CLogicNPCCounterSetNPCCounterThinkDelegate(nint a1);

    private static IUnmanagedFunction<CLogicNPCCounterSetNPCCounterThinkDelegate>? CLogicNPCCounterSetNPCCounterThinkUnmanagedFunction;
    private static Guid CLogicNPCCounterSetNPCCounterThinkHookGuid;

    private static IUnmanagedFunction<CLogicNPCCounterSetNPCCounterThinkDelegate> CLogicNPCCounterSetNPCCounterThinkGetUnmanagedFunction()
    {
        if (CLogicNPCCounterSetNPCCounterThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CLogicNPCCounter", "CLogicNPCCounterSetNPCCounterThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CLogicNPCCounter::CLogicNPCCounterSetNPCCounterThink.");
            }
            CLogicNPCCounterSetNPCCounterThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CLogicNPCCounterSetNPCCounterThinkDelegate>(address);
        }
        return CLogicNPCCounterSetNPCCounterThinkUnmanagedFunction;
    }

    internal static Guid HookCLogicNPCCounterSetNPCCounterThink()
    {
        CLogicNPCCounterSetNPCCounterThinkHookGuid = CLogicNPCCounterSetNPCCounterThinkGetUnmanagedFunction().AddHook(next => (a1) => CLogicNPCCounterSetNPCCounterThinkPipeline(a1, () => next()(a1)));
        return CLogicNPCCounterSetNPCCounterThinkHookGuid;
    }

    internal static Guid UnhookCLogicNPCCounterSetNPCCounterThink()
    {
        CLogicNPCCounterSetNPCCounterThinkGetUnmanagedFunction().RemoveHook(CLogicNPCCounterSetNPCCounterThinkHookGuid);
        return Guid.Empty;
    }

    private static void CLogicNPCCounterSetNPCCounterThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CLogicNPCCounter>(a1);

            var preCtx = new CLogicNPCCounterSetNPCCounterThinkPreContext { SchemaObject = schemaObject };
            InvokeCLogicNPCCounterSetNPCCounterThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CLogicNPCCounterSetNPCCounterThinkPostContext { SchemaObject = schemaObject };
            InvokeCLogicNPCCounterSetNPCCounterThinkPost(ref postCtx);
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

    internal static void InvokeCLogicNPCCounterSetNPCCounterThink(nint a1)
    {
        CLogicNPCCounterSetNPCCounterThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCLogicNPCCounterSetNPCCounterThinkPre(ref CLogicNPCCounterSetNPCCounterThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCLogicNPCCounterSetNPCCounterThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCLogicNPCCounterSetNPCCounterThinkPost(ref CLogicNPCCounterSetNPCCounterThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCLogicNPCCounterSetNPCCounterThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CLogicNPCCounterSetNPCCounterThinkHook : ICLogicNPCCounterSetNPCCounterThinkHook
{
    private event OnCLogicNPCCounterSetNPCCounterThinkPreDelegate? _Pre;
    private event OnCLogicNPCCounterSetNPCCounterThinkPostDelegate? _Post;

    public event OnCLogicNPCCounterSetNPCCounterThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CLogicNPCCounterSetNPCCounterThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicNPCCounterSetNPCCounterThink);
            }
        }
    }

    public event OnCLogicNPCCounterSetNPCCounterThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CLogicNPCCounterSetNPCCounterThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicNPCCounterSetNPCCounterThink);
            }
        }
    }

    public void InvokePre(ref CLogicNPCCounterSetNPCCounterThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CLogicNPCCounterSetNPCCounterThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicNPCCounterSetNPCCounterThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicNPCCounterSetNPCCounterThink);
        }
    }

    public void Invoke(CLogicNPCCounter schemaObject) => DatamapHooksPublisher.InvokeCLogicNPCCounterSetNPCCounterThink(schemaObject.Address);
}