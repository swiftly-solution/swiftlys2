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
    private delegate void CChickenChickenUseDelegate(nint a1);

    private static IUnmanagedFunction<CChickenChickenUseDelegate>? CChickenChickenUseUnmanagedFunction;
    private static Guid CChickenChickenUseHookGuid;

    private static IUnmanagedFunction<CChickenChickenUseDelegate> CChickenChickenUseGetUnmanagedFunction()
    {
        if (CChickenChickenUseUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CChicken", "CChickenChickenUse");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CChicken::CChickenChickenUse.");
            }
            CChickenChickenUseUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CChickenChickenUseDelegate>(address);
        }
        return CChickenChickenUseUnmanagedFunction;
    }

    internal static Guid HookCChickenChickenUse()
    {
        CChickenChickenUseHookGuid = CChickenChickenUseGetUnmanagedFunction().AddHook(next => (a1) => CChickenChickenUsePipeline(a1, () => next()(a1)));
        return CChickenChickenUseHookGuid;
    }

    internal static Guid UnhookCChickenChickenUse()
    {
        CChickenChickenUseGetUnmanagedFunction().RemoveHook(CChickenChickenUseHookGuid);
        return Guid.Empty;
    }

    private static void CChickenChickenUsePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CChicken>(a1);

            var preCtx = new CChickenChickenUsePreContext { SchemaObject = schemaObject };
            InvokeCChickenChickenUsePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CChickenChickenUsePostContext { SchemaObject = schemaObject };
            InvokeCChickenChickenUsePost(ref postCtx);
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

    internal static void InvokeCChickenChickenUse(nint a1)
    {
        CChickenChickenUseGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCChickenChickenUsePre(ref CChickenChickenUsePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCChickenChickenUsePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCChickenChickenUsePost(ref CChickenChickenUsePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCChickenChickenUsePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CChickenChickenUseHook : ICChickenChickenUseHook
{
    private event OnCChickenChickenUsePreDelegate? _Pre;
    private event OnCChickenChickenUsePostDelegate? _Post;

    public event OnCChickenChickenUsePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CChickenChickenUse);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CChickenChickenUse);
            }
        }
    }

    public event OnCChickenChickenUsePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CChickenChickenUse);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CChickenChickenUse);
            }
        }
    }

    public void InvokePre(ref CChickenChickenUsePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CChickenChickenUsePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CChickenChickenUse);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CChickenChickenUse);
        }
    }

    public void Invoke(CChicken schemaObject) => DatamapHooksPublisher.InvokeCChickenChickenUse(schemaObject.Address);
}