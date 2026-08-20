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
    private delegate void CCSPlayerControllerResourceDataThinkDelegate(nint a1);

    private static IUnmanagedFunction<CCSPlayerControllerResourceDataThinkDelegate>? CCSPlayerControllerResourceDataThinkUnmanagedFunction;
    private static Guid CCSPlayerControllerResourceDataThinkHookGuid;

    private static IUnmanagedFunction<CCSPlayerControllerResourceDataThinkDelegate> CCSPlayerControllerResourceDataThinkGetUnmanagedFunction()
    {
        if (CCSPlayerControllerResourceDataThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CCSPlayerController", "CCSPlayerControllerResourceDataThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CCSPlayerController::CCSPlayerControllerResourceDataThink.");
            }
            CCSPlayerControllerResourceDataThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerControllerResourceDataThinkDelegate>(address);
        }
        return CCSPlayerControllerResourceDataThinkUnmanagedFunction;
    }

    internal static Guid HookCCSPlayerControllerResourceDataThink()
    {
        CCSPlayerControllerResourceDataThinkHookGuid = CCSPlayerControllerResourceDataThinkGetUnmanagedFunction().AddHook(next => (a1) => CCSPlayerControllerResourceDataThinkPipeline(a1, () => next()(a1)));
        return CCSPlayerControllerResourceDataThinkHookGuid;
    }

    internal static Guid UnhookCCSPlayerControllerResourceDataThink()
    {
        CCSPlayerControllerResourceDataThinkGetUnmanagedFunction().RemoveHook(CCSPlayerControllerResourceDataThinkHookGuid);
        return Guid.Empty;
    }

    private static void CCSPlayerControllerResourceDataThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CCSPlayerController>(a1);

            var preCtx = new CCSPlayerControllerResourceDataThinkPreContext { SchemaObject = schemaObject };
            InvokeCCSPlayerControllerResourceDataThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CCSPlayerControllerResourceDataThinkPostContext { SchemaObject = schemaObject };
            InvokeCCSPlayerControllerResourceDataThinkPost(ref postCtx);
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

    internal static void InvokeCCSPlayerControllerResourceDataThink(nint a1)
    {
        CCSPlayerControllerResourceDataThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCCSPlayerControllerResourceDataThinkPre(ref CCSPlayerControllerResourceDataThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerControllerResourceDataThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCCSPlayerControllerResourceDataThinkPost(ref CCSPlayerControllerResourceDataThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerControllerResourceDataThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CCSPlayerControllerResourceDataThinkHook : ICCSPlayerControllerResourceDataThinkHook
{
    private event OnCCSPlayerControllerResourceDataThinkPreDelegate? _Pre;
    private event OnCCSPlayerControllerResourceDataThinkPostDelegate? _Post;

    public event OnCCSPlayerControllerResourceDataThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerControllerResourceDataThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerResourceDataThink);
            }
        }
    }

    public event OnCCSPlayerControllerResourceDataThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerControllerResourceDataThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerResourceDataThink);
            }
        }
    }

    public void InvokePre(ref CCSPlayerControllerResourceDataThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CCSPlayerControllerResourceDataThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerResourceDataThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerResourceDataThink);
        }
    }

    public void Invoke(CCSPlayerController schemaObject) => DatamapHooksPublisher.InvokeCCSPlayerControllerResourceDataThink(schemaObject.Address);
}