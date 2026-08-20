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
    private delegate void CCSPlayerControllerResetForceTeamThinkDelegate(nint a1);

    private static IUnmanagedFunction<CCSPlayerControllerResetForceTeamThinkDelegate>? CCSPlayerControllerResetForceTeamThinkUnmanagedFunction;
    private static Guid CCSPlayerControllerResetForceTeamThinkHookGuid;

    private static IUnmanagedFunction<CCSPlayerControllerResetForceTeamThinkDelegate> CCSPlayerControllerResetForceTeamThinkGetUnmanagedFunction()
    {
        if (CCSPlayerControllerResetForceTeamThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CCSPlayerController", "CCSPlayerControllerResetForceTeamThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CCSPlayerController::CCSPlayerControllerResetForceTeamThink.");
            }
            CCSPlayerControllerResetForceTeamThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerControllerResetForceTeamThinkDelegate>(address);
        }
        return CCSPlayerControllerResetForceTeamThinkUnmanagedFunction;
    }

    internal static Guid HookCCSPlayerControllerResetForceTeamThink()
    {
        CCSPlayerControllerResetForceTeamThinkHookGuid = CCSPlayerControllerResetForceTeamThinkGetUnmanagedFunction().AddHook(next => (a1) => CCSPlayerControllerResetForceTeamThinkPipeline(a1, () => next()(a1)));
        return CCSPlayerControllerResetForceTeamThinkHookGuid;
    }

    internal static Guid UnhookCCSPlayerControllerResetForceTeamThink()
    {
        CCSPlayerControllerResetForceTeamThinkGetUnmanagedFunction().RemoveHook(CCSPlayerControllerResetForceTeamThinkHookGuid);
        return Guid.Empty;
    }

    private static void CCSPlayerControllerResetForceTeamThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CCSPlayerController>(a1);

            var preCtx = new CCSPlayerControllerResetForceTeamThinkPreContext { SchemaObject = schemaObject };
            InvokeCCSPlayerControllerResetForceTeamThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CCSPlayerControllerResetForceTeamThinkPostContext { SchemaObject = schemaObject };
            InvokeCCSPlayerControllerResetForceTeamThinkPost(ref postCtx);
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

    internal static void InvokeCCSPlayerControllerResetForceTeamThink(nint a1)
    {
        CCSPlayerControllerResetForceTeamThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCCSPlayerControllerResetForceTeamThinkPre(ref CCSPlayerControllerResetForceTeamThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerControllerResetForceTeamThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCCSPlayerControllerResetForceTeamThinkPost(ref CCSPlayerControllerResetForceTeamThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerControllerResetForceTeamThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CCSPlayerControllerResetForceTeamThinkHook : ICCSPlayerControllerResetForceTeamThinkHook
{
    private event OnCCSPlayerControllerResetForceTeamThinkPreDelegate? _Pre;
    private event OnCCSPlayerControllerResetForceTeamThinkPostDelegate? _Post;

    public event OnCCSPlayerControllerResetForceTeamThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerControllerResetForceTeamThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerResetForceTeamThink);
            }
        }
    }

    public event OnCCSPlayerControllerResetForceTeamThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerControllerResetForceTeamThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerResetForceTeamThink);
            }
        }
    }

    public void InvokePre(ref CCSPlayerControllerResetForceTeamThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CCSPlayerControllerResetForceTeamThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerResetForceTeamThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerResetForceTeamThink);
        }
    }

    public void Invoke(CCSPlayerController schemaObject) => DatamapHooksPublisher.InvokeCCSPlayerControllerResetForceTeamThink(schemaObject.Address);
}