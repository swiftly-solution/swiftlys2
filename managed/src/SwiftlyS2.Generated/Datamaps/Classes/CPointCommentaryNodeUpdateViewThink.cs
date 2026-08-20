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
    private delegate void CPointCommentaryNodeUpdateViewThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPointCommentaryNodeUpdateViewThinkDelegate>? CPointCommentaryNodeUpdateViewThinkUnmanagedFunction;
    private static Guid CPointCommentaryNodeUpdateViewThinkHookGuid;

    private static IUnmanagedFunction<CPointCommentaryNodeUpdateViewThinkDelegate> CPointCommentaryNodeUpdateViewThinkGetUnmanagedFunction()
    {
        if (CPointCommentaryNodeUpdateViewThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPointCommentaryNode", "CPointCommentaryNodeUpdateViewThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPointCommentaryNode::CPointCommentaryNodeUpdateViewThink.");
            }
            CPointCommentaryNodeUpdateViewThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPointCommentaryNodeUpdateViewThinkDelegate>(address);
        }
        return CPointCommentaryNodeUpdateViewThinkUnmanagedFunction;
    }

    internal static Guid HookCPointCommentaryNodeUpdateViewThink()
    {
        CPointCommentaryNodeUpdateViewThinkHookGuid = CPointCommentaryNodeUpdateViewThinkGetUnmanagedFunction().AddHook(next => (a1) => CPointCommentaryNodeUpdateViewThinkPipeline(a1, () => next()(a1)));
        return CPointCommentaryNodeUpdateViewThinkHookGuid;
    }

    internal static Guid UnhookCPointCommentaryNodeUpdateViewThink()
    {
        CPointCommentaryNodeUpdateViewThinkGetUnmanagedFunction().RemoveHook(CPointCommentaryNodeUpdateViewThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPointCommentaryNodeUpdateViewThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPointCommentaryNode>(a1);

            var preCtx = new CPointCommentaryNodeUpdateViewThinkPreContext { SchemaObject = schemaObject };
            InvokeCPointCommentaryNodeUpdateViewThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPointCommentaryNodeUpdateViewThinkPostContext { SchemaObject = schemaObject };
            InvokeCPointCommentaryNodeUpdateViewThinkPost(ref postCtx);
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

    internal static void InvokeCPointCommentaryNodeUpdateViewThink(nint a1)
    {
        CPointCommentaryNodeUpdateViewThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPointCommentaryNodeUpdateViewThinkPre(ref CPointCommentaryNodeUpdateViewThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointCommentaryNodeUpdateViewThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPointCommentaryNodeUpdateViewThinkPost(ref CPointCommentaryNodeUpdateViewThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointCommentaryNodeUpdateViewThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPointCommentaryNodeUpdateViewThinkHook : ICPointCommentaryNodeUpdateViewThinkHook
{
    private event OnCPointCommentaryNodeUpdateViewThinkPreDelegate? _Pre;
    private event OnCPointCommentaryNodeUpdateViewThinkPostDelegate? _Post;

    public event OnCPointCommentaryNodeUpdateViewThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointCommentaryNodeUpdateViewThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeUpdateViewThink);
            }
        }
    }

    public event OnCPointCommentaryNodeUpdateViewThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointCommentaryNodeUpdateViewThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeUpdateViewThink);
            }
        }
    }

    public void InvokePre(ref CPointCommentaryNodeUpdateViewThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPointCommentaryNodeUpdateViewThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeUpdateViewThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeUpdateViewThink);
        }
    }

    public void Invoke(CPointCommentaryNode schemaObject) => DatamapHooksPublisher.InvokeCPointCommentaryNodeUpdateViewThink(schemaObject.Address);
}