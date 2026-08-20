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
    private delegate void CFishPoolUpdateDelegate(nint a1);

    private static IUnmanagedFunction<CFishPoolUpdateDelegate>? CFishPoolUpdateUnmanagedFunction;
    private static Guid CFishPoolUpdateHookGuid;

    private static IUnmanagedFunction<CFishPoolUpdateDelegate> CFishPoolUpdateGetUnmanagedFunction()
    {
        if (CFishPoolUpdateUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CFishPool", "CFishPoolUpdate");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CFishPool::CFishPoolUpdate.");
            }
            CFishPoolUpdateUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CFishPoolUpdateDelegate>(address);
        }
        return CFishPoolUpdateUnmanagedFunction;
    }

    internal static Guid HookCFishPoolUpdate()
    {
        CFishPoolUpdateHookGuid = CFishPoolUpdateGetUnmanagedFunction().AddHook(next => (a1) => CFishPoolUpdatePipeline(a1, () => next()(a1)));
        return CFishPoolUpdateHookGuid;
    }

    internal static Guid UnhookCFishPoolUpdate()
    {
        CFishPoolUpdateGetUnmanagedFunction().RemoveHook(CFishPoolUpdateHookGuid);
        return Guid.Empty;
    }

    private static void CFishPoolUpdatePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CFishPool>(a1);

            var preCtx = new CFishPoolUpdatePreContext { SchemaObject = schemaObject };
            InvokeCFishPoolUpdatePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CFishPoolUpdatePostContext { SchemaObject = schemaObject };
            InvokeCFishPoolUpdatePost(ref postCtx);
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

    internal static void InvokeCFishPoolUpdate(nint a1)
    {
        CFishPoolUpdateGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCFishPoolUpdatePre(ref CFishPoolUpdatePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFishPoolUpdatePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCFishPoolUpdatePost(ref CFishPoolUpdatePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCFishPoolUpdatePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CFishPoolUpdateHook : ICFishPoolUpdateHook
{
    private event OnCFishPoolUpdatePreDelegate? _Pre;
    private event OnCFishPoolUpdatePostDelegate? _Post;

    public event OnCFishPoolUpdatePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFishPoolUpdate);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFishPoolUpdate);
            }
        }
    }

    public event OnCFishPoolUpdatePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CFishPoolUpdate);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFishPoolUpdate);
            }
        }
    }

    public void InvokePre(ref CFishPoolUpdatePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CFishPoolUpdatePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFishPoolUpdate);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CFishPoolUpdate);
        }
    }

    public void Invoke(CFishPool schemaObject) => DatamapHooksPublisher.InvokeCFishPoolUpdate(schemaObject.Address);
}