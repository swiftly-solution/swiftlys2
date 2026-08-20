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
    private delegate void CMultiLightProxyApproachBrightnessThinkDelegate(nint a1);

    private static IUnmanagedFunction<CMultiLightProxyApproachBrightnessThinkDelegate>? CMultiLightProxyApproachBrightnessThinkUnmanagedFunction;
    private static Guid CMultiLightProxyApproachBrightnessThinkHookGuid;

    private static IUnmanagedFunction<CMultiLightProxyApproachBrightnessThinkDelegate> CMultiLightProxyApproachBrightnessThinkGetUnmanagedFunction()
    {
        if (CMultiLightProxyApproachBrightnessThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CMultiLightProxy", "CMultiLightProxyApproachBrightnessThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CMultiLightProxy::CMultiLightProxyApproachBrightnessThink.");
            }
            CMultiLightProxyApproachBrightnessThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CMultiLightProxyApproachBrightnessThinkDelegate>(address);
        }
        return CMultiLightProxyApproachBrightnessThinkUnmanagedFunction;
    }

    internal static Guid HookCMultiLightProxyApproachBrightnessThink()
    {
        CMultiLightProxyApproachBrightnessThinkHookGuid = CMultiLightProxyApproachBrightnessThinkGetUnmanagedFunction().AddHook(next => (a1) => CMultiLightProxyApproachBrightnessThinkPipeline(a1, () => next()(a1)));
        return CMultiLightProxyApproachBrightnessThinkHookGuid;
    }

    internal static Guid UnhookCMultiLightProxyApproachBrightnessThink()
    {
        CMultiLightProxyApproachBrightnessThinkGetUnmanagedFunction().RemoveHook(CMultiLightProxyApproachBrightnessThinkHookGuid);
        return Guid.Empty;
    }

    private static void CMultiLightProxyApproachBrightnessThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CMultiLightProxy>(a1);

            var preCtx = new CMultiLightProxyApproachBrightnessThinkPreContext { SchemaObject = schemaObject };
            InvokeCMultiLightProxyApproachBrightnessThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CMultiLightProxyApproachBrightnessThinkPostContext { SchemaObject = schemaObject };
            InvokeCMultiLightProxyApproachBrightnessThinkPost(ref postCtx);
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

    internal static void InvokeCMultiLightProxyApproachBrightnessThink(nint a1)
    {
        CMultiLightProxyApproachBrightnessThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCMultiLightProxyApproachBrightnessThinkPre(ref CMultiLightProxyApproachBrightnessThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMultiLightProxyApproachBrightnessThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCMultiLightProxyApproachBrightnessThinkPost(ref CMultiLightProxyApproachBrightnessThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMultiLightProxyApproachBrightnessThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CMultiLightProxyApproachBrightnessThinkHook : ICMultiLightProxyApproachBrightnessThinkHook
{
    private event OnCMultiLightProxyApproachBrightnessThinkPreDelegate? _Pre;
    private event OnCMultiLightProxyApproachBrightnessThinkPostDelegate? _Post;

    public event OnCMultiLightProxyApproachBrightnessThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMultiLightProxyApproachBrightnessThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMultiLightProxyApproachBrightnessThink);
            }
        }
    }

    public event OnCMultiLightProxyApproachBrightnessThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMultiLightProxyApproachBrightnessThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMultiLightProxyApproachBrightnessThink);
            }
        }
    }

    public void InvokePre(ref CMultiLightProxyApproachBrightnessThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CMultiLightProxyApproachBrightnessThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMultiLightProxyApproachBrightnessThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMultiLightProxyApproachBrightnessThink);
        }
    }

    public void Invoke(CMultiLightProxy schemaObject) => DatamapHooksPublisher.InvokeCMultiLightProxyApproachBrightnessThink(schemaObject.Address);
}