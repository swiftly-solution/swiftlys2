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
    private delegate void CRagdollPropSettleThinkDelegate(nint a1);

    private static IUnmanagedFunction<CRagdollPropSettleThinkDelegate>? CRagdollPropSettleThinkUnmanagedFunction;
    private static Guid CRagdollPropSettleThinkHookGuid;

    private static IUnmanagedFunction<CRagdollPropSettleThinkDelegate> CRagdollPropSettleThinkGetUnmanagedFunction()
    {
        if (CRagdollPropSettleThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CRagdollProp", "CRagdollPropSettleThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CRagdollProp::CRagdollPropSettleThink.");
            }
            CRagdollPropSettleThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CRagdollPropSettleThinkDelegate>(address);
        }
        return CRagdollPropSettleThinkUnmanagedFunction;
    }

    internal static Guid HookCRagdollPropSettleThink()
    {
        CRagdollPropSettleThinkHookGuid = CRagdollPropSettleThinkGetUnmanagedFunction().AddHook(next => (a1) => CRagdollPropSettleThinkPipeline(a1, () => next()(a1)));
        return CRagdollPropSettleThinkHookGuid;
    }

    internal static Guid UnhookCRagdollPropSettleThink()
    {
        CRagdollPropSettleThinkGetUnmanagedFunction().RemoveHook(CRagdollPropSettleThinkHookGuid);
        return Guid.Empty;
    }

    private static void CRagdollPropSettleThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CRagdollProp>(a1);

            var preCtx = new CRagdollPropSettleThinkPreContext { SchemaObject = schemaObject };
            InvokeCRagdollPropSettleThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CRagdollPropSettleThinkPostContext { SchemaObject = schemaObject };
            InvokeCRagdollPropSettleThinkPost(ref postCtx);
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

    internal static void InvokeCRagdollPropSettleThink(nint a1)
    {
        CRagdollPropSettleThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCRagdollPropSettleThinkPre(ref CRagdollPropSettleThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCRagdollPropSettleThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCRagdollPropSettleThinkPost(ref CRagdollPropSettleThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCRagdollPropSettleThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CRagdollPropSettleThinkHook : ICRagdollPropSettleThinkHook
{
    private event OnCRagdollPropSettleThinkPreDelegate? _Pre;
    private event OnCRagdollPropSettleThinkPostDelegate? _Post;

    public event OnCRagdollPropSettleThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CRagdollPropSettleThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropSettleThink);
            }
        }
    }

    public event OnCRagdollPropSettleThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CRagdollPropSettleThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropSettleThink);
            }
        }
    }

    public void InvokePre(ref CRagdollPropSettleThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CRagdollPropSettleThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropSettleThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropSettleThink);
        }
    }

    public void Invoke(CRagdollProp schemaObject) => DatamapHooksPublisher.InvokeCRagdollPropSettleThink(schemaObject.Address);
}