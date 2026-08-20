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
    private delegate void CPhysHingeSoundThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPhysHingeSoundThinkDelegate>? CPhysHingeSoundThinkUnmanagedFunction;
    private static Guid CPhysHingeSoundThinkHookGuid;

    private static IUnmanagedFunction<CPhysHingeSoundThinkDelegate> CPhysHingeSoundThinkGetUnmanagedFunction()
    {
        if (CPhysHingeSoundThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPhysHinge", "CPhysHingeSoundThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPhysHinge::CPhysHingeSoundThink.");
            }
            CPhysHingeSoundThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPhysHingeSoundThinkDelegate>(address);
        }
        return CPhysHingeSoundThinkUnmanagedFunction;
    }

    internal static Guid HookCPhysHingeSoundThink()
    {
        CPhysHingeSoundThinkHookGuid = CPhysHingeSoundThinkGetUnmanagedFunction().AddHook(next => (a1) => CPhysHingeSoundThinkPipeline(a1, () => next()(a1)));
        return CPhysHingeSoundThinkHookGuid;
    }

    internal static Guid UnhookCPhysHingeSoundThink()
    {
        CPhysHingeSoundThinkGetUnmanagedFunction().RemoveHook(CPhysHingeSoundThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPhysHingeSoundThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPhysHinge>(a1);

            var preCtx = new CPhysHingeSoundThinkPreContext { SchemaObject = schemaObject };
            InvokeCPhysHingeSoundThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPhysHingeSoundThinkPostContext { SchemaObject = schemaObject };
            InvokeCPhysHingeSoundThinkPost(ref postCtx);
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

    internal static void InvokeCPhysHingeSoundThink(nint a1)
    {
        CPhysHingeSoundThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPhysHingeSoundThinkPre(ref CPhysHingeSoundThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysHingeSoundThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPhysHingeSoundThinkPost(ref CPhysHingeSoundThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPhysHingeSoundThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPhysHingeSoundThinkHook : ICPhysHingeSoundThinkHook
{
    private event OnCPhysHingeSoundThinkPreDelegate? _Pre;
    private event OnCPhysHingeSoundThinkPostDelegate? _Post;

    public event OnCPhysHingeSoundThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysHingeSoundThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysHingeSoundThink);
            }
        }
    }

    public event OnCPhysHingeSoundThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPhysHingeSoundThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysHingeSoundThink);
            }
        }
    }

    public void InvokePre(ref CPhysHingeSoundThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPhysHingeSoundThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysHingeSoundThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPhysHingeSoundThink);
        }
    }

    public void Invoke(CPhysHinge schemaObject) => DatamapHooksPublisher.InvokeCPhysHingeSoundThink(schemaObject.Address);
}