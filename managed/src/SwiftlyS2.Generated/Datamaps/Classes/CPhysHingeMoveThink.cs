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
    private delegate void CPhysHingeMoveThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPhysHingeMoveThinkDelegate>? CPhysHingeMoveThinkUnmanagedFunction;
    private static Guid CPhysHingeMoveThinkHookGuid;

    private static IUnmanagedFunction<CPhysHingeMoveThinkDelegate> CPhysHingeMoveThinkGetUnmanagedFunction()
    {
        if (CPhysHingeMoveThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysHinge", "CPhysHingeMoveThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysHinge::CPhysHingeMoveThink.");
            }
            CPhysHingeMoveThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysHingeMoveThinkDelegate>(address);
        }
        return CPhysHingeMoveThinkUnmanagedFunction;
    }

    internal static Guid HookCPhysHingeMoveThink()
    {
        CPhysHingeMoveThinkHookGuid = CPhysHingeMoveThinkGetUnmanagedFunction().AddHook(next => (a1) => CPhysHingeMoveThinkPipeline(a1, () => next()(a1)));
        return CPhysHingeMoveThinkHookGuid;
    }

    internal static Guid UnhookCPhysHingeMoveThink()
    {
        CPhysHingeMoveThinkGetUnmanagedFunction().RemoveHook(CPhysHingeMoveThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPhysHingeMoveThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysHinge>(a1);

            var preCtx = new CPhysHingeMoveThinkPreContext { SchemaObject = schemaObject };
            InvokeCPhysHingeMoveThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysHingeMoveThinkPostContext { SchemaObject = schemaObject };
            InvokeCPhysHingeMoveThinkPost(ref postCtx);
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

    internal static void InvokeCPhysHingeMoveThink(nint a1)
    {
        CPhysHingeMoveThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysHingeMoveThinkPre(ref CPhysHingeMoveThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysHingeMoveThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysHingeMoveThinkPost(ref CPhysHingeMoveThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysHingeMoveThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysHingeMoveThinkHook : ICPhysHingeMoveThinkHook
{
    private event OnCPhysHingeMoveThinkPreDelegate? _Pre;
    private event OnCPhysHingeMoveThinkPostDelegate? _Post;

    public event OnCPhysHingeMoveThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysHingeMoveThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysHingeMoveThink);
            }
        }
    }

    public event OnCPhysHingeMoveThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysHingeMoveThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysHingeMoveThink);
            }
        }
    }

    public void InvokePre(ref CPhysHingeMoveThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysHingeMoveThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysHingeMoveThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysHingeMoveThink);
        }
    }

    public void Invoke(CPhysHinge schemaObject) => DatamapHooksPublisher.InvokeCPhysHingeMoveThink(schemaObject.Address);
}