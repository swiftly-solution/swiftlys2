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
    private delegate void CPathMoverEntitySpawnerSpawnThinkDelegate(nint a1);

    private static IUnmanagedFunction<CPathMoverEntitySpawnerSpawnThinkDelegate>? CPathMoverEntitySpawnerSpawnThinkUnmanagedFunction;
    private static Guid CPathMoverEntitySpawnerSpawnThinkHookGuid;

    private static IUnmanagedFunction<CPathMoverEntitySpawnerSpawnThinkDelegate> CPathMoverEntitySpawnerSpawnThinkGetUnmanagedFunction()
    {
        if (CPathMoverEntitySpawnerSpawnThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CPathMoverEntitySpawner", "CPathMoverEntitySpawnerSpawnThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CPathMoverEntitySpawner::CPathMoverEntitySpawnerSpawnThink.");
            }
            CPathMoverEntitySpawnerSpawnThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CPathMoverEntitySpawnerSpawnThinkDelegate>(address);
        }
        return CPathMoverEntitySpawnerSpawnThinkUnmanagedFunction;
    }

    internal static Guid HookCPathMoverEntitySpawnerSpawnThink()
    {
        CPathMoverEntitySpawnerSpawnThinkHookGuid = CPathMoverEntitySpawnerSpawnThinkGetUnmanagedFunction().AddHook(next => (a1) => CPathMoverEntitySpawnerSpawnThinkPipeline(a1, () => next()(a1)));
        return CPathMoverEntitySpawnerSpawnThinkHookGuid;
    }

    internal static Guid UnhookCPathMoverEntitySpawnerSpawnThink()
    {
        CPathMoverEntitySpawnerSpawnThinkGetUnmanagedFunction().RemoveHook(CPathMoverEntitySpawnerSpawnThinkHookGuid);
        return Guid.Empty;
    }

    private static void CPathMoverEntitySpawnerSpawnThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CPathMoverEntitySpawner>(a1);

            var preCtx = new CPathMoverEntitySpawnerSpawnThinkPreContext { SchemaObject = schemaObject };
            InvokeCPathMoverEntitySpawnerSpawnThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CPathMoverEntitySpawnerSpawnThinkPostContext { SchemaObject = schemaObject };
            InvokeCPathMoverEntitySpawnerSpawnThinkPost(ref postCtx);
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

    internal static void InvokeCPathMoverEntitySpawnerSpawnThink(nint a1)
    {
        CPathMoverEntitySpawnerSpawnThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCPathMoverEntitySpawnerSpawnThinkPre(ref CPathMoverEntitySpawnerSpawnThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPathMoverEntitySpawnerSpawnThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCPathMoverEntitySpawnerSpawnThinkPost(ref CPathMoverEntitySpawnerSpawnThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCPathMoverEntitySpawnerSpawnThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CPathMoverEntitySpawnerSpawnThinkHook : ICPathMoverEntitySpawnerSpawnThinkHook
{
    private event OnCPathMoverEntitySpawnerSpawnThinkPreDelegate? _Pre;
    private event OnCPathMoverEntitySpawnerSpawnThinkPostDelegate? _Post;

    public event OnCPathMoverEntitySpawnerSpawnThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPathMoverEntitySpawnerSpawnThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPathMoverEntitySpawnerSpawnThink);
            }
        }
    }

    public event OnCPathMoverEntitySpawnerSpawnThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CPathMoverEntitySpawnerSpawnThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPathMoverEntitySpawnerSpawnThink);
            }
        }
    }

    public void InvokePre(ref CPathMoverEntitySpawnerSpawnThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CPathMoverEntitySpawnerSpawnThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPathMoverEntitySpawnerSpawnThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CPathMoverEntitySpawnerSpawnThink);
        }
    }

    public void Invoke(CPathMoverEntitySpawner schemaObject) => DatamapHooksPublisher.InvokeCPathMoverEntitySpawnerSpawnThink(schemaObject.Address);
}