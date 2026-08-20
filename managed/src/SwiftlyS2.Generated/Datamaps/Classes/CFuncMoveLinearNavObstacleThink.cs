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
    private delegate void CFuncMoveLinearNavObstacleThinkDelegate(nint a1);

    private static IUnmanagedFunction<CFuncMoveLinearNavObstacleThinkDelegate>? CFuncMoveLinearNavObstacleThinkUnmanagedFunction;
    private static Guid CFuncMoveLinearNavObstacleThinkHookGuid;

    private static IUnmanagedFunction<CFuncMoveLinearNavObstacleThinkDelegate> CFuncMoveLinearNavObstacleThinkGetUnmanagedFunction()
    {
        if (CFuncMoveLinearNavObstacleThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncMoveLinear", "CFuncMoveLinearNavObstacleThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncMoveLinear::CFuncMoveLinearNavObstacleThink.");
            }
            CFuncMoveLinearNavObstacleThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncMoveLinearNavObstacleThinkDelegate>(address);
        }
        return CFuncMoveLinearNavObstacleThinkUnmanagedFunction;
    }

    internal static Guid HookCFuncMoveLinearNavObstacleThink()
    {
        CFuncMoveLinearNavObstacleThinkHookGuid = CFuncMoveLinearNavObstacleThinkGetUnmanagedFunction().AddHook(next => (a1) => CFuncMoveLinearNavObstacleThinkPipeline(a1, () => next()(a1)));
        return CFuncMoveLinearNavObstacleThinkHookGuid;
    }

    internal static Guid UnhookCFuncMoveLinearNavObstacleThink()
    {
        CFuncMoveLinearNavObstacleThinkGetUnmanagedFunction().RemoveHook(CFuncMoveLinearNavObstacleThinkHookGuid);
        return Guid.Empty;
    }

    private static void CFuncMoveLinearNavObstacleThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncMoveLinear>(a1);

            var preCtx = new CFuncMoveLinearNavObstacleThinkPreContext { SchemaObject = schemaObject };
            InvokeCFuncMoveLinearNavObstacleThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncMoveLinearNavObstacleThinkPostContext { SchemaObject = schemaObject };
            InvokeCFuncMoveLinearNavObstacleThinkPost(ref postCtx);
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

    internal static void InvokeCFuncMoveLinearNavObstacleThink(nint a1)
    {
        CFuncMoveLinearNavObstacleThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncMoveLinearNavObstacleThinkPre(ref CFuncMoveLinearNavObstacleThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncMoveLinearNavObstacleThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncMoveLinearNavObstacleThinkPost(ref CFuncMoveLinearNavObstacleThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncMoveLinearNavObstacleThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncMoveLinearNavObstacleThinkHook : ICFuncMoveLinearNavObstacleThinkHook
{
    private event OnCFuncMoveLinearNavObstacleThinkPreDelegate? _Pre;
    private event OnCFuncMoveLinearNavObstacleThinkPostDelegate? _Post;

    public event OnCFuncMoveLinearNavObstacleThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncMoveLinearNavObstacleThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncMoveLinearNavObstacleThink);
            }
        }
    }

    public event OnCFuncMoveLinearNavObstacleThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncMoveLinearNavObstacleThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncMoveLinearNavObstacleThink);
            }
        }
    }

    public void InvokePre(ref CFuncMoveLinearNavObstacleThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncMoveLinearNavObstacleThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncMoveLinearNavObstacleThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncMoveLinearNavObstacleThink);
        }
    }

    public void Invoke(CFuncMoveLinear schemaObject) => DatamapHooksPublisher.InvokeCFuncMoveLinearNavObstacleThink(schemaObject.Address);
}