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
    private delegate void CAmbientGenericRampThinkDelegate(nint a1);

    private static IUnmanagedFunction<CAmbientGenericRampThinkDelegate>? CAmbientGenericRampThinkUnmanagedFunction;
    private static Guid CAmbientGenericRampThinkHookGuid;

    private static IUnmanagedFunction<CAmbientGenericRampThinkDelegate> CAmbientGenericRampThinkGetUnmanagedFunction()
    {
        if (CAmbientGenericRampThinkUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CAmbientGeneric", "CAmbientGenericRampThink");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CAmbientGeneric::CAmbientGenericRampThink.");
            }
            CAmbientGenericRampThinkUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CAmbientGenericRampThinkDelegate>(address);
        }
        return CAmbientGenericRampThinkUnmanagedFunction;
    }

    internal static Guid HookCAmbientGenericRampThink()
    {
        CAmbientGenericRampThinkHookGuid = CAmbientGenericRampThinkGetUnmanagedFunction().AddHook(next => (a1) => CAmbientGenericRampThinkPipeline(a1, () => next()(a1)));
        return CAmbientGenericRampThinkHookGuid;
    }

    internal static Guid UnhookCAmbientGenericRampThink()
    {
        CAmbientGenericRampThinkGetUnmanagedFunction().RemoveHook(CAmbientGenericRampThinkHookGuid);
        return Guid.Empty;
    }

    private static void CAmbientGenericRampThinkPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CAmbientGeneric>(a1);

            var preCtx = new CAmbientGenericRampThinkPreContext { SchemaObject = schemaObject };
            InvokeCAmbientGenericRampThinkPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CAmbientGenericRampThinkPostContext { SchemaObject = schemaObject };
            InvokeCAmbientGenericRampThinkPost(ref postCtx);
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

    internal static void InvokeCAmbientGenericRampThink(nint a1)
    {
        CAmbientGenericRampThinkGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCAmbientGenericRampThinkPre(ref CAmbientGenericRampThinkPreContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CAmbientGenericRampThink);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCAmbientGenericRampThinkPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }

    internal static void InvokeCAmbientGenericRampThinkPost(ref CAmbientGenericRampThinkPostContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CAmbientGenericRampThink);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCAmbientGenericRampThinkPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }
}

internal sealed class CAmbientGenericRampThinkHook(GameHooksService owner) : ICAmbientGenericRampThinkHook
{
    private event OnCAmbientGenericRampThinkPreDelegate? _Pre;
    private event OnCAmbientGenericRampThinkPostDelegate? _Post;

    public event OnCAmbientGenericRampThinkPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CAmbientGenericRampThink, owner);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CAmbientGenericRampThink, owner);
            }
        }
    }

    public event OnCAmbientGenericRampThinkPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CAmbientGenericRampThink, owner);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CAmbientGenericRampThink, owner);
            }
        }
    }

    public void InvokePre(ref CAmbientGenericRampThinkPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CAmbientGenericRampThinkPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CAmbientGenericRampThink, owner);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CAmbientGenericRampThink, owner);
        }
    }

    public void Invoke(CAmbientGeneric schemaObject) => DatamapHooksPublisher.InvokeCAmbientGenericRampThink(schemaObject.Address);
}