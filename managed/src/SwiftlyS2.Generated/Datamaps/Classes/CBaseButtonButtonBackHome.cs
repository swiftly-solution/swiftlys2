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
    private delegate void CBaseButtonButtonBackHomeDelegate(nint a1);

    private static IUnmanagedFunction<CBaseButtonButtonBackHomeDelegate>? CBaseButtonButtonBackHomeUnmanagedFunction;
    private static Guid CBaseButtonButtonBackHomeHookGuid;

    private static IUnmanagedFunction<CBaseButtonButtonBackHomeDelegate> CBaseButtonButtonBackHomeGetUnmanagedFunction()
    {
        if (CBaseButtonButtonBackHomeUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseButton", "CBaseButtonButtonBackHome");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseButton::CBaseButtonButtonBackHome.");
            }
            CBaseButtonButtonBackHomeUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseButtonButtonBackHomeDelegate>(address);
        }
        return CBaseButtonButtonBackHomeUnmanagedFunction;
    }

    internal static Guid HookCBaseButtonButtonBackHome()
    {
        CBaseButtonButtonBackHomeHookGuid = CBaseButtonButtonBackHomeGetUnmanagedFunction().AddHook(next => (a1) => CBaseButtonButtonBackHomePipeline(a1, () => next()(a1)));
        return CBaseButtonButtonBackHomeHookGuid;
    }

    internal static Guid UnhookCBaseButtonButtonBackHome()
    {
        CBaseButtonButtonBackHomeGetUnmanagedFunction().RemoveHook(CBaseButtonButtonBackHomeHookGuid);
        return Guid.Empty;
    }

    private static void CBaseButtonButtonBackHomePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseButton>(a1);

            var preCtx = new CBaseButtonButtonBackHomePreContext { SchemaObject = schemaObject };
            InvokeCBaseButtonButtonBackHomePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseButtonButtonBackHomePostContext { SchemaObject = schemaObject };
            InvokeCBaseButtonButtonBackHomePost(ref postCtx);
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

    internal static void InvokeCBaseButtonButtonBackHome(nint a1)
    {
        CBaseButtonButtonBackHomeGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseButtonButtonBackHomePre(ref CBaseButtonButtonBackHomePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonButtonBackHomePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseButtonButtonBackHomePost(ref CBaseButtonButtonBackHomePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseButtonButtonBackHomePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseButtonButtonBackHomeHook : ICBaseButtonButtonBackHomeHook
{
    private event OnCBaseButtonButtonBackHomePreDelegate? _Pre;
    private event OnCBaseButtonButtonBackHomePostDelegate? _Post;

    public event OnCBaseButtonButtonBackHomePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonButtonBackHome);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonBackHome);
            }
        }
    }

    public event OnCBaseButtonButtonBackHomePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseButtonButtonBackHome);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonBackHome);
            }
        }
    }

    public void InvokePre(ref CBaseButtonButtonBackHomePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseButtonButtonBackHomePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonBackHome);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseButtonButtonBackHome);
        }
    }

    public void Invoke(CBaseButton schemaObject) => DatamapHooksPublisher.InvokeCBaseButtonButtonBackHome(schemaObject.Address);
}