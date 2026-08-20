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
    private delegate void CMapVetoPickControllerVoteControllerThinkDelegate(nint a1);

    private static IUnmanagedFunction<CMapVetoPickControllerVoteControllerThinkDelegate>? CMapVetoPickControllerVoteControllerThinkUnmanagedFunction;
    private static Guid CMapVetoPickControllerVoteControllerThinkHookGuid;

    private static IUnmanagedFunction<CMapVetoPickControllerVoteControllerThinkDelegate> CMapVetoPickControllerVoteControllerThinkGetUnmanagedFunction()
    {
        if (CMapVetoPickControllerVoteControllerThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CMapVetoPickController", "CMapVetoPickControllerVoteControllerThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CMapVetoPickController::CMapVetoPickControllerVoteControllerThink.");
            }
            CMapVetoPickControllerVoteControllerThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CMapVetoPickControllerVoteControllerThinkDelegate>(address);
        }
        return CMapVetoPickControllerVoteControllerThinkUnmanagedFunction;
    }

    internal static Guid HookCMapVetoPickControllerVoteControllerThink()
    {
        CMapVetoPickControllerVoteControllerThinkHookGuid = CMapVetoPickControllerVoteControllerThinkGetUnmanagedFunction().AddHook(next => (a1) => CMapVetoPickControllerVoteControllerThinkPipeline(a1, () => next()(a1)));
        return CMapVetoPickControllerVoteControllerThinkHookGuid;
    }

    internal static Guid UnhookCMapVetoPickControllerVoteControllerThink()
    {
        CMapVetoPickControllerVoteControllerThinkGetUnmanagedFunction().RemoveHook(CMapVetoPickControllerVoteControllerThinkHookGuid);
        return Guid.Empty;
    }

    private static void CMapVetoPickControllerVoteControllerThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CMapVetoPickController>(a1);

            var preCtx = new CMapVetoPickControllerVoteControllerThinkPreContext { SchemaObject = schemaObject };
            InvokeCMapVetoPickControllerVoteControllerThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CMapVetoPickControllerVoteControllerThinkPostContext { SchemaObject = schemaObject };
            InvokeCMapVetoPickControllerVoteControllerThinkPost(ref postCtx);
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

    internal static void InvokeCMapVetoPickControllerVoteControllerThink(nint a1)
    {
        CMapVetoPickControllerVoteControllerThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCMapVetoPickControllerVoteControllerThinkPre(ref CMapVetoPickControllerVoteControllerThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMapVetoPickControllerVoteControllerThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCMapVetoPickControllerVoteControllerThinkPost(ref CMapVetoPickControllerVoteControllerThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMapVetoPickControllerVoteControllerThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CMapVetoPickControllerVoteControllerThinkHook : ICMapVetoPickControllerVoteControllerThinkHook
{
    private event OnCMapVetoPickControllerVoteControllerThinkPreDelegate? _Pre;
    private event OnCMapVetoPickControllerVoteControllerThinkPostDelegate? _Post;

    public event OnCMapVetoPickControllerVoteControllerThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMapVetoPickControllerVoteControllerThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMapVetoPickControllerVoteControllerThink);
            }
        }
    }

    public event OnCMapVetoPickControllerVoteControllerThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMapVetoPickControllerVoteControllerThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMapVetoPickControllerVoteControllerThink);
            }
        }
    }

    public void InvokePre(ref CMapVetoPickControllerVoteControllerThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CMapVetoPickControllerVoteControllerThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMapVetoPickControllerVoteControllerThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMapVetoPickControllerVoteControllerThink);
        }
    }

    public void Invoke(CMapVetoPickController schemaObject) => DatamapHooksPublisher.InvokeCMapVetoPickControllerVoteControllerThink(schemaObject.Address);
}