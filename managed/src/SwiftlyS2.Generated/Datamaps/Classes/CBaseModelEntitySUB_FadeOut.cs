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
    private delegate void CBaseModelEntitySUB_FadeOutDelegate(nint a1);

    private static IUnmanagedFunction<CBaseModelEntitySUB_FadeOutDelegate>? CBaseModelEntitySUB_FadeOutUnmanagedFunction;
    private static Guid CBaseModelEntitySUB_FadeOutHookGuid;

    private static IUnmanagedFunction<CBaseModelEntitySUB_FadeOutDelegate> CBaseModelEntitySUB_FadeOutGetUnmanagedFunction()
    {
        if (CBaseModelEntitySUB_FadeOutUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseModelEntity", "CBaseModelEntitySUB_FadeOut");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseModelEntity::CBaseModelEntitySUB_FadeOut.");
            }
            CBaseModelEntitySUB_FadeOutUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseModelEntitySUB_FadeOutDelegate>(address);
        }
        return CBaseModelEntitySUB_FadeOutUnmanagedFunction;
    }

    internal static Guid HookCBaseModelEntitySUB_FadeOut()
    {
        CBaseModelEntitySUB_FadeOutHookGuid = CBaseModelEntitySUB_FadeOutGetUnmanagedFunction().AddHook(next => (a1) => CBaseModelEntitySUB_FadeOutPipeline(a1, () => next()(a1)));
        return CBaseModelEntitySUB_FadeOutHookGuid;
    }

    internal static Guid UnhookCBaseModelEntitySUB_FadeOut()
    {
        CBaseModelEntitySUB_FadeOutGetUnmanagedFunction().RemoveHook(CBaseModelEntitySUB_FadeOutHookGuid);
        return Guid.Empty;
    }

    private static void CBaseModelEntitySUB_FadeOutPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseModelEntity>(a1);

            var preCtx = new CBaseModelEntitySUB_FadeOutPreContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_FadeOutPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseModelEntitySUB_FadeOutPostContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntitySUB_FadeOutPost(ref postCtx);
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

    internal static void InvokeCBaseModelEntitySUB_FadeOut(nint a1)
    {
        CBaseModelEntitySUB_FadeOutGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseModelEntitySUB_FadeOutPre(ref CBaseModelEntitySUB_FadeOutPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_FadeOutPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseModelEntitySUB_FadeOutPost(ref CBaseModelEntitySUB_FadeOutPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntitySUB_FadeOutPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseModelEntitySUB_FadeOutHook : ICBaseModelEntitySUB_FadeOutHook
{
    private event OnCBaseModelEntitySUB_FadeOutPreDelegate? _Pre;
    private event OnCBaseModelEntitySUB_FadeOutPostDelegate? _Post;

    public event OnCBaseModelEntitySUB_FadeOutPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_FadeOut);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_FadeOut);
            }
        }
    }

    public event OnCBaseModelEntitySUB_FadeOutPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntitySUB_FadeOut);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_FadeOut);
            }
        }
    }

    public void InvokePre(ref CBaseModelEntitySUB_FadeOutPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseModelEntitySUB_FadeOutPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_FadeOut);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntitySUB_FadeOut);
        }
    }

    public void Invoke(CBaseModelEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseModelEntitySUB_FadeOut(schemaObject.Address);
}