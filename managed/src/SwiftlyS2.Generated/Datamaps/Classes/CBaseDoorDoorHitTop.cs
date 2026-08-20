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
    private delegate void CBaseDoorDoorHitTopDelegate(nint a1);

    private static IUnmanagedFunction<CBaseDoorDoorHitTopDelegate>? CBaseDoorDoorHitTopUnmanagedFunction;
    private static Guid CBaseDoorDoorHitTopHookGuid;

    private static IUnmanagedFunction<CBaseDoorDoorHitTopDelegate> CBaseDoorDoorHitTopGetUnmanagedFunction()
    {
        if (CBaseDoorDoorHitTopUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseDoor", "CBaseDoorDoorHitTop");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseDoor::CBaseDoorDoorHitTop.");
            }
            CBaseDoorDoorHitTopUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseDoorDoorHitTopDelegate>(address);
        }
        return CBaseDoorDoorHitTopUnmanagedFunction;
    }

    internal static Guid HookCBaseDoorDoorHitTop()
    {
        CBaseDoorDoorHitTopHookGuid = CBaseDoorDoorHitTopGetUnmanagedFunction().AddHook(next => (a1) => CBaseDoorDoorHitTopPipeline(a1, () => next()(a1)));
        return CBaseDoorDoorHitTopHookGuid;
    }

    internal static Guid UnhookCBaseDoorDoorHitTop()
    {
        CBaseDoorDoorHitTopGetUnmanagedFunction().RemoveHook(CBaseDoorDoorHitTopHookGuid);
        return Guid.Empty;
    }

    private static void CBaseDoorDoorHitTopPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseDoor>(a1);

            var preCtx = new CBaseDoorDoorHitTopPreContext { SchemaObject = schemaObject };
            InvokeCBaseDoorDoorHitTopPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseDoorDoorHitTopPostContext { SchemaObject = schemaObject };
            InvokeCBaseDoorDoorHitTopPost(ref postCtx);
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

    internal static void InvokeCBaseDoorDoorHitTop(nint a1)
    {
        CBaseDoorDoorHitTopGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseDoorDoorHitTopPre(ref CBaseDoorDoorHitTopPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseDoorDoorHitTopPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseDoorDoorHitTopPost(ref CBaseDoorDoorHitTopPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseDoorDoorHitTopPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseDoorDoorHitTopHook : ICBaseDoorDoorHitTopHook
{
    private event OnCBaseDoorDoorHitTopPreDelegate? _Pre;
    private event OnCBaseDoorDoorHitTopPostDelegate? _Post;

    public event OnCBaseDoorDoorHitTopPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorDoorHitTop);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorHitTop);
            }
        }
    }

    public event OnCBaseDoorDoorHitTopPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorDoorHitTop);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorHitTop);
            }
        }
    }

    public void InvokePre(ref CBaseDoorDoorHitTopPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseDoorDoorHitTopPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorHitTop);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorHitTop);
        }
    }

    public void Invoke(CBaseDoor schemaObject) => DatamapHooksPublisher.InvokeCBaseDoorDoorHitTop(schemaObject.Address);
}