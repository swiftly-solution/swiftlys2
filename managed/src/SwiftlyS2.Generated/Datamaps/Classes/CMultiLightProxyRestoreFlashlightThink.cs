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
    private delegate void CMultiLightProxyRestoreFlashlightThinkDelegate(nint a1);

    private static IUnmanagedFunction<CMultiLightProxyRestoreFlashlightThinkDelegate>? CMultiLightProxyRestoreFlashlightThinkUnmanagedFunction;
    private static Guid CMultiLightProxyRestoreFlashlightThinkHookGuid;

    private static IUnmanagedFunction<CMultiLightProxyRestoreFlashlightThinkDelegate> CMultiLightProxyRestoreFlashlightThinkGetUnmanagedFunction()
    {
        if (CMultiLightProxyRestoreFlashlightThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CMultiLightProxy", "CMultiLightProxyRestoreFlashlightThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CMultiLightProxy::CMultiLightProxyRestoreFlashlightThink.");
            }
            CMultiLightProxyRestoreFlashlightThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CMultiLightProxyRestoreFlashlightThinkDelegate>(address);
        }
        return CMultiLightProxyRestoreFlashlightThinkUnmanagedFunction;
    }

    internal static Guid HookCMultiLightProxyRestoreFlashlightThink()
    {
        CMultiLightProxyRestoreFlashlightThinkHookGuid = CMultiLightProxyRestoreFlashlightThinkGetUnmanagedFunction().AddHook(next => (a1) => CMultiLightProxyRestoreFlashlightThinkPipeline(a1, () => next()(a1)));
        return CMultiLightProxyRestoreFlashlightThinkHookGuid;
    }

    internal static Guid UnhookCMultiLightProxyRestoreFlashlightThink()
    {
        CMultiLightProxyRestoreFlashlightThinkGetUnmanagedFunction().RemoveHook(CMultiLightProxyRestoreFlashlightThinkHookGuid);
        return Guid.Empty;
    }

    private static void CMultiLightProxyRestoreFlashlightThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CMultiLightProxy>(a1);

            var preCtx = new CMultiLightProxyRestoreFlashlightThinkPreContext { SchemaObject = schemaObject };
            InvokeCMultiLightProxyRestoreFlashlightThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CMultiLightProxyRestoreFlashlightThinkPostContext { SchemaObject = schemaObject };
            InvokeCMultiLightProxyRestoreFlashlightThinkPost(ref postCtx);
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

    internal static void InvokeCMultiLightProxyRestoreFlashlightThink(nint a1)
    {
        CMultiLightProxyRestoreFlashlightThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCMultiLightProxyRestoreFlashlightThinkPre(ref CMultiLightProxyRestoreFlashlightThinkPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMultiLightProxyRestoreFlashlightThinkPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCMultiLightProxyRestoreFlashlightThinkPost(ref CMultiLightProxyRestoreFlashlightThinkPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCMultiLightProxyRestoreFlashlightThinkPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CMultiLightProxyRestoreFlashlightThinkHook : ICMultiLightProxyRestoreFlashlightThinkHook
{
    private event OnCMultiLightProxyRestoreFlashlightThinkPreDelegate? _Pre;
    private event OnCMultiLightProxyRestoreFlashlightThinkPostDelegate? _Post;

    public event OnCMultiLightProxyRestoreFlashlightThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMultiLightProxyRestoreFlashlightThink);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMultiLightProxyRestoreFlashlightThink);
            }
        }
    }

    public event OnCMultiLightProxyRestoreFlashlightThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CMultiLightProxyRestoreFlashlightThink);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMultiLightProxyRestoreFlashlightThink);
            }
        }
    }

    public void InvokePre(ref CMultiLightProxyRestoreFlashlightThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CMultiLightProxyRestoreFlashlightThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMultiLightProxyRestoreFlashlightThink);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CMultiLightProxyRestoreFlashlightThink);
        }
    }

    public void Invoke(CMultiLightProxy schemaObject) => DatamapHooksPublisher.InvokeCMultiLightProxyRestoreFlashlightThink(schemaObject.Address);
}