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
    private delegate void CEnvSparkSparkThinkDelegate(nint a1);

    private static IUnmanagedFunction<CEnvSparkSparkThinkDelegate>? CEnvSparkSparkThinkUnmanagedFunction;
    private static Guid CEnvSparkSparkThinkHookGuid;

    private static IUnmanagedFunction<CEnvSparkSparkThinkDelegate> CEnvSparkSparkThinkGetUnmanagedFunction()
    {
        if (CEnvSparkSparkThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CEnvSpark", "CEnvSparkSparkThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CEnvSpark::CEnvSparkSparkThink.");
            }
            CEnvSparkSparkThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CEnvSparkSparkThinkDelegate>(address);
        }
        return CEnvSparkSparkThinkUnmanagedFunction;
    }

    internal static Guid HookCEnvSparkSparkThink()
    {
        CEnvSparkSparkThinkHookGuid = CEnvSparkSparkThinkGetUnmanagedFunction().AddHook(next => (a1) => CEnvSparkSparkThinkPipeline(a1, () => next()(a1)));
        return CEnvSparkSparkThinkHookGuid;
    }

    internal static Guid UnhookCEnvSparkSparkThink()
    {
        CEnvSparkSparkThinkGetUnmanagedFunction().RemoveHook(CEnvSparkSparkThinkHookGuid);
        return Guid.Empty;
    }

    private static void CEnvSparkSparkThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CEnvSpark>(a1);

            var preCtx = new CEnvSparkSparkThinkPreContext { SchemaObject = schemaObject };
            InvokeCEnvSparkSparkThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CEnvSparkSparkThinkPostContext { SchemaObject = schemaObject };
            InvokeCEnvSparkSparkThinkPost(ref postCtx);
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

    internal static void InvokeCEnvSparkSparkThink(nint a1)
    {
        CEnvSparkSparkThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCEnvSparkSparkThinkPre(ref CEnvSparkSparkThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvSparkSparkThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCEnvSparkSparkThinkPost(ref CEnvSparkSparkThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCEnvSparkSparkThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CEnvSparkSparkThinkHook : ICEnvSparkSparkThinkHook
{
    private event OnCEnvSparkSparkThinkPreDelegate? _Pre;
    private event OnCEnvSparkSparkThinkPostDelegate? _Post;

    public event OnCEnvSparkSparkThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvSparkSparkThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvSparkSparkThink);
            }
        }
    }

    public event OnCEnvSparkSparkThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CEnvSparkSparkThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvSparkSparkThink);
            }
        }
    }

    public void InvokePre(ref CEnvSparkSparkThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CEnvSparkSparkThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvSparkSparkThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CEnvSparkSparkThink);
        }
    }

    public void Invoke(CEnvSpark schemaObject) => DatamapHooksPublisher.InvokeCEnvSparkSparkThink(schemaObject.Address);
}