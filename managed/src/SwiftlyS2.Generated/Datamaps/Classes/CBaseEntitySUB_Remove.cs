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
    private delegate void CBaseEntitySUB_RemoveDelegate(nint a1);

    private static IUnmanagedFunction<CBaseEntitySUB_RemoveDelegate>? CBaseEntitySUB_RemoveUnmanagedFunction;
    private static Guid CBaseEntitySUB_RemoveHookGuid;

    private static IUnmanagedFunction<CBaseEntitySUB_RemoveDelegate> CBaseEntitySUB_RemoveGetUnmanagedFunction()
    {
        if (CBaseEntitySUB_RemoveUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseEntity", "CBaseEntitySUB_Remove");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseEntity::CBaseEntitySUB_Remove.");
            }
            CBaseEntitySUB_RemoveUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseEntitySUB_RemoveDelegate>(address);
        }
        return CBaseEntitySUB_RemoveUnmanagedFunction;
    }

    internal static Guid HookCBaseEntitySUB_Remove()
    {
        CBaseEntitySUB_RemoveHookGuid = CBaseEntitySUB_RemoveGetUnmanagedFunction().AddHook(next => (a1) => CBaseEntitySUB_RemovePipeline(a1, () => next()(a1)));
        return CBaseEntitySUB_RemoveHookGuid;
    }

    internal static Guid UnhookCBaseEntitySUB_Remove()
    {
        CBaseEntitySUB_RemoveGetUnmanagedFunction().RemoveHook(CBaseEntitySUB_RemoveHookGuid);
        return Guid.Empty;
    }

    private static void CBaseEntitySUB_RemovePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseEntity>(a1);

            var preCtx = new CBaseEntitySUB_RemovePreContext { SchemaObject = schemaObject };
            InvokeCBaseEntitySUB_RemovePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseEntitySUB_RemovePostContext { SchemaObject = schemaObject };
            InvokeCBaseEntitySUB_RemovePost(ref postCtx);
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

    internal static void InvokeCBaseEntitySUB_Remove(nint a1)
    {
        CBaseEntitySUB_RemoveGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseEntitySUB_RemovePre(ref CBaseEntitySUB_RemovePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseEntitySUB_RemovePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseEntitySUB_RemovePost(ref CBaseEntitySUB_RemovePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseEntitySUB_RemovePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseEntitySUB_RemoveHook : ICBaseEntitySUB_RemoveHook
{
    private event OnCBaseEntitySUB_RemovePreDelegate? _Pre;
    private event OnCBaseEntitySUB_RemovePostDelegate? _Post;

    public event OnCBaseEntitySUB_RemovePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseEntitySUB_Remove);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_Remove);
            }
        }
    }

    public event OnCBaseEntitySUB_RemovePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseEntitySUB_Remove);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_Remove);
            }
        }
    }

    public void InvokePre(ref CBaseEntitySUB_RemovePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseEntitySUB_RemovePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_Remove);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseEntitySUB_Remove);
        }
    }

    public void Invoke(CBaseEntity schemaObject) => DatamapHooksPublisher.InvokeCBaseEntitySUB_Remove(schemaObject.Address);
}