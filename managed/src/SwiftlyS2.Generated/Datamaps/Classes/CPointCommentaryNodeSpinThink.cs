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
    private delegate void CPointCommentaryNodeSpinThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPointCommentaryNodeSpinThinkDelegate>? CPointCommentaryNodeSpinThinkUnmanagedFunction;
    private static Guid CPointCommentaryNodeSpinThinkHookGuid;

    private static IUnmanagedFunction<CPointCommentaryNodeSpinThinkDelegate> CPointCommentaryNodeSpinThinkGetUnmanagedFunction()
    {
        if (CPointCommentaryNodeSpinThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPointCommentaryNode", "CPointCommentaryNodeSpinThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPointCommentaryNode::CPointCommentaryNodeSpinThink.");
            }
            CPointCommentaryNodeSpinThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPointCommentaryNodeSpinThinkDelegate>(address);
        }
        return CPointCommentaryNodeSpinThinkUnmanagedFunction;
    }

    internal static Guid HookCPointCommentaryNodeSpinThink()
    {
        CPointCommentaryNodeSpinThinkHookGuid = CPointCommentaryNodeSpinThinkGetUnmanagedFunction().AddHook(next => (a1) => CPointCommentaryNodeSpinThinkPipeline(a1, () => next()(a1)));
        return CPointCommentaryNodeSpinThinkHookGuid;
    }

    internal static Guid UnhookCPointCommentaryNodeSpinThink()
    {
        CPointCommentaryNodeSpinThinkGetUnmanagedFunction().RemoveHook(CPointCommentaryNodeSpinThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPointCommentaryNodeSpinThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPointCommentaryNode>(a1);

            var preCtx = new CPointCommentaryNodeSpinThinkPreContext { SchemaObject = schemaObject };
            InvokeCPointCommentaryNodeSpinThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPointCommentaryNodeSpinThinkPostContext { SchemaObject = schemaObject };
            InvokeCPointCommentaryNodeSpinThinkPost(ref postCtx);
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

    internal static void InvokeCPointCommentaryNodeSpinThink(nint a1)
    {
        CPointCommentaryNodeSpinThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPointCommentaryNodeSpinThinkPre(ref CPointCommentaryNodeSpinThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointCommentaryNodeSpinThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPointCommentaryNodeSpinThinkPost(ref CPointCommentaryNodeSpinThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPointCommentaryNodeSpinThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPointCommentaryNodeSpinThinkHook : ICPointCommentaryNodeSpinThinkHook
{
    private event OnCPointCommentaryNodeSpinThinkPreDelegate? _Pre;
    private event OnCPointCommentaryNodeSpinThinkPostDelegate? _Post;

    public event OnCPointCommentaryNodeSpinThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointCommentaryNodeSpinThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeSpinThink);
            }
        }
    }

    public event OnCPointCommentaryNodeSpinThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPointCommentaryNodeSpinThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeSpinThink);
            }
        }
    }

    public void InvokePre(ref CPointCommentaryNodeSpinThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPointCommentaryNodeSpinThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeSpinThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPointCommentaryNodeSpinThink);
        }
    }

    public void Invoke(CPointCommentaryNode schemaObject) => DatamapHooksPublisher.InvokeCPointCommentaryNodeSpinThink(schemaObject.Address);
}