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
    private delegate void CBasePropDoorDoorOpenMoveDoneDelegate(nint a1);

    private static IUnmanagedFunction<CBasePropDoorDoorOpenMoveDoneDelegate>? CBasePropDoorDoorOpenMoveDoneUnmanagedFunction;
    private static Guid CBasePropDoorDoorOpenMoveDoneHookGuid;

    private static IUnmanagedFunction<CBasePropDoorDoorOpenMoveDoneDelegate> CBasePropDoorDoorOpenMoveDoneGetUnmanagedFunction()
    {
        if (CBasePropDoorDoorOpenMoveDoneUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBasePropDoor", "CBasePropDoorDoorOpenMoveDone");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBasePropDoor::CBasePropDoorDoorOpenMoveDone.");
            }
            CBasePropDoorDoorOpenMoveDoneUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBasePropDoorDoorOpenMoveDoneDelegate>(address);
        }
        return CBasePropDoorDoorOpenMoveDoneUnmanagedFunction;
    }

    internal static Guid HookCBasePropDoorDoorOpenMoveDone()
    {
        CBasePropDoorDoorOpenMoveDoneHookGuid = CBasePropDoorDoorOpenMoveDoneGetUnmanagedFunction().AddHook(next => (a1) => CBasePropDoorDoorOpenMoveDonePipeline(a1, () => next()(a1)));
        return CBasePropDoorDoorOpenMoveDoneHookGuid;
    }

    internal static Guid UnhookCBasePropDoorDoorOpenMoveDone()
    {
        CBasePropDoorDoorOpenMoveDoneGetUnmanagedFunction().RemoveHook(CBasePropDoorDoorOpenMoveDoneHookGuid);
        return Guid.Empty;
    }

    private static void CBasePropDoorDoorOpenMoveDonePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBasePropDoor>(a1);

            var preCtx = new CBasePropDoorDoorOpenMoveDonePreContext { SchemaObject = schemaObject };
            InvokeCBasePropDoorDoorOpenMoveDonePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBasePropDoorDoorOpenMoveDonePostContext { SchemaObject = schemaObject };
            InvokeCBasePropDoorDoorOpenMoveDonePost(ref postCtx);
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

    internal static void InvokeCBasePropDoorDoorOpenMoveDone(nint a1)
    {
        CBasePropDoorDoorOpenMoveDoneGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBasePropDoorDoorOpenMoveDonePre(ref CBasePropDoorDoorOpenMoveDonePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBasePropDoorDoorOpenMoveDonePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBasePropDoorDoorOpenMoveDonePost(ref CBasePropDoorDoorOpenMoveDonePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBasePropDoorDoorOpenMoveDonePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBasePropDoorDoorOpenMoveDoneHook : ICBasePropDoorDoorOpenMoveDoneHook
{
    private event OnCBasePropDoorDoorOpenMoveDonePreDelegate? _Pre;
    private event OnCBasePropDoorDoorOpenMoveDonePostDelegate? _Post;

    public event OnCBasePropDoorDoorOpenMoveDonePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBasePropDoorDoorOpenMoveDone);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDoorOpenMoveDone);
            }
        }
    }

    public event OnCBasePropDoorDoorOpenMoveDonePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBasePropDoorDoorOpenMoveDone);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDoorOpenMoveDone);
            }
        }
    }

    public void InvokePre(ref CBasePropDoorDoorOpenMoveDonePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBasePropDoorDoorOpenMoveDonePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDoorOpenMoveDone);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDoorOpenMoveDone);
        }
    }

    public void Invoke(CBasePropDoor schemaObject) => DatamapHooksPublisher.InvokeCBasePropDoorDoorOpenMoveDone(schemaObject.Address);
}