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
    private delegate void CHostageHostageUseDelegate(nint a1);

    private static IUnmanagedFunction<CHostageHostageUseDelegate>? CHostageHostageUseUnmanagedFunction;
    private static Guid CHostageHostageUseHookGuid;

    private static IUnmanagedFunction<CHostageHostageUseDelegate> CHostageHostageUseGetUnmanagedFunction()
    {
        if (CHostageHostageUseUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CHostage", "CHostageHostageUse");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CHostage::CHostageHostageUse.");
            }
            CHostageHostageUseUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CHostageHostageUseDelegate>(address);
        }
        return CHostageHostageUseUnmanagedFunction;
    }

    internal static Guid HookCHostageHostageUse()
    {
        CHostageHostageUseHookGuid = CHostageHostageUseGetUnmanagedFunction().AddHook(next => (a1) => CHostageHostageUsePipeline(a1, () => next()(a1)));
        return CHostageHostageUseHookGuid;
    }

    internal static Guid UnhookCHostageHostageUse()
    {
        CHostageHostageUseGetUnmanagedFunction().RemoveHook(CHostageHostageUseHookGuid);
        return Guid.Empty;
    }

    private static void CHostageHostageUsePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CHostage>(a1);

            var preCtx = new CHostageHostageUsePreContext { SchemaObject = schemaObject };
            InvokeCHostageHostageUsePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CHostageHostageUsePostContext { SchemaObject = schemaObject };
            InvokeCHostageHostageUsePost(ref postCtx);
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

    internal static void InvokeCHostageHostageUse(nint a1)
    {
        CHostageHostageUseGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCHostageHostageUsePre(ref CHostageHostageUsePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCHostageHostageUsePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCHostageHostageUsePost(ref CHostageHostageUsePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCHostageHostageUsePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CHostageHostageUseHook : ICHostageHostageUseHook
{
    private event OnCHostageHostageUsePreDelegate? _Pre;
    private event OnCHostageHostageUsePostDelegate? _Post;

    public event OnCHostageHostageUsePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CHostageHostageUse);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CHostageHostageUse);
            }
        }
    }

    public event OnCHostageHostageUsePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CHostageHostageUse);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CHostageHostageUse);
            }
        }
    }

    public void InvokePre(ref CHostageHostageUsePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CHostageHostageUsePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CHostageHostageUse);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CHostageHostageUse);
        }
    }

    public void Invoke(CHostage schemaObject) => DatamapHooksPublisher.InvokeCHostageHostageUse(schemaObject.Address);
}