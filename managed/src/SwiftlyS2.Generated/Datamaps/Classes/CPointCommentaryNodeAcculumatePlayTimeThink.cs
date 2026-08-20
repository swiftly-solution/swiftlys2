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
    private delegate void CPointCommentaryNodeAcculumatePlayTimeThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPointCommentaryNodeAcculumatePlayTimeThinkDelegate>? CPointCommentaryNodeAcculumatePlayTimeThinkUnmanagedFunction;
    private static Guid CPointCommentaryNodeAcculumatePlayTimeThinkHookGuid;

    private static IUnmanagedFunction<CPointCommentaryNodeAcculumatePlayTimeThinkDelegate> CPointCommentaryNodeAcculumatePlayTimeThinkGetUnmanagedFunction()
    {
        if (CPointCommentaryNodeAcculumatePlayTimeThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPointCommentaryNode", "CPointCommentaryNodeAcculumatePlayTimeThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPointCommentaryNode::CPointCommentaryNodeAcculumatePlayTimeThink.");
            }
            CPointCommentaryNodeAcculumatePlayTimeThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPointCommentaryNodeAcculumatePlayTimeThinkDelegate>(address);
        }
        return CPointCommentaryNodeAcculumatePlayTimeThinkUnmanagedFunction;
    }

    internal static Guid HookCPointCommentaryNodeAcculumatePlayTimeThink()
    {
        CPointCommentaryNodeAcculumatePlayTimeThinkHookGuid = CPointCommentaryNodeAcculumatePlayTimeThinkGetUnmanagedFunction().AddHook(next => (a1) => CPointCommentaryNodeAcculumatePlayTimeThinkPipeline(a1, () => next()(a1)));
        return CPointCommentaryNodeAcculumatePlayTimeThinkHookGuid;
    }

    internal static Guid UnhookCPointCommentaryNodeAcculumatePlayTimeThink()
    {
        CPointCommentaryNodeAcculumatePlayTimeThinkGetUnmanagedFunction().RemoveHook(CPointCommentaryNodeAcculumatePlayTimeThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPointCommentaryNodeAcculumatePlayTimeThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPointCommentaryNode>(a1);

            var preCtx = new CPointCommentaryNodeAcculumatePlayTimeThinkPreContext { SchemaObject = schemaObject };
            InvokeCPointCommentaryNodeAcculumatePlayTimeThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPointCommentaryNodeAcculumatePlayTimeThinkPostContext { SchemaObject = schemaObject };
            InvokeCPointCommentaryNodeAcculumatePlayTimeThinkPost(ref postCtx);
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

    internal static void InvokeCPointCommentaryNodeAcculumatePlayTimeThink(nint a1)
    {
        CPointCommentaryNodeAcculumatePlayTimeThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPointCommentaryNodeAcculumatePlayTimeThinkPre(ref CPointCommentaryNodeAcculumatePlayTimeThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointCommentaryNodeAcculumatePlayTimeThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPointCommentaryNodeAcculumatePlayTimeThinkPost(ref CPointCommentaryNodeAcculumatePlayTimeThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointCommentaryNodeAcculumatePlayTimeThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPointCommentaryNodeAcculumatePlayTimeThinkHook : ICPointCommentaryNodeAcculumatePlayTimeThinkHook
{
    private event OnCPointCommentaryNodeAcculumatePlayTimeThinkPreDelegate? _Pre;
    private event OnCPointCommentaryNodeAcculumatePlayTimeThinkPostDelegate? _Post;

    public event OnCPointCommentaryNodeAcculumatePlayTimeThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointCommentaryNodeAcculumatePlayTimeThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeAcculumatePlayTimeThink);
            }
        }
    }

    public event OnCPointCommentaryNodeAcculumatePlayTimeThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointCommentaryNodeAcculumatePlayTimeThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeAcculumatePlayTimeThink);
            }
        }
    }

    public void InvokePre(ref CPointCommentaryNodeAcculumatePlayTimeThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPointCommentaryNodeAcculumatePlayTimeThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeAcculumatePlayTimeThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeAcculumatePlayTimeThink);
        }
    }

    public void Invoke(CPointCommentaryNode schemaObject) => DatamapHooksPublisher.InvokeCPointCommentaryNodeAcculumatePlayTimeThink(schemaObject.Address);
}