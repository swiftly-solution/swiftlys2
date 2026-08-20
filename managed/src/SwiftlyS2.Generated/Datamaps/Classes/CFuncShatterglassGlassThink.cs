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
    private delegate void CFuncShatterglassGlassThinkDelegate(nint a1);

    private static IUnmanagedFunction<CFuncShatterglassGlassThinkDelegate>? CFuncShatterglassGlassThinkUnmanagedFunction;
    private static Guid CFuncShatterglassGlassThinkHookGuid;

    private static IUnmanagedFunction<CFuncShatterglassGlassThinkDelegate> CFuncShatterglassGlassThinkGetUnmanagedFunction()
    {
        if (CFuncShatterglassGlassThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFuncShatterglass", "CFuncShatterglassGlassThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFuncShatterglass::CFuncShatterglassGlassThink.");
            }
            CFuncShatterglassGlassThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFuncShatterglassGlassThinkDelegate>(address);
        }
        return CFuncShatterglassGlassThinkUnmanagedFunction;
    }

    internal static Guid HookCFuncShatterglassGlassThink()
    {
        CFuncShatterglassGlassThinkHookGuid = CFuncShatterglassGlassThinkGetUnmanagedFunction().AddHook(next => (a1) => CFuncShatterglassGlassThinkPipeline(a1, () => next()(a1)));
        return CFuncShatterglassGlassThinkHookGuid;
    }

    internal static Guid UnhookCFuncShatterglassGlassThink()
    {
        CFuncShatterglassGlassThinkGetUnmanagedFunction().RemoveHook(CFuncShatterglassGlassThinkHookGuid);
        return Guid.Empty;
    }

    private static void CFuncShatterglassGlassThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFuncShatterglass>(a1);

            var preCtx = new CFuncShatterglassGlassThinkPreContext { SchemaObject = schemaObject };
            InvokeCFuncShatterglassGlassThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFuncShatterglassGlassThinkPostContext { SchemaObject = schemaObject };
            InvokeCFuncShatterglassGlassThinkPost(ref postCtx);
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

    internal static void InvokeCFuncShatterglassGlassThink(nint a1)
    {
        CFuncShatterglassGlassThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFuncShatterglassGlassThinkPre(ref CFuncShatterglassGlassThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncShatterglassGlassThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFuncShatterglassGlassThinkPost(ref CFuncShatterglassGlassThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFuncShatterglassGlassThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFuncShatterglassGlassThinkHook : ICFuncShatterglassGlassThinkHook
{
    private event OnCFuncShatterglassGlassThinkPreDelegate? _Pre;
    private event OnCFuncShatterglassGlassThinkPostDelegate? _Post;

    public event OnCFuncShatterglassGlassThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncShatterglassGlassThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncShatterglassGlassThink);
            }
        }
    }

    public event OnCFuncShatterglassGlassThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFuncShatterglassGlassThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncShatterglassGlassThink);
            }
        }
    }

    public void InvokePre(ref CFuncShatterglassGlassThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFuncShatterglassGlassThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncShatterglassGlassThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFuncShatterglassGlassThink);
        }
    }

    public void Invoke(CFuncShatterglass schemaObject) => DatamapHooksPublisher.InvokeCFuncShatterglassGlassThink(schemaObject.Address);
}