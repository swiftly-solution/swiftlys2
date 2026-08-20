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
    private delegate void CBaseAnimGraphChoreoServicesThinkDelegate(nint a1);

    private static IUnmanagedFunction<CBaseAnimGraphChoreoServicesThinkDelegate>? CBaseAnimGraphChoreoServicesThinkUnmanagedFunction;
    private static Guid CBaseAnimGraphChoreoServicesThinkHookGuid;

    private static IUnmanagedFunction<CBaseAnimGraphChoreoServicesThinkDelegate> CBaseAnimGraphChoreoServicesThinkGetUnmanagedFunction()
    {
        if (CBaseAnimGraphChoreoServicesThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBaseAnimGraph", "CBaseAnimGraphChoreoServicesThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBaseAnimGraph::CBaseAnimGraphChoreoServicesThink.");
            }
            CBaseAnimGraphChoreoServicesThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBaseAnimGraphChoreoServicesThinkDelegate>(address);
        }
        return CBaseAnimGraphChoreoServicesThinkUnmanagedFunction;
    }

    internal static Guid HookCBaseAnimGraphChoreoServicesThink()
    {
        CBaseAnimGraphChoreoServicesThinkHookGuid = CBaseAnimGraphChoreoServicesThinkGetUnmanagedFunction().AddHook(next => (a1) => CBaseAnimGraphChoreoServicesThinkPipeline(a1, () => next()(a1)));
        return CBaseAnimGraphChoreoServicesThinkHookGuid;
    }

    internal static Guid UnhookCBaseAnimGraphChoreoServicesThink()
    {
        CBaseAnimGraphChoreoServicesThinkGetUnmanagedFunction().RemoveHook(CBaseAnimGraphChoreoServicesThinkHookGuid);
        return Guid.Empty;
    }

    private static void CBaseAnimGraphChoreoServicesThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBaseAnimGraph>(a1);

            var preCtx = new CBaseAnimGraphChoreoServicesThinkPreContext { SchemaObject = schemaObject };
            InvokeCBaseAnimGraphChoreoServicesThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBaseAnimGraphChoreoServicesThinkPostContext { SchemaObject = schemaObject };
            InvokeCBaseAnimGraphChoreoServicesThinkPost(ref postCtx);
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

    internal static void InvokeCBaseAnimGraphChoreoServicesThink(nint a1)
    {
        CBaseAnimGraphChoreoServicesThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBaseAnimGraphChoreoServicesThinkPre(ref CBaseAnimGraphChoreoServicesThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseAnimGraphChoreoServicesThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBaseAnimGraphChoreoServicesThinkPost(ref CBaseAnimGraphChoreoServicesThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBaseAnimGraphChoreoServicesThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBaseAnimGraphChoreoServicesThinkHook : ICBaseAnimGraphChoreoServicesThinkHook
{
    private event OnCBaseAnimGraphChoreoServicesThinkPreDelegate? _Pre;
    private event OnCBaseAnimGraphChoreoServicesThinkPostDelegate? _Post;

    public event OnCBaseAnimGraphChoreoServicesThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseAnimGraphChoreoServicesThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseAnimGraphChoreoServicesThink);
            }
        }
    }

    public event OnCBaseAnimGraphChoreoServicesThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBaseAnimGraphChoreoServicesThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseAnimGraphChoreoServicesThink);
            }
        }
    }

    public void InvokePre(ref CBaseAnimGraphChoreoServicesThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBaseAnimGraphChoreoServicesThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseAnimGraphChoreoServicesThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBaseAnimGraphChoreoServicesThink);
        }
    }

    public void Invoke(CBaseAnimGraph schemaObject) => DatamapHooksPublisher.InvokeCBaseAnimGraphChoreoServicesThink(schemaObject.Address);
}