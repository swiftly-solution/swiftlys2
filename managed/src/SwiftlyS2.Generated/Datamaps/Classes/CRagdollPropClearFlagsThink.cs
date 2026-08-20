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
    private delegate void CRagdollPropClearFlagsThinkDelegate(nint a1);

    private static IUnmanagedFunction<CRagdollPropClearFlagsThinkDelegate>? CRagdollPropClearFlagsThinkUnmanagedFunction;
    private static Guid CRagdollPropClearFlagsThinkHookGuid;

    private static IUnmanagedFunction<CRagdollPropClearFlagsThinkDelegate> CRagdollPropClearFlagsThinkGetUnmanagedFunction()
    {
        if (CRagdollPropClearFlagsThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CRagdollProp", "CRagdollPropClearFlagsThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CRagdollProp::CRagdollPropClearFlagsThink.");
            }
            CRagdollPropClearFlagsThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CRagdollPropClearFlagsThinkDelegate>(address);
        }
        return CRagdollPropClearFlagsThinkUnmanagedFunction;
    }

    internal static Guid HookCRagdollPropClearFlagsThink()
    {
        CRagdollPropClearFlagsThinkHookGuid = CRagdollPropClearFlagsThinkGetUnmanagedFunction().AddHook(next => (a1) => CRagdollPropClearFlagsThinkPipeline(a1, () => next()(a1)));
        return CRagdollPropClearFlagsThinkHookGuid;
    }

    internal static Guid UnhookCRagdollPropClearFlagsThink()
    {
        CRagdollPropClearFlagsThinkGetUnmanagedFunction().RemoveHook(CRagdollPropClearFlagsThinkHookGuid);
        return Guid.Empty;
    }

    private static void CRagdollPropClearFlagsThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CRagdollProp>(a1);

            var preCtx = new CRagdollPropClearFlagsThinkPreContext { SchemaObject = schemaObject };
            InvokeCRagdollPropClearFlagsThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CRagdollPropClearFlagsThinkPostContext { SchemaObject = schemaObject };
            InvokeCRagdollPropClearFlagsThinkPost(ref postCtx);
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

    internal static void InvokeCRagdollPropClearFlagsThink(nint a1)
    {
        CRagdollPropClearFlagsThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCRagdollPropClearFlagsThinkPre(ref CRagdollPropClearFlagsThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCRagdollPropClearFlagsThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCRagdollPropClearFlagsThinkPost(ref CRagdollPropClearFlagsThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCRagdollPropClearFlagsThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CRagdollPropClearFlagsThinkHook : ICRagdollPropClearFlagsThinkHook
{
    private event OnCRagdollPropClearFlagsThinkPreDelegate? _Pre;
    private event OnCRagdollPropClearFlagsThinkPostDelegate? _Post;

    public event OnCRagdollPropClearFlagsThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CRagdollPropClearFlagsThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropClearFlagsThink);
            }
        }
    }

    public event OnCRagdollPropClearFlagsThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CRagdollPropClearFlagsThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropClearFlagsThink);
            }
        }
    }

    public void InvokePre(ref CRagdollPropClearFlagsThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CRagdollPropClearFlagsThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropClearFlagsThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropClearFlagsThink);
        }
    }

    public void Invoke(CRagdollProp schemaObject) => DatamapHooksPublisher.InvokeCRagdollPropClearFlagsThink(schemaObject.Address);
}