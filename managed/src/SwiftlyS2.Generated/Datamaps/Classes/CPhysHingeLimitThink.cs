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
    private delegate void CPhysHingeLimitThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPhysHingeLimitThinkDelegate>? CPhysHingeLimitThinkUnmanagedFunction;
    private static Guid CPhysHingeLimitThinkHookGuid;

    private static IUnmanagedFunction<CPhysHingeLimitThinkDelegate> CPhysHingeLimitThinkGetUnmanagedFunction()
    {
        if (CPhysHingeLimitThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysHinge", "CPhysHingeLimitThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysHinge::CPhysHingeLimitThink.");
            }
            CPhysHingeLimitThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysHingeLimitThinkDelegate>(address);
        }
        return CPhysHingeLimitThinkUnmanagedFunction;
    }

    internal static Guid HookCPhysHingeLimitThink()
    {
        CPhysHingeLimitThinkHookGuid = CPhysHingeLimitThinkGetUnmanagedFunction().AddHook(next => (a1) => CPhysHingeLimitThinkPipeline(a1, () => next()(a1)));
        return CPhysHingeLimitThinkHookGuid;
    }

    internal static Guid UnhookCPhysHingeLimitThink()
    {
        CPhysHingeLimitThinkGetUnmanagedFunction().RemoveHook(CPhysHingeLimitThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPhysHingeLimitThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysHinge>(a1);

            var preCtx = new CPhysHingeLimitThinkPreContext { SchemaObject = schemaObject };
            InvokeCPhysHingeLimitThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysHingeLimitThinkPostContext { SchemaObject = schemaObject };
            InvokeCPhysHingeLimitThinkPost(ref postCtx);
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

    internal static void InvokeCPhysHingeLimitThink(nint a1)
    {
        CPhysHingeLimitThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysHingeLimitThinkPre(ref CPhysHingeLimitThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysHingeLimitThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysHingeLimitThinkPost(ref CPhysHingeLimitThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysHingeLimitThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysHingeLimitThinkHook : ICPhysHingeLimitThinkHook
{
    private event OnCPhysHingeLimitThinkPreDelegate? _Pre;
    private event OnCPhysHingeLimitThinkPostDelegate? _Post;

    public event OnCPhysHingeLimitThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysHingeLimitThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysHingeLimitThink);
            }
        }
    }

    public event OnCPhysHingeLimitThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysHingeLimitThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysHingeLimitThink);
            }
        }
    }

    public void InvokePre(ref CPhysHingeLimitThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysHingeLimitThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysHingeLimitThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysHingeLimitThink);
        }
    }

    public void Invoke(CPhysHinge schemaObject) => DatamapHooksPublisher.InvokeCPhysHingeLimitThink(schemaObject.Address);
}