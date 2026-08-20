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
    private delegate void CBaseDoorDoorTouchDelegate(nint a1);

    private static IUnmanagedFunction<CBaseDoorDoorTouchDelegate>? CBaseDoorDoorTouchUnmanagedFunction;
    private static Guid CBaseDoorDoorTouchHookGuid;

    private static IUnmanagedFunction<CBaseDoorDoorTouchDelegate> CBaseDoorDoorTouchGetUnmanagedFunction()
    {
        if (CBaseDoorDoorTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseDoor", "CBaseDoorDoorTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseDoor::CBaseDoorDoorTouch.");
            }
            CBaseDoorDoorTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseDoorDoorTouchDelegate>(address);
        }
        return CBaseDoorDoorTouchUnmanagedFunction;
    }

    internal static Guid HookCBaseDoorDoorTouch()
    {
        CBaseDoorDoorTouchHookGuid = CBaseDoorDoorTouchGetUnmanagedFunction().AddHook(next => (a1) => CBaseDoorDoorTouchPipeline(a1, () => next()(a1)));
        return CBaseDoorDoorTouchHookGuid;
    }

    internal static Guid UnhookCBaseDoorDoorTouch()
    {
        CBaseDoorDoorTouchGetUnmanagedFunction().RemoveHook(CBaseDoorDoorTouchHookGuid);
        return Guid.Empty;
    }

    private static void CBaseDoorDoorTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseDoor>(a1);

            var preCtx = new CBaseDoorDoorTouchPreContext { SchemaObject = schemaObject };
            InvokeCBaseDoorDoorTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseDoorDoorTouchPostContext { SchemaObject = schemaObject };
            InvokeCBaseDoorDoorTouchPost(ref postCtx);
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

    internal static void InvokeCBaseDoorDoorTouch(nint a1)
    {
        CBaseDoorDoorTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseDoorDoorTouchPre(ref CBaseDoorDoorTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseDoorDoorTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseDoorDoorTouchPost(ref CBaseDoorDoorTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseDoorDoorTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseDoorDoorTouchHook : ICBaseDoorDoorTouchHook
{
    private event OnCBaseDoorDoorTouchPreDelegate? _Pre;
    private event OnCBaseDoorDoorTouchPostDelegate? _Post;

    public event OnCBaseDoorDoorTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorDoorTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorTouch);
            }
        }
    }

    public event OnCBaseDoorDoorTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseDoorDoorTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorTouch);
            }
        }
    }

    public void InvokePre(ref CBaseDoorDoorTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseDoorDoorTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseDoorDoorTouch);
        }
    }

    public void Invoke(CBaseDoor schemaObject) => DatamapHooksPublisher.InvokeCBaseDoorDoorTouch(schemaObject.Address);
}