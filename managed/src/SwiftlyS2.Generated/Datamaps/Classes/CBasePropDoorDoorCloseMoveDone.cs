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
    private delegate void CBasePropDoorDoorCloseMoveDoneDelegate(nint a1);

    private static IUnmanagedFunction<CBasePropDoorDoorCloseMoveDoneDelegate>? CBasePropDoorDoorCloseMoveDoneUnmanagedFunction;
    private static Guid CBasePropDoorDoorCloseMoveDoneHookGuid;

    private static IUnmanagedFunction<CBasePropDoorDoorCloseMoveDoneDelegate> CBasePropDoorDoorCloseMoveDoneGetUnmanagedFunction()
    {
        if (CBasePropDoorDoorCloseMoveDoneUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBasePropDoor", "CBasePropDoorDoorCloseMoveDone");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBasePropDoor::CBasePropDoorDoorCloseMoveDone.");
            }
            CBasePropDoorDoorCloseMoveDoneUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBasePropDoorDoorCloseMoveDoneDelegate>(address);
        }
        return CBasePropDoorDoorCloseMoveDoneUnmanagedFunction;
    }

    internal static Guid HookCBasePropDoorDoorCloseMoveDone()
    {
        CBasePropDoorDoorCloseMoveDoneHookGuid = CBasePropDoorDoorCloseMoveDoneGetUnmanagedFunction().AddHook(next => (a1) => CBasePropDoorDoorCloseMoveDonePipeline(a1, () => next()(a1)));
        return CBasePropDoorDoorCloseMoveDoneHookGuid;
    }

    internal static Guid UnhookCBasePropDoorDoorCloseMoveDone()
    {
        CBasePropDoorDoorCloseMoveDoneGetUnmanagedFunction().RemoveHook(CBasePropDoorDoorCloseMoveDoneHookGuid);
        return Guid.Empty;
    }

    private static void CBasePropDoorDoorCloseMoveDonePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBasePropDoor>(a1);

            var preCtx = new CBasePropDoorDoorCloseMoveDonePreContext { SchemaObject = schemaObject };
            InvokeCBasePropDoorDoorCloseMoveDonePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBasePropDoorDoorCloseMoveDonePostContext { SchemaObject = schemaObject };
            InvokeCBasePropDoorDoorCloseMoveDonePost(ref postCtx);
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

    internal static void InvokeCBasePropDoorDoorCloseMoveDone(nint a1)
    {
        CBasePropDoorDoorCloseMoveDoneGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBasePropDoorDoorCloseMoveDonePre(ref CBasePropDoorDoorCloseMoveDonePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBasePropDoorDoorCloseMoveDonePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBasePropDoorDoorCloseMoveDonePost(ref CBasePropDoorDoorCloseMoveDonePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBasePropDoorDoorCloseMoveDonePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBasePropDoorDoorCloseMoveDoneHook : ICBasePropDoorDoorCloseMoveDoneHook
{
    private event OnCBasePropDoorDoorCloseMoveDonePreDelegate? _Pre;
    private event OnCBasePropDoorDoorCloseMoveDonePostDelegate? _Post;

    public event OnCBasePropDoorDoorCloseMoveDonePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBasePropDoorDoorCloseMoveDone);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDoorCloseMoveDone);
            }
        }
    }

    public event OnCBasePropDoorDoorCloseMoveDonePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBasePropDoorDoorCloseMoveDone);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDoorCloseMoveDone);
            }
        }
    }

    public void InvokePre(ref CBasePropDoorDoorCloseMoveDonePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBasePropDoorDoorCloseMoveDonePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDoorCloseMoveDone);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDoorCloseMoveDone);
        }
    }

    public void Invoke(CBasePropDoor schemaObject) => DatamapHooksPublisher.InvokeCBasePropDoorDoorCloseMoveDone(schemaObject.Address);
}