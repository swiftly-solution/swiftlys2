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
    private delegate void CCSPlayerControllerInventoryUpdateThinkDelegate(nint a1);

    private static IUnmanagedFunction<CCSPlayerControllerInventoryUpdateThinkDelegate>? CCSPlayerControllerInventoryUpdateThinkUnmanagedFunction;
    private static Guid CCSPlayerControllerInventoryUpdateThinkHookGuid;

    private static IUnmanagedFunction<CCSPlayerControllerInventoryUpdateThinkDelegate> CCSPlayerControllerInventoryUpdateThinkGetUnmanagedFunction()
    {
        if (CCSPlayerControllerInventoryUpdateThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CCSPlayerController", "CCSPlayerControllerInventoryUpdateThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CCSPlayerController::CCSPlayerControllerInventoryUpdateThink.");
            }
            CCSPlayerControllerInventoryUpdateThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerControllerInventoryUpdateThinkDelegate>(address);
        }
        return CCSPlayerControllerInventoryUpdateThinkUnmanagedFunction;
    }

    internal static Guid HookCCSPlayerControllerInventoryUpdateThink()
    {
        CCSPlayerControllerInventoryUpdateThinkHookGuid = CCSPlayerControllerInventoryUpdateThinkGetUnmanagedFunction().AddHook(next => (a1) => CCSPlayerControllerInventoryUpdateThinkPipeline(a1, () => next()(a1)));
        return CCSPlayerControllerInventoryUpdateThinkHookGuid;
    }

    internal static Guid UnhookCCSPlayerControllerInventoryUpdateThink()
    {
        CCSPlayerControllerInventoryUpdateThinkGetUnmanagedFunction().RemoveHook(CCSPlayerControllerInventoryUpdateThinkHookGuid);
        return Guid.Empty;
    }

    private static void CCSPlayerControllerInventoryUpdateThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CCSPlayerController>(a1);

            var preCtx = new CCSPlayerControllerInventoryUpdateThinkPreContext { SchemaObject = schemaObject };
            InvokeCCSPlayerControllerInventoryUpdateThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CCSPlayerControllerInventoryUpdateThinkPostContext { SchemaObject = schemaObject };
            InvokeCCSPlayerControllerInventoryUpdateThinkPost(ref postCtx);
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

    internal static void InvokeCCSPlayerControllerInventoryUpdateThink(nint a1)
    {
        CCSPlayerControllerInventoryUpdateThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCCSPlayerControllerInventoryUpdateThinkPre(ref CCSPlayerControllerInventoryUpdateThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerControllerInventoryUpdateThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCCSPlayerControllerInventoryUpdateThinkPost(ref CCSPlayerControllerInventoryUpdateThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerControllerInventoryUpdateThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CCSPlayerControllerInventoryUpdateThinkHook : ICCSPlayerControllerInventoryUpdateThinkHook
{
    private event OnCCSPlayerControllerInventoryUpdateThinkPreDelegate? _Pre;
    private event OnCCSPlayerControllerInventoryUpdateThinkPostDelegate? _Post;

    public event OnCCSPlayerControllerInventoryUpdateThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerControllerInventoryUpdateThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerInventoryUpdateThink);
            }
        }
    }

    public event OnCCSPlayerControllerInventoryUpdateThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerControllerInventoryUpdateThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerInventoryUpdateThink);
            }
        }
    }

    public void InvokePre(ref CCSPlayerControllerInventoryUpdateThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CCSPlayerControllerInventoryUpdateThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerInventoryUpdateThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerInventoryUpdateThink);
        }
    }

    public void Invoke(CCSPlayerController schemaObject) => DatamapHooksPublisher.InvokeCCSPlayerControllerInventoryUpdateThink(schemaObject.Address);
}