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
    private delegate void CBarnLightThink_LightStyleEventDelegate(nint a1);

    private static IUnmanagedFunction<CBarnLightThink_LightStyleEventDelegate>? CBarnLightThink_LightStyleEventUnmanagedFunction;
    private static Guid CBarnLightThink_LightStyleEventHookGuid;

    private static IUnmanagedFunction<CBarnLightThink_LightStyleEventDelegate> CBarnLightThink_LightStyleEventGetUnmanagedFunction()
    {
        if (CBarnLightThink_LightStyleEventUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBarnLight", "CBarnLightThink_LightStyleEvent");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBarnLight::CBarnLightThink_LightStyleEvent.");
            }
            CBarnLightThink_LightStyleEventUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBarnLightThink_LightStyleEventDelegate>(address);
        }
        return CBarnLightThink_LightStyleEventUnmanagedFunction;
    }

    internal static Guid HookCBarnLightThink_LightStyleEvent()
    {
        CBarnLightThink_LightStyleEventHookGuid = CBarnLightThink_LightStyleEventGetUnmanagedFunction().AddHook(next => (a1) => CBarnLightThink_LightStyleEventPipeline(a1, () => next()(a1)));
        return CBarnLightThink_LightStyleEventHookGuid;
    }

    internal static Guid UnhookCBarnLightThink_LightStyleEvent()
    {
        CBarnLightThink_LightStyleEventGetUnmanagedFunction().RemoveHook(CBarnLightThink_LightStyleEventHookGuid);
        return Guid.Empty;
    }

    private static void CBarnLightThink_LightStyleEventPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBarnLight>(a1);

            var preCtx = new CBarnLightThink_LightStyleEventPreContext { SchemaObject = schemaObject };
            InvokeCBarnLightThink_LightStyleEventPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBarnLightThink_LightStyleEventPostContext { SchemaObject = schemaObject };
            InvokeCBarnLightThink_LightStyleEventPost(ref postCtx);
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

    internal static void InvokeCBarnLightThink_LightStyleEvent(nint a1)
    {
        CBarnLightThink_LightStyleEventGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBarnLightThink_LightStyleEventPre(ref CBarnLightThink_LightStyleEventPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBarnLightThink_LightStyleEventPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBarnLightThink_LightStyleEventPost(ref CBarnLightThink_LightStyleEventPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBarnLightThink_LightStyleEventPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBarnLightThink_LightStyleEventHook : ICBarnLightThink_LightStyleEventHook
{
    private event OnCBarnLightThink_LightStyleEventPreDelegate? _Pre;
    private event OnCBarnLightThink_LightStyleEventPostDelegate? _Post;

    public event OnCBarnLightThink_LightStyleEventPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBarnLightThink_LightStyleEvent);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBarnLightThink_LightStyleEvent);
            }
        }
    }

    public event OnCBarnLightThink_LightStyleEventPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBarnLightThink_LightStyleEvent);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBarnLightThink_LightStyleEvent);
            }
        }
    }

    public void InvokePre(ref CBarnLightThink_LightStyleEventPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBarnLightThink_LightStyleEventPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBarnLightThink_LightStyleEvent);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBarnLightThink_LightStyleEvent);
        }
    }

    public void Invoke(CBarnLight schemaObject) => DatamapHooksPublisher.InvokeCBarnLightThink_LightStyleEvent(schemaObject.Address);
}