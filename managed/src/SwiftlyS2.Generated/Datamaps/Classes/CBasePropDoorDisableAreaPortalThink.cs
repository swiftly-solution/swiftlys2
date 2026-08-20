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
    private delegate void CBasePropDoorDisableAreaPortalThinkDelegate(nint a1);

    private static IUnmanagedFunction<CBasePropDoorDisableAreaPortalThinkDelegate>? CBasePropDoorDisableAreaPortalThinkUnmanagedFunction;
    private static Guid CBasePropDoorDisableAreaPortalThinkHookGuid;

    private static IUnmanagedFunction<CBasePropDoorDisableAreaPortalThinkDelegate> CBasePropDoorDisableAreaPortalThinkGetUnmanagedFunction()
    {
        if (CBasePropDoorDisableAreaPortalThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBasePropDoor", "CBasePropDoorDisableAreaPortalThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBasePropDoor::CBasePropDoorDisableAreaPortalThink.");
            }
            CBasePropDoorDisableAreaPortalThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBasePropDoorDisableAreaPortalThinkDelegate>(address);
        }
        return CBasePropDoorDisableAreaPortalThinkUnmanagedFunction;
    }

    internal static Guid HookCBasePropDoorDisableAreaPortalThink()
    {
        CBasePropDoorDisableAreaPortalThinkHookGuid = CBasePropDoorDisableAreaPortalThinkGetUnmanagedFunction().AddHook(next => (a1) => CBasePropDoorDisableAreaPortalThinkPipeline(a1, () => next()(a1)));
        return CBasePropDoorDisableAreaPortalThinkHookGuid;
    }

    internal static Guid UnhookCBasePropDoorDisableAreaPortalThink()
    {
        CBasePropDoorDisableAreaPortalThinkGetUnmanagedFunction().RemoveHook(CBasePropDoorDisableAreaPortalThinkHookGuid);
        return Guid.Empty;
    }

    private static void CBasePropDoorDisableAreaPortalThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBasePropDoor>(a1);

            var preCtx = new CBasePropDoorDisableAreaPortalThinkPreContext { SchemaObject = schemaObject };
            InvokeCBasePropDoorDisableAreaPortalThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBasePropDoorDisableAreaPortalThinkPostContext { SchemaObject = schemaObject };
            InvokeCBasePropDoorDisableAreaPortalThinkPost(ref postCtx);
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

    internal static void InvokeCBasePropDoorDisableAreaPortalThink(nint a1)
    {
        CBasePropDoorDisableAreaPortalThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBasePropDoorDisableAreaPortalThinkPre(ref CBasePropDoorDisableAreaPortalThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBasePropDoorDisableAreaPortalThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBasePropDoorDisableAreaPortalThinkPost(ref CBasePropDoorDisableAreaPortalThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBasePropDoorDisableAreaPortalThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBasePropDoorDisableAreaPortalThinkHook : ICBasePropDoorDisableAreaPortalThinkHook
{
    private event OnCBasePropDoorDisableAreaPortalThinkPreDelegate? _Pre;
    private event OnCBasePropDoorDisableAreaPortalThinkPostDelegate? _Post;

    public event OnCBasePropDoorDisableAreaPortalThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBasePropDoorDisableAreaPortalThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDisableAreaPortalThink);
            }
        }
    }

    public event OnCBasePropDoorDisableAreaPortalThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBasePropDoorDisableAreaPortalThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDisableAreaPortalThink);
            }
        }
    }

    public void InvokePre(ref CBasePropDoorDisableAreaPortalThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBasePropDoorDisableAreaPortalThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDisableAreaPortalThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBasePropDoorDisableAreaPortalThink);
        }
    }

    public void Invoke(CBasePropDoor schemaObject) => DatamapHooksPublisher.InvokeCBasePropDoorDisableAreaPortalThink(schemaObject.Address);
}