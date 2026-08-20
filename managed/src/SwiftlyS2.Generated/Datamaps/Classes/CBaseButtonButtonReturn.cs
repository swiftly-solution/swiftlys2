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
    private delegate void CBaseButtonButtonReturnDelegate(nint a1);

    private static IUnmanagedFunction<CBaseButtonButtonReturnDelegate>? CBaseButtonButtonReturnUnmanagedFunction;
    private static Guid CBaseButtonButtonReturnHookGuid;

    private static IUnmanagedFunction<CBaseButtonButtonReturnDelegate> CBaseButtonButtonReturnGetUnmanagedFunction()
    {
        if (CBaseButtonButtonReturnUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseButton", "CBaseButtonButtonReturn");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseButton::CBaseButtonButtonReturn.");
            }
            CBaseButtonButtonReturnUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseButtonButtonReturnDelegate>(address);
        }
        return CBaseButtonButtonReturnUnmanagedFunction;
    }

    internal static Guid HookCBaseButtonButtonReturn()
    {
        CBaseButtonButtonReturnHookGuid = CBaseButtonButtonReturnGetUnmanagedFunction().AddHook(next => (a1) => CBaseButtonButtonReturnPipeline(a1, () => next()(a1)));
        return CBaseButtonButtonReturnHookGuid;
    }

    internal static Guid UnhookCBaseButtonButtonReturn()
    {
        CBaseButtonButtonReturnGetUnmanagedFunction().RemoveHook(CBaseButtonButtonReturnHookGuid);
        return Guid.Empty;
    }

    private static void CBaseButtonButtonReturnPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseButton>(a1);

            var preCtx = new CBaseButtonButtonReturnPreContext { SchemaObject = schemaObject };
            InvokeCBaseButtonButtonReturnPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseButtonButtonReturnPostContext { SchemaObject = schemaObject };
            InvokeCBaseButtonButtonReturnPost(ref postCtx);
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

    internal static void InvokeCBaseButtonButtonReturn(nint a1)
    {
        CBaseButtonButtonReturnGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseButtonButtonReturnPre(ref CBaseButtonButtonReturnPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonButtonReturnPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseButtonButtonReturnPost(ref CBaseButtonButtonReturnPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonButtonReturnPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseButtonButtonReturnHook : ICBaseButtonButtonReturnHook
{
    private event OnCBaseButtonButtonReturnPreDelegate? _Pre;
    private event OnCBaseButtonButtonReturnPostDelegate? _Post;

    public event OnCBaseButtonButtonReturnPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonButtonReturn);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonReturn);
            }
        }
    }

    public event OnCBaseButtonButtonReturnPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonButtonReturn);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonReturn);
            }
        }
    }

    public void InvokePre(ref CBaseButtonButtonReturnPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseButtonButtonReturnPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonReturn);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonReturn);
        }
    }

    public void Invoke(CBaseButton schemaObject) => DatamapHooksPublisher.InvokeCBaseButtonButtonReturn(schemaObject.Address);
}