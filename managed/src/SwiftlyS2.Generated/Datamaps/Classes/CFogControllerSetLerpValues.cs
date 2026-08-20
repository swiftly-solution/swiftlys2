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
    private delegate void CFogControllerSetLerpValuesDelegate(nint a1);

    private static IUnmanagedFunction<CFogControllerSetLerpValuesDelegate>? CFogControllerSetLerpValuesUnmanagedFunction;
    private static Guid CFogControllerSetLerpValuesHookGuid;

    private static IUnmanagedFunction<CFogControllerSetLerpValuesDelegate> CFogControllerSetLerpValuesGetUnmanagedFunction()
    {
        if (CFogControllerSetLerpValuesUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFogController", "CFogControllerSetLerpValues");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFogController::CFogControllerSetLerpValues.");
            }
            CFogControllerSetLerpValuesUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFogControllerSetLerpValuesDelegate>(address);
        }
        return CFogControllerSetLerpValuesUnmanagedFunction;
    }

    internal static Guid HookCFogControllerSetLerpValues()
    {
        CFogControllerSetLerpValuesHookGuid = CFogControllerSetLerpValuesGetUnmanagedFunction().AddHook(next => (a1) => CFogControllerSetLerpValuesPipeline(a1, () => next()(a1)));
        return CFogControllerSetLerpValuesHookGuid;
    }

    internal static Guid UnhookCFogControllerSetLerpValues()
    {
        CFogControllerSetLerpValuesGetUnmanagedFunction().RemoveHook(CFogControllerSetLerpValuesHookGuid);
        return Guid.Empty;
    }

    private static void CFogControllerSetLerpValuesPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFogController>(a1);

            var preCtx = new CFogControllerSetLerpValuesPreContext { SchemaObject = schemaObject };
            InvokeCFogControllerSetLerpValuesPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFogControllerSetLerpValuesPostContext { SchemaObject = schemaObject };
            InvokeCFogControllerSetLerpValuesPost(ref postCtx);
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

    internal static void InvokeCFogControllerSetLerpValues(nint a1)
    {
        CFogControllerSetLerpValuesGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFogControllerSetLerpValuesPre(ref CFogControllerSetLerpValuesPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFogControllerSetLerpValuesPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFogControllerSetLerpValuesPost(ref CFogControllerSetLerpValuesPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFogControllerSetLerpValuesPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFogControllerSetLerpValuesHook : ICFogControllerSetLerpValuesHook
{
    private event OnCFogControllerSetLerpValuesPreDelegate? _Pre;
    private event OnCFogControllerSetLerpValuesPostDelegate? _Post;

    public event OnCFogControllerSetLerpValuesPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFogControllerSetLerpValues);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFogControllerSetLerpValues);
            }
        }
    }

    public event OnCFogControllerSetLerpValuesPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFogControllerSetLerpValues);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFogControllerSetLerpValues);
            }
        }
    }

    public void InvokePre(ref CFogControllerSetLerpValuesPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFogControllerSetLerpValuesPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFogControllerSetLerpValues);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFogControllerSetLerpValues);
        }
    }

    public void Invoke(CFogController schemaObject) => DatamapHooksPublisher.InvokeCFogControllerSetLerpValues(schemaObject.Address);
}