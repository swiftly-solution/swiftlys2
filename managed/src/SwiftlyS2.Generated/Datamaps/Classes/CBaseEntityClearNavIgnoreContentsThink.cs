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
    private delegate void CBaseEntityClearNavIgnoreContentsThinkDelegate(nint a1);

    private static IUnmanagedFunction<CBaseEntityClearNavIgnoreContentsThinkDelegate>? CBaseEntityClearNavIgnoreContentsThinkUnmanagedFunction;
    private static Guid CBaseEntityClearNavIgnoreContentsThinkHookGuid;

    private static IUnmanagedFunction<CBaseEntityClearNavIgnoreContentsThinkDelegate> CBaseEntityClearNavIgnoreContentsThinkGetUnmanagedFunction()
    {
        if (CBaseEntityClearNavIgnoreContentsThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseEntity", "CBaseEntityClearNavIgnoreContentsThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseEntity::CBaseEntityClearNavIgnoreContentsThink.");
            }
            CBaseEntityClearNavIgnoreContentsThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseEntityClearNavIgnoreContentsThinkDelegate>(address);
        }
        return CBaseEntityClearNavIgnoreContentsThinkUnmanagedFunction;
    }

    internal static Guid HookCBaseEntityClearNavIgnoreContentsThink()
    {
        CBaseEntityClearNavIgnoreContentsThinkHookGuid = CBaseEntityClearNavIgnoreContentsThinkGetUnmanagedFunction().AddHook(next => (a1) => CBaseEntityClearNavIgnoreContentsThinkPipeline(a1, () => next()(a1)));
        return CBaseEntityClearNavIgnoreContentsThinkHookGuid;
    }

    internal static Guid UnhookCBaseEntityClearNavIgnoreContentsThink()
    {
        CBaseEntityClearNavIgnoreContentsThinkGetUnmanagedFunction().RemoveHook(CBaseEntityClearNavIgnoreContentsThinkHookGuid);
        return Guid.Empty;
    }

    private static void CBaseEntityClearNavIgnoreContentsThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseEntity>(a1);

            var preCtx = new CBaseEntityClearNavIgnoreContentsThinkPreContext { SchemaObject = schemaObject };
            InvokeCBaseEntityClearNavIgnoreContentsThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseEntityClearNavIgnoreContentsThinkPostContext { SchemaObject = schemaObject };
            InvokeCBaseEntityClearNavIgnoreContentsThinkPost(ref postCtx);
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

    internal static void InvokeCBaseEntityClearNavIgnoreContentsThink(nint a1)
    {
        CBaseEntityClearNavIgnoreContentsThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseEntityClearNavIgnoreContentsThinkPre(ref CBaseEntityClearNavIgnoreContentsThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseEntityClearNavIgnoreContentsThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseEntityClearNavIgnoreContentsThinkPost(ref CBaseEntityClearNavIgnoreContentsThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseEntityClearNavIgnoreContentsThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseEntityClearNavIgnoreContentsThinkHook : ICBaseEntityClearNavIgnoreContentsThinkHook
{
    private event OnCBaseEntityClearNavIgnoreContentsThinkPreDelegate? _Pre;
    private event OnCBaseEntityClearNavIgnoreContentsThinkPostDelegate? _Post;

    public event OnCBaseEntityClearNavIgnoreContentsThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseEntityClearNavIgnoreContentsThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntityClearNavIgnoreContentsThink);
            }
        }
    }

    public event OnCBaseEntityClearNavIgnoreContentsThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseEntityClearNavIgnoreContentsThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntityClearNavIgnoreContentsThink);
            }
        }
    }

    public void InvokePre(ref CBaseEntityClearNavIgnoreContentsThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseEntityClearNavIgnoreContentsThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntityClearNavIgnoreContentsThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntityClearNavIgnoreContentsThink);
        }
    }

    public void Invoke(CBaseEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseEntityClearNavIgnoreContentsThink(schemaObject.Address);
}