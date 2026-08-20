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
    private delegate void CLogicActiveAutosaveSaveThinkDelegate(nint a1);

    private static IUnmanagedFunction<CLogicActiveAutosaveSaveThinkDelegate>? CLogicActiveAutosaveSaveThinkUnmanagedFunction;
    private static Guid CLogicActiveAutosaveSaveThinkHookGuid;

    private static IUnmanagedFunction<CLogicActiveAutosaveSaveThinkDelegate> CLogicActiveAutosaveSaveThinkGetUnmanagedFunction()
    {
        if (CLogicActiveAutosaveSaveThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CLogicActiveAutosave", "CLogicActiveAutosaveSaveThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CLogicActiveAutosave::CLogicActiveAutosaveSaveThink.");
            }
            CLogicActiveAutosaveSaveThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CLogicActiveAutosaveSaveThinkDelegate>(address);
        }
        return CLogicActiveAutosaveSaveThinkUnmanagedFunction;
    }

    internal static Guid HookCLogicActiveAutosaveSaveThink()
    {
        CLogicActiveAutosaveSaveThinkHookGuid = CLogicActiveAutosaveSaveThinkGetUnmanagedFunction().AddHook(next => (a1) => CLogicActiveAutosaveSaveThinkPipeline(a1, () => next()(a1)));
        return CLogicActiveAutosaveSaveThinkHookGuid;
    }

    internal static Guid UnhookCLogicActiveAutosaveSaveThink()
    {
        CLogicActiveAutosaveSaveThinkGetUnmanagedFunction().RemoveHook(CLogicActiveAutosaveSaveThinkHookGuid);
        return Guid.Empty;
    }

    private static void CLogicActiveAutosaveSaveThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CLogicActiveAutosave>(a1);

            var preCtx = new CLogicActiveAutosaveSaveThinkPreContext { SchemaObject = schemaObject };
            InvokeCLogicActiveAutosaveSaveThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CLogicActiveAutosaveSaveThinkPostContext { SchemaObject = schemaObject };
            InvokeCLogicActiveAutosaveSaveThinkPost(ref postCtx);
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

    internal static void InvokeCLogicActiveAutosaveSaveThink(nint a1)
    {
        CLogicActiveAutosaveSaveThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCLogicActiveAutosaveSaveThinkPre(ref CLogicActiveAutosaveSaveThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCLogicActiveAutosaveSaveThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCLogicActiveAutosaveSaveThinkPost(ref CLogicActiveAutosaveSaveThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCLogicActiveAutosaveSaveThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CLogicActiveAutosaveSaveThinkHook : ICLogicActiveAutosaveSaveThinkHook
{
    private event OnCLogicActiveAutosaveSaveThinkPreDelegate? _Pre;
    private event OnCLogicActiveAutosaveSaveThinkPostDelegate? _Post;

    public event OnCLogicActiveAutosaveSaveThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CLogicActiveAutosaveSaveThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicActiveAutosaveSaveThink);
            }
        }
    }

    public event OnCLogicActiveAutosaveSaveThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CLogicActiveAutosaveSaveThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicActiveAutosaveSaveThink);
            }
        }
    }

    public void InvokePre(ref CLogicActiveAutosaveSaveThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CLogicActiveAutosaveSaveThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicActiveAutosaveSaveThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicActiveAutosaveSaveThink);
        }
    }

    public void Invoke(CLogicActiveAutosave schemaObject) => DatamapHooksPublisher.InvokeCLogicActiveAutosaveSaveThink(schemaObject.Address);
}