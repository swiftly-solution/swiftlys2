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
    private delegate void CFuncTrackChangeFindDelegate(nint a1);

    private static IUnmanagedFunction<CFuncTrackChangeFindDelegate>? CFuncTrackChangeFindUnmanagedFunction;
    private static Guid CFuncTrackChangeFindHookGuid;

    private static IUnmanagedFunction<CFuncTrackChangeFindDelegate> CFuncTrackChangeFindGetUnmanagedFunction()
    {
        if (CFuncTrackChangeFindUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncTrackChange", "CFuncTrackChangeFind");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncTrackChange::CFuncTrackChangeFind.");
            }
            CFuncTrackChangeFindUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncTrackChangeFindDelegate>(address);
        }
        return CFuncTrackChangeFindUnmanagedFunction;
    }

    internal static Guid HookCFuncTrackChangeFind()
    {
        CFuncTrackChangeFindHookGuid = CFuncTrackChangeFindGetUnmanagedFunction().AddHook(next => (a1) => CFuncTrackChangeFindPipeline(a1, () => next()(a1)));
        return CFuncTrackChangeFindHookGuid;
    }

    internal static Guid UnhookCFuncTrackChangeFind()
    {
        CFuncTrackChangeFindGetUnmanagedFunction().RemoveHook(CFuncTrackChangeFindHookGuid);
        return Guid.Empty;
    }

    private static void CFuncTrackChangeFindPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncTrackChange>(a1);

            var preCtx = new CFuncTrackChangeFindPreContext { SchemaObject = schemaObject };
            InvokeCFuncTrackChangeFindPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncTrackChangeFindPostContext { SchemaObject = schemaObject };
            InvokeCFuncTrackChangeFindPost(ref postCtx);
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

    internal static void InvokeCFuncTrackChangeFind(nint a1)
    {
        CFuncTrackChangeFindGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncTrackChangeFindPre(ref CFuncTrackChangeFindPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrackChangeFindPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncTrackChangeFindPost(ref CFuncTrackChangeFindPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncTrackChangeFindPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncTrackChangeFindHook : ICFuncTrackChangeFindHook
{
    private event OnCFuncTrackChangeFindPreDelegate? _Pre;
    private event OnCFuncTrackChangeFindPostDelegate? _Post;

    public event OnCFuncTrackChangeFindPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrackChangeFind);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackChangeFind);
            }
        }
    }

    public event OnCFuncTrackChangeFindPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncTrackChangeFind);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackChangeFind);
            }
        }
    }

    public void InvokePre(ref CFuncTrackChangeFindPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncTrackChangeFindPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackChangeFind);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncTrackChangeFind);
        }
    }

    public void Invoke(CFuncTrackChange schemaObject) => DatamapHooksPublisher.InvokeCFuncTrackChangeFind(schemaObject.Address);
}