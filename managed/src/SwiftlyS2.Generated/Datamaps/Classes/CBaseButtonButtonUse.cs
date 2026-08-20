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
    private delegate void CBaseButtonButtonUseDelegate(nint a1);

    private static IUnmanagedFunction<CBaseButtonButtonUseDelegate>? CBaseButtonButtonUseUnmanagedFunction;
    private static Guid CBaseButtonButtonUseHookGuid;

    private static IUnmanagedFunction<CBaseButtonButtonUseDelegate> CBaseButtonButtonUseGetUnmanagedFunction()
    {
        if (CBaseButtonButtonUseUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseButton", "CBaseButtonButtonUse");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseButton::CBaseButtonButtonUse.");
            }
            CBaseButtonButtonUseUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseButtonButtonUseDelegate>(address);
        }
        return CBaseButtonButtonUseUnmanagedFunction;
    }

    internal static Guid HookCBaseButtonButtonUse()
    {
        CBaseButtonButtonUseHookGuid = CBaseButtonButtonUseGetUnmanagedFunction().AddHook(next => (a1) => CBaseButtonButtonUsePipeline(a1, () => next()(a1)));
        return CBaseButtonButtonUseHookGuid;
    }

    internal static Guid UnhookCBaseButtonButtonUse()
    {
        CBaseButtonButtonUseGetUnmanagedFunction().RemoveHook(CBaseButtonButtonUseHookGuid);
        return Guid.Empty;
    }

    private static void CBaseButtonButtonUsePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseButton>(a1);

            var preCtx = new CBaseButtonButtonUsePreContext { SchemaObject = schemaObject };
            InvokeCBaseButtonButtonUsePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseButtonButtonUsePostContext { SchemaObject = schemaObject };
            InvokeCBaseButtonButtonUsePost(ref postCtx);
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

    internal static void InvokeCBaseButtonButtonUse(nint a1)
    {
        CBaseButtonButtonUseGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseButtonButtonUsePre(ref CBaseButtonButtonUsePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonButtonUsePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseButtonButtonUsePost(ref CBaseButtonButtonUsePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonButtonUsePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseButtonButtonUseHook : ICBaseButtonButtonUseHook
{
    private event OnCBaseButtonButtonUsePreDelegate? _Pre;
    private event OnCBaseButtonButtonUsePostDelegate? _Post;

    public event OnCBaseButtonButtonUsePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonButtonUse);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonUse);
            }
        }
    }

    public event OnCBaseButtonButtonUsePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonButtonUse);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonUse);
            }
        }
    }

    public void InvokePre(ref CBaseButtonButtonUsePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseButtonButtonUsePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonUse);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonUse);
        }
    }

    public void Invoke(CBaseButton schemaObject) => DatamapHooksPublisher.InvokeCBaseButtonButtonUse(schemaObject.Address);
}