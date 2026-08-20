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
    private delegate void CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkDelegate(nint a1);

    private static IUnmanagedFunction<CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkDelegate>? CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkUnmanagedFunction;
    private static Guid CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHookGuid;

    private static IUnmanagedFunction<CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkDelegate> CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkGetUnmanagedFunction()
    {
        if (CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CInfoSpawnGroupLoadUnload", "CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CInfoSpawnGroupLoadUnload::CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink.");
            }
            CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkDelegate>(address);
        }
        return CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkUnmanagedFunction;
    }

    internal static Guid HookCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink()
    {
        CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHookGuid = CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkGetUnmanagedFunction().AddHook(next => (a1) => CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPipeline(a1, () => next()(a1)));
        return CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHookGuid;
    }

    internal static Guid UnhookCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink()
    {
        CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkGetUnmanagedFunction().RemoveHook(CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHookGuid);
        return Guid.Empty;
    }

    private static void CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CInfoSpawnGroupLoadUnload>(a1);

            var preCtx = new CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPreContext { SchemaObject = schemaObject };
            InvokeCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPostContext { SchemaObject = schemaObject };
            InvokeCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPost(ref postCtx);
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

    internal static void InvokeCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink(nint a1)
    {
        CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPre(ref CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPost(ref CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook : ICInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkHook
{
    private event OnCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPreDelegate? _Pre;
    private event OnCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPostDelegate? _Post;

    public event OnCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink);
            }
        }
    }

    public event OnCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink);
            }
        }
    }

    public void InvokePre(ref CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink);
        }
    }

    public void Invoke(CInfoSpawnGroupLoadUnload schemaObject) => DatamapHooksPublisher.InvokeCInfoSpawnGroupLoadUnloadSpawnGroupLoadingThink(schemaObject.Address);
}