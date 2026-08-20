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
    private delegate void CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkDelegate(nint a1);

    private static IUnmanagedFunction<CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkDelegate>? CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkUnmanagedFunction;
    private static Guid CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHookGuid;

    private static IUnmanagedFunction<CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkDelegate> CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkGetUnmanagedFunction()
    {
        if (CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CInfoSpawnGroupLoadUnload", "CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CInfoSpawnGroupLoadUnload::CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink.");
            }
            CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkDelegate>(address);
        }
        return CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkUnmanagedFunction;
    }

    internal static Guid HookCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink()
    {
        CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHookGuid = CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkGetUnmanagedFunction().AddHook(next => (a1) => CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPipeline(a1, () => next()(a1)));
        return CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHookGuid;
    }

    internal static Guid UnhookCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink()
    {
        CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkGetUnmanagedFunction().RemoveHook(CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHookGuid);
        return Guid.Empty;
    }

    private static void CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CInfoSpawnGroupLoadUnload>(a1);

            var preCtx = new CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPreContext { SchemaObject = schemaObject };
            InvokeCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPostContext { SchemaObject = schemaObject };
            InvokeCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPost(ref postCtx);
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

    internal static void InvokeCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink(nint a1)
    {
        CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPre(ref CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPost(ref CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook : ICInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkHook
{
    private event OnCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPreDelegate? _Pre;
    private event OnCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPostDelegate? _Post;

    public event OnCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink);
            }
        }
    }

    public event OnCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink);
            }
        }
    }

    public void InvokePre(ref CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink);
        }
    }

    public void Invoke(CInfoSpawnGroupLoadUnload schemaObject) => DatamapHooksPublisher.InvokeCInfoSpawnGroupLoadUnloadSpawnGroupUnloadingThink(schemaObject.Address);
}