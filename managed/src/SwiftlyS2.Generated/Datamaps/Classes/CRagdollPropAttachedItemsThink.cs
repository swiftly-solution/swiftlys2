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
    private delegate void CRagdollPropAttachedItemsThinkDelegate(nint a1);

    private static IUnmanagedFunction<CRagdollPropAttachedItemsThinkDelegate>? CRagdollPropAttachedItemsThinkUnmanagedFunction;
    private static Guid CRagdollPropAttachedItemsThinkHookGuid;

    private static IUnmanagedFunction<CRagdollPropAttachedItemsThinkDelegate> CRagdollPropAttachedItemsThinkGetUnmanagedFunction()
    {
        if (CRagdollPropAttachedItemsThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CRagdollProp", "CRagdollPropAttachedItemsThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CRagdollProp::CRagdollPropAttachedItemsThink.");
            }
            CRagdollPropAttachedItemsThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CRagdollPropAttachedItemsThinkDelegate>(address);
        }
        return CRagdollPropAttachedItemsThinkUnmanagedFunction;
    }

    internal static Guid HookCRagdollPropAttachedItemsThink()
    {
        CRagdollPropAttachedItemsThinkHookGuid = CRagdollPropAttachedItemsThinkGetUnmanagedFunction().AddHook(next => (a1) => CRagdollPropAttachedItemsThinkPipeline(a1, () => next()(a1)));
        return CRagdollPropAttachedItemsThinkHookGuid;
    }

    internal static Guid UnhookCRagdollPropAttachedItemsThink()
    {
        CRagdollPropAttachedItemsThinkGetUnmanagedFunction().RemoveHook(CRagdollPropAttachedItemsThinkHookGuid);
        return Guid.Empty;
    }

    private static void CRagdollPropAttachedItemsThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CRagdollProp>(a1);

            var preCtx = new CRagdollPropAttachedItemsThinkPreContext { SchemaObject = schemaObject };
            InvokeCRagdollPropAttachedItemsThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CRagdollPropAttachedItemsThinkPostContext { SchemaObject = schemaObject };
            InvokeCRagdollPropAttachedItemsThinkPost(ref postCtx);
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

    internal static void InvokeCRagdollPropAttachedItemsThink(nint a1)
    {
        CRagdollPropAttachedItemsThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCRagdollPropAttachedItemsThinkPre(ref CRagdollPropAttachedItemsThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCRagdollPropAttachedItemsThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCRagdollPropAttachedItemsThinkPost(ref CRagdollPropAttachedItemsThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCRagdollPropAttachedItemsThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CRagdollPropAttachedItemsThinkHook : ICRagdollPropAttachedItemsThinkHook
{
    private event OnCRagdollPropAttachedItemsThinkPreDelegate? _Pre;
    private event OnCRagdollPropAttachedItemsThinkPostDelegate? _Post;

    public event OnCRagdollPropAttachedItemsThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CRagdollPropAttachedItemsThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropAttachedItemsThink);
            }
        }
    }

    public event OnCRagdollPropAttachedItemsThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CRagdollPropAttachedItemsThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropAttachedItemsThink);
            }
        }
    }

    public void InvokePre(ref CRagdollPropAttachedItemsThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CRagdollPropAttachedItemsThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropAttachedItemsThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropAttachedItemsThink);
        }
    }

    public void Invoke(CRagdollProp schemaObject) => DatamapHooksPublisher.InvokeCRagdollPropAttachedItemsThink(schemaObject.Address);
}