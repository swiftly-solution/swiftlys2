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
    private delegate void CCSPlayerPawnPushawayThinkDelegate(nint a1);

    private static IUnmanagedFunction<CCSPlayerPawnPushawayThinkDelegate>? CCSPlayerPawnPushawayThinkUnmanagedFunction;
    private static Guid CCSPlayerPawnPushawayThinkHookGuid;

    private static IUnmanagedFunction<CCSPlayerPawnPushawayThinkDelegate> CCSPlayerPawnPushawayThinkGetUnmanagedFunction()
    {
        if (CCSPlayerPawnPushawayThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CCSPlayerPawn", "CCSPlayerPawnPushawayThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CCSPlayerPawn::CCSPlayerPawnPushawayThink.");
            }
            CCSPlayerPawnPushawayThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerPawnPushawayThinkDelegate>(address);
        }
        return CCSPlayerPawnPushawayThinkUnmanagedFunction;
    }

    internal static Guid HookCCSPlayerPawnPushawayThink()
    {
        CCSPlayerPawnPushawayThinkHookGuid = CCSPlayerPawnPushawayThinkGetUnmanagedFunction().AddHook(next => (a1) => CCSPlayerPawnPushawayThinkPipeline(a1, () => next()(a1)));
        return CCSPlayerPawnPushawayThinkHookGuid;
    }

    internal static Guid UnhookCCSPlayerPawnPushawayThink()
    {
        CCSPlayerPawnPushawayThinkGetUnmanagedFunction().RemoveHook(CCSPlayerPawnPushawayThinkHookGuid);
        return Guid.Empty;
    }

    private static void CCSPlayerPawnPushawayThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CCSPlayerPawn>(a1);

            var preCtx = new CCSPlayerPawnPushawayThinkPreContext { SchemaObject = schemaObject };
            InvokeCCSPlayerPawnPushawayThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CCSPlayerPawnPushawayThinkPostContext { SchemaObject = schemaObject };
            InvokeCCSPlayerPawnPushawayThinkPost(ref postCtx);
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

    internal static void InvokeCCSPlayerPawnPushawayThink(nint a1)
    {
        CCSPlayerPawnPushawayThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCCSPlayerPawnPushawayThinkPre(ref CCSPlayerPawnPushawayThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerPawnPushawayThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCCSPlayerPawnPushawayThinkPost(ref CCSPlayerPawnPushawayThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerPawnPushawayThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CCSPlayerPawnPushawayThinkHook : ICCSPlayerPawnPushawayThinkHook
{
    private event OnCCSPlayerPawnPushawayThinkPreDelegate? _Pre;
    private event OnCCSPlayerPawnPushawayThinkPostDelegate? _Post;

    public event OnCCSPlayerPawnPushawayThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerPawnPushawayThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerPawnPushawayThink);
            }
        }
    }

    public event OnCCSPlayerPawnPushawayThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerPawnPushawayThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerPawnPushawayThink);
            }
        }
    }

    public void InvokePre(ref CCSPlayerPawnPushawayThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CCSPlayerPawnPushawayThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerPawnPushawayThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerPawnPushawayThink);
        }
    }

    public void Invoke(CCSPlayerPawn schemaObject) => DatamapHooksPublisher.InvokeCCSPlayerPawnPushawayThink(schemaObject.Address);
}