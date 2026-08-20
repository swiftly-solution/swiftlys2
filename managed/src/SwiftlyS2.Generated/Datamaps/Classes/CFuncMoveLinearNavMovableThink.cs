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
    private delegate void CFuncMoveLinearNavMovableThinkDelegate(nint a1);

    private static IUnmanagedFunction<CFuncMoveLinearNavMovableThinkDelegate>? CFuncMoveLinearNavMovableThinkUnmanagedFunction;
    private static Guid CFuncMoveLinearNavMovableThinkHookGuid;

    private static IUnmanagedFunction<CFuncMoveLinearNavMovableThinkDelegate> CFuncMoveLinearNavMovableThinkGetUnmanagedFunction()
    {
        if (CFuncMoveLinearNavMovableThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncMoveLinear", "CFuncMoveLinearNavMovableThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncMoveLinear::CFuncMoveLinearNavMovableThink.");
            }
            CFuncMoveLinearNavMovableThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncMoveLinearNavMovableThinkDelegate>(address);
        }
        return CFuncMoveLinearNavMovableThinkUnmanagedFunction;
    }

    internal static Guid HookCFuncMoveLinearNavMovableThink()
    {
        CFuncMoveLinearNavMovableThinkHookGuid = CFuncMoveLinearNavMovableThinkGetUnmanagedFunction().AddHook(next => (a1) => CFuncMoveLinearNavMovableThinkPipeline(a1, () => next()(a1)));
        return CFuncMoveLinearNavMovableThinkHookGuid;
    }

    internal static Guid UnhookCFuncMoveLinearNavMovableThink()
    {
        CFuncMoveLinearNavMovableThinkGetUnmanagedFunction().RemoveHook(CFuncMoveLinearNavMovableThinkHookGuid);
        return Guid.Empty;
    }

    private static void CFuncMoveLinearNavMovableThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncMoveLinear>(a1);

            var preCtx = new CFuncMoveLinearNavMovableThinkPreContext { SchemaObject = schemaObject };
            InvokeCFuncMoveLinearNavMovableThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncMoveLinearNavMovableThinkPostContext { SchemaObject = schemaObject };
            InvokeCFuncMoveLinearNavMovableThinkPost(ref postCtx);
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

    internal static void InvokeCFuncMoveLinearNavMovableThink(nint a1)
    {
        CFuncMoveLinearNavMovableThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncMoveLinearNavMovableThinkPre(ref CFuncMoveLinearNavMovableThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncMoveLinearNavMovableThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncMoveLinearNavMovableThinkPost(ref CFuncMoveLinearNavMovableThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncMoveLinearNavMovableThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncMoveLinearNavMovableThinkHook : ICFuncMoveLinearNavMovableThinkHook
{
    private event OnCFuncMoveLinearNavMovableThinkPreDelegate? _Pre;
    private event OnCFuncMoveLinearNavMovableThinkPostDelegate? _Post;

    public event OnCFuncMoveLinearNavMovableThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncMoveLinearNavMovableThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncMoveLinearNavMovableThink);
            }
        }
    }

    public event OnCFuncMoveLinearNavMovableThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncMoveLinearNavMovableThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncMoveLinearNavMovableThink);
            }
        }
    }

    public void InvokePre(ref CFuncMoveLinearNavMovableThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncMoveLinearNavMovableThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncMoveLinearNavMovableThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncMoveLinearNavMovableThink);
        }
    }

    public void Invoke(CFuncMoveLinear schemaObject) => DatamapHooksPublisher.InvokeCFuncMoveLinearNavMovableThink(schemaObject.Address);
}