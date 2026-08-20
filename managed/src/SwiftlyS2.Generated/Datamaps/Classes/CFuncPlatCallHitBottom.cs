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
    private delegate void CFuncPlatCallHitBottomDelegate(nint a1);

    private static IUnmanagedFunction<CFuncPlatCallHitBottomDelegate>? CFuncPlatCallHitBottomUnmanagedFunction;
    private static Guid CFuncPlatCallHitBottomHookGuid;

    private static IUnmanagedFunction<CFuncPlatCallHitBottomDelegate> CFuncPlatCallHitBottomGetUnmanagedFunction()
    {
        if (CFuncPlatCallHitBottomUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncPlat", "CFuncPlatCallHitBottom");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncPlat::CFuncPlatCallHitBottom.");
            }
            CFuncPlatCallHitBottomUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncPlatCallHitBottomDelegate>(address);
        }
        return CFuncPlatCallHitBottomUnmanagedFunction;
    }

    internal static Guid HookCFuncPlatCallHitBottom()
    {
        CFuncPlatCallHitBottomHookGuid = CFuncPlatCallHitBottomGetUnmanagedFunction().AddHook(next => (a1) => CFuncPlatCallHitBottomPipeline(a1, () => next()(a1)));
        return CFuncPlatCallHitBottomHookGuid;
    }

    internal static Guid UnhookCFuncPlatCallHitBottom()
    {
        CFuncPlatCallHitBottomGetUnmanagedFunction().RemoveHook(CFuncPlatCallHitBottomHookGuid);
        return Guid.Empty;
    }

    private static void CFuncPlatCallHitBottomPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncPlat>(a1);

            var preCtx = new CFuncPlatCallHitBottomPreContext { SchemaObject = schemaObject };
            InvokeCFuncPlatCallHitBottomPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncPlatCallHitBottomPostContext { SchemaObject = schemaObject };
            InvokeCFuncPlatCallHitBottomPost(ref postCtx);
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

    internal static void InvokeCFuncPlatCallHitBottom(nint a1)
    {
        CFuncPlatCallHitBottomGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncPlatCallHitBottomPre(ref CFuncPlatCallHitBottomPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncPlatCallHitBottomPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncPlatCallHitBottomPost(ref CFuncPlatCallHitBottomPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncPlatCallHitBottomPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncPlatCallHitBottomHook : ICFuncPlatCallHitBottomHook
{
    private event OnCFuncPlatCallHitBottomPreDelegate? _Pre;
    private event OnCFuncPlatCallHitBottomPostDelegate? _Post;

    public event OnCFuncPlatCallHitBottomPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncPlatCallHitBottom);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatCallHitBottom);
            }
        }
    }

    public event OnCFuncPlatCallHitBottomPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncPlatCallHitBottom);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatCallHitBottom);
            }
        }
    }

    public void InvokePre(ref CFuncPlatCallHitBottomPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncPlatCallHitBottomPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatCallHitBottom);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncPlatCallHitBottom);
        }
    }

    public void Invoke(CFuncPlat schemaObject) => DatamapHooksPublisher.InvokeCFuncPlatCallHitBottom(schemaObject.Address);
}