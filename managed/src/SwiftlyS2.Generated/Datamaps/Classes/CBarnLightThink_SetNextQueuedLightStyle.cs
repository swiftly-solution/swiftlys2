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
    private delegate void CBarnLightThink_SetNextQueuedLightStyleDelegate(nint a1);

    private static IUnmanagedFunction<CBarnLightThink_SetNextQueuedLightStyleDelegate>? CBarnLightThink_SetNextQueuedLightStyleUnmanagedFunction;
    private static Guid CBarnLightThink_SetNextQueuedLightStyleHookGuid;

    private static IUnmanagedFunction<CBarnLightThink_SetNextQueuedLightStyleDelegate> CBarnLightThink_SetNextQueuedLightStyleGetUnmanagedFunction()
    {
        if (CBarnLightThink_SetNextQueuedLightStyleUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBarnLight", "CBarnLightThink_SetNextQueuedLightStyle");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBarnLight::CBarnLightThink_SetNextQueuedLightStyle.");
            }
            CBarnLightThink_SetNextQueuedLightStyleUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBarnLightThink_SetNextQueuedLightStyleDelegate>(address);
        }
        return CBarnLightThink_SetNextQueuedLightStyleUnmanagedFunction;
    }

    internal static Guid HookCBarnLightThink_SetNextQueuedLightStyle()
    {
        CBarnLightThink_SetNextQueuedLightStyleHookGuid = CBarnLightThink_SetNextQueuedLightStyleGetUnmanagedFunction().AddHook(next => (a1) => CBarnLightThink_SetNextQueuedLightStylePipeline(a1, () => next()(a1)));
        return CBarnLightThink_SetNextQueuedLightStyleHookGuid;
    }

    internal static Guid UnhookCBarnLightThink_SetNextQueuedLightStyle()
    {
        CBarnLightThink_SetNextQueuedLightStyleGetUnmanagedFunction().RemoveHook(CBarnLightThink_SetNextQueuedLightStyleHookGuid);
        return Guid.Empty;
    }

    private static void CBarnLightThink_SetNextQueuedLightStylePipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBarnLight>(a1);

            var preCtx = new CBarnLightThink_SetNextQueuedLightStylePreContext { SchemaObject = schemaObject };
            InvokeCBarnLightThink_SetNextQueuedLightStylePre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBarnLightThink_SetNextQueuedLightStylePostContext { SchemaObject = schemaObject };
            InvokeCBarnLightThink_SetNextQueuedLightStylePost(ref postCtx);
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

    internal static void InvokeCBarnLightThink_SetNextQueuedLightStyle(nint a1)
    {
        CBarnLightThink_SetNextQueuedLightStyleGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBarnLightThink_SetNextQueuedLightStylePre(ref CBarnLightThink_SetNextQueuedLightStylePreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBarnLightThink_SetNextQueuedLightStylePre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBarnLightThink_SetNextQueuedLightStylePost(ref CBarnLightThink_SetNextQueuedLightStylePostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBarnLightThink_SetNextQueuedLightStylePost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBarnLightThink_SetNextQueuedLightStyleHook : ICBarnLightThink_SetNextQueuedLightStyleHook
{
    private event OnCBarnLightThink_SetNextQueuedLightStylePreDelegate? _Pre;
    private event OnCBarnLightThink_SetNextQueuedLightStylePostDelegate? _Post;

    public event OnCBarnLightThink_SetNextQueuedLightStylePreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBarnLightThink_SetNextQueuedLightStyle);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBarnLightThink_SetNextQueuedLightStyle);
            }
        }
    }

    public event OnCBarnLightThink_SetNextQueuedLightStylePostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBarnLightThink_SetNextQueuedLightStyle);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBarnLightThink_SetNextQueuedLightStyle);
            }
        }
    }

    public void InvokePre(ref CBarnLightThink_SetNextQueuedLightStylePreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBarnLightThink_SetNextQueuedLightStylePostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBarnLightThink_SetNextQueuedLightStyle);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBarnLightThink_SetNextQueuedLightStyle);
        }
    }

    public void Invoke(CBarnLight schemaObject) => DatamapHooksPublisher.InvokeCBarnLightThink_SetNextQueuedLightStyle(schemaObject.Address);
}