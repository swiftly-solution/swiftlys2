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
    private delegate void CLogicGameStateReportSetGameStateReportThinkDelegate(nint a1);

    private static IUnmanagedFunction<CLogicGameStateReportSetGameStateReportThinkDelegate>? CLogicGameStateReportSetGameStateReportThinkUnmanagedFunction;
    private static Guid CLogicGameStateReportSetGameStateReportThinkHookGuid;

    private static IUnmanagedFunction<CLogicGameStateReportSetGameStateReportThinkDelegate> CLogicGameStateReportSetGameStateReportThinkGetUnmanagedFunction()
    {
        if (CLogicGameStateReportSetGameStateReportThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CLogicGameStateReport", "CLogicGameStateReportSetGameStateReportThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CLogicGameStateReport::CLogicGameStateReportSetGameStateReportThink.");
            }
            CLogicGameStateReportSetGameStateReportThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CLogicGameStateReportSetGameStateReportThinkDelegate>(address);
        }
        return CLogicGameStateReportSetGameStateReportThinkUnmanagedFunction;
    }

    internal static Guid HookCLogicGameStateReportSetGameStateReportThink()
    {
        CLogicGameStateReportSetGameStateReportThinkHookGuid = CLogicGameStateReportSetGameStateReportThinkGetUnmanagedFunction().AddHook(next => (a1) => CLogicGameStateReportSetGameStateReportThinkPipeline(a1, () => next()(a1)));
        return CLogicGameStateReportSetGameStateReportThinkHookGuid;
    }

    internal static Guid UnhookCLogicGameStateReportSetGameStateReportThink()
    {
        CLogicGameStateReportSetGameStateReportThinkGetUnmanagedFunction().RemoveHook(CLogicGameStateReportSetGameStateReportThinkHookGuid);
        return Guid.Empty;
    }

    private static void CLogicGameStateReportSetGameStateReportThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CLogicGameStateReport>(a1);

            var preCtx = new CLogicGameStateReportSetGameStateReportThinkPreContext { SchemaObject = schemaObject };
            InvokeCLogicGameStateReportSetGameStateReportThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CLogicGameStateReportSetGameStateReportThinkPostContext { SchemaObject = schemaObject };
            InvokeCLogicGameStateReportSetGameStateReportThinkPost(ref postCtx);
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

    internal static void InvokeCLogicGameStateReportSetGameStateReportThink(nint a1)
    {
        CLogicGameStateReportSetGameStateReportThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCLogicGameStateReportSetGameStateReportThinkPre(ref CLogicGameStateReportSetGameStateReportThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCLogicGameStateReportSetGameStateReportThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCLogicGameStateReportSetGameStateReportThinkPost(ref CLogicGameStateReportSetGameStateReportThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCLogicGameStateReportSetGameStateReportThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CLogicGameStateReportSetGameStateReportThinkHook : ICLogicGameStateReportSetGameStateReportThinkHook
{
    private event OnCLogicGameStateReportSetGameStateReportThinkPreDelegate? _Pre;
    private event OnCLogicGameStateReportSetGameStateReportThinkPostDelegate? _Post;

    public event OnCLogicGameStateReportSetGameStateReportThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CLogicGameStateReportSetGameStateReportThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicGameStateReportSetGameStateReportThink);
            }
        }
    }

    public event OnCLogicGameStateReportSetGameStateReportThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CLogicGameStateReportSetGameStateReportThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicGameStateReportSetGameStateReportThink);
            }
        }
    }

    public void InvokePre(ref CLogicGameStateReportSetGameStateReportThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CLogicGameStateReportSetGameStateReportThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicGameStateReportSetGameStateReportThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicGameStateReportSetGameStateReportThink);
        }
    }

    public void Invoke(CLogicGameStateReport schemaObject) => DatamapHooksPublisher.InvokeCLogicGameStateReportSetGameStateReportThink(schemaObject.Address);
}