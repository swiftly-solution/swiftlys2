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
    private delegate void CTriggerGravityGravityTouchDelegate(nint a1);

    private static IUnmanagedFunction<CTriggerGravityGravityTouchDelegate>? CTriggerGravityGravityTouchUnmanagedFunction;
    private static Guid CTriggerGravityGravityTouchHookGuid;

    private static IUnmanagedFunction<CTriggerGravityGravityTouchDelegate> CTriggerGravityGravityTouchGetUnmanagedFunction()
    {
        if (CTriggerGravityGravityTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CTriggerGravity", "CTriggerGravityGravityTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CTriggerGravity::CTriggerGravityGravityTouch.");
            }
            CTriggerGravityGravityTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CTriggerGravityGravityTouchDelegate>(address);
        }
        return CTriggerGravityGravityTouchUnmanagedFunction;
    }

    internal static Guid HookCTriggerGravityGravityTouch()
    {
        CTriggerGravityGravityTouchHookGuid = CTriggerGravityGravityTouchGetUnmanagedFunction().AddHook(next => (a1) => CTriggerGravityGravityTouchPipeline(a1, () => next()(a1)));
        return CTriggerGravityGravityTouchHookGuid;
    }

    internal static Guid UnhookCTriggerGravityGravityTouch()
    {
        CTriggerGravityGravityTouchGetUnmanagedFunction().RemoveHook(CTriggerGravityGravityTouchHookGuid);
        return Guid.Empty;
    }

    private static void CTriggerGravityGravityTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CTriggerGravity>(a1);

            var preCtx = new CTriggerGravityGravityTouchPreContext { SchemaObject = schemaObject };
            InvokeCTriggerGravityGravityTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CTriggerGravityGravityTouchPostContext { SchemaObject = schemaObject };
            InvokeCTriggerGravityGravityTouchPost(ref postCtx);
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

    internal static void InvokeCTriggerGravityGravityTouch(nint a1)
    {
        CTriggerGravityGravityTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCTriggerGravityGravityTouchPre(ref CTriggerGravityGravityTouchPreContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CTriggerGravityGravityTouch);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCTriggerGravityGravityTouchPre(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }

    internal static void InvokeCTriggerGravityGravityTouchPost(ref CTriggerGravityGravityTouchPostContext ctx)
    {
        var subs = GetSubscribers(DatamapHookListener.CTriggerGravityGravityTouch);
        for (var i = 0; i < subs.Length; i++)
        {
            subs[i].InvokeCTriggerGravityGravityTouchPost(ref ctx);
            if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
            {
                return;
            }
        }
    }
}

internal sealed class CTriggerGravityGravityTouchHook(GameHooksService owner) : ICTriggerGravityGravityTouchHook
{
    private event OnCTriggerGravityGravityTouchPreDelegate? _Pre;
    private event OnCTriggerGravityGravityTouchPostDelegate? _Post;

    public event OnCTriggerGravityGravityTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerGravityGravityTouch, owner);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerGravityGravityTouch, owner);
            }
        }
    }

    public event OnCTriggerGravityGravityTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CTriggerGravityGravityTouch, owner);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerGravityGravityTouch, owner);
            }
        }
    }

    public void InvokePre(ref CTriggerGravityGravityTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CTriggerGravityGravityTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerGravityGravityTouch, owner);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CTriggerGravityGravityTouch, owner);
        }
    }

    public void Invoke(CTriggerGravity schemaObject) => DatamapHooksPublisher.InvokeCTriggerGravityGravityTouch(schemaObject.Address);
}