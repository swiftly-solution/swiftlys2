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
    private delegate void CEnvEntityMakerCheckSpawnThinkDelegate(nint a1);

    private static IUnmanagedFunction<CEnvEntityMakerCheckSpawnThinkDelegate>? CEnvEntityMakerCheckSpawnThinkUnmanagedFunction;
    private static Guid CEnvEntityMakerCheckSpawnThinkHookGuid;

    private static IUnmanagedFunction<CEnvEntityMakerCheckSpawnThinkDelegate> CEnvEntityMakerCheckSpawnThinkGetUnmanagedFunction()
    {
        if (CEnvEntityMakerCheckSpawnThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CEnvEntityMaker", "CEnvEntityMakerCheckSpawnThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CEnvEntityMaker::CEnvEntityMakerCheckSpawnThink.");
            }
            CEnvEntityMakerCheckSpawnThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CEnvEntityMakerCheckSpawnThinkDelegate>(address);
        }
        return CEnvEntityMakerCheckSpawnThinkUnmanagedFunction;
    }

    internal static Guid HookCEnvEntityMakerCheckSpawnThink()
    {
        CEnvEntityMakerCheckSpawnThinkHookGuid = CEnvEntityMakerCheckSpawnThinkGetUnmanagedFunction().AddHook(next => (a1) => CEnvEntityMakerCheckSpawnThinkPipeline(a1, () => next()(a1)));
        return CEnvEntityMakerCheckSpawnThinkHookGuid;
    }

    internal static Guid UnhookCEnvEntityMakerCheckSpawnThink()
    {
        CEnvEntityMakerCheckSpawnThinkGetUnmanagedFunction().RemoveHook(CEnvEntityMakerCheckSpawnThinkHookGuid);
        return Guid.Empty;
    }

    private static void CEnvEntityMakerCheckSpawnThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CEnvEntityMaker>(a1);

            var preCtx = new CEnvEntityMakerCheckSpawnThinkPreContext { SchemaObject = schemaObject };
            InvokeCEnvEntityMakerCheckSpawnThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CEnvEntityMakerCheckSpawnThinkPostContext { SchemaObject = schemaObject };
            InvokeCEnvEntityMakerCheckSpawnThinkPost(ref postCtx);
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

    internal static void InvokeCEnvEntityMakerCheckSpawnThink(nint a1)
    {
        CEnvEntityMakerCheckSpawnThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCEnvEntityMakerCheckSpawnThinkPre(ref CEnvEntityMakerCheckSpawnThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvEntityMakerCheckSpawnThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCEnvEntityMakerCheckSpawnThinkPost(ref CEnvEntityMakerCheckSpawnThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvEntityMakerCheckSpawnThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CEnvEntityMakerCheckSpawnThinkHook : ICEnvEntityMakerCheckSpawnThinkHook
{
    private event OnCEnvEntityMakerCheckSpawnThinkPreDelegate? _Pre;
    private event OnCEnvEntityMakerCheckSpawnThinkPostDelegate? _Post;

    public event OnCEnvEntityMakerCheckSpawnThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvEntityMakerCheckSpawnThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvEntityMakerCheckSpawnThink);
            }
        }
    }

    public event OnCEnvEntityMakerCheckSpawnThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvEntityMakerCheckSpawnThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvEntityMakerCheckSpawnThink);
            }
        }
    }

    public void InvokePre(ref CEnvEntityMakerCheckSpawnThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CEnvEntityMakerCheckSpawnThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvEntityMakerCheckSpawnThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvEntityMakerCheckSpawnThink);
        }
    }

    public void Invoke(CEnvEntityMaker schemaObject) => DatamapHooksPublisher.InvokeCEnvEntityMakerCheckSpawnThink(schemaObject.Address);
}