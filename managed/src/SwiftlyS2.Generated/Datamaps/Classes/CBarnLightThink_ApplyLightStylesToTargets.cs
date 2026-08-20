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
    private delegate void CBarnLightThink_ApplyLightStylesToTargetsDelegate(nint a1);

    private static IUnmanagedFunction<CBarnLightThink_ApplyLightStylesToTargetsDelegate>? CBarnLightThink_ApplyLightStylesToTargetsUnmanagedFunction;
    private static Guid CBarnLightThink_ApplyLightStylesToTargetsHookGuid;

    private static IUnmanagedFunction<CBarnLightThink_ApplyLightStylesToTargetsDelegate> CBarnLightThink_ApplyLightStylesToTargetsGetUnmanagedFunction()
    {
        if (CBarnLightThink_ApplyLightStylesToTargetsUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBarnLight", "CBarnLightThink_ApplyLightStylesToTargets");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBarnLight::CBarnLightThink_ApplyLightStylesToTargets.");
            }
            CBarnLightThink_ApplyLightStylesToTargetsUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBarnLightThink_ApplyLightStylesToTargetsDelegate>(address);
        }
        return CBarnLightThink_ApplyLightStylesToTargetsUnmanagedFunction;
    }

    internal static Guid HookCBarnLightThink_ApplyLightStylesToTargets()
    {
        CBarnLightThink_ApplyLightStylesToTargetsHookGuid = CBarnLightThink_ApplyLightStylesToTargetsGetUnmanagedFunction().AddHook(next => (a1) => CBarnLightThink_ApplyLightStylesToTargetsPipeline(a1, () => next()(a1)));
        return CBarnLightThink_ApplyLightStylesToTargetsHookGuid;
    }

    internal static Guid UnhookCBarnLightThink_ApplyLightStylesToTargets()
    {
        CBarnLightThink_ApplyLightStylesToTargetsGetUnmanagedFunction().RemoveHook(CBarnLightThink_ApplyLightStylesToTargetsHookGuid);
        return Guid.Empty;
    }

    private static void CBarnLightThink_ApplyLightStylesToTargetsPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBarnLight>(a1);

            var preCtx = new CBarnLightThink_ApplyLightStylesToTargetsPreContext { SchemaObject = schemaObject };
            InvokeCBarnLightThink_ApplyLightStylesToTargetsPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBarnLightThink_ApplyLightStylesToTargetsPostContext { SchemaObject = schemaObject };
            InvokeCBarnLightThink_ApplyLightStylesToTargetsPost(ref postCtx);
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

    internal static void InvokeCBarnLightThink_ApplyLightStylesToTargets(nint a1)
    {
        CBarnLightThink_ApplyLightStylesToTargetsGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBarnLightThink_ApplyLightStylesToTargetsPre(ref CBarnLightThink_ApplyLightStylesToTargetsPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBarnLightThink_ApplyLightStylesToTargetsPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBarnLightThink_ApplyLightStylesToTargetsPost(ref CBarnLightThink_ApplyLightStylesToTargetsPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBarnLightThink_ApplyLightStylesToTargetsPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBarnLightThink_ApplyLightStylesToTargetsHook : ICBarnLightThink_ApplyLightStylesToTargetsHook
{
    private event OnCBarnLightThink_ApplyLightStylesToTargetsPreDelegate? _Pre;
    private event OnCBarnLightThink_ApplyLightStylesToTargetsPostDelegate? _Post;

    public event OnCBarnLightThink_ApplyLightStylesToTargetsPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBarnLightThink_ApplyLightStylesToTargets);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBarnLightThink_ApplyLightStylesToTargets);
            }
        }
    }

    public event OnCBarnLightThink_ApplyLightStylesToTargetsPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBarnLightThink_ApplyLightStylesToTargets);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBarnLightThink_ApplyLightStylesToTargets);
            }
        }
    }

    public void InvokePre(ref CBarnLightThink_ApplyLightStylesToTargetsPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBarnLightThink_ApplyLightStylesToTargetsPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBarnLightThink_ApplyLightStylesToTargets);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBarnLightThink_ApplyLightStylesToTargets);
        }
    }

    public void Invoke(CBarnLight schemaObject) => DatamapHooksPublisher.InvokeCBarnLightThink_ApplyLightStylesToTargets(schemaObject.Address);
}