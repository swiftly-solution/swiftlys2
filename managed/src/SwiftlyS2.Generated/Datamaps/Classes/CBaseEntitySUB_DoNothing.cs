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
    private delegate void CBaseEntitySUB_DoNothingDelegate(nint a1);

    private static IUnmanagedFunction<CBaseEntitySUB_DoNothingDelegate>? CBaseEntitySUB_DoNothingUnmanagedFunction;
    private static Guid CBaseEntitySUB_DoNothingHookGuid;

    private static IUnmanagedFunction<CBaseEntitySUB_DoNothingDelegate> CBaseEntitySUB_DoNothingGetUnmanagedFunction()
    {
        if (CBaseEntitySUB_DoNothingUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseEntity", "CBaseEntitySUB_DoNothing");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseEntity::CBaseEntitySUB_DoNothing.");
            }
            CBaseEntitySUB_DoNothingUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseEntitySUB_DoNothingDelegate>(address);
        }
        return CBaseEntitySUB_DoNothingUnmanagedFunction;
    }

    internal static Guid HookCBaseEntitySUB_DoNothing()
    {
        CBaseEntitySUB_DoNothingHookGuid = CBaseEntitySUB_DoNothingGetUnmanagedFunction().AddHook(next => (a1) => CBaseEntitySUB_DoNothingPipeline(a1, () => next()(a1)));
        return CBaseEntitySUB_DoNothingHookGuid;
    }

    internal static Guid UnhookCBaseEntitySUB_DoNothing()
    {
        CBaseEntitySUB_DoNothingGetUnmanagedFunction().RemoveHook(CBaseEntitySUB_DoNothingHookGuid);
        return Guid.Empty;
    }

    private static void CBaseEntitySUB_DoNothingPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseEntity>(a1);

            var preCtx = new CBaseEntitySUB_DoNothingPreContext { SchemaObject = schemaObject };
            InvokeCBaseEntitySUB_DoNothingPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseEntitySUB_DoNothingPostContext { SchemaObject = schemaObject };
            InvokeCBaseEntitySUB_DoNothingPost(ref postCtx);
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

    internal static void InvokeCBaseEntitySUB_DoNothing(nint a1)
    {
        CBaseEntitySUB_DoNothingGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseEntitySUB_DoNothingPre(ref CBaseEntitySUB_DoNothingPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseEntitySUB_DoNothingPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseEntitySUB_DoNothingPost(ref CBaseEntitySUB_DoNothingPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseEntitySUB_DoNothingPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseEntitySUB_DoNothingHook : ICBaseEntitySUB_DoNothingHook
{
    private event OnCBaseEntitySUB_DoNothingPreDelegate? _Pre;
    private event OnCBaseEntitySUB_DoNothingPostDelegate? _Post;

    public event OnCBaseEntitySUB_DoNothingPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseEntitySUB_DoNothing);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_DoNothing);
            }
        }
    }

    public event OnCBaseEntitySUB_DoNothingPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseEntitySUB_DoNothing);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_DoNothing);
            }
        }
    }

    public void InvokePre(ref CBaseEntitySUB_DoNothingPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseEntitySUB_DoNothingPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_DoNothing);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_DoNothing);
        }
    }

    public void Invoke(CBaseEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseEntitySUB_DoNothing(schemaObject.Address);
}