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
    private delegate void CPointCommentaryNodeUpdateViewPostThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPointCommentaryNodeUpdateViewPostThinkDelegate>? CPointCommentaryNodeUpdateViewPostThinkUnmanagedFunction;
    private static Guid CPointCommentaryNodeUpdateViewPostThinkHookGuid;

    private static IUnmanagedFunction<CPointCommentaryNodeUpdateViewPostThinkDelegate> CPointCommentaryNodeUpdateViewPostThinkGetUnmanagedFunction()
    {
        if (CPointCommentaryNodeUpdateViewPostThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPointCommentaryNode", "CPointCommentaryNodeUpdateViewPostThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPointCommentaryNode::CPointCommentaryNodeUpdateViewPostThink.");
            }
            CPointCommentaryNodeUpdateViewPostThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPointCommentaryNodeUpdateViewPostThinkDelegate>(address);
        }
        return CPointCommentaryNodeUpdateViewPostThinkUnmanagedFunction;
    }

    internal static Guid HookCPointCommentaryNodeUpdateViewPostThink()
    {
        CPointCommentaryNodeUpdateViewPostThinkHookGuid = CPointCommentaryNodeUpdateViewPostThinkGetUnmanagedFunction().AddHook(next => (a1) => CPointCommentaryNodeUpdateViewPostThinkPipeline(a1, () => next()(a1)));
        return CPointCommentaryNodeUpdateViewPostThinkHookGuid;
    }

    internal static Guid UnhookCPointCommentaryNodeUpdateViewPostThink()
    {
        CPointCommentaryNodeUpdateViewPostThinkGetUnmanagedFunction().RemoveHook(CPointCommentaryNodeUpdateViewPostThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPointCommentaryNodeUpdateViewPostThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPointCommentaryNode>(a1);

            var preCtx = new CPointCommentaryNodeUpdateViewPostThinkPreContext { SchemaObject = schemaObject };
            InvokeCPointCommentaryNodeUpdateViewPostThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPointCommentaryNodeUpdateViewPostThinkPostContext { SchemaObject = schemaObject };
            InvokeCPointCommentaryNodeUpdateViewPostThinkPost(ref postCtx);
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

    internal static void InvokeCPointCommentaryNodeUpdateViewPostThink(nint a1)
    {
        CPointCommentaryNodeUpdateViewPostThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPointCommentaryNodeUpdateViewPostThinkPre(ref CPointCommentaryNodeUpdateViewPostThinkPreContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CPointCommentaryNodeUpdateViewPostThink);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCPointCommentaryNodeUpdateViewPostThinkPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }

    internal static void InvokeCPointCommentaryNodeUpdateViewPostThinkPost(ref CPointCommentaryNodeUpdateViewPostThinkPostContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CPointCommentaryNodeUpdateViewPostThink);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCPointCommentaryNodeUpdateViewPostThinkPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }
}

internal sealed class CPointCommentaryNodeUpdateViewPostThinkHook(GameHooksService owner) : ICPointCommentaryNodeUpdateViewPostThinkHook
{
    private event OnCPointCommentaryNodeUpdateViewPostThinkPreDelegate? _Pre;
    private event OnCPointCommentaryNodeUpdateViewPostThinkPostDelegate? _Post;

    public event OnCPointCommentaryNodeUpdateViewPostThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointCommentaryNodeUpdateViewPostThink, owner);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeUpdateViewPostThink, owner);
            }
        }
    }

    public event OnCPointCommentaryNodeUpdateViewPostThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointCommentaryNodeUpdateViewPostThink, owner);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeUpdateViewPostThink, owner);
            }
        }
    }

    public void InvokePre(ref CPointCommentaryNodeUpdateViewPostThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPointCommentaryNodeUpdateViewPostThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeUpdateViewPostThink, owner);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeUpdateViewPostThink, owner);
        }
    }

    public void Invoke(CPointCommentaryNode schemaObject) => DatamapHooksPublisher.InvokeCPointCommentaryNodeUpdateViewPostThink(schemaObject.Address);
}