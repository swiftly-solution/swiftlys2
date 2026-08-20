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
    private delegate void CBaseEntitySUB_KillSelfDelegate(nint a1);

    private static IUnmanagedFunction<CBaseEntitySUB_KillSelfDelegate>? CBaseEntitySUB_KillSelfUnmanagedFunction;
    private static Guid CBaseEntitySUB_KillSelfHookGuid;

    private static IUnmanagedFunction<CBaseEntitySUB_KillSelfDelegate> CBaseEntitySUB_KillSelfGetUnmanagedFunction()
    {
        if (CBaseEntitySUB_KillSelfUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseEntity", "CBaseEntitySUB_KillSelf");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseEntity::CBaseEntitySUB_KillSelf.");
            }
            CBaseEntitySUB_KillSelfUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseEntitySUB_KillSelfDelegate>(address);
        }
        return CBaseEntitySUB_KillSelfUnmanagedFunction;
    }

    internal static Guid HookCBaseEntitySUB_KillSelf()
    {
        CBaseEntitySUB_KillSelfHookGuid = CBaseEntitySUB_KillSelfGetUnmanagedFunction().AddHook(next => (a1) => CBaseEntitySUB_KillSelfPipeline(a1, () => next()(a1)));
        return CBaseEntitySUB_KillSelfHookGuid;
    }

    internal static Guid UnhookCBaseEntitySUB_KillSelf()
    {
        CBaseEntitySUB_KillSelfGetUnmanagedFunction().RemoveHook(CBaseEntitySUB_KillSelfHookGuid);
        return Guid.Empty;
    }

    private static void CBaseEntitySUB_KillSelfPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseEntity>(a1);

            var preCtx = new CBaseEntitySUB_KillSelfPreContext { SchemaObject = schemaObject };
            InvokeCBaseEntitySUB_KillSelfPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseEntitySUB_KillSelfPostContext { SchemaObject = schemaObject };
            InvokeCBaseEntitySUB_KillSelfPost(ref postCtx);
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

    internal static void InvokeCBaseEntitySUB_KillSelf(nint a1)
    {
        CBaseEntitySUB_KillSelfGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseEntitySUB_KillSelfPre(ref CBaseEntitySUB_KillSelfPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseEntitySUB_KillSelfPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseEntitySUB_KillSelfPost(ref CBaseEntitySUB_KillSelfPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseEntitySUB_KillSelfPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseEntitySUB_KillSelfHook : ICBaseEntitySUB_KillSelfHook
{
    private event OnCBaseEntitySUB_KillSelfPreDelegate? _Pre;
    private event OnCBaseEntitySUB_KillSelfPostDelegate? _Post;

    public event OnCBaseEntitySUB_KillSelfPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseEntitySUB_KillSelf);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_KillSelf);
            }
        }
    }

    public event OnCBaseEntitySUB_KillSelfPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseEntitySUB_KillSelf);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_KillSelf);
            }
        }
    }

    public void InvokePre(ref CBaseEntitySUB_KillSelfPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseEntitySUB_KillSelfPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_KillSelf);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_KillSelf);
        }
    }

    public void Invoke(CBaseEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseEntitySUB_KillSelf(schemaObject.Address);
}