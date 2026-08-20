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
    private delegate void CRevertSavedLoadThinkDelegate(nint a1);

    private static IUnmanagedFunction<CRevertSavedLoadThinkDelegate>? CRevertSavedLoadThinkUnmanagedFunction;
    private static Guid CRevertSavedLoadThinkHookGuid;

    private static IUnmanagedFunction<CRevertSavedLoadThinkDelegate> CRevertSavedLoadThinkGetUnmanagedFunction()
    {
        if (CRevertSavedLoadThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CRevertSaved", "CRevertSavedLoadThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CRevertSaved::CRevertSavedLoadThink.");
            }
            CRevertSavedLoadThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CRevertSavedLoadThinkDelegate>(address);
        }
        return CRevertSavedLoadThinkUnmanagedFunction;
    }

    internal static Guid HookCRevertSavedLoadThink()
    {
        CRevertSavedLoadThinkHookGuid = CRevertSavedLoadThinkGetUnmanagedFunction().AddHook(next => (a1) => CRevertSavedLoadThinkPipeline(a1, () => next()(a1)));
        return CRevertSavedLoadThinkHookGuid;
    }

    internal static Guid UnhookCRevertSavedLoadThink()
    {
        CRevertSavedLoadThinkGetUnmanagedFunction().RemoveHook(CRevertSavedLoadThinkHookGuid);
        return Guid.Empty;
    }

    private static void CRevertSavedLoadThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CRevertSaved>(a1);

            var preCtx = new CRevertSavedLoadThinkPreContext { SchemaObject = schemaObject };
            InvokeCRevertSavedLoadThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CRevertSavedLoadThinkPostContext { SchemaObject = schemaObject };
            InvokeCRevertSavedLoadThinkPost(ref postCtx);
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

    internal static void InvokeCRevertSavedLoadThink(nint a1)
    {
        CRevertSavedLoadThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCRevertSavedLoadThinkPre(ref CRevertSavedLoadThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCRevertSavedLoadThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCRevertSavedLoadThinkPost(ref CRevertSavedLoadThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCRevertSavedLoadThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CRevertSavedLoadThinkHook : ICRevertSavedLoadThinkHook
{
    private event OnCRevertSavedLoadThinkPreDelegate? _Pre;
    private event OnCRevertSavedLoadThinkPostDelegate? _Post;

    public event OnCRevertSavedLoadThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CRevertSavedLoadThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRevertSavedLoadThink);
            }
        }
    }

    public event OnCRevertSavedLoadThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CRevertSavedLoadThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRevertSavedLoadThink);
            }
        }
    }

    public void InvokePre(ref CRevertSavedLoadThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CRevertSavedLoadThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRevertSavedLoadThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRevertSavedLoadThink);
        }
    }

    public void Invoke(CRevertSaved schemaObject) => DatamapHooksPublisher.InvokeCRevertSavedLoadThink(schemaObject.Address);
}