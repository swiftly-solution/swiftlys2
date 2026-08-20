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
    private delegate void CCSPlayerControllerPlayerForceTeamThinkDelegate(nint a1);

    private static IUnmanagedFunction<CCSPlayerControllerPlayerForceTeamThinkDelegate>? CCSPlayerControllerPlayerForceTeamThinkUnmanagedFunction;
    private static Guid CCSPlayerControllerPlayerForceTeamThinkHookGuid;

    private static IUnmanagedFunction<CCSPlayerControllerPlayerForceTeamThinkDelegate> CCSPlayerControllerPlayerForceTeamThinkGetUnmanagedFunction()
    {
        if (CCSPlayerControllerPlayerForceTeamThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CCSPlayerController", "CCSPlayerControllerPlayerForceTeamThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CCSPlayerController::CCSPlayerControllerPlayerForceTeamThink.");
            }
            CCSPlayerControllerPlayerForceTeamThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CCSPlayerControllerPlayerForceTeamThinkDelegate>(address);
        }
        return CCSPlayerControllerPlayerForceTeamThinkUnmanagedFunction;
    }

    internal static Guid HookCCSPlayerControllerPlayerForceTeamThink()
    {
        CCSPlayerControllerPlayerForceTeamThinkHookGuid = CCSPlayerControllerPlayerForceTeamThinkGetUnmanagedFunction().AddHook(next => (a1) => CCSPlayerControllerPlayerForceTeamThinkPipeline(a1, () => next()(a1)));
        return CCSPlayerControllerPlayerForceTeamThinkHookGuid;
    }

    internal static Guid UnhookCCSPlayerControllerPlayerForceTeamThink()
    {
        CCSPlayerControllerPlayerForceTeamThinkGetUnmanagedFunction().RemoveHook(CCSPlayerControllerPlayerForceTeamThinkHookGuid);
        return Guid.Empty;
    }

    private static void CCSPlayerControllerPlayerForceTeamThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CCSPlayerController>(a1);

            var preCtx = new CCSPlayerControllerPlayerForceTeamThinkPreContext { SchemaObject = schemaObject };
            InvokeCCSPlayerControllerPlayerForceTeamThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CCSPlayerControllerPlayerForceTeamThinkPostContext { SchemaObject = schemaObject };
            InvokeCCSPlayerControllerPlayerForceTeamThinkPost(ref postCtx);
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

    internal static void InvokeCCSPlayerControllerPlayerForceTeamThink(nint a1)
    {
        CCSPlayerControllerPlayerForceTeamThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCCSPlayerControllerPlayerForceTeamThinkPre(ref CCSPlayerControllerPlayerForceTeamThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerControllerPlayerForceTeamThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCCSPlayerControllerPlayerForceTeamThinkPost(ref CCSPlayerControllerPlayerForceTeamThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCCSPlayerControllerPlayerForceTeamThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CCSPlayerControllerPlayerForceTeamThinkHook : ICCSPlayerControllerPlayerForceTeamThinkHook
{
    private event OnCCSPlayerControllerPlayerForceTeamThinkPreDelegate? _Pre;
    private event OnCCSPlayerControllerPlayerForceTeamThinkPostDelegate? _Post;

    public event OnCCSPlayerControllerPlayerForceTeamThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerControllerPlayerForceTeamThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerPlayerForceTeamThink);
            }
        }
    }

    public event OnCCSPlayerControllerPlayerForceTeamThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CCSPlayerControllerPlayerForceTeamThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerPlayerForceTeamThink);
            }
        }
    }

    public void InvokePre(ref CCSPlayerControllerPlayerForceTeamThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CCSPlayerControllerPlayerForceTeamThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerPlayerForceTeamThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CCSPlayerControllerPlayerForceTeamThink);
        }
    }

    public void Invoke(CCSPlayerController schemaObject) => DatamapHooksPublisher.InvokeCCSPlayerControllerPlayerForceTeamThink(schemaObject.Address);
}