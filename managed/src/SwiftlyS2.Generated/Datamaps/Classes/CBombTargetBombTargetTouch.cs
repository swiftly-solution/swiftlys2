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
    private delegate void CBombTargetBombTargetTouchDelegate(nint a1);

    private static IUnmanagedFunction<CBombTargetBombTargetTouchDelegate>? CBombTargetBombTargetTouchUnmanagedFunction;
    private static Guid CBombTargetBombTargetTouchHookGuid;

    private static IUnmanagedFunction<CBombTargetBombTargetTouchDelegate> CBombTargetBombTargetTouchGetUnmanagedFunction()
    {
        if (CBombTargetBombTargetTouchUnmanagedFunction == null)
        {
            if (_core == null)
            {
                throw new InvalidOperationException("GameHooksCore is not initialized.");
            }
            var address = NativeSchema.GetDatamapFunction("CBombTarget", "CBombTargetBombTargetTouch");
            if (address == nint.Zero)
            {
                throw new InvalidOperationException("Failed to find the address of the datamap function CBombTarget::CBombTargetBombTargetTouch.");
            }
            CBombTargetBombTargetTouchUnmanagedFunction = _core.Memory.GetUnmanagedFunctionByAddress<CBombTargetBombTargetTouchDelegate>(address);
        }
        return CBombTargetBombTargetTouchUnmanagedFunction;
    }

    internal static Guid HookCBombTargetBombTargetTouch()
    {
        CBombTargetBombTargetTouchHookGuid = CBombTargetBombTargetTouchGetUnmanagedFunction().AddHook(next => (a1) => CBombTargetBombTargetTouchPipeline(a1, () => next()(a1)));
        return CBombTargetBombTargetTouchHookGuid;
    }

    internal static Guid UnhookCBombTargetBombTargetTouch()
    {
        CBombTargetBombTargetTouchGetUnmanagedFunction().RemoveHook(CBombTargetBombTargetTouchHookGuid);
        return Guid.Empty;
    }

    private static void CBombTargetBombTargetTouchPipeline(nint a1, Action callOriginal)
    {
        try
        {
            var schemaObject = Helper.AsSchema<CBombTarget>(a1);

            var preCtx = new CBombTargetBombTargetTouchPreContext { SchemaObject = schemaObject };
            InvokeCBombTargetBombTargetTouchPre(ref preCtx);
            if (preCtx.HookResult == HookResult.Stop || preCtx.HookResult == HookResult.CancelOriginal)
            {
                return;
            }

            callOriginal();

            var postCtx = new CBombTargetBombTargetTouchPostContext { SchemaObject = schemaObject };
            InvokeCBombTargetBombTargetTouchPost(ref postCtx);
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

    internal static void InvokeCBombTargetBombTargetTouch(nint a1)
    {
        CBombTargetBombTargetTouchGetUnmanagedFunction().CallOriginal(a1);
    }

    internal static void InvokeCBombTargetBombTargetTouchPre(ref CBombTargetBombTargetTouchPreContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBombTargetBombTargetTouchPre(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }

    internal static void InvokeCBombTargetBombTargetTouchPost(ref CBombTargetBombTargetTouchPostContext ctx)
    {
        lock (subscribersLock)
        {
            for (var i = 0; i < subscribers.Count; i++)
            {
                subscribers[i].InvokeCBombTargetBombTargetTouchPost(ref ctx);
                if (ctx.HookResult == HookResult.Stop || ctx.HookResult == HookResult.Handled)
                {
                    return;
                }
            }
        }
    }
}

internal sealed class CBombTargetBombTargetTouchHook : ICBombTargetBombTargetTouchHook
{
    private event OnCBombTargetBombTargetTouchPreDelegate? _Pre;
    private event OnCBombTargetBombTargetTouchPostDelegate? _Post;

    public event OnCBombTargetBombTargetTouchPreDelegate Pre
    {
        add
        {
            if (_Pre == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBombTargetBombTargetTouch);
            }
            _Pre += value;
        }
        remove
        {
            _Pre -= value;
            if (_Pre == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBombTargetBombTargetTouch);
            }
        }
    }

    public event OnCBombTargetBombTargetTouchPostDelegate Post
    {
        add
        {
            if (_Post == null)
            {
                DatamapHooksPublisher.AddHookListener(DatamapHookListener.CBombTargetBombTargetTouch);
            }
            _Post += value;
        }
        remove
        {
            _Post -= value;
            if (_Post == null)
            {
                DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBombTargetBombTargetTouch);
            }
        }
    }

    public void InvokePre(ref CBombTargetBombTargetTouchPreContext ctx) => _Pre?.Invoke(ref ctx);
    public void InvokePost(ref CBombTargetBombTargetTouchPostContext ctx) => _Post?.Invoke(ref ctx);

    public bool HasPreListeners => _Pre != null;
    public bool HasPostListeners => _Post != null;

    public void UnregisterListeners()
    {
        if (_Pre != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBombTargetBombTargetTouch);
        }
        if (_Post != null)
        {
            DatamapHooksPublisher.RemoveHookListener(DatamapHookListener.CBombTargetBombTargetTouch);
        }
    }

    public void Invoke(CBombTarget schemaObject) => DatamapHooksPublisher.InvokeCBombTargetBombTargetTouch(schemaObject.Address);
}