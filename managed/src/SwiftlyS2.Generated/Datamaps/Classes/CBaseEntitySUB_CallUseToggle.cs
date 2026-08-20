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
    private delegate void CBaseEntitySUB_CallUseToggleDelegate(nint a1);

    private static IUnmanagedFunction<CBaseEntitySUB_CallUseToggleDelegate>? CBaseEntitySUB_CallUseToggleUnmanagedFunction;
    private static Guid CBaseEntitySUB_CallUseToggleHookGuid;

    private static IUnmanagedFunction<CBaseEntitySUB_CallUseToggleDelegate> CBaseEntitySUB_CallUseToggleGetUnmanagedFunction()
    {
        if (CBaseEntitySUB_CallUseToggleUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseEntity", "CBaseEntitySUB_CallUseToggle");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseEntity::CBaseEntitySUB_CallUseToggle.");
            }
            CBaseEntitySUB_CallUseToggleUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseEntitySUB_CallUseToggleDelegate>(address);
        }
        return CBaseEntitySUB_CallUseToggleUnmanagedFunction;
    }

    internal static Guid HookCBaseEntitySUB_CallUseToggle()
    {
        CBaseEntitySUB_CallUseToggleHookGuid = CBaseEntitySUB_CallUseToggleGetUnmanagedFunction().AddHook(next => (a1) => CBaseEntitySUB_CallUseTogglePipeline(a1, () => next()(a1)));
        return CBaseEntitySUB_CallUseToggleHookGuid;
    }

    internal static Guid UnhookCBaseEntitySUB_CallUseToggle()
    {
        CBaseEntitySUB_CallUseToggleGetUnmanagedFunction().RemoveHook(CBaseEntitySUB_CallUseToggleHookGuid);
        return Guid.Empty;
    }

    private static void CBaseEntitySUB_CallUseTogglePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseEntity>(a1);

            var preCtx = new CBaseEntitySUB_CallUseTogglePreContext { SchemaObject = schemaObject };
            InvokeCBaseEntitySUB_CallUseTogglePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseEntitySUB_CallUseTogglePostContext { SchemaObject = schemaObject };
            InvokeCBaseEntitySUB_CallUseTogglePost(ref postCtx);
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

    internal static void InvokeCBaseEntitySUB_CallUseToggle(nint a1)
    {
        CBaseEntitySUB_CallUseToggleGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseEntitySUB_CallUseTogglePre(ref CBaseEntitySUB_CallUseTogglePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseEntitySUB_CallUseTogglePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseEntitySUB_CallUseTogglePost(ref CBaseEntitySUB_CallUseTogglePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseEntitySUB_CallUseTogglePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseEntitySUB_CallUseToggleHook : ICBaseEntitySUB_CallUseToggleHook
{
    private event OnCBaseEntitySUB_CallUseTogglePreDelegate? _Pre;
    private event OnCBaseEntitySUB_CallUseTogglePostDelegate? _Post;

    public event OnCBaseEntitySUB_CallUseTogglePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseEntitySUB_CallUseToggle);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_CallUseToggle);
            }
        }
    }

    public event OnCBaseEntitySUB_CallUseTogglePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseEntitySUB_CallUseToggle);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_CallUseToggle);
            }
        }
    }

    public void InvokePre(ref CBaseEntitySUB_CallUseTogglePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseEntitySUB_CallUseTogglePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_CallUseToggle);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_CallUseToggle);
        }
    }

    public void Invoke(CBaseEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseEntitySUB_CallUseToggle(schemaObject.Address);
}