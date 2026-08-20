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
    private delegate void CRagdollPropFadeOutThinkDelegate(nint a1);

    private static IUnmanagedFunction<CRagdollPropFadeOutThinkDelegate>? CRagdollPropFadeOutThinkUnmanagedFunction;
    private static Guid CRagdollPropFadeOutThinkHookGuid;

    private static IUnmanagedFunction<CRagdollPropFadeOutThinkDelegate> CRagdollPropFadeOutThinkGetUnmanagedFunction()
    {
        if (CRagdollPropFadeOutThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CRagdollProp", "CRagdollPropFadeOutThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CRagdollProp::CRagdollPropFadeOutThink.");
            }
            CRagdollPropFadeOutThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CRagdollPropFadeOutThinkDelegate>(address);
        }
        return CRagdollPropFadeOutThinkUnmanagedFunction;
    }

    internal static Guid HookCRagdollPropFadeOutThink()
    {
        CRagdollPropFadeOutThinkHookGuid = CRagdollPropFadeOutThinkGetUnmanagedFunction().AddHook(next => (a1) => CRagdollPropFadeOutThinkPipeline(a1, () => next()(a1)));
        return CRagdollPropFadeOutThinkHookGuid;
    }

    internal static Guid UnhookCRagdollPropFadeOutThink()
    {
        CRagdollPropFadeOutThinkGetUnmanagedFunction().RemoveHook(CRagdollPropFadeOutThinkHookGuid);
        return Guid.Empty;
    }

    private static void CRagdollPropFadeOutThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CRagdollProp>(a1);

            var preCtx = new CRagdollPropFadeOutThinkPreContext { SchemaObject = schemaObject };
            InvokeCRagdollPropFadeOutThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CRagdollPropFadeOutThinkPostContext { SchemaObject = schemaObject };
            InvokeCRagdollPropFadeOutThinkPost(ref postCtx);
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

    internal static void InvokeCRagdollPropFadeOutThink(nint a1)
    {
        CRagdollPropFadeOutThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCRagdollPropFadeOutThinkPre(ref CRagdollPropFadeOutThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCRagdollPropFadeOutThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCRagdollPropFadeOutThinkPost(ref CRagdollPropFadeOutThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCRagdollPropFadeOutThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CRagdollPropFadeOutThinkHook : ICRagdollPropFadeOutThinkHook
{
    private event OnCRagdollPropFadeOutThinkPreDelegate? _Pre;
    private event OnCRagdollPropFadeOutThinkPostDelegate? _Post;

    public event OnCRagdollPropFadeOutThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CRagdollPropFadeOutThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropFadeOutThink);
            }
        }
    }

    public event OnCRagdollPropFadeOutThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CRagdollPropFadeOutThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropFadeOutThink);
            }
        }
    }

    public void InvokePre(ref CRagdollPropFadeOutThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CRagdollPropFadeOutThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropFadeOutThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CRagdollPropFadeOutThink);
        }
    }

    public void Invoke(CRagdollProp schemaObject) => DatamapHooksPublisher.InvokeCRagdollPropFadeOutThink(schemaObject.Address);
}