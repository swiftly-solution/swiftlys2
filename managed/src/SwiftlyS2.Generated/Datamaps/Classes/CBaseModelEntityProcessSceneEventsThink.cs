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
    private delegate void CBaseModelEntityProcessSceneEventsThinkDelegate(nint a1);

    private static IUnmanagedFunction<CBaseModelEntityProcessSceneEventsThinkDelegate>? CBaseModelEntityProcessSceneEventsThinkUnmanagedFunction;
    private static Guid CBaseModelEntityProcessSceneEventsThinkHookGuid;

    private static IUnmanagedFunction<CBaseModelEntityProcessSceneEventsThinkDelegate> CBaseModelEntityProcessSceneEventsThinkGetUnmanagedFunction()
    {
        if (CBaseModelEntityProcessSceneEventsThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseModelEntity", "CBaseModelEntityProcessSceneEventsThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseModelEntity::CBaseModelEntityProcessSceneEventsThink.");
            }
            CBaseModelEntityProcessSceneEventsThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseModelEntityProcessSceneEventsThinkDelegate>(address);
        }
        return CBaseModelEntityProcessSceneEventsThinkUnmanagedFunction;
    }

    internal static Guid HookCBaseModelEntityProcessSceneEventsThink()
    {
        CBaseModelEntityProcessSceneEventsThinkHookGuid = CBaseModelEntityProcessSceneEventsThinkGetUnmanagedFunction().AddHook(next => (a1) => CBaseModelEntityProcessSceneEventsThinkPipeline(a1, () => next()(a1)));
        return CBaseModelEntityProcessSceneEventsThinkHookGuid;
    }

    internal static Guid UnhookCBaseModelEntityProcessSceneEventsThink()
    {
        CBaseModelEntityProcessSceneEventsThinkGetUnmanagedFunction().RemoveHook(CBaseModelEntityProcessSceneEventsThinkHookGuid);
        return Guid.Empty;
    }

    private static void CBaseModelEntityProcessSceneEventsThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseModelEntity>(a1);

            var preCtx = new CBaseModelEntityProcessSceneEventsThinkPreContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntityProcessSceneEventsThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseModelEntityProcessSceneEventsThinkPostContext { SchemaObject = schemaObject };
            InvokeCBaseModelEntityProcessSceneEventsThinkPost(ref postCtx);
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

    internal static void InvokeCBaseModelEntityProcessSceneEventsThink(nint a1)
    {
        CBaseModelEntityProcessSceneEventsThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseModelEntityProcessSceneEventsThinkPre(ref CBaseModelEntityProcessSceneEventsThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntityProcessSceneEventsThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseModelEntityProcessSceneEventsThinkPost(ref CBaseModelEntityProcessSceneEventsThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseModelEntityProcessSceneEventsThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseModelEntityProcessSceneEventsThinkHook : ICBaseModelEntityProcessSceneEventsThinkHook
{
    private event OnCBaseModelEntityProcessSceneEventsThinkPreDelegate? _Pre;
    private event OnCBaseModelEntityProcessSceneEventsThinkPostDelegate? _Post;

    public event OnCBaseModelEntityProcessSceneEventsThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntityProcessSceneEventsThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntityProcessSceneEventsThink);
            }
        }
    }

    public event OnCBaseModelEntityProcessSceneEventsThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseModelEntityProcessSceneEventsThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntityProcessSceneEventsThink);
            }
        }
    }

    public void InvokePre(ref CBaseModelEntityProcessSceneEventsThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseModelEntityProcessSceneEventsThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntityProcessSceneEventsThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseModelEntityProcessSceneEventsThink);
        }
    }

    public void Invoke(CBaseModelEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseModelEntityProcessSceneEventsThink(schemaObject.Address);
}