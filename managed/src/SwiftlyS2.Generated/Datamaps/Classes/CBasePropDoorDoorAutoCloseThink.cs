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
    private delegate void CBasePropDoorDoorAutoCloseThinkDelegate(nint a1);

    private static IUnmanagedFunction<CBasePropDoorDoorAutoCloseThinkDelegate>? CBasePropDoorDoorAutoCloseThinkUnmanagedFunction;
    private static Guid CBasePropDoorDoorAutoCloseThinkHookGuid;

    private static IUnmanagedFunction<CBasePropDoorDoorAutoCloseThinkDelegate> CBasePropDoorDoorAutoCloseThinkGetUnmanagedFunction()
    {
        if (CBasePropDoorDoorAutoCloseThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBasePropDoor", "CBasePropDoorDoorAutoCloseThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBasePropDoor::CBasePropDoorDoorAutoCloseThink.");
            }
            CBasePropDoorDoorAutoCloseThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBasePropDoorDoorAutoCloseThinkDelegate>(address);
        }
        return CBasePropDoorDoorAutoCloseThinkUnmanagedFunction;
    }

    internal static Guid HookCBasePropDoorDoorAutoCloseThink()
    {
        CBasePropDoorDoorAutoCloseThinkHookGuid = CBasePropDoorDoorAutoCloseThinkGetUnmanagedFunction().AddHook(next => (a1) => CBasePropDoorDoorAutoCloseThinkPipeline(a1, () => next()(a1)));
        return CBasePropDoorDoorAutoCloseThinkHookGuid;
    }

    internal static Guid UnhookCBasePropDoorDoorAutoCloseThink()
    {
        CBasePropDoorDoorAutoCloseThinkGetUnmanagedFunction().RemoveHook(CBasePropDoorDoorAutoCloseThinkHookGuid);
        return Guid.Empty;
    }

    private static void CBasePropDoorDoorAutoCloseThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBasePropDoor>(a1);

            var preCtx = new CBasePropDoorDoorAutoCloseThinkPreContext { SchemaObject = schemaObject };
            InvokeCBasePropDoorDoorAutoCloseThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBasePropDoorDoorAutoCloseThinkPostContext { SchemaObject = schemaObject };
            InvokeCBasePropDoorDoorAutoCloseThinkPost(ref postCtx);
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

    internal static void InvokeCBasePropDoorDoorAutoCloseThink(nint a1)
    {
        CBasePropDoorDoorAutoCloseThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBasePropDoorDoorAutoCloseThinkPre(ref CBasePropDoorDoorAutoCloseThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBasePropDoorDoorAutoCloseThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBasePropDoorDoorAutoCloseThinkPost(ref CBasePropDoorDoorAutoCloseThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBasePropDoorDoorAutoCloseThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBasePropDoorDoorAutoCloseThinkHook : ICBasePropDoorDoorAutoCloseThinkHook
{
    private event OnCBasePropDoorDoorAutoCloseThinkPreDelegate? _Pre;
    private event OnCBasePropDoorDoorAutoCloseThinkPostDelegate? _Post;

    public event OnCBasePropDoorDoorAutoCloseThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBasePropDoorDoorAutoCloseThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDoorAutoCloseThink);
            }
        }
    }

    public event OnCBasePropDoorDoorAutoCloseThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBasePropDoorDoorAutoCloseThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDoorAutoCloseThink);
            }
        }
    }

    public void InvokePre(ref CBasePropDoorDoorAutoCloseThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBasePropDoorDoorAutoCloseThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDoorAutoCloseThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDoorAutoCloseThink);
        }
    }

    public void Invoke(CBasePropDoor schemaObject) => DatamapHooksPublisher.InvokeCBasePropDoorDoorAutoCloseThink(schemaObject.Address);
}