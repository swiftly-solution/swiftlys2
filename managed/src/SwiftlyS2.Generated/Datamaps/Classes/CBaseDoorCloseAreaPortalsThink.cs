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
    private delegate void CBaseDoorCloseAreaPortalsThinkDelegate(nint a1);

    private static IUnmanagedFunction<CBaseDoorCloseAreaPortalsThinkDelegate>? CBaseDoorCloseAreaPortalsThinkUnmanagedFunction;
    private static Guid CBaseDoorCloseAreaPortalsThinkHookGuid;

    private static IUnmanagedFunction<CBaseDoorCloseAreaPortalsThinkDelegate> CBaseDoorCloseAreaPortalsThinkGetUnmanagedFunction()
    {
        if (CBaseDoorCloseAreaPortalsThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseDoor", "CBaseDoorCloseAreaPortalsThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseDoor::CBaseDoorCloseAreaPortalsThink.");
            }
            CBaseDoorCloseAreaPortalsThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseDoorCloseAreaPortalsThinkDelegate>(address);
        }
        return CBaseDoorCloseAreaPortalsThinkUnmanagedFunction;
    }

    internal static Guid HookCBaseDoorCloseAreaPortalsThink()
    {
        CBaseDoorCloseAreaPortalsThinkHookGuid = CBaseDoorCloseAreaPortalsThinkGetUnmanagedFunction().AddHook(next => (a1) => CBaseDoorCloseAreaPortalsThinkPipeline(a1, () => next()(a1)));
        return CBaseDoorCloseAreaPortalsThinkHookGuid;
    }

    internal static Guid UnhookCBaseDoorCloseAreaPortalsThink()
    {
        CBaseDoorCloseAreaPortalsThinkGetUnmanagedFunction().RemoveHook(CBaseDoorCloseAreaPortalsThinkHookGuid);
        return Guid.Empty;
    }

    private static void CBaseDoorCloseAreaPortalsThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseDoor>(a1);

            var preCtx = new CBaseDoorCloseAreaPortalsThinkPreContext { SchemaObject = schemaObject };
            InvokeCBaseDoorCloseAreaPortalsThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseDoorCloseAreaPortalsThinkPostContext { SchemaObject = schemaObject };
            InvokeCBaseDoorCloseAreaPortalsThinkPost(ref postCtx);
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

    internal static void InvokeCBaseDoorCloseAreaPortalsThink(nint a1)
    {
        CBaseDoorCloseAreaPortalsThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseDoorCloseAreaPortalsThinkPre(ref CBaseDoorCloseAreaPortalsThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseDoorCloseAreaPortalsThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseDoorCloseAreaPortalsThinkPost(ref CBaseDoorCloseAreaPortalsThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseDoorCloseAreaPortalsThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseDoorCloseAreaPortalsThinkHook : ICBaseDoorCloseAreaPortalsThinkHook
{
    private event OnCBaseDoorCloseAreaPortalsThinkPreDelegate? _Pre;
    private event OnCBaseDoorCloseAreaPortalsThinkPostDelegate? _Post;

    public event OnCBaseDoorCloseAreaPortalsThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorCloseAreaPortalsThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorCloseAreaPortalsThink);
            }
        }
    }

    public event OnCBaseDoorCloseAreaPortalsThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorCloseAreaPortalsThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorCloseAreaPortalsThink);
            }
        }
    }

    public void InvokePre(ref CBaseDoorCloseAreaPortalsThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseDoorCloseAreaPortalsThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorCloseAreaPortalsThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorCloseAreaPortalsThink);
        }
    }

    public void Invoke(CBaseDoor schemaObject) => DatamapHooksPublisher.InvokeCBaseDoorCloseAreaPortalsThink(schemaObject.Address);
}