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
    private delegate void CPathMoverEntitySpawnerDebugThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPathMoverEntitySpawnerDebugThinkDelegate>? CPathMoverEntitySpawnerDebugThinkUnmanagedFunction;
    private static Guid CPathMoverEntitySpawnerDebugThinkHookGuid;

    private static IUnmanagedFunction<CPathMoverEntitySpawnerDebugThinkDelegate> CPathMoverEntitySpawnerDebugThinkGetUnmanagedFunction()
    {
        if (CPathMoverEntitySpawnerDebugThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPathMoverEntitySpawner", "CPathMoverEntitySpawnerDebugThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPathMoverEntitySpawner::CPathMoverEntitySpawnerDebugThink.");
            }
            CPathMoverEntitySpawnerDebugThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPathMoverEntitySpawnerDebugThinkDelegate>(address);
        }
        return CPathMoverEntitySpawnerDebugThinkUnmanagedFunction;
    }

    internal static Guid HookCPathMoverEntitySpawnerDebugThink()
    {
        CPathMoverEntitySpawnerDebugThinkHookGuid = CPathMoverEntitySpawnerDebugThinkGetUnmanagedFunction().AddHook(next => (a1) => CPathMoverEntitySpawnerDebugThinkPipeline(a1, () => next()(a1)));
        return CPathMoverEntitySpawnerDebugThinkHookGuid;
    }

    internal static Guid UnhookCPathMoverEntitySpawnerDebugThink()
    {
        CPathMoverEntitySpawnerDebugThinkGetUnmanagedFunction().RemoveHook(CPathMoverEntitySpawnerDebugThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPathMoverEntitySpawnerDebugThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPathMoverEntitySpawner>(a1);

            var preCtx = new CPathMoverEntitySpawnerDebugThinkPreContext { SchemaObject = schemaObject };
            InvokeCPathMoverEntitySpawnerDebugThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPathMoverEntitySpawnerDebugThinkPostContext { SchemaObject = schemaObject };
            InvokeCPathMoverEntitySpawnerDebugThinkPost(ref postCtx);
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

    internal static void InvokeCPathMoverEntitySpawnerDebugThink(nint a1)
    {
        CPathMoverEntitySpawnerDebugThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPathMoverEntitySpawnerDebugThinkPre(ref CPathMoverEntitySpawnerDebugThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPathMoverEntitySpawnerDebugThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPathMoverEntitySpawnerDebugThinkPost(ref CPathMoverEntitySpawnerDebugThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPathMoverEntitySpawnerDebugThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPathMoverEntitySpawnerDebugThinkHook : ICPathMoverEntitySpawnerDebugThinkHook
{
    private event OnCPathMoverEntitySpawnerDebugThinkPreDelegate? _Pre;
    private event OnCPathMoverEntitySpawnerDebugThinkPostDelegate? _Post;

    public event OnCPathMoverEntitySpawnerDebugThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPathMoverEntitySpawnerDebugThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPathMoverEntitySpawnerDebugThink);
            }
        }
    }

    public event OnCPathMoverEntitySpawnerDebugThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPathMoverEntitySpawnerDebugThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPathMoverEntitySpawnerDebugThink);
            }
        }
    }

    public void InvokePre(ref CPathMoverEntitySpawnerDebugThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPathMoverEntitySpawnerDebugThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPathMoverEntitySpawnerDebugThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPathMoverEntitySpawnerDebugThink);
        }
    }

    public void Invoke(CPathMoverEntitySpawner schemaObject) => DatamapHooksPublisher.InvokeCPathMoverEntitySpawnerDebugThink(schemaObject.Address);
}