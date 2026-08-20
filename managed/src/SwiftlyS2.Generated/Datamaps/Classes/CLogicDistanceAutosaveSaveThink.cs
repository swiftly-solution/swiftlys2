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
    private delegate void CLogicDistanceAutosaveSaveThinkDelegate(nint a1);

    private static IUnmanagedFunction<CLogicDistanceAutosaveSaveThinkDelegate>? CLogicDistanceAutosaveSaveThinkUnmanagedFunction;
    private static Guid CLogicDistanceAutosaveSaveThinkHookGuid;

    private static IUnmanagedFunction<CLogicDistanceAutosaveSaveThinkDelegate> CLogicDistanceAutosaveSaveThinkGetUnmanagedFunction()
    {
        if (CLogicDistanceAutosaveSaveThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CLogicDistanceAutosave", "CLogicDistanceAutosaveSaveThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CLogicDistanceAutosave::CLogicDistanceAutosaveSaveThink.");
            }
            CLogicDistanceAutosaveSaveThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CLogicDistanceAutosaveSaveThinkDelegate>(address);
        }
        return CLogicDistanceAutosaveSaveThinkUnmanagedFunction;
    }

    internal static Guid HookCLogicDistanceAutosaveSaveThink()
    {
        CLogicDistanceAutosaveSaveThinkHookGuid = CLogicDistanceAutosaveSaveThinkGetUnmanagedFunction().AddHook(next => (a1) => CLogicDistanceAutosaveSaveThinkPipeline(a1, () => next()(a1)));
        return CLogicDistanceAutosaveSaveThinkHookGuid;
    }

    internal static Guid UnhookCLogicDistanceAutosaveSaveThink()
    {
        CLogicDistanceAutosaveSaveThinkGetUnmanagedFunction().RemoveHook(CLogicDistanceAutosaveSaveThinkHookGuid);
        return Guid.Empty;
    }

    private static void CLogicDistanceAutosaveSaveThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CLogicDistanceAutosave>(a1);

            var preCtx = new CLogicDistanceAutosaveSaveThinkPreContext { SchemaObject = schemaObject };
            InvokeCLogicDistanceAutosaveSaveThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CLogicDistanceAutosaveSaveThinkPostContext { SchemaObject = schemaObject };
            InvokeCLogicDistanceAutosaveSaveThinkPost(ref postCtx);
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

    internal static void InvokeCLogicDistanceAutosaveSaveThink(nint a1)
    {
        CLogicDistanceAutosaveSaveThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCLogicDistanceAutosaveSaveThinkPre(ref CLogicDistanceAutosaveSaveThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCLogicDistanceAutosaveSaveThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCLogicDistanceAutosaveSaveThinkPost(ref CLogicDistanceAutosaveSaveThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCLogicDistanceAutosaveSaveThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CLogicDistanceAutosaveSaveThinkHook : ICLogicDistanceAutosaveSaveThinkHook
{
    private event OnCLogicDistanceAutosaveSaveThinkPreDelegate? _Pre;
    private event OnCLogicDistanceAutosaveSaveThinkPostDelegate? _Post;

    public event OnCLogicDistanceAutosaveSaveThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CLogicDistanceAutosaveSaveThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicDistanceAutosaveSaveThink);
            }
        }
    }

    public event OnCLogicDistanceAutosaveSaveThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CLogicDistanceAutosaveSaveThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicDistanceAutosaveSaveThink);
            }
        }
    }

    public void InvokePre(ref CLogicDistanceAutosaveSaveThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CLogicDistanceAutosaveSaveThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicDistanceAutosaveSaveThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CLogicDistanceAutosaveSaveThink);
        }
    }

    public void Invoke(CLogicDistanceAutosave schemaObject) => DatamapHooksPublisher.InvokeCLogicDistanceAutosaveSaveThink(schemaObject.Address);
}