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
    private delegate void CPathNodeParentedMoveThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPathNodeParentedMoveThinkDelegate>? CPathNodeParentedMoveThinkUnmanagedFunction;
    private static Guid CPathNodeParentedMoveThinkHookGuid;

    private static IUnmanagedFunction<CPathNodeParentedMoveThinkDelegate> CPathNodeParentedMoveThinkGetUnmanagedFunction()
    {
        if (CPathNodeParentedMoveThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPathNode", "CPathNodeParentedMoveThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPathNode::CPathNodeParentedMoveThink.");
            }
            CPathNodeParentedMoveThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPathNodeParentedMoveThinkDelegate>(address);
        }
        return CPathNodeParentedMoveThinkUnmanagedFunction;
    }

    internal static Guid HookCPathNodeParentedMoveThink()
    {
        CPathNodeParentedMoveThinkHookGuid = CPathNodeParentedMoveThinkGetUnmanagedFunction().AddHook(next => (a1) => CPathNodeParentedMoveThinkPipeline(a1, () => next()(a1)));
        return CPathNodeParentedMoveThinkHookGuid;
    }

    internal static Guid UnhookCPathNodeParentedMoveThink()
    {
        CPathNodeParentedMoveThinkGetUnmanagedFunction().RemoveHook(CPathNodeParentedMoveThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPathNodeParentedMoveThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPathNode>(a1);

            var preCtx = new CPathNodeParentedMoveThinkPreContext { SchemaObject = schemaObject };
            InvokeCPathNodeParentedMoveThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPathNodeParentedMoveThinkPostContext { SchemaObject = schemaObject };
            InvokeCPathNodeParentedMoveThinkPost(ref postCtx);
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

    internal static void InvokeCPathNodeParentedMoveThink(nint a1)
    {
        CPathNodeParentedMoveThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPathNodeParentedMoveThinkPre(ref CPathNodeParentedMoveThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPathNodeParentedMoveThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPathNodeParentedMoveThinkPost(ref CPathNodeParentedMoveThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPathNodeParentedMoveThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPathNodeParentedMoveThinkHook : ICPathNodeParentedMoveThinkHook
{
    private event OnCPathNodeParentedMoveThinkPreDelegate? _Pre;
    private event OnCPathNodeParentedMoveThinkPostDelegate? _Post;

    public event OnCPathNodeParentedMoveThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPathNodeParentedMoveThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPathNodeParentedMoveThink);
            }
        }
    }

    public event OnCPathNodeParentedMoveThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPathNodeParentedMoveThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPathNodeParentedMoveThink);
            }
        }
    }

    public void InvokePre(ref CPathNodeParentedMoveThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPathNodeParentedMoveThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPathNodeParentedMoveThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPathNodeParentedMoveThink);
        }
    }

    public void Invoke(CPathNode schemaObject) => DatamapHooksPublisher.InvokeCPathNodeParentedMoveThink(schemaObject.Address);
}