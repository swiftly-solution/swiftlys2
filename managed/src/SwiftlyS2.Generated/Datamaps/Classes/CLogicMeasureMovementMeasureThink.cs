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
    private delegate void CLogicMeasureMovementMeasureThinkDelegate(nint a1);

    private static IUnmanagedFunction<CLogicMeasureMovementMeasureThinkDelegate>? CLogicMeasureMovementMeasureThinkUnmanagedFunction;
    private static Guid CLogicMeasureMovementMeasureThinkHookGuid;

    private static IUnmanagedFunction<CLogicMeasureMovementMeasureThinkDelegate> CLogicMeasureMovementMeasureThinkGetUnmanagedFunction()
    {
        if (CLogicMeasureMovementMeasureThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CLogicMeasureMovement", "CLogicMeasureMovementMeasureThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CLogicMeasureMovement::CLogicMeasureMovementMeasureThink.");
            }
            CLogicMeasureMovementMeasureThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CLogicMeasureMovementMeasureThinkDelegate>(address);
        }
        return CLogicMeasureMovementMeasureThinkUnmanagedFunction;
    }

    internal static Guid HookCLogicMeasureMovementMeasureThink()
    {
        CLogicMeasureMovementMeasureThinkHookGuid = CLogicMeasureMovementMeasureThinkGetUnmanagedFunction().AddHook(next => (a1) => CLogicMeasureMovementMeasureThinkPipeline(a1, () => next()(a1)));
        return CLogicMeasureMovementMeasureThinkHookGuid;
    }

    internal static Guid UnhookCLogicMeasureMovementMeasureThink()
    {
        CLogicMeasureMovementMeasureThinkGetUnmanagedFunction().RemoveHook(CLogicMeasureMovementMeasureThinkHookGuid);
        return Guid.Empty;
    }

    private static void CLogicMeasureMovementMeasureThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CLogicMeasureMovement>(a1);

            var preCtx = new CLogicMeasureMovementMeasureThinkPreContext { SchemaObject = schemaObject };
            InvokeCLogicMeasureMovementMeasureThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CLogicMeasureMovementMeasureThinkPostContext { SchemaObject = schemaObject };
            InvokeCLogicMeasureMovementMeasureThinkPost(ref postCtx);
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

    internal static void InvokeCLogicMeasureMovementMeasureThink(nint a1)
    {
        CLogicMeasureMovementMeasureThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCLogicMeasureMovementMeasureThinkPre(ref CLogicMeasureMovementMeasureThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCLogicMeasureMovementMeasureThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCLogicMeasureMovementMeasureThinkPost(ref CLogicMeasureMovementMeasureThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCLogicMeasureMovementMeasureThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CLogicMeasureMovementMeasureThinkHook : ICLogicMeasureMovementMeasureThinkHook
{
    private event OnCLogicMeasureMovementMeasureThinkPreDelegate? _Pre;
    private event OnCLogicMeasureMovementMeasureThinkPostDelegate? _Post;

    public event OnCLogicMeasureMovementMeasureThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CLogicMeasureMovementMeasureThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicMeasureMovementMeasureThink);
            }
        }
    }

    public event OnCLogicMeasureMovementMeasureThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CLogicMeasureMovementMeasureThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicMeasureMovementMeasureThink);
            }
        }
    }

    public void InvokePre(ref CLogicMeasureMovementMeasureThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CLogicMeasureMovementMeasureThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicMeasureMovementMeasureThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicMeasureMovementMeasureThink);
        }
    }

    public void Invoke(CLogicMeasureMovement schemaObject) => DatamapHooksPublisher.InvokeCLogicMeasureMovementMeasureThink(schemaObject.Address);
}