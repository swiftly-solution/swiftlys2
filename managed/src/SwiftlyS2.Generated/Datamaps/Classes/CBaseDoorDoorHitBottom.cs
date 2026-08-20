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
    private delegate void CBaseDoorDoorHitBottomDelegate(nint a1);

    private static IUnmanagedFunction<CBaseDoorDoorHitBottomDelegate>? CBaseDoorDoorHitBottomUnmanagedFunction;
    private static Guid CBaseDoorDoorHitBottomHookGuid;

    private static IUnmanagedFunction<CBaseDoorDoorHitBottomDelegate> CBaseDoorDoorHitBottomGetUnmanagedFunction()
    {
        if (CBaseDoorDoorHitBottomUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseDoor", "CBaseDoorDoorHitBottom");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseDoor::CBaseDoorDoorHitBottom.");
            }
            CBaseDoorDoorHitBottomUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseDoorDoorHitBottomDelegate>(address);
        }
        return CBaseDoorDoorHitBottomUnmanagedFunction;
    }

    internal static Guid HookCBaseDoorDoorHitBottom()
    {
        CBaseDoorDoorHitBottomHookGuid = CBaseDoorDoorHitBottomGetUnmanagedFunction().AddHook(next => (a1) => CBaseDoorDoorHitBottomPipeline(a1, () => next()(a1)));
        return CBaseDoorDoorHitBottomHookGuid;
    }

    internal static Guid UnhookCBaseDoorDoorHitBottom()
    {
        CBaseDoorDoorHitBottomGetUnmanagedFunction().RemoveHook(CBaseDoorDoorHitBottomHookGuid);
        return Guid.Empty;
    }

    private static void CBaseDoorDoorHitBottomPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseDoor>(a1);

            var preCtx = new CBaseDoorDoorHitBottomPreContext { SchemaObject = schemaObject };
            InvokeCBaseDoorDoorHitBottomPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseDoorDoorHitBottomPostContext { SchemaObject = schemaObject };
            InvokeCBaseDoorDoorHitBottomPost(ref postCtx);
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

    internal static void InvokeCBaseDoorDoorHitBottom(nint a1)
    {
        CBaseDoorDoorHitBottomGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseDoorDoorHitBottomPre(ref CBaseDoorDoorHitBottomPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseDoorDoorHitBottomPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseDoorDoorHitBottomPost(ref CBaseDoorDoorHitBottomPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseDoorDoorHitBottomPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseDoorDoorHitBottomHook : ICBaseDoorDoorHitBottomHook
{
    private event OnCBaseDoorDoorHitBottomPreDelegate? _Pre;
    private event OnCBaseDoorDoorHitBottomPostDelegate? _Post;

    public event OnCBaseDoorDoorHitBottomPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorDoorHitBottom);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorHitBottom);
            }
        }
    }

    public event OnCBaseDoorDoorHitBottomPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorDoorHitBottom);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorHitBottom);
            }
        }
    }

    public void InvokePre(ref CBaseDoorDoorHitBottomPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseDoorDoorHitBottomPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorHitBottom);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorHitBottom);
        }
    }

    public void Invoke(CBaseDoor schemaObject) => DatamapHooksPublisher.InvokeCBaseDoorDoorHitBottom(schemaObject.Address);
}